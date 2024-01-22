using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX61 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x61;

        public V3RequestXAFX61()
        {
        }

        public V3RequestXAFX61(DataMessage msg) : base(msg)
        {
        }
    }
}