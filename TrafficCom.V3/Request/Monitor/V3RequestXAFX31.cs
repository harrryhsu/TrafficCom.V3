using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public enum MonitorOfflineDisplayMode
    {
        OffDisplay = 1,

        Loop,

        DisplayCurrent,
    }

    public class V3RequestXAFX31 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x31;

        public MonitorOfflineDisplayMode OfflineDisplayMode { get; set; }

        public V3RequestXAFX31()
        {
        }

        public V3RequestXAFX31(DataMessage msg) : base(msg)
        {
            OfflineDisplayMode = (MonitorOfflineDisplayMode)msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)OfflineDisplayMode
            };
        }
    }
}