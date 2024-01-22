using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX92 : V3Request
    {
        public override byte Cmd1 => 0x0f;

        public override byte Cmd2 => 0x12;

        public byte Diff { get; set; }

        public V3RequestX0FX92()
        {
        }

        public V3RequestX0FX92(DataMessage msg) : base(msg)
        {
            Diff = msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                Diff
            };
        }
    }
}