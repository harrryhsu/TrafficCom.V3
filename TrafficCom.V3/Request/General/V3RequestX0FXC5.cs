using TrafficCom.V3.Connection;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FXC5 : V3RequestX0FX15
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0xC5;

        public V3RequestX0FXC5()
        {
        }

        public V3RequestX0FXC5(DataMessage msg) : base(msg)
        {
        }
    }
}