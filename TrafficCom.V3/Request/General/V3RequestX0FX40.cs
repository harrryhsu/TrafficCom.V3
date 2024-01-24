using TrafficCom.V3.Connection;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX40 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x40;

        public byte EquipmentNumber { get; set; }

        public bool SearchAllEquipment
        {
            get => EquipmentNumber == 0xFF;
            set
            {
                if (value)
                {
                    EquipmentNumber = 0xFF;
                }
                else
                {
                    EquipmentNumber = 0;
                }
            }
        }

        public V3RequestX0FX40()
        {
        }

        public V3RequestX0FX40(DataMessage msg) : base(msg)
        {
            EquipmentNumber = msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            if (EquipmentNumber < 0 || EquipmentNumber > 8) throw new V3Exception("EquipmentNumber out of range");

            return new List<byte>
            {
                EquipmentNumber
            };
        }
    }
}