using Api.Services;
using TrafficCom.V3.Helper;

namespace TrafficCom.V3.Test
{
    [NonParallelizable]
    [TestFixture]
    public class Tests : V3TestBase
    {
        private MonitorClient _monitor;

        private MonitorTextEntry _textCache;

        public override async Task Setup()
        {
            await base.Setup();
            _monitor = new(_client);
            _textCache = new();
            _server.OnMessageReceived += async (sender, con, request) =>
            {
                if (request is V3RequestXAFX11 setTextRequest)
                {
                    _textCache.Text = setTextRequest.Text;
                    _textCache.Id = setTextRequest.TextId;
                    await sender.SendAsync(new V3RequestX0FX80());
                }
                else if (request is V3RequestXAFX12 setColorRequest)
                {
                    _textCache.BackgroundColor = setColorRequest.BackgroundColor;
                    _textCache.TextColor = setColorRequest.TextColor;
                    _textCache.BlinkInterval = setColorRequest.BlinkInterval;
                    _textCache.HBound = setColorRequest.HBound;
                    _textCache.VBound = setColorRequest.VBound;
                    _textCache.HSpace = setColorRequest.HSpace;
                    _textCache.VSpace = setColorRequest.VSpace;
                    await sender.SendAsync(new V3RequestX0FX80());
                }
                else if (request is V3RequestXAFX41 getTextRequest)
                {
                    await sender.SendAsync(new V3RequestXAFXC1
                    {
                        TextId = _textCache.Id,
                        Text = _textCache.Text,
                    });
                }
                else if (request is V3RequestXAFX42 getColorRequest)
                {
                    await sender.SendAsync(new V3RequestXAFXC2
                    {
                        TextId = _textCache.Id,
                        TextLength = (byte)EncoderHelper.GetBig5EncodedLength(_textCache.Text),
                        BackgroundColor = _textCache.BackgroundColor,
                        TextColor = _textCache.TextColor,
                        BlinkInterval = _textCache.BlinkInterval,
                        HBound = _textCache.HBound,
                        VBound = _textCache.VBound,
                        HSpace = _textCache.HSpace,
                        VSpace = _textCache.VSpace
                    });
                }
            };
        }

        [Test]
        public async Task Text_Send()
        {
            var entry = new MonitorTextEntry
            {
                Id = 1,
                Text = "Test",
                Show = true,
                TextColor = MonitorColor.Red,
                BackgroundColor = MonitorColor.Green,
                BlinkInterval = MonitorBlinkInterval.S0P5,
                HBound = 1,
                VBound = 2,
                HSpace = 3,
                VSpace = 4,
            };

            await _monitor.SendSingleText(entry);
            var reply = await _monitor.GetSingleText(entry.Id);

            Assert.Multiple(() =>
            {
                Assert.That(entry.Text, Is.EqualTo(reply.Text));
                Assert.That(entry.TextColor, Is.EqualTo(reply.TextColor));
                Assert.That(entry.BackgroundColor, Is.EqualTo(reply.BackgroundColor));
                Assert.That(entry.BlinkInterval, Is.EqualTo(reply.BlinkInterval));
                Assert.That(entry.HBound, Is.EqualTo(reply.HBound));
                Assert.That(entry.VBound, Is.EqualTo(reply.VBound));
                Assert.That(entry.HSpace, Is.EqualTo(reply.HSpace));
                Assert.That(entry.VSpace, Is.EqualTo(reply.VSpace));
            });
        }
    }
}