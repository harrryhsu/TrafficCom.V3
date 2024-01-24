using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX6E : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x6E;

        public V3RequestXAFX6E()
        {
        }

        public V3RequestXAFX6E(DataMessage msg) : base(msg)
        {
        }
    }
}