using System;
using System.Collections.Generic;
using System.Text;

namespace CDS
{
    // 一个Queue实现栈操作
    public class MyStack
    {

        Queue<int> q = new Queue<int>();
        int top_elem = 0;
        public MyStack()
        {
        }

        public void Push(int x)
        {
            q.Enqueue(x);
            top_elem = x;
        }

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

    /**
     * Your MyStack object will be instantiated and called as such:
     * MyStack obj = new MyStack();
     * obj.Push(x);
     * int param_2 = obj.Pop();
     * int param_3 = obj.Top();
     * bool param_4 = obj.Empty();
     */
}
