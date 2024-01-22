using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX47 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x47;

        public V3RequestXAFX47()
        {
        }

        public V3RequestXAFX47(DataMessage msg) : base(msg)
        {
        }
    }
}