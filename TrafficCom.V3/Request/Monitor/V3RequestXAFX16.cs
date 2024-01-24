using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX16 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x16;

        public ushort Big5Code { get; set; }

        public byte FrameRow { get; set; }

        public byte FrameColumn { get; set; }

        public byte FrameTotal { get; set; }

        public byte FrameNumber { get; set; }

        public List<byte> Bitmap { get; set; }

        public V3RequestXAFX16()
        {
        }

        public V3RequestXAFX16(DataMessage msg) : base(msg)
        {
            Big5Code = (ushort)(msg.Data[1] & msg.Data[0] << 8);
            FrameRow = msg.Data[2];
            FrameColumn = msg.Data[3];
            FrameTotal = msg.Data[4];
            FrameNumber = msg.Data[5];
            Bitmap = msg.Data.GetRange(6, msg.Data.Count - 5);
        }

        protected override List<byte> BuildData()
        {
            var res = new List<byte>
            {
                (byte)(Big5Code>>8),
                (byte)Big5Code,
                FrameRow,
                FrameColumn,
                FrameTotal,
                FrameNumber,
            };
            res.AddRange(Bitmap);
            return res;
        }
    }
}