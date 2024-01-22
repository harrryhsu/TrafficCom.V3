using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public enum MonitorBrightness
    {
        B65 = 0b0001,

        B80 = 0b0010,

        B100 = 0b0100,
    }

    public enum MonitorBrightnessMode
    {
        Disable = 0b0001,

        Force = 0b0010,

        RealTime = 0b1000,
    }

    public class V3RequestXAFX3E : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x3E;

        public MonitorBrightness Brightness { get; set; }

        public MonitorBrightnessMode BrightnessMode { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public V3RequestXAFX3E()
        {
        }

        public V3RequestXAFX3E(DataMessage msg) : base(msg)
        {
            Brightness = (MonitorBrightness)(msg.Data[0] & 0b1111);
            BrightnessMode = (MonitorBrightnessMode)(msg.Data[0] >> 4);
            From = new DateTime(1900, 1, 1, msg.Data[1], msg.Data[2], 0);
            To = new DateTime(1900, 1, 1, msg.Data[3], msg.Data[4], 0);
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)((byte)Brightness & ((byte)BrightnessMode<<4)),
                (byte)From.Hour,
                (byte)From.Minute,
                (byte)To.Hour,
                (byte)To.Minute,
            };
        }
    }
}