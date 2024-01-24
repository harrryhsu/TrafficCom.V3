using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public enum V3HardwareReportInterval
    {
        Stop = 0,

        S1,

        S2,

        S5,

        M1,

        M5
    }

    public class V3RequestX0FX14 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x14;

        public V3HardwareReportInterval Interval { get; set; }

        public V3RequestX0FX14()
        {
        }

        public V3RequestX0FX14(DataMessage msg) : base(msg)
        {
            Interval = (V3HardwareReportInterval)msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)Interval
            };
        }
    }
}