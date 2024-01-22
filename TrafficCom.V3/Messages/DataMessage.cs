namespace TrafficCom.V3.Messages
{
    public class DataMessage : Message
    {
        public List<byte> Data { get; set; } = new();

        public byte Cmd1 { get; set; }

        public byte Cmd2 { get; set; }

        public override MessageType Type { get => MessageType.Data; }

        public DataMessage()
        {
        }

        public DataMessage(IEnumerable<byte> data)
        {
            var list = data.ToList();
            Seq = list[2];
            Addr = (ushort)((list[3] << 8) | list[4]);
            Cmd1 = list[7];
            Cmd2 = list[8];

            var len = (ushort)((list[5] << 8) | list[6]);
            Data = list.GetRange(9, len - 12);
        }

        public override List<byte> Build()
        {
            var len = 12 + Data.Count;
            var data = new List<byte>
            {
                DLE,
                STX,
                Seq,
                (byte)(Addr>>8),
                (byte)(Addr),
                (byte)(len>>8),
                (byte)(len),
                Cmd1,
                Cmd2
            };

            data.AddRange(Data);
            data.Add(DLE);
            data.Add(ETX);
            data.Add(CRC(data));

            return data;
        }
    }
}