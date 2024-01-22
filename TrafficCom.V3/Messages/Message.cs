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

        public static byte CRC(IEnumerable<byte> data)
        {
            byte ck = 0;
            foreach (var d in data)
            {
                ck ^= d;
            }
            return ck;
        }

        public static bool VerifyCRC(List<byte> data)
        {
            return data.Last() == CRC(data.GetRange(0, data.Count - 1));
        }

        public abstract List<byte> Build();

        public override string ToString()
        {
            return string.Join(" ", Build().Select(x => x.ToString()));
        }
    }
}