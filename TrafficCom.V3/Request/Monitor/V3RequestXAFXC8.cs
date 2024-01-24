using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXC8 : V3RequestXAFX18
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xC8;

        public V3RequestXAFXC8()
        {
        }

        public V3RequestXAFXC8(DataMessage msg) : base(msg)
        {
        }
    }
}