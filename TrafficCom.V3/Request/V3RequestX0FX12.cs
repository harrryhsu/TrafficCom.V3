using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX12 : V3Request
    {
        public override byte Cmd1 => 0x0f;

        public override byte Cmd2 => 0x12;

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Week { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Second { get; set; }

        public V3RequestX0FX12()
        {
        }

        public V3RequestX0FX12(DataMessage msg) : base(msg)
        {
            Year = msg.Data[0];
            Month = msg.Data[1];
            Day = msg.Data[2];
            Week = msg.Data[3];
            Hour = msg.Data[4];
            Minute = msg.Data[5];
            Second = msg.Data[6];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte> {
                (byte)Year,
                (byte)Month,
                (byte)Day,
                (byte)Week,
                (byte)Hour,
                (byte)Minute,
                (byte)Second,
            };
        }
    }
}