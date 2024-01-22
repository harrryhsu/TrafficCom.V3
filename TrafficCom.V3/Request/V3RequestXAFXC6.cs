using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXC6 : V3RequestXAFX16
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xC6;

        public V3RequestXAFXC6()
        {
        }

        public V3RequestXAFXC6(DataMessage msg) : base(msg)
        {
        }
    }
}