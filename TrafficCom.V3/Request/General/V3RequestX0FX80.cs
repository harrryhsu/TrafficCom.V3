using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX80 : V3Request
    {
        public override byte Cmd1 => 0x0f;

        public override byte Cmd2 => 0x80;

        public byte EquipmentCode { get; set; }

        public byte CommandCode { get; set; }

        public V3RequestX0FX80()
        {
        }

        public V3RequestX0FX80(DataMessage msg) : base(msg)
        {
            EquipmentCode = msg.Data[0];
            CommandCode = msg.Data[1];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                EquipmentCode,
                CommandCode
            };
        }
    }
}