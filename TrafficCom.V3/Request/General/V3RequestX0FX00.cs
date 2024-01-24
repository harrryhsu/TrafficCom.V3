using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX00 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x00;

        public DateTime Time { get; set; }

        public V3RequestX0FX00()
        {
        }

        public V3RequestX0FX00(DataMessage msg) : base(msg)
        {
            Time = new DateTime(1900, msg.Data[0], msg.Data[1], msg.Data[2], msg.Data[3], 0);
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)Time.Month,
                (byte)Time.Day,
                (byte)Time.Hour,
                (byte)Time.Minute
            };
        }
    }
}