using TrafficCom.V3.Helper;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFXC3 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0xC3;

        public string Text { get; set; }

        public byte TextId { get; set; }

        public V3RequestXAFXC3()
        {
        }

        public V3RequestXAFXC3(DataMessage msg) : base(msg)
        {
            TextId = msg.Data[0];
            var textLength = msg.Data[1];
            Text = EncoderHelper.DecodeBig5(msg.Data.GetRange(2, msg.Data.Count - 2));
        }

        protected override List<byte> BuildData()
        {
            var textBytes = EncoderHelper.EncodeBig5(Text);
            var textLength = (byte)(textBytes.Length / 2);

            var data = new List<byte>
            {
                TextId,
                textLength,
            };

            data.AddRange(textBytes);
            return data;
        }
    }
}