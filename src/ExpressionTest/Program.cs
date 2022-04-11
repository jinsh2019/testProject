using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbset = new List<int>();
            var list = dbset.Where(x => x > 100);
            Func<int, bool> predicate = x => x > 100;
            Expression<Func<int, bool>> expressionTree1 = x => x > 100;
            Console.WriteLine("Hello World!");
        }
    }
}
