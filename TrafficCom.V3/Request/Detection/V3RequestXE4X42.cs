using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXE4X42 : V3Request
    {
        public override byte Cmd1 => 0xe4;

        public override byte Cmd2 => 0x42;

        public V3RequestXE4X42()
        {
        }

        public V3RequestXE4X42(DataMessage msg) : base(msg)
        {
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>();
        }
    }
}