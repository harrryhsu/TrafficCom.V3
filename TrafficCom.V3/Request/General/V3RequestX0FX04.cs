using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX04 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x04;

        public ushort HardwareStatus { get; set; }

        public V3RequestX0FX04()
        {
        }

        public V3RequestX0FX04(DataMessage msg) : base(msg)
        {
            HardwareStatus = (ushort)(msg.Data[0] << 8 & msg.Data[1]);
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)(HardwareStatus >> 8),
                (byte)HardwareStatus
            };
        }
    }
}