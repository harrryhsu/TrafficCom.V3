using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX90 : V3RequestX0FX10
    {
        public override byte Cmd1 => 0x0f;

        public override byte Cmd2 => 0x90;

        public V3RequestX0FX90()
        {
        }

        public V3RequestX0FX90(DataMessage msg) : base(msg)
        {
        }
    }
}