using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX40 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x40;

        public V3RequestXAFX40()
        {
        }

        public V3RequestXAFX40(DataMessage msg) : base(msg)
        {
        }
    }
}