using System.Reflection;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public abstract class V3Request
    {
        public DataMessage DataMessage { get; protected set; }

        public abstract byte Cmd1 { get; }

        public abstract byte Cmd2 { get; }

        public byte Seq { get; set; } = Message.RandomSeq;

        public ushort Addr { get; set; } = 0x1;

        public V3Request()
        {
        }

        public V3Request(DataMessage msg)
        {
            DataMessage = msg;
            Seq = msg.Seq;
            Addr = msg.Addr;
        }

        protected virtual List<byte> BuildData()
        {
            return new List<byte>();
        }

        public virtual DataMessage Build()
        {
            return new DataMessage
            {
                Seq = Seq,
                Addr = Addr,
                Cmd1 = Cmd1,
                Cmd2 = Cmd2,
                Data = BuildData(),
            };
        }

        private static readonly Lazy<List<V3Request>> RequestCache = new(() =>
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsAssignableTo(typeof(V3Request)) && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as V3Request).ToList();
        });

        public static V3Request Create(DataMessage msg)
        {
            var cache = RequestCache.Value.FirstOrDefault(x => x.Cmd1 == msg.Cmd1 && x.Cmd2 == msg.Cmd2);

            if (cache == null) return new V3RequestUnknown(msg);
            return Activator.CreateInstance(cache.GetType(), msg) as V3Request;
        }
    }
}