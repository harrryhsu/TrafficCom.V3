namespace TrafficCom.V3.Test
{
    [NonParallelizable]
    [TestFixture]
    public class Connection : V3TestBase
    {
        [Test]
        public async Task Basic_Data_Exchange()
        {
            var waiter = _server.WaitForMessageAsync<V3RequestXAFX14>();
            await _client.SendAsync(new V3RequestXAFX14 { });

            var message = await waiter;

            Assert.That(message, Is.InstanceOf(typeof(V3RequestXAFX14)));
        }

        [Test]
        public async Task Two_Way_Data_Exchange()
        {
            await Task.WhenAll(
                Task.Run(async () =>
                {
                    var message = await _server.WaitForMessageAsync<V3RequestXAFX14>();
                    Assert.That(message, Is.InstanceOf(typeof(V3RequestXAFX14)));
                    await _server.SendAsync(new V3RequestXAFXC4 { });
                }),
                Task.Run(async () =>
                {
                    var message = await _client.SendAndWaitForReplyAsync<V3RequestXAFXC4>(new V3RequestXAFX14 { });
                    Assert.That(message, Is.InstanceOf(typeof(V3RequestXAFXC4)));
                })
            );
        }
    }
}