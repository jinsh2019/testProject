namespace July.algorithms.day5
{
    // 单调递增
    internal class MonotonicQueue
    {
        LinkedList<int> linkedList = new LinkedList<int>();
        public void Push(int n)
        {
            while (linkedList.Count != 0 && linkedList.Last != null && linkedList.Last.Value < n)
            {
                linkedList.RemoveLast();
            }
            linkedList.AddLast(n);
        }

        public void Pop(int n)
        {
            if (n == linkedList.First.Value)
                linkedList.RemoveFirst();
        }

        public int Max()
        {
            return linkedList.First.Value;
        }
    }
}
