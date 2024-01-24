using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX91 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x91;

        public V3RequestX0FX91()
        {
        }

        public V3RequestX0FX91(DataMessage msg) : base(msg)
        {
        }
    }
}