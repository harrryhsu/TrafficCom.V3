using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX10 : V3Request
    {
        public override byte Cmd1 => 0x0f;

        public override byte Cmd2 => 0x10;

        public byte Reset1 { get; set; }

        public byte Reset2 { get; set; }

        public V3RequestX0FX10()
        {
        }

        public V3RequestX0FX10(DataMessage msg) : base(msg)
        {
            Reset1 = msg.Data[0];
            Reset2 = msg.Data[1];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte> { Reset1, Reset2 };
        }
    }
}