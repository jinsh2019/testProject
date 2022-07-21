namespace July.algorithms.day4
{
    // 单调队列
    internal class MonotonicQueue
    {
        LinkedList<int> q = new LinkedList<int>();
        // 入栈
        public void Push(int n)
        {
            // 删除大于n的元素
            while (q.Count != 0 && q.Last() < n)
                q.RemoveLast();
            // 添加
            q.AddLast(n);
        }
        // 弹出n
        public void Pop(int n)
        {
            if (n == q.First())
                q.RemoveFirst();
        }

        // 获取最大元素
        public int Max()
        {
            return q.First();
        }
    }
}
