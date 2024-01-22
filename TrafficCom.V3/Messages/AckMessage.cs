namespace TrafficCom.V3.Messages
{
    public class AckMessage : Message
    {
        public override MessageType Type { get => MessageType.Ack; }

        public AckMessage()
        {
        }

        public AckMessage(IEnumerable<byte> data)
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
                ACK,
                Seq,
                (byte)(Addr>>8),
                (byte)(Addr),
                0,
                8,
            };

            data.Add(CRC(data));

            return data;
        }
    }
}