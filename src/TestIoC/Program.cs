using System;
using Microsoft.Extensions.DependencyInjection;
using TestIoC.IService;

namespace TestIoC
{
    class Program
    {
        // IoC 服务注册 + 服务提供
        static void Main(string[] args)
        {
            Test2();
            Console.WriteLine("Hello World!");
        }

        static void Test2()
        {
            // 1.0  容器
            IServiceCollection  serviceCollection = new ServiceCollection();
            // 1 ** 注册
            serviceCollection.AddTransient(typeof(IServiceA), typeof(ObjectA));
            serviceCollection.AddTransient(typeof(IServiceB), typeof(ObjectB));
            // 3. **服务提供
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            // 3.1  获取
            var serviceB = serviceProvider.GetService<IServiceB>();
        }
    }
}
