using System.Net;
using System.Net.Sockets;

namespace TrafficCom.V3.Connection
{
    public class V3Server : V3Client, IHostedService
    {
        private readonly TcpListener _tcp;

        public V3Server(int port, int timeout = DefaultTimeout) : base(new IPEndPoint(IPAddress.Any, port), timeout)
        {
            _tcp = new(Target);
        }

        public override Task ConnectAsync(CancellationToken token = default)
        {
            _tcp.Start();
            _ = V3ServerWorker();
            Online = true;
            return Task.CompletedTask;
        }

        private async Task V3ServerWorker()
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    TcpClient handler = await _tcp.AcceptTcpClientAsync(_cts.Token);
                    NetworkStream stream = handler.GetStream();
                    var con = new TcpConnection { TcpClient = handler, NetworkStream = stream };
                    con.NetworkStream.ReadTimeout = TimeoutMs;
                    con.NetworkStream.WriteTimeout = TimeoutMs;
                    _clients.Add(con);
                    _ = V3ClientWorker(con);
                }
                catch
                {
                }
            }
        }

        public async Task StartAsync(CancellationToken ct)
        {
            await ConnectAsync(ct);
        }

        public Task StopAsync(CancellationToken ct)
        {
            Dispose();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
            _tcp.Stop();
        }
    }
}