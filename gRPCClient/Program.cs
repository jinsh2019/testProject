using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcService1;

namespace gRPCClient
{
    class Program
    {
        static void Main(string[] args)
        {
            await TestHello();
            Console.WriteLine("Hello World!");
        }

        private static async Task TestHello()
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new Greeter.GreeterClient(channel);
                var reply =await client.SayHelloAsync(new HelloRequest() {Name = "Clay 老师"});
                Console.WriteLine("服务端说："+ reply.Message);
            }
        }
    }
}
