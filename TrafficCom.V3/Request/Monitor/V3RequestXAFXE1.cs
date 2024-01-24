using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXE1 : V3RequestXAFX31
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xE1;

        public V3RequestXAFXE1()
        {
        }

        public V3RequestXAFXE1(DataMessage msg) : base(msg)
        {
        }
    }
}