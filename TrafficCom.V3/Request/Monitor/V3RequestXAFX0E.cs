using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX0E : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x0E;

        public bool IsStart { get; set; }

        public V3RequestXAFX0E()
        {
        }

        public V3RequestXAFX0E(DataMessage msg) : base(msg)
        {
            IsStart = msg.Data[0] == 1;
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)(IsStart ? 1 : 0),
            };
        }
    }
}