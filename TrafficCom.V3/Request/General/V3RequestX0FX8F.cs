using TrafficCom.V3.Connection;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestX0FX8F : V3Request
    {
        public override byte Cmd1 => 0x0F;

        public override byte Cmd2 => 0x8F;

        public ushort Address { get; set; }

        public V3Request Message { get; set; }

        public V3RequestX0FX8F()
        {
        }

        public V3RequestX0FX8F(DataMessage msg) : base(msg)
        {
            Address = (byte)(msg.Data[0] << 8 & msg.Data[1]);
            Message = msg.Client.RequestFactory.Create(new DataMessage
            {
                Addr = Address,
                Cmd1 = msg.Data[2],
                Cmd2 = msg.Data[3],
                Seq = msg.Seq,
                Data = msg.Data.GetRange(4, msg.Data.Count - 4)
            });
        }

        protected override List<byte> BuildData()
        {
            if (Message == null) throw new V3Exception("V3RequestX0FX8F proxy message cannot be null");

            var res = new List<byte>
            {
                (byte)(Address>>8),
                (byte)Address,
            };
            res.AddRange(Message.Build().Data);
            return res;
        }
    }
}