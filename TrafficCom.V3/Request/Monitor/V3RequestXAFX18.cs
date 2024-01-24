using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX18 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x18;

        public ushort PatternCode { get; set; }

        public byte FrameRow { get; set; }

        public byte FrameColumn { get; set; }

        public byte FrameTotal { get; set; }

        public byte FrameNumber { get; set; }

        public List<byte> ColorMap { get; set; }

        public V3RequestXAFX18()
        {
        }

        public V3RequestXAFX18(DataMessage msg) : base(msg)
        {
            PatternCode = (ushort)(msg.Data[1] & msg.Data[0] << 8);
            FrameRow = msg.Data[2];
            FrameColumn = msg.Data[3];
            FrameTotal = msg.Data[4];
            FrameNumber = msg.Data[5];
            ColorMap = msg.Data.GetRange(6, msg.Data.Count - 5);
        }

        protected override List<byte> BuildData()
        {
            var res = new List<byte>
            {
                (byte)(PatternCode>>8),
                (byte)PatternCode,
                FrameRow,
                FrameColumn,
                FrameTotal,
                FrameNumber,
            };
            res.AddRange(ColorMap);
            return res;
        }
    }
}