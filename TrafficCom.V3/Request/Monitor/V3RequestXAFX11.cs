using TrafficCom.V3.Helper;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestXAFX11 : V3Request
    {
        public override byte Cmd1 => 0xAF;

        public override byte Cmd2 => 0x11;

        public string Text { get; set; }

        public byte TextId { get; set; }

        public bool Show { get; set; }

        public V3RequestXAFX11()
        {
        }

        public V3RequestXAFX11(DataMessage msg) : base(msg)
        {
            TextId = msg.Data[0];
            Show = msg.Data[1] == 1;
            var textLength = msg.Data[2];
            Text = EncoderHelper.DecodeBig5(msg.Data.GetRange(3, msg.Data.Count - 3));
        }

        protected override List<byte> BuildData()
        {
            var textBytes = EncoderHelper.EncodeBig5(Text);
            var textLength = (byte)(textBytes.Length / 2);

            var data = new List<byte>
            {
                TextId,
                (byte)(Show ? 1:0),
                textLength,
            };

            data.AddRange(textBytes);
            return data;
        }
    }
}