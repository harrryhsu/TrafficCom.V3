using System.Text;

namespace TrafficCom.V3.Helper
{
    public static class EncoderHelper
    {
        private static readonly Encoding Big5Encoder = CodePagesEncodingProvider.Instance.GetEncoding("big5");

        public static byte[] EncodeBig5(string str)
        {
            return Big5Encoder.GetBytes(str);
        }

        public static string DecodeBig5(IEnumerable<byte> data)
        {
            return Big5Encoder.GetString(data.ToArray());
        }

        public static int GetBig5EncodedLength(string str)
        {
            return EncodeBig5(str).Length;
        }
    }
}