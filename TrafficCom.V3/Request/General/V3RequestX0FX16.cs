using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public enum V3DbLock
    {
        Unlock = 0,

        Lock,

        PartialLock,
    }

    public class V3RequestX0FX16 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x16;

        public V3DbLock LockDb { get; set; }

        public V3RequestX0FX16()
        {
        }

        public V3RequestX0FX16(DataMessage msg) : base(msg)
        {
            LockDb = (V3DbLock)msg.Data[0];
        }

        protected override List<byte> BuildData()
        {
            return new List<byte>
            {
                (byte)LockDb
            };
        }
    }
}