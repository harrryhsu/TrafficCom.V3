using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX14 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x14;

        public V3RequestXAFX14()
        {
        }

        public V3RequestXAFX14(DataMessage msg) : base(msg)
        {
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
            };
        }
    }
}