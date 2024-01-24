using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX42 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x42;

        public byte TextId { get; set; }

        public V3RequestXAFX42()
        {
        }

        public V3RequestXAFX42(DataMessage msg) : base(msg)
        {
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                TextId,
            };
        }
    }
}