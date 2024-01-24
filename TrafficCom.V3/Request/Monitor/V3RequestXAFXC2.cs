using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXC2 : V3RequestXAFX12
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xC2;

        public V3RequestXAFXC2()
        {
        }

        public V3RequestXAFXC2(DataMessage msg) : base(msg)
        {
        }
    }
}