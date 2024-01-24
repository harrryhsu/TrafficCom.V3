using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public enum V3ErrorCode
    {
        MessageUndefined = 0b1,

        UnableToReply = 0b10,

        ParameterError = 0b100,

        CountError = 0b1000,

        EquipmentTypeError = 0b10000,

        Timeout = 0b100000,

        ExceedHardwareLimit = 0b1000000,

        Excluded = 0b10000000,
    }

    public class V3RequestX0FX81 : V3Request
    {
        public override byte Cmd1 => 0x0f;

        public override byte Cmd2 => 0x81;

        public byte EquipmentCode { get; set; }

        public byte CommandCode { get; set; }

        public V3ErrorCode ErrorCode { get; set; }

        public byte ParameterNumber { get; set; }

        public V3RequestX0FX81()
        {
        }

        public V3RequestX0FX81(DataMessage msg) : base(msg)
        {
            EquipmentCode = msg.Data[0];
            CommandCode = msg.Data[1];
            ErrorCode = (V3ErrorCode)msg.Data[2];
            ParameterNumber = msg.Data[3];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                EquipmentCode,
                CommandCode,
                (byte)ErrorCode,
                ParameterNumber
            };
        }
    }
}