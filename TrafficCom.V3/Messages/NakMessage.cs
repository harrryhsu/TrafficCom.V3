namespace TrafficCom.V3.Messages
{
    public class NakMessage : Message
    {
        public override MessageType Type { get => MessageType.Nak; }

        public byte Error { get; set; }

        public NakMessage()
        {
            IsNak = true;
        }

        public NakMessage(IEnumerable<byte> data) : this()
        {
            var list = data.ToList();
            Seq = list[2];
            Addr = (ushort)((list[3] << 8) | list[4]);
        }

        public override List<byte> Build()
        {
            var data = new List<byte>
            {
                DLE,
                NAK,
                Seq,
                (byte)(Addr>>8),
                (byte)(Addr),
                0,
                9,
                Error,
            };

            data.Add(CRC(data));

            return data;
        }
    }
}