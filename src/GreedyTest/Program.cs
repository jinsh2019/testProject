using System;
using static System.Console;
namespace GreedyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int capacity = 10;
            int[] weights = { 4, 6, 3, 2, 5 };
            int[] values = { 9, 3, 1, 6, 5 };
            WriteLine("背包最大价值:" + Goods.getHighestValue(capacity, weights, values));

        }
    }
}
