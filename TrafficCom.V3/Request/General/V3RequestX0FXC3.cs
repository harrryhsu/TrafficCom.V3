using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FXC3 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0xC3;

        public DateTime Time { get; set; }

        public byte CompanyId { get; set; }

        public byte Version { get; set; }

        public V3CommandLevel CommandLevel { get; set; }

        public V3RequestX0FXC3()
        {
        }

        public V3RequestX0FXC3(DataMessage msg) : base(msg)
        {
            Time = new DateTime(msg.Data[0], msg.Data[1], msg.Data[2]);
            CompanyId = msg.Data[3];
            Version = msg.Data[4];
            CommandLevel = (V3CommandLevel)msg.Data[5];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)Time.Year,
                (byte)Time.Month,
                (byte)Time.Day,
                CompanyId,
                Version,
                (byte)CommandLevel
            };
        }
    }
}