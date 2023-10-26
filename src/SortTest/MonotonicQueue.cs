using System.Collections.Generic;

namespace SortTest
{
    /// <summary>
    /// 单调队列：存储大数
    /// </summary>
    public class MonotonicQueue
    {
        LinkedList<int> linkedList = new LinkedList<int>();
        /// <summary>
        /// 入列:移除小数
        /// </summary>
        /// <param name="n"></param>
        public void Enqueue(int n)
        {
            while (linkedList.Count >0 && linkedList.Last.Value < n)
            {
                linkedList.RemoveLast();
            }
            linkedList.AddLast(n);
        }
        /// <summary>
        ///  出列 n
        /// </summary>
        /// <param name="n"></param>
        public void Dequeue(int n)
        {
            if (n == linkedList.First.Value)
                linkedList.RemoveFirst();
        }
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <returns></returns>
        public int Max()
        {
            return linkedList.First.Value;
        }

        /// <summary>
        /// 239. 滑动窗口最大值
        /// 大小为k的滑动窗口内的最大值
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            MonotonicQueue window = new MonotonicQueue();
            List<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i < k - 1)
                    window.Enqueue(nums[i]);
                else
                {
                    window.Enqueue(nums[i]);
                    res.Add(window.Max());
                    window.Dequeue(nums[i - k + 1]);
                }
            }

            return res.ToArray();
        }
    }
}
