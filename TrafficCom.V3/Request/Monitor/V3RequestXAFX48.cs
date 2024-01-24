using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX48 : V3RequestXAFX18
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x48;

        public V3RequestXAFX48()
        {
        }

        public V3RequestXAFX48(DataMessage msg) : base(msg)
        {
            PatternCode = (ushort)(msg.Data[1] & msg.Data[0] << 8);
            FrameRow = msg.Data[2];
            FrameColumn = msg.Data[3];
            FrameNumber = msg.Data[5];
        }

        protected override List<byte> BuildData()
        {
            var res = new List<byte>
            {
                (byte)(PatternCode>>8),
                (byte)PatternCode,
                FrameRow,
                FrameColumn,
                FrameNumber,
            };
            res.AddRange(ColorMap);
            return res;
        }
    }
}