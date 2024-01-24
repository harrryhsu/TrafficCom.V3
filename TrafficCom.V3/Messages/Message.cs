using TrafficCom.V3.Connection;

namespace TrafficCom.V3.Messages
{
    public enum MessageType
    {
        Data,

        Ack,

        Nak
    }

    public abstract class Message
    {
        private static readonly Random _rand = new();

        public static byte RandomSeq => (byte)_rand.NextInt64();

        public const byte DLE = 0xAA;

        public const byte STX = 0xBB;

        public const byte ETX = 0xCC;

        public const byte ACK = 0xDD;

        public const byte NAK = 0xEE;

        public abstract MessageType Type { get; }

        public byte Seq { get; set; } = RandomSeq;

        public ushort Addr { get; set; } = 0x1;

        public bool IsNak { get; protected set; } = false;

        public V3Client Client { get; set; }

        public static byte CRC(IEnumerable<byte> data)
        {
            return CRC(data, 0, data.Count());
        }

        public static byte CRC(IEnumerable<byte> data, int start, int end)
        {
            if (end > data.Count() || start > end || start < 0) throw new IndexOutOfRangeException("Invalid Range");

            byte ck = 0;
            for (var i = start; i < end; i++)
            {
                ck ^= data.ElementAt(i);
            }

            return ck;
        }

        public static bool VerifyCRC(IEnumerable<byte> data)
        {
            return VerifyCRC(data, 0, data.Count());
        }

        public static bool VerifyCRC(IEnumerable<byte> data, int start, int end)
        {
            return data.ElementAt(end - 1) == CRC(data, start, end - 1);
        }

        public abstract List<byte> Build();

        public override string ToString()
        {
            return string.Join(" ", Build().Select(x => x.ToString()));
        }
    }
}