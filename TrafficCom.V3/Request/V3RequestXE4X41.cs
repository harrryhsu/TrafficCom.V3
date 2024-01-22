using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXE4X41 : V3Request
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public override byte Cmd1 => 0xe4;

        public override byte Cmd2 => 0x41;

        public V3RequestXE4X41()
        {
        }

        public V3RequestXE4X41(DataMessage msg) : base(msg)
        {
            From = new DateTime(msg.Data[0] + 1911, msg.Data[1], msg.Data[2], msg.Data[3], msg.Data[4], msg.Data[5]);
            To = new DateTime(msg.Data[6] + 1911, msg.Data[7], msg.Data[8], msg.Data[9], msg.Data[10], msg.Data[11]);
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)(From.Year-1911),
                (byte)From.Month,
                (byte)From.Day,
                (byte)From.Hour,
                (byte)From.Minute,
                (byte)From.Second,
                (byte)(From.Year-1911),
                (byte)To.Month,
                (byte)To.Day,
                (byte)To.Hour,
                (byte)To.Minute,
                (byte)To.Second,
            };
        }
    }
}