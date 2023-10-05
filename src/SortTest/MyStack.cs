using System.Collections.Generic;

namespace SortTest
{
    /// <summary>
    /// 225. 用队列实现栈
    /// </summary>
    public class MyStack
    {
        private Queue<int> q;
        private int top_elem;

        public MyStack()
        {
            q = new Queue<int>();
            top_elem = 0;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        /// <param name="x"></param>
        public void Push(int x)
        {
            q.Enqueue(x);
            top_elem = x;
        }
        /// <summary>
        /// O(n)
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            int size = q.Count;
            while (size > 2)
            {
                q.Enqueue(q.Dequeue());
                size--;
            }

            top_elem = q.Peek();
            q.Enqueue(q.Dequeue());
            return q.Dequeue();
        }

        public int Top()
        {
            return top_elem;
        }

        public bool Empty()
        {
            return q.Count == 0;
        }
    }
}
