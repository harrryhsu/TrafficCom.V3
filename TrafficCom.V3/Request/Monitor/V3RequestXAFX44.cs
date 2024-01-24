using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX44 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x44;

        public V3RequestXAFX44()
        {
        }

        public V3RequestXAFX44(DataMessage msg) : base(msg)
        {
        }
    }
}