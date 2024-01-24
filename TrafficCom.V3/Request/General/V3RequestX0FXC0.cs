using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FXC0 : V3RequestX0FX40
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0xC0;

        public byte Count { get; set; }

        public Dictionary<byte, ushort> EquipmentIds = new();

        public V3RequestX0FXC0()
        {
        }

        public V3RequestX0FXC0(DataMessage msg) : base(msg)
        {
            Count = msg.Data[1];
            for (int i = 0; i < Count * 3; i += 3)
            {
                EquipmentIds[msg.Data[i]] = (ushort)(msg.Data[i + 1] << 8 & msg.Data[i + 2]);
            }
        }

        protected override List<byte> BuildData()
        {
            var res = new List<byte>
            {
                EquipmentNumber,
                Count,
            };
            foreach (var item in EquipmentIds)
            {
                res.Add(item.Key);
                res.Add((byte)(item.Value >> 8));
                res.Add((byte)item.Value);
            }
            return res;
        }
    }
}