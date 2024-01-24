using System.Reflection;
using TrafficCom.V3.Connection;
using TrafficCom.V3.Messages;

namespace TrafficCom.V3.Request
{
    public class V3RequestFactory
    {
        private readonly Dictionary<KeyValuePair<byte, byte>, Type> _requestCache;

        public V3RequestFactory()
        {
            _requestCache = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsAssignableTo(typeof(V3Request)) && !x.IsAbstract && x != typeof(V3RequestUnknown))
                .Select(x => Activator.CreateInstance(x) as V3Request)
                .ToDictionary(x => KeyValuePair.Create(x.Cmd1, x.Cmd2), x => x.GetType());
        }

        public void VerifyCache()
        {
            foreach (var item in _requestCache)
            {
                CheckRequestName(item.Key, item.Value);
            }
        }

        public void CheckRequestName(KeyValuePair<byte, byte> key, Type type)
        {
            var obj = Activator.CreateInstance(type) as V3Request;
            if (type.Name != $"V3RequestX{key.Key:X2}X{key.Value:X2}")
                throw new V3Exception($"Request {type.Name} does not abide naming convention");
        }

        public V3Request Create(DataMessage msg)
        {
            var key = KeyValuePair.Create(msg.Cmd1, msg.Cmd2);
            if (_requestCache.ContainsKey(key))
            {
                return Activator.CreateInstance(_requestCache[key], msg) as V3Request;
            }
            else
            {
                return new V3RequestUnknown(msg);
            }
        }

        public void Set(KeyValuePair<byte, byte> key, Type type)
        {
            if (!type.IsAssignableTo(typeof(V3Request))) throw new V3Exception("Request type must be of type V3Request");
            CheckRequestName(key, type);
            _requestCache[key] = type;
        }
    }
}