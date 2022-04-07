using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        public void test99()
        {
            // we can tell our substitute to return a value for a call:
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            Assert.That(calculator.Add(1, 2), Is.EqualTo(3));

            // We can check that our substitute received a call, and did not receive others
            calculator.Add(1, 2);
            calculator.Received().Add(1, 2);
            calculator.DidNotReceive().Add(5, 7);

            // We can also work with properties using the Returns syntax we use for methods, or just stick with plain old property setters (for read/write properties)
            calculator.Mode.Returns("DEC");
            Assert.That(calculator.Mode, Is.EqualTo("DEC"));

            calculator.Mode = "HEX";
            Assert.That(calculator.Mode, Is.EqualTo("HEX"));

            // NSubstitute supports argument matching for setting return values and asserting a call was received
            calculator.Add(10, -5);
            calculator.Received().Add(10, Arg.Any<int>());
            calculator.Received().Add(10, Arg.Is<int>(x => x < 0));

            // We can use argument matching as well as passing a function to Returns() to get some more behaviour out of our substitute (possibly too much, but that’s your call):
            calculator
                   .Add(Arg.Any<int>(), Arg.Any<int>())
                   .Returns(x => (int)x[0] + (int)x[1]);
            Assert.That(calculator.Add(5, 10), Is.EqualTo(15));

            // Returns() can also be called with multiple arguments to set up a sequence of return values.
            calculator.Mode.Returns("HEX", "DEC", "BIN");
            Assert.That(calculator.Mode, Is.EqualTo("HEX"));
            Assert.That(calculator.Mode, Is.EqualTo("DEC"));
            Assert.That(calculator.Mode, Is.EqualTo("BIN"));

            // Finally, we can raise events on our substitutes (unfortunately C# dramatically restricts the extent to which this syntax can be cleaned up):
            bool eventWasRaised = false;
            calculator.PoweringUp += (sender, args) => eventWasRaised = true;

            calculator.PoweringUp += Raise.Event();
            Assert.That(eventWasRaised);
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine("Size of int:{0}", sizeof(int));
            Console.WriteLine("Size of double:{0}", sizeof(double));

        }

        [Test]
        public void Test2()
        {
            // array 类型
            int[] arr = { 3, 2, 1 };

            // HashSet
            HashSet<int> hInt = new HashSet<int>();
            hInt.Add(10);
            hInt.Add(120);
            hInt.Add(50);
            Assert.AreEqual(true, hInt.TryGetValue(10, out int rs1));
            // Assert.AreEqual(true, hInt.TryGetValue(20,out int rs2));
            // Assert.AreEqual(true, hInt.TryGetValue(0,out int rs3));

            // HashTable
            Hashtable ht = new Hashtable();
            ht.Add(1, "s");
            ht.Add(2, "j");
            ht.Add(3, "z");
            // ht.Add(3, "u");
            ht.Remove(4);

            // 非排序 key/value 进行Hash分布 两个集合
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.EnsureCapacity(2);
            dic.Add(1, "s");
            dic.Add(7, "j");
            dic.Add(8, "z");
            dic.Add(3, "z");
            Assert.AreEqual(true, dic.TryGetValue(3, out string rs4));

            // bst 实现排序
            SortedList<int, string> sList = new SortedList<int, string>();
            sList.Add(1, "s");
            sList.Add(7, "j");
            sList.Add(8, "z");
            sList.Add(3, "z");

            // 使用两个List进行排序 浪费空间但是有很多方法
            SortedDictionary<int, string> sDic = new SortedDictionary<int, string>();
            sDic.Add(1, "s");
            sDic.Add(7, "j");
            sDic.Add(8, "z");
            sDic.Add(3, "z");

            // 双向链表
            LinkedList<int> lList = new LinkedList<int>();
            lList.AddAfter(lList.Find(3), new LinkedListNode<int>(9));

            // 单链表
            List<int> list = new List<int>();

            // 队列
            Queue q = new Queue();
            q.Enqueue("1");
            q.Dequeue();

            // 栈
            Stack stack = new Stack();
            stack.Push("1");
            stack.Pop();
            stack.Peek();

        }
    }
}