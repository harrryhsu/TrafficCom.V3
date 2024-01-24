using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX12 : V3Request
    {
        public override byte Cmd1 => 0x0f;

        public override byte Cmd2 => 0x12;

        public DateTime Time { get; set; } = DateTime.Now;

        public DayOfWeek Week { get; set; }

        public V3RequestX0FX12()
        {
        }

        public V3RequestX0FX12(DataMessage msg) : base(msg)
        {
            Time = new DateTime(
                msg.Data[0],
                msg.Data[1],
                msg.Data[2],
                msg.Data[4],
                msg.Data[5],
                msg.Data[5]
            );

            Week = (DayOfWeek)(msg.Data[3] % 7);
        }

        protected override List<byte> BuildData()
        {
            return new List<byte> {
                (byte)Time.Year,
                (byte)Time.Month,
                (byte)Time.Day,
                (byte)(Week == DayOfWeek.Sunday ? 7 : (byte)Week),
                (byte)Time.Hour,
                (byte)Time.Minute,
                (byte)Time.Second,
            };
        }
    }
}