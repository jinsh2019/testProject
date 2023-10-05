namespace July.algorithms.day12
{
    internal class MonotonicQueue
    {
        LinkedList<int> linkedList = new LinkedList<int>();
        // desc
        public void Push(int val)
        {
            while (linkedList.Count != 0 && linkedList.Last != null && linkedList.Last.Value < val)
                linkedList.RemoveLast();
            linkedList.AddLast(val);
        }

        public void Pop(int val)
        {
            if (val == linkedList.First.Value)
                linkedList.RemoveFirst();
        }

        public int Max()
        {
            return linkedList.First.Value;
        }

        public List<int> MaxSlidingWindow(int[] nums, int k)
        {
            MonotonicQueue window = new MonotonicQueue();
            List<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i < k - 1) // 留一个空位
                    window.Push(nums[i]);
                else
                {
                    window.Push(nums[i]); // 填满window 大小
                    res.Add(window.Max()); // 取出最大值
                    window.Pop(nums[i - k + 1]);// 弹出窗口最左边的树
                }
            }

            return res;
        }
    }
}
