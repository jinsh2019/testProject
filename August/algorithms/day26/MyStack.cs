namespace August.algorithms.day26
{
    internal class MyStack
    {
        private Queue<int> q;
        private int top_elem;

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

        public int Pop()
        {
            int size = q.Count;
            while (size > 2)
            {
                q.Enqueue(q.Dequeue());
                size--;
            }
            // 初始化倒数第二个元素作为top_elem
            top_elem = q.Peek();
            // 将倒数第二个压入队列
            q.Enqueue(q.Dequeue());
            // 返回最后一个元素
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
