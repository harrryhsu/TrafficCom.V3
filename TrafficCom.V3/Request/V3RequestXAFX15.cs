using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX15 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x15;

        public byte TextId { get; set; }

        public byte StartX { get; set; }

        public byte StartY { get; set; }

        public byte WindowWide { get; set; }

        public byte WindowHigh { get; set; }

        public byte StepX { get; set; }

        public byte StepY { get; set; }

        public byte Speed { get; set; }

        public byte Loops { get; set; }

        public V3RequestXAFX15()
        {
        }

        public V3RequestXAFX15(DataMessage msg) : base(msg)
        {
            TextId = msg.Data[0];
            StartX = msg.Data[1];
            StartY = msg.Data[2];
            WindowWide = msg.Data[3];
            WindowHigh = msg.Data[4];
            StepX = msg.Data[5];
            StepY = msg.Data[6];
            Speed = msg.Data[7];
            Loops = msg.Data[8];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                TextId,
                StartX,
                StartY,
                WindowWide,
                WindowHigh,
                StepX,
                StepY,
                Speed,
                Loops,
            };
        }
    }
}