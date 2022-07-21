namespace July.algorithms.day11
{
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
            {
                q.RemoveFirst();
            }
        }
        public int Max()
        {
            return q.First();
        }

        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            MonotonicQueue window = new MonotonicQueue();
            List<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i < k - 1)
                    window.Push(nums[i]);
                else
                {
                    window.Push(nums[i]);
                    res.Add(window.Max());
                    window.Pop(nums[i - k + 1]);
                }
            }

            int[] arr = new int[res.Count];
            for (int i = 0; i < res.Count; i++)
                arr[i] = res[i];
            return arr;
        }
    }
}
