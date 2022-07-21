using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.daily
{
    internal class day18
    {
        public int FindMinArrowShots(int[][] points)
        {
            if (points == null || points.Length == 0)
                return -1;
            int m = points.Length;
            Array.Sort(points, (a, b) => a[1].CompareTo(b[1]));
            int count = 1; // 不想交的数量
            int end = points[0][1];
            for (int i = 0; i < m; i++)
            {
                if (points[i][0] > end)
                {
                    count++;
                    end = points[i][1];
                }
            }
            return count;
        }

        public bool IncreasingTriplet(int[] nums)
        {
            if (nums == null || nums.Length < 3)
                return false;
            int first = nums[0], second = int.MaxValue;
            for (int i = 1; i < nums.Length; i++)
            {
                int num = nums[i];
                if (num > second)
                    return true;
                else if (num < first)
                    first = num;
                else if (num > first)
                    second = num;
            }
            return false;
        }


        public int LengthOfLIS(int[] nums)
        {
            // dp[i] 表示以 nums[i] 这个数结尾的最长递增子序列的长度
            int[] dp = new int[nums.Length];
            // base case: dp 数组全都初始化为 1
            Array.Fill(dp, 1);
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j]) // 以i结尾的最长递增子序列,当nums[i]> nums[j]时， 对dp数组进行更新
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }
                res = Math.Max(res, dp[i]);
            }
            return res;
        }

        public int SubarraySum(int[] nums, int k)
        {
            // 前缀和 -> 该前缀和（的值）出现的次数
            Dictionary<int, int> preSum = new Dictionary<int, int>();
            // 前缀和 0 出现 1 次
            preSum.Add(0, 1); // 用于sum-k = 0 时，即sum=k时，初始化前缀和为0时，出现一次

            int sum = 0;
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i]; // 计算前缀和
                                // 查找有多少个 sum[i] 等于 sum[j] - k
                if (preSum.ContainsKey(sum - k))
                    res += preSum[sum - k];

                if (preSum.ContainsKey(sum))
                    preSum[sum] = preSum[sum] + 1;
                else
                    preSum.Add(sum, 1);
            }
            return res;


        }

        public string AddStrings(string num1, string num2)
        {
            int i = num1.Length - 1, j = num2.Length - 1, add = 0;
            StringBuilder ans = new StringBuilder();
            while (i >= 0 || j >= 0 || add != 0)
            {
                int x = i >= 0 ? num1[i] - '0' : 0;
                int y = j >= 0 ? num2[j] - '0' : 0;
                int result = x + y + add;
                ans.Append(result % 10);
                add = result / 10;
                i--;
                j--;
            }
            char[] arr = ans.ToString().ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public int LongestPalindrome(string s)
        {
            if (s == null)
                return 0;

            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (char c in s)
            {
                if (map.ContainsKey(c))
                    map[c]++;
                else
                    map.Add(c, 1);
            }

            int cnt = 0;
            bool flag = false;
            foreach (int k in map.Keys)
            {
                int n = map[k] / 2; // n 对
                int m = map[k] % 2; // 余数
                if (m == 1 && !flag)
                {
                    cnt++;
                    flag = true;
                }
                if (n > 0)
                    cnt += n * 2;
            }
            return cnt;
        }

        public int LargestRectangleArea(int[] heights)
        {
            int n = heights.Length;
            int[] left = new int[n]; // 左边达到的最大距离
            int[] right = new int[n];// 右边达到的最大距离


            Stack<int> mono_stack = new Stack<int>(); // 存储索引，索引映射的值是单调递减
            for (int i = 0; i < n; i++)
            {
                while (mono_stack.Count != 0 && heights[mono_stack.Peek()] >= heights[i]) 
                {
                    mono_stack.Pop();
                }
                left[i] = (mono_stack.Count == 0 ? -1 : mono_stack.Peek());
                mono_stack.Push(i);
            }

            mono_stack.Clear();
            for (int i = n - 1; i >= 0; --i)
            {
                while (mono_stack.Count != 0 && heights[mono_stack.Peek()] >= heights[i])
                {
                    mono_stack.Pop();
                }
                right[i] = (mono_stack.Count == 0 ? n : mono_stack.Peek());
                mono_stack.Push(i);
            }

            int ans = 0;
            for (int i = 0; i < n; i++)
            {
                ans = Math.Max(ans, (right[i] - left[i] - 1) * heights[i]);
            }
            return ans;
        }

    }
}
