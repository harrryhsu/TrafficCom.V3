using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FXC6 : V3RequestX0FX16
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0xC6;

        public V3RequestX0FXC6()
        {
        }

        public V3RequestX0FXC6(DataMessage msg) : base(msg)
        {
        }
    }
}