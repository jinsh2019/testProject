using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.@base
{
    internal class MyStack
    {
        Queue<int> q;
        int top_elem;
        public MyStack()
        {
            q = new Queue<int>();
            top_elem = 0;
        }

        public void Push(int x)
        {
            q.Enqueue(x);
            top_elem = x;
        }

        // 先进后出， FILO
        // pop the last elem
        public int Pop()
        {
            int size = q.Count;
            while (size > 2)
            {
                q.Enqueue(q.Dequeue());
                size--;
            }
            top_elem = q.Peek(); // set top_elem by using the last second value
            q.Enqueue(q.Dequeue()); // enQueue the last sencond value to stack top
            return q.Dequeue(); // pop the last dequeue
        }

        public int Top()
        {
            return top_elem;
        }

        public bool Empty()
        {
            return q.Count == 0 ? true : false;
        }
    }
}
