using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestUnknown : V3Request
    {
        public override byte Cmd1 => 0;

        public override byte Cmd2 => 0;

        public V3RequestUnknown()
        {
        }

        public V3RequestUnknown(DataMessage msg) : base(msg)
        {
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>();
        }
    }
}