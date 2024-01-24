using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX10 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x10;

        public byte DisplayTime { get; set; }

        public List<byte> TextIds { get; set; } = new();

        public V3RequestXAFX10()
        {
        }

        public V3RequestXAFX10(DataMessage msg) : base(msg)
        {
            DisplayTime = msg.Data[0];
            var count = msg.Data[1];
            TextIds = msg.Data.GetRange(2, count);
        }

        protected override List<byte> BuildData()
        {
            var data = new List<byte>
            {
                DisplayTime,
                (byte)TextIds.Count,
            };

            data.AddRange(TextIds);

            return data;
        }
    }
}