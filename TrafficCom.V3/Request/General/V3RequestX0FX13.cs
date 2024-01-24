using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public enum V3CommandLevel
    {
        BWirelessBasic = 0,

        BBasic,

        ABBasicAdvance,

        ABOAll,
    }

    public class V3RequestX0FX13 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x13;

        public V3CommandLevel CommandLevel { get; set; }

        public V3RequestX0FX13()
        {
        }

        public V3RequestX0FX13(DataMessage msg) : base(msg)
        {
            CommandLevel = (V3CommandLevel)msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)CommandLevel
            };
        }
    }
}