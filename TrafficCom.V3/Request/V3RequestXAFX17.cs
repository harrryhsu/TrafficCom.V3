using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX17 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x17;

        public ushort Big5Code { get; set; }

        public V3RequestXAFX17()
        {
        }

        public V3RequestXAFX17(DataMessage msg) : base(msg)
        {
            Big5Code = msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)Big5Code,
                (byte)(Big5Code<<8),
            };
        }
    }
}