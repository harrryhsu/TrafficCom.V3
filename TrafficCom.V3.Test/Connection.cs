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
                    await Task.Delay(100);
                    var message = await _client.SendAndWaitForReplyAsync<V3RequestXAFXC4>(new V3RequestXAFX14 { });
                    Assert.That(message, Is.InstanceOf(typeof(V3RequestXAFXC4)));
                })
            );
        }

        [Test]
        public void Request_Command_Code_Match()
        {
            _client.RequestFactory.VerifyCache();
        }

        [Test]
        public void Request_Command_Code_Not_Match()
        {
            Assert.Catch(() =>
            {
                _client.RequestFactory.Set(KeyValuePair.Create<byte, byte>(1, 2), typeof(V3RequestXE4X02));
            });
        }
    }
}