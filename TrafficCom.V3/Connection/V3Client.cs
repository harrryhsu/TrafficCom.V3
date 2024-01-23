using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using TrafficCom.V3.Messages;
using TrafficCom.V3.Request;

namespace TrafficCom.V3.Connection
{
    public delegate Task MessageReceivedEventHandler(V3Client sender, TcpConnection con, V3Request request);

    public class V3Client : IDisposable
    {
        protected readonly List<TcpConnection> _clients = new();

        protected readonly CancellationTokenSource _cts = new();

        protected readonly ConcurrentDictionary<byte, MessageRequest> _request = new();

        protected readonly ConcurrentDictionary<KeyValuePair<byte, byte>, ReplyTask> _query = new();

        public IPEndPoint Target { get; protected set; }

        public int TimeoutMs { get; protected set; }

        public bool Online { get; protected set; }

        public bool Debug { get; set; }

        public Action<string> Logger { get; set; } = Console.WriteLine;

        public event MessageReceivedEventHandler OnMessageReceived;

        public const int DefaultTimeout = 500;

        public V3Client(string ip, int port, int timeout = DefaultTimeout)
        {
            TimeoutMs = timeout;
            Target = IPEndPoint.Parse(ip);
            Target.Port = port;
        }

        public V3Client(IPEndPoint target, int timeout = DefaultTimeout)
        {
            TimeoutMs = timeout;
            Target = target;
        }

        private void LogDebug(string message)
        {
            if (Debug) Logger?.Invoke(message);
        }

        public virtual async Task ConnectAsync(CancellationToken cts = default)
        {
            var tcp = new TcpClient();
            try
            {
                await tcp.ConnectAsync(Target, cts);
                var con = new TcpConnection { TcpClient = tcp, NetworkStream = tcp.GetStream() };
                con.NetworkStream.ReadTimeout = TimeoutMs;
                con.NetworkStream.WriteTimeout = TimeoutMs;
                _clients.Add(con);
                _ = V3ClientWorker(con);
                Online = true;
            }
            catch (Exception ex)
            {
                throw new V3Exception("Connection Failure", ex);
            }
        }

        protected async Task TcpRead(NetworkStream ns, byte[] buffer, int offset, int count)
        {
            var value = await ns.ReadAsync(buffer.AsMemory(offset, count), _cts.Token);
            if (value != count) throw new TimeoutException();
        }

        protected async Task V3ClientWorker(TcpConnection con)
        {
            var buffer = new byte[4096];
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    con.NetworkStream.ReadTimeout = Timeout.Infinite;
                    await TcpRead(con.NetworkStream, buffer, 0, 2);
                    con.NetworkStream.ReadTimeout = TimeoutMs;

                    if (buffer[1] == Message.STX)
                    {
                        await TcpRead(con.NetworkStream, buffer, 2, 5);
                        var length = (buffer[5] << 8) | buffer[6];
                        await TcpRead(con.NetworkStream, buffer, 7, length - 7);

                        if (Message.VerifyCRC(buffer.ToList().GetRange(0, length)))
                        {
                            var msg = new DataMessage(buffer);
                            V3Request request = null;

                            try
                            {
                                request = V3Request.Create(msg);
                            }
                            catch (Exception ex)
                            {
                                LogDebug($"RECV {msg.Cmd1:X} {msg.Cmd2:X}, Error Constructing Request Object");
                                await con.NetworkStream.WriteAsync(new NakMessage() { Seq = msg.Seq }.Build().ToArray(), _cts.Token);
                                if (_request.ContainsKey(msg.Seq))
                                {
                                    _request[msg.Seq].Task.SetException(new V3Exception("Error Constructing Request Object", ex));
                                    _request.Remove(msg.Seq, out _);
                                }
                                continue;
                            }

                            await con.NetworkStream.WriteAsync(new AckMessage() { Seq = msg.Seq }.Build().ToArray(), _cts.Token);

                            var key = KeyValuePair.Create(request.Cmd1, request.Cmd2);
                            if (_query.ContainsKey(key))
                            {
                                LogDebug($"RECV {msg.Cmd1:X} {msg.Cmd2:X}, Setting Result");
                                _query[key].Task.TrySetResult(request);
                            }
                            else
                            {
                                LogDebug($"RECV {msg.Cmd1:X} {msg.Cmd2:X}, Invoking Event");
                                _ = OnMessageReceived?.Invoke(this, con, request);
                            }
                        }
                        else
                        {
                            LogDebug($"Message failed CRC");
                        }
                    }
                    else if (buffer[1] == Message.ACK)
                    {
                        await TcpRead(con.NetworkStream, buffer, 2, 6);

                        if (Message.VerifyCRC(buffer.ToList().GetRange(0, 8)))
                        {
                            LogDebug($"RECV ACK");
                            var msg = new AckMessage(buffer);
                            if (_request.ContainsKey(msg.Seq))
                            {
                                _request[msg.Seq].Task.TrySetResult(msg);
                                _request.Remove(msg.Seq, out _);
                            }
                        }
                        else
                        {
                            LogDebug($"Message failed CRC");
                        }
                    }
                    else if (buffer[1] == Message.NAK)
                    {
                        await TcpRead(con.NetworkStream, buffer, 2, 7);

                        if (Message.VerifyCRC(buffer.ToList().GetRange(0, 9)))
                        {
                            LogDebug($"RECV NAK");
                            var msg = new NakMessage(buffer);
                            if (_request.ContainsKey(msg.Seq))
                            {
                                _request[msg.Seq].Task.TrySetResult(msg);
                                _request.Remove(msg.Seq, out _);
                            }
                        }
                        else
                        {
                            LogDebug($"Message failed CRC");
                        }
                    }
                }
                catch (TimeoutException)
                {
                    Array.Clear(buffer);
                    await con.NetworkStream.FlushAsync();
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    await con.NetworkStream.DisposeAsync();
                    con.TcpClient.Dispose();
                    _clients.Remove(con);
                    LogDebug($"Error while handling tcp stream, {ex}");
                    return;
                }
            }
        }

        public async Task<TResponse> SendAndWaitForReplyAsync<TResponse>(V3Request msg, CancellationToken ct = default) where TResponse : V3Request
        {
            return await SendAndWaitForReplyAsync<TResponse>(_clients.First(), msg, ct);
        }

        public async Task<TResponse> SendAndWaitForReplyAsync<TResponse>(TcpConnection con, V3Request msg, CancellationToken ct = default) where TResponse : V3Request
        {
            var waiter = WaitForMessageAsync<TResponse>(ct);
            var ack = await SendAsync(con, msg, ct);
            var result = await waiter;
            if (ack.IsNak) throw new V3Exception("Request Rejected");
            return result;
        }

        public async Task<TResponse> WaitForMessageAsync<TResponse>(CancellationToken ct = default) where TResponse : V3Request
        {
            var instance = Activator.CreateInstance(typeof(TResponse)) as V3Request;
            var req = new ReplyTask();
            var key = KeyValuePair.Create(instance.Cmd1, instance.Cmd2);
            _query[key] = req;

            try
            {
                var response = await req.Task.Task.WaitAsync(TimeSpan.FromMilliseconds(TimeoutMs), ct);
                return response as TResponse;
            }
            catch (TimeoutException)
            {
                LogDebug($"WAIR FOR {instance.Cmd1:X} {instance.Cmd2:X}, Timeout");
                throw new V3Exception("Request Timeout");
            }
            catch (TaskCanceledException)
            {
                LogDebug($"WAIR FOR {instance.Cmd1:X} {instance.Cmd2:X}, Connection Disposed");
                throw new V3Exception("Connection Disposed");
            }
            catch (Exception ex)
            {
                LogDebug($"WAIR FOR {instance.Cmd1:X} {instance.Cmd2:X}, Error {ex}");
                throw new V3Exception($"Unhandle Error, {ex}");
            }
            finally
            {
                _query.Remove(key, out _);
            }
        }

        public async Task<Message> SendAsync(V3Request msg, CancellationToken ct = default)
        {
            return await SendAsync(_clients.First(), msg, ct);
        }

        public async Task<Message> SendAsync(TcpConnection con, V3Request msg, CancellationToken ct = default)
        {
            if (!Online) throw new V3Exception("Client Not Connected");
            await con.Lock.WaitAsync(ct);

            try
            {
                var dataMessage = msg.Build();
                var req = new MessageRequest
                {
                    Message = dataMessage,
                };
                _request[msg.Seq] = req;

                await con.NetworkStream.WriteAsync(dataMessage.Build().ToArray(), ct);
                LogDebug($"SEND {msg.Cmd1:X} {msg.Cmd2:X}");

                try
                {
                    var res = await req.Task.Task.WaitAsync(TimeSpan.FromMilliseconds(TimeoutMs), ct);
                    _request.Remove(msg.Seq, out _);
                    return res;
                }
                catch (TimeoutException)
                {
                    LogDebug($"SEND {msg.Cmd1:X} {msg.Cmd2:X}, Timeout");
                    throw new V3Exception("Send Timeout");
                }
                catch (TaskCanceledException)
                {
                    LogDebug($"SEND {msg.Cmd1:X} {msg.Cmd2:X}, Connection Disposed");
                    throw new V3Exception("Connection Disposed");
                }
                catch (Exception ex)
                {
                    LogDebug($"SEND {msg.Cmd1:X} {msg.Cmd2:X}, Error {ex}");
                    throw new V3Exception($"Unhandle Error, {ex}");
                }
            }
            finally
            {
                con.Lock.Release();
            }
        }

        public virtual void Dispose()
        {
            Online = false;
            _cts.Cancel();
            foreach (var client in _clients)
            {
                client.NetworkStream.Dispose();
                client.TcpClient.Close();
                client.TcpClient.Dispose();
            }
            _clients.Clear();
        }
    }

    public class V3Exception : Exception
    {
        public V3Exception(string msg) : base(msg)
        {
        }

        public V3Exception(string msg, Exception ex) : base(msg, ex)
        {
        }
    }

    public class TcpConnection
    {
        public TcpClient TcpClient { get; set; }

        public NetworkStream NetworkStream { get; set; }

        public SemaphoreSlim Lock { get; set; } = new(1, 1);
    }

    public class MessageRequest
    {
        public TaskCompletionSource<Message> Task { get; set; } = new();

        public Message Message { get; set; }
    }

    public class ReplyTask
    {
        public TaskCompletionSource<V3Request> Task { get; set; } = new();
    }
}