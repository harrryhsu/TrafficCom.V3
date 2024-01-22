using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXEE : V3RequestXAFX3E
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xEE;

        public V3RequestXAFXEE()
        {
        }

        public V3RequestXAFXEE(DataMessage msg) : base(msg)
        {
        }
    }
}