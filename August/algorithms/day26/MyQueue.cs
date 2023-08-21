namespace August.algorithms.day26
{
    internal class MyQueue
    {
        // s1 是一个临时存储空间
        // s2 是一个队列FIFO
        Stack<int> s1, s2;

        public MyQueue()
        {
            s1 = new Stack<int>();
            s2 = new Stack<int>();
        }

        public void Push(int x)
        {
            s1.Push(x);
        }

        public int Pop()
        {
            Peek();
            // 先把S2 pop完之后，在将s1压进去
            return s2.Pop(); 
        }

        private int Peek()
        {
            // s2 不为空时，s2.peek出来的是FIFO
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
