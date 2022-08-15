namespace July.algorithms.day22;

/// <summary>
/// 使用linkedList 实现单调递减队列
/// </summary>
public class MonotonicQueue
{
    LinkedList<int> q = new LinkedList<int>();

    public void Push(int n)
    {
        while (q.Count != 0 && q.Last() < n)
            q.RemoveLast();
        q.AddLast(n);
    }
    /// <summary>
    /// pop指定的Value
    /// </summary>
    /// <param name="n"></param>
    public void Pop(int n)
    {
        // 如果是第一个进入window的值,执行remove操作
        // 否则window的大小是足够的
        if (n == q.First()) 
            q.RemoveFirst();
    }

    public int Max() => q.First();

    // 滑动窗口最大值
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
                // 从前往后Pop出来一个，以便下次Push进去
                // i-k+1
            }
        }

        int[] arr = new int[res.Count];
        for (int i = 0; i < res.Count; i++)
            arr[i] = res[i];

        return arr;
    }
}