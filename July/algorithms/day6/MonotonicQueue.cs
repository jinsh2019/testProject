namespace July.algorithms.day6
{
    internal class MonotonicQueue
    {
        LinkedList<int> linkedList = new LinkedList<int>();
        public void Push(int n)
        {
            while (linkedList.Count != 0 && linkedList.Last != null && linkedList.Last.Value < n)
                linkedList.RemoveLast();
            linkedList.AddLast(n);
        }
        public void Pop(int n)
        {
            if (n == linkedList.First())
                linkedList.RemoveFirst();
        }
        public int Max()
        {
            return linkedList.First.Value;
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
                    window.Pop(nums[i - k + 1]); // del the first in element
                }
            }

            int[] arr = new int[res.Count];
            for (int i = 0; i < res.Count; i++)
            {
                arr[i] = res[i];
            }
            return arr;
        }
    }
}
