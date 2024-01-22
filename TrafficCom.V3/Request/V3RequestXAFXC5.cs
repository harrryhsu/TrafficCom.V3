using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXC5 : V3RequestXAFX15
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xC5;

        public V3RequestXAFXC5()
        {
        }

        public V3RequestXAFXC5(DataMessage msg) : base(msg)
        {
        }
    }
}