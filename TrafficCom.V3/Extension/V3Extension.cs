using TrafficCom.V3.Connection;
using TrafficCom.V3.Request;

namespace TrafficCom.V3.Extension
{
    public interface IV3Callback
    {
        public Task Callback(V3Client sender, TcpConnection con, V3Request request);
    }

    public static class V3Extension
    {
        public static void AddTrafficComV3(this IServiceCollection collection, int port, int timeout)
        {
            collection.AddSingleton<V3Server, V3Server>(p => new V3Server(port, timeout));
            collection.AddHostedService(p => p.GetService<V3Server>());
        }

        public static void UseTrafficComV3<TCallback>(this IApplicationBuilder app) where TCallback : IV3Callback
        {
            var server = app.ApplicationServices.GetService<V3Server>();
            server.OnMessageReceived += async (V3Client sender, TcpConnection con, V3Request request) =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var cb = scope.ServiceProvider.GetService<TCallback>() as IV3Callback;
                    await cb.Callback(sender, con, request);
                }
            };
        }
    }
}