using System.Text;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXE4X02 : V3Request
    {
        public override byte Cmd1 => 0xe4;

        public override byte Cmd2 => 0x02;

        public ushort VehicleNo { get; set; }

        public byte VehicleLen { get; set; }

        public List<V3RequestVehicleRecord> VehicleRecords { get; set; } = new();

        public V3RequestXE4X02()
        {
        }

        public V3RequestXE4X02(DataMessage msg) : base(msg)
        {
            VehicleNo = (ushort)(msg.Data[0] << 8 | msg.Data[1]);
            VehicleLen = msg.Data[2];

            var offset = 3;
            var data = msg.Data.ToArray();
            for (var i = 0; i < VehicleNo; i++)
            {
                VehicleRecords.Add(new V3RequestVehicleRecord
                {
                    VehicleId = Encoding.ASCII.GetString(data, offset, VehicleLen),
                    LaneNo = data[offset + VehicleLen],
                    Color = data[offset + VehicleLen + 1],
                    Recorded = new DateTime(
                        data[offset + VehicleLen + 2] + 1911,
                        data[offset + VehicleLen + 3],
                        data[offset + VehicleLen + 4],
                        data[offset + VehicleLen + 5],
                        data[offset + VehicleLen + 6],
                        data[offset + VehicleLen + 7]
                    )
                });

                offset += VehicleLen + 8;
            }
        }

        protected override List<byte> BuildData()
        {
            var data = new List<byte>
            {
                (byte)(VehicleNo>>8),
                (byte)VehicleNo,
                VehicleLen
            };

            foreach (var record in VehicleRecords)
            {
                var id = Encoding.ASCII.GetBytes(record.VehicleId);
                var idPad = new byte[VehicleLen];
                Array.Fill<byte>(idPad, 0x20);
                Buffer.BlockCopy(id, 0, idPad, 0, id.Length);

                data.AddRange(idPad);
                data.Add(record.LaneNo);
                data.Add(record.Color);
                data.AddRange(new byte[]
                {
                    (byte)(record.Recorded.Year-1911),
                    (byte)record.Recorded.Month,
                    (byte)record.Recorded.Day,
                    (byte)record.Recorded.Hour,
                    (byte)record.Recorded.Minute,
                    (byte)record.Recorded.Second,
                });
            }

            return data;
        }
    }
}