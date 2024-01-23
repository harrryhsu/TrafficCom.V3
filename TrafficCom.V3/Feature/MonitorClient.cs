using TrafficCom.V3.Connection;
using TrafficCom.V3.Helper;
using TrafficCom.V3.Request;

namespace TrafficCom.V3.Feature
{
    public class MonitorClient
    {
        private readonly V3Client _client;

        public MonitorClient(V3Client client)
        {
            _client = client;
        }

        public async Task SetLoopingDisplay(MonitorLoopingSetting setting)
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestXAFX10
            {
                TextIds = setting.TextIds.ToList(),
                DisplayTime = setting.Interval,
            });
        }

        public async Task<MonitorLoopingSetting> GetLoopingDisplay()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestXAFXC0>(new V3RequestXAFX40 { });
            return new MonitorLoopingSetting
            {
                Interval = res.DisplayTime,
                TextIds = res.TextIds.ToList(),
            };
        }

        public async Task SendSingleText(MonitorTextEntry entry)
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestXAFX11
            {
                TextId = entry.Id,
                Show = entry.Show,
                Text = entry.Text,
            });

            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestXAFX12
            {
                TextId = entry.Id,
                TextLength = (byte)EncoderHelper.GetBig5EncodedLength(entry.Text),
                BackgroundColor = entry.BackgroundColor,
                TextColor = entry.TextColor,
                BlinkInterval = entry.BlinkInterval,
                HBound = entry.HBound,
                VBound = entry.VBound,
                HSpace = entry.HSpace,
                VSpace = entry.VSpace,
            });
        }

        public async Task<MonitorTextEntry> GetSingleText(byte id)
        {
            var text = await _client.SendAndWaitForReplyAsync<V3RequestXAFXC1>(new V3RequestXAFX41
            {
                TextId = id,
            });

            var color = await _client.SendAndWaitForReplyAsync<V3RequestXAFXC2>(new V3RequestXAFX42
            {
                TextId = id,
            });

            return new MonitorTextEntry
            {
                Id = id,
                Text = text.Text,
                BackgroundColor = color.BackgroundColor,
                TextColor = color.TextColor,
                BlinkInterval = color.BlinkInterval,
                HBound = color.HBound,
                VBound = color.VBound,
                HSpace = color.HSpace,
                VSpace = color.VSpace,
            };
        }

        public async Task SetCurrentDisplay(byte id)
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestXAFX13
            {
                TextId = id,
            });
        }

        public async Task<MonitorTextEntry> GetCurrentDisplay()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestXAFXC3>(new V3RequestXAFX43 { });
            return await GetSingleText(res.TextId);
        }

        public async Task ClearCurrentDisplay()
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestXAFX14 { });
        }
    }

    public class MonitorTextEntry
    {
        public byte Id { get; set; }

        public string Text { get; set; }

        public bool Show { get; set; }

        public MonitorColor TextColor { get; set; }

        public MonitorColor BackgroundColor { get; set; }

        public MonitorBlinkInterval BlinkInterval { get; set; }

        public byte VBound { get; set; }

        public byte HBound { get; set; }

        public ushort VSpace { get; set; }

        public ushort HSpace { get; set; }
    }

    public class MonitorLoopingSetting
    {
        public List<byte> TextIds { get; set; } = new();

        public byte Interval { get; set; }
    }
}