using System;
using System.Threading;

namespace ConsoleHelper
{
    internal class Program
    {
        // SemaphoreSlim 对访问资源的线程数，进行限制
        static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(4);             // 1. 声明 SemaphoreSlim 变量     

        public static void AccessDatabase(string name, int seconds)
        {
            Console.WriteLine($"{name} waits to access a database.");
            _semaphoreSlim.Wait();                                              // 2. 阻止当前线程，直到它可以进入
            Console.WriteLine($"{name} was granted an access to a database.");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine($"{name} is completed.");
            _semaphoreSlim.Release();                                           // 3. 释放线程可进入线程个数
        }

        static void Main(string[] args)
        {
            // SemaphoreSlim
            for (int i = 0; i <= 6; i++)
            {
                string threadName = "Thread " + i;                              // 启动第 Thread+i 个线程
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
                t.Start();
            }

            Console.ReadLine();
        }
    }
}
