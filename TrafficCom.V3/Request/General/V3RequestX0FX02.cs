using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX02 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x02;

        public V3RequestX0FX02()
        {
        }

        public V3RequestX0FX02(DataMessage msg) : base(msg)
        {
        }
    }
}