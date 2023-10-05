namespace July.algorithms.day4
{
    // 单调队列
    internal class MonotonicQueue
    {
        LinkedList<int> linkedList = new LinkedList<int>();
        // 入栈
        public void Push(int n)
        {
            // 删除大于n的元素
            while (linkedList.Count != 0 && linkedList.Last != null && linkedList.Last.Value < n)
                linkedList.RemoveLast();
            // 添加
            linkedList.AddLast(n);
        }
        // 弹出n
        public void Pop(int n)
        {
            if (n == linkedList.First.Value)
                linkedList.RemoveFirst();
        }

        // 获取最大元素
        public int Max()
        {
            return linkedList.First.Value;
        }
    }
}
