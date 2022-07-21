namespace July.algorithms.day6
{
    internal class MinStack
    {
        public Stack<int> s1, s2;
        public MinStack()
        {
            s1 = new Stack<int>();
            s2 = new Stack<int>();
        }
        public void Push(int val)
        {
            s1.Push(val);
            if (s2.Count == 0 || s2.Peek() >= val)
            {
                s2.Push(val);
            }
        }

        public void Pop()
        {
            if (s1.Count != 0)
            {
                int top = s1.Pop();
                if (top == s2.Peek())
                {
                    s2.Pop();
                }
            }
        }

        public int Top()
        {
            if (s1.Count != 0)
                return s1.Peek();
            throw new Exception();
        }

        public int GetMin()
        {
            if (s2.Count != 0)
                return s2.Peek();
            throw new Exception();
        }
    }
}
