using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX8E : V3RequestX0FX8F
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x8E;

        public V3RequestX0FX8E()
        {
        }

        public V3RequestX0FX8E(DataMessage msg) : base(msg)
        {
        }
    }
}