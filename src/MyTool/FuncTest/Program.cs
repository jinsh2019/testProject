using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using static System.Console;

namespace FuncTest
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class NewStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region  事件、发布者、订阅者、链接程序
            //// 事件、发布者、订阅者、链接程序
            //var dealer = new CarDealer();
            //var a  = new Consumer("甲"); // 消费者
            //dealer.NewCarInfoEvent += a.NewCarIsHere; // 链接程序
            //dealer.NewCar("宝马");
            // 文档管理器
            //var docmgr = new DocManager<Word>();
            //docmgr.AddWord(new Word());
            #endregion

            #region 表达式树
            #region 0. 入门
            //Func<string> func = () => "委托";
            //func();

            //Expression<Func<string>> func_exp = () => "委托";

            ////Expression<Func<string, string>> func_exp1 = (x) => x + "委托";
            ////Expression<Func<int, int, int>> func_exp2 = (x, y) => (x + y) * x;
            //// IL Spy	Expression<Func<string>> func_exp = Expression.Lambda<Func<string>>(Expression.Constant("委托", typeof(string)), Array.Empty<ParameterExpression>());
            //Console.WriteLine(func_exp.Compile());
            //Console.WriteLine(func_exp.Compile().Invoke()); 
            #endregion

            #region 1. 简单
            //// 1. 
            //Expression<Func<int>> funcExp = () => 5;
            //ConstantExpression constExp = Expression.Constant(5, typeof(int));
            //Expression<Func<int>> funcExp2 = Expression.Lambda<Func<int>>(constExp, null);
            //var rs = funcExp2.Compile().Invoke();
            //Console.WriteLine(rs); 
            #endregion

            #region 2. 中等
            //// 2.
            //Expression<Func<int, int>> funcExp = (y) => y + 5;
            //ParameterExpression paramExp = Expression.Parameter(typeof(int), "y");
            //ConstantExpression constExp = Expression.Constant(5);
            //BinaryExpression binaryExp = Expression.Add(constExp, paramExp);
            //Expression<Func<int, int>> funcExp2 = Expression.Lambda<Func<int, int>>(binaryExp, new ParameterExpression[] { paramExp });
            //var rs = funcExp2.Compile().Invoke(5);
            //Console.WriteLine(rs); 
            #endregion

            #region 3. 偏难
            Expression<Func<int, int, int>> funcExp = (x, y) => 5 + y * y - x;
            #endregion

            #region 4. 应用 mapper
            var student = new Student() { Id = 1, Name = "张三", Age = 18 };
            NewStudent newStudent2 = ExpressionMapper<Student, NewStudent>.Mapper(student);
            #endregion

            #endregion


            int[] t1 = { 1, 2, 3, 4, 5, 4, 5 };
            GetFirstSameNums(t1);

            ReadLine();
        }

        // 找到第一个相同的数
        private static int GetFirstSameNums(int[] nums)
        {
            if (nums == null || nums.Length < 2)
                return -1;
           
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1])
                {
                    return i - 1;
                }
            }

            return -1;
        }
        /// <summary>
        /// 泛型，函数式编程
        /// </summary>
        public static void FunTest()
        {
            // IEnumerable<T>, ICollection<T>, IList<T>, ISet<T>, IDictionary<Tkey,TValue>, ILookup<TKey,TValue>,IComparer<T>

            // IEnumerable<T> 迭代器
            // ICollection<T> method of manipulate generic collections
            // IList<T>
            // ILookup<TKey,TValue> 一个键多个值
            // Queue<T> Enqueue + Dequeue -
            // Stack<T> push +  pop -
            // SortedList<TKey,TValue> 有序链表                    实现为基于数组的列表
            // 字典 Dictionary<Tkey,TValue>  
            // 有序字典 Dictionary<Tkey,TValue> 二叉搜索树为key排序 - 实现为字典
            // HashSet<T> 包含不重复元素的无需列表 SortedSet<T> 包含不重复元素的有序列表


            // 函数编程: 1.避免状态突变和2.将函数作为第一个类
            // 高阶函数: 将另一个函数作为参数，或者返回一个函数
            var list = new List<int>();
            // list.Where 将另一个函数作为参数 .
            // 纯函数:始终传递的相同参数返回相同的结果，不产生副作用

            // 语法
            // 1. 表达式体成员
            // 2. 扩展方法
            //  using static 声明
            // 本地函数
            // 元组
            //(string s, int i, int o) t = ("aaa", 1, 2);
            //WriteLine($"{ t.s},{t.i},{t.o}");
            // 模式匹配 is as 三种模式： const，type，var

            WriteLine("Hello World!");
        }
        private static int A()
        {
            int add(int x, int y) => x + y; // 本地函数
            //int add(int x, int y)
            //{
            //    return x + y;
            //}
            int result = add(37, 2);
            return result;
        }
    }

}
