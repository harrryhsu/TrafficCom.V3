using TrafficCom.V3.Connection;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX15 : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x15;

        public byte[] Password { get; set; } = new byte[6];

        public V3RequestX0FX15()
        {
        }

        public V3RequestX0FX15(DataMessage msg) : base(msg)
        {
            if (msg.Data.Count != 6) throw new V3Exception("Password must by 6 byte");
            Password = msg.Data.GetRange(0, 6).ToArray();
        }

        protected override List<byte> BuildData()
        {
            if (Password.Length != 6) throw new V3Exception("Password must by 6 byte");
            return Password.ToList();
        }
    }
}