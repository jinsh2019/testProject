using System.Text;
using CDS;

namespace July.daily;

public class day23
{
    // 73,74,75,71,69,72,76,73
    public int[] DailyTemperatures(int[] nums)
    {
        int[] res = new int[nums.Length];
        Stack<int> s = new Stack<int>();
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            while (s.Count > 0 && nums[s.Peek()] <= nums[i])
                s.Pop();
            res[i] = s.Count == 0 ? 0 : (s.Peek() - i);  // 差返回的是距离;相同温度，返回为0
            s.Push(i); // 存储的是索引
        }
        return res;
    }

    public int FindUnsortedSubarray(int[] nums)
    {
        //初始化
        int len = nums.Length;
        int min = nums[len - 1], max = nums[0];
        int left = 0, right = -1;
        for (int i = 0; i < len; i++)
        {
            if (nums[i] < max)
                right = i;      // 寻找右边界 index
            else
                max = nums[i];  // 寻找右边界最大val

            if (nums[len - i - 1] > min)
                left = len - i - 1;      // 寻找左边界 index
            else
                min = nums[len - i - 1]; // 寻找左边界最大val
        }

        return right - left + 1;
    }

    public int SubarraySum(int[] nums, int k)
    {
        Dictionary<int, int> preSum = new Dictionary<int, int>();
        preSum.Add(0, 1);
        int sum = 0, res = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
            if (preSum.ContainsKey(sum - k))
                res += preSum[sum - k];

            if (preSum.ContainsKey(sum))
                preSum[sum] = preSum[sum] + 1;
            else
                preSum.Add(sum, 1);
        }
        return res;
    }

    int maxDiameter = 0; // 定义最大直径 maxDiameter
    public int DiameterOfBinaryTree(TreeNode root)
    {
        maxDepth(root);
        return maxDiameter;
    }
    // 问题分解的方式解决
    int maxDepth(TreeNode root)
    {
        if (root == null)
            return 0;

        int leftMax = maxDepth(root.left);
        int rightMax = maxDepth(root.right);

        // postOrder logic
        maxDiameter = Math.Max(maxDiameter, leftMax + rightMax);

        return 1 + Math.Max(leftMax, rightMax);
    }


    IList<IList<int>> res = new List<IList<int>>();
    private int level = 0;

    public IList<IList<int>> PathSum(TreeNode root, int targetSum)
    {
        traverse(root, targetSum, new LinkedList<int>());
        return res;
    }

    void traverse(TreeNode root, int sum, LinkedList<int> path)
    {
        if (root == null) return;

        path.AddLast(root.val); // track
        #region 记日志 track
        level++;
        StringBuilder tb = new StringBuilder();
        for (int i = 0; i < level; i++)
        {
            tb.Append("+");
        }
        Console.Write(tb.ToString());
        path.ToList().ForEach(x => Console.Write(x + ","));
        Console.WriteLine();
        #endregion
        // logic
        if (root.left == null && root.right == null && root.val == sum)
        {
            res.Add(new List<int>(path));
        }
        int target = sum - root.val;
        // logic end
        traverse(root.left, target, path);
        traverse(root.right, target, path);
        path.RemoveLast();  // back
        #region 记日志 back
        StringBuilder tb1 = new StringBuilder();
        for (int i = 0; i < level; i++)
        {
            tb1.Append("-");
        }
        Console.Write(tb1.ToString());
        path.ToList().ForEach(x => Console.Write(x + ","));
        Console.WriteLine();
        level--;
        #endregion

    }
}