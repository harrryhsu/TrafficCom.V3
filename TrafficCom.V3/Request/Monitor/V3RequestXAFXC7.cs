using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXC7 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xC7;

        public byte ErrorModuleNo { get; set; }

        public List<byte> DisplayModuleId { get; set; }

        public V3RequestXAFXC7()
        {
        }

        public V3RequestXAFXC7(DataMessage msg) : base(msg)
        {
            ErrorModuleNo = msg.Data[0];
            DisplayModuleId = msg.Data.GetRange(1, msg.Data.Count - 1);
        }

        protected override List<byte> BuildData()
        {
            var res = new List<byte>
            {
                ErrorModuleNo
            };
            res.AddRange(DisplayModuleId);
            return res;
        }
    }
}