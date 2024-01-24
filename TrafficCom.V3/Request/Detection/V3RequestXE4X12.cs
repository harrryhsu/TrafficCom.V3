using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXE4X12 : V3Request
    {
        public override byte Cmd1 => 0xe4;

        public override byte Cmd2 => 0x12;

        public byte VehicleIdCycle { get; set; }

        public V3RequestXE4X12()
        {
        }

        public V3RequestXE4X12(DataMessage msg) : base(msg)
        {
            VehicleIdCycle = msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte> { VehicleIdCycle };
        }
    }
}