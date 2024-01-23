using System.Diagnostics;

namespace TrafficCom.V3.Test
{
    [NonParallelizable]
    [TestFixture]
    public abstract class V3TestBase
    {
        protected V3Client _client;

        protected V3Server _server;

        [SetUp]
        public virtual async Task Setup()
        {
            _server = new V3Server(1000, 3000);
            _client = new V3Client("127.0.0.1", 1000, 3000);
            _server.Debug = true;
            _client.Debug = true;
            _server.Logger = (msg) => Console.WriteLine(DateTime.Now.ToString("ss:fff") + " Server " + msg);
            _client.Logger = (msg) => Console.WriteLine(DateTime.Now.ToString("ss:fff") + " Client " + msg);

            await _server.ConnectAsync();
            await _client.ConnectAsync();
        }

        [TearDown]
        public virtual void Cleanup()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }
    }
}