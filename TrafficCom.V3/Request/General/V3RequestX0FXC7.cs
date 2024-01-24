using TrafficCom.V3.Connection;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FXC7 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0xC7;

        public ushort Protocol { get; set; }

        public bool IsSupported { get; set; }

        public V3RequestX0FXC7()
        {
        }

        public V3RequestX0FXC7(DataMessage msg) : base(msg)
        {
            if (msg.Data.Count == 2)
            {
                Protocol = msg.Data[0];
                IsSupported = msg.Data[1] == 1;
            }
            else if (msg.Data.Count == 3)
            {
                Protocol = (ushort)(msg.Data[0] << 8 & msg.Data[1]);
                IsSupported = msg.Data[2] == 1;
            }
            else throw new V3Exception("Invalid protocol size");
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)(Protocol >> 8),
                (byte)Protocol,
                (byte)(IsSupported ? 1 : 0)
            };
        }
    }
}