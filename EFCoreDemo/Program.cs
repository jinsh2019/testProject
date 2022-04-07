using System;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EFCoreDbContext();
            context.Database.EnsureCreated();
            Console.WriteLine("Hello World!");
        }
    }
}
