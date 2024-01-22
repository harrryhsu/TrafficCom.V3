using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX13 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x13;

        public byte TextId { get; set; }

        public V3RequestXAFX13()
        {
        }

        public V3RequestXAFX13(DataMessage msg) : base(msg)
        {
            TextId = msg.Data[0];
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