using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXC0 : V3RequestXAFX10
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xC0;

        public V3RequestXAFXC0()
        {
        }

        public V3RequestXAFXC0(DataMessage msg) : base(msg)
        {
        }
    }
}