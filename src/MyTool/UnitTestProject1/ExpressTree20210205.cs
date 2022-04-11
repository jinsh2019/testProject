using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class ExpressTree20210205
    {
        [TestMethod]
        public void WarmMethod()
        {
            Func<string> func = () => "委托1";
            Console.WriteLine(func); // System.Func`1[System.String]
            Console.WriteLine(func()); // 委托1

            Expression<Func<string>> func_exp = () => "委托2";
            Console.WriteLine(func_exp); // () => "委托2"
            Console.WriteLine(func_exp.Compile()); // System.Func`1[System.String]
            Console.WriteLine(func_exp.Compile().Invoke()); // 委托2
        }
    }
}