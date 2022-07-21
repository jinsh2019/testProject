namespace July.algorithms.day5
{
    // 单调递增
    internal class MonotonicQueue
    {
        LinkedList<int> q = new LinkedList<int>();
        public void Push(int n)
        {
            while (q.Count != 0 && q.Last() < n)
            {
                q.RemoveLast();
            }
            q.AddLast(n);
        }

        public void Pop(int n)
        {
            if (n == q.First())
                q.RemoveFirst();
        }

        public int Max()
        {
            return q.First();
        }
    }
}
