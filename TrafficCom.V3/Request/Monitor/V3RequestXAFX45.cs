using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX45 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x45;

        public V3RequestXAFX45()
        {
        }

        public V3RequestXAFX45(DataMessage msg) : base(msg)
        {
        }
    }
}