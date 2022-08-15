using july2.date;

namespace july2
{
    internal class Program
    {
        // 栈，托管堆、寄存器，高频堆(static)， 
        static void Main(string[] args)
        {
            var sum = Sum(10, 11, 12);
            Console.WriteLine(sum);
        }

        private static int Sum(int a, int b, int c)
        {
            return a + b + b;
        }
    }
}