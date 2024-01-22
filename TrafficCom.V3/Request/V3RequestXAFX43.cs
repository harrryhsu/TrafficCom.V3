using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX43 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x43;

        public V3RequestXAFX43()
        {
        }

        public V3RequestXAFX43(DataMessage msg) : base(msg)
        {
        }
    }
}