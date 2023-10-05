using System.Collections.Generic;

namespace SortTest
{
    /// <summary>
    /// 232. 用栈实现队列
    /// </summary>
    public class MyQueue
    {
        // s1 辅助
        Stack<int> s1, s2;

        public MyQueue()
        {
            s1 = new Stack<int>();
            s2 = new Stack<int>();
        }

        /// <summary>
        /// O(1)
        /// </summary>
        /// <param name="x"></param>
        public void Push(int x)
        {
            s1.Push(x);
        }

        /// <summary>
        /// O(n)
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            Peek();
            return s2.Pop();
        }

        /// <summary>
        /// O(n)
        /// </summary>
        /// <returns></returns>
        public int Peek()
        {
            if (s2.Count == 0)
            {
                while (s1.Count != 0)
                {
                    s2.Push(s1.Pop());
                }
            }

            return s2.Peek();
        }

        public bool Empty()
        {
            return s1.Count == 0 && s2.Count == 0;
        }
    }
}
