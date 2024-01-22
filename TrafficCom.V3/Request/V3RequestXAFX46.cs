using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX46 : V3RequestXAFX16
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x46;

        public V3RequestXAFX46()
        {
        }

        public V3RequestXAFX46(DataMessage msg) : base(msg)
        {
            Big5Code = msg.Data[0];
            FrameRow = msg.Data[1];
            FrameColumn = msg.Data[2];
            FrameNumber = msg.Data[4];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)Big5Code,
                (byte)(Big5Code<<8),
                FrameRow,
                FrameColumn,
                FrameNumber,
            };
        }
    }
}