using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FXC2 : V3RequestX0FX12
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0xC2;

        public V3RequestX0FXC2()
        {
        }

        public V3RequestX0FXC2(DataMessage msg) : base(msg)
        {
        }
    }
}