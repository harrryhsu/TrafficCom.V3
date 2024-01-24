using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public enum MonitorColor
    {
        Black = 0,

        Green,

        Red,

        Yellow
    }

    public enum MonitorBlinkInterval
    {
        None = 0,

        S1,

        S0P5,

        S2,
    }

    public class V3RequestXAFX12 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x12;

        public byte TextId { get; set; }

        public byte TextLength { get; set; }

        public MonitorColor TextColor { get; set; }

        public MonitorColor BackgroundColor { get; set; }

        public MonitorBlinkInterval BlinkInterval { get; set; }

        public byte VBound { get; set; }

        public byte HBound { get; set; }

        public ushort VSpace { get; set; }

        public ushort HSpace { get; set; }

        public V3RequestXAFX12()
        {
        }

        public V3RequestXAFX12(DataMessage msg) : base(msg)
        {
            TextId = msg.Data[0];
            TextLength = msg.Data[1];
            TextColor = (MonitorColor)(msg.Data[2] & 0b11);
            BackgroundColor = (MonitorColor)(msg.Data[2] >> 2 & 0b11);
            BlinkInterval = (MonitorBlinkInterval)(msg.Data[2] >> 4 & 0b11);
            VBound = msg.Data[TextLength + 2];
            HBound = msg.Data[TextLength + 3];
            VSpace = (ushort)(msg.Data[TextLength + 4] << 8 | msg.Data[TextLength + 5]);
            HSpace = (ushort)(msg.Data[TextLength + 6] << 8 | msg.Data[TextLength + 7]);
        }

        protected override List<byte> BuildData()
        {
            var data = new List<byte>
            {
                TextId,
                TextLength,
            };

            var color = (byte)BlinkInterval << 4 | (byte)BackgroundColor << 2 | (byte)TextColor;

            data.AddRange(Enumerable.Repeat((byte)color, TextLength));
            data.AddRange(new byte[]
            {
                VBound,
                HBound,
                (byte)(VSpace>>8),
                (byte)VSpace,
                (byte)(HSpace>>8),
                (byte)HSpace,
            });

            return data;
        }
    }
}