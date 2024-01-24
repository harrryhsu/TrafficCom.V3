using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FXC4 : V3RequestX0FX14
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0xC4;

        public V3RequestX0FXC4()
        {
        }

        public V3RequestX0FXC4(DataMessage msg) : base(msg)
        {
        }
    }
}