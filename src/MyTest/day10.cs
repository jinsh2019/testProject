using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    // DP table
    internal class day10
    {
        // 1. 两数之和 改版
        // 返回的不是索引，是结果
        public int[] TwoSum(int[] nums, int target)
        {
            Array.Sort(nums);
            int lo = 0, hi = nums.Length - 1;
            while (lo < hi)
            {
                int sum = nums[lo] + nums[hi];
                if (sum < target)
                {
                    lo++;
                }
                else if (sum > target)
                {
                    hi--;
                }
                else if (sum == target)
                {
                    return new int[] { lo, hi };
                }
            }

            return new int[2];
        }

        public List<List<int>> TwoSumTarget(int[] nums, int target)
        {
            Array.Sort(nums);
            List<List<int>> res = new List<List<int>>();
            // 记录索引lo 和 hi 最初对应的值
            int lo = 0, hi = nums.Length - 1;
            while (lo < hi)
            {
                int sum = nums[lo] + nums[hi];
                // 
                int left = nums[lo], right = nums[hi];
                if (sum < target)
                    lo++;
                if (sum > target)
                    hi--;
                else if (sum == target)
                {
                    res.Add(new List<int>() { lo, hi });
                    // 跳过所有重复的元素
                    while (lo < hi && nums[lo] == left)
                        lo++;
                    while (lo < hi && nums[hi] == right)
                        hi--;
                }
            }
            return res;
        }

        public List<List<int>> TwoSumTargetUpGrade(int[] nums, int target)
        {
            // nums 数组必须有序
            Array.Sort(nums);
            int lo = 0, hi = nums.Length - 1;
            List<List<int>> res = new List<List<int>>();

            while (lo < hi)
            {
                int sum = nums[lo] + nums[hi];
                int left = nums[lo], right = nums[hi];
                if (sum < target)
                {
                    while (lo < hi && nums[lo] == left) lo++;

                }
                else if (sum > target)
                {
                    while (lo < hi && nums[hi] == right) hi--;

                }
                else if (sum == target)
                {
                    res.Add(new List<int>() { lo, hi });
                    while (lo < hi && nums[lo] == left)
                    {
                        lo++;
                    }
                    while (lo < hi && nums[hi] == right)
                    {
                        hi--;
                    }
                }
            }

            return res;
        }

        public List<List<int>> TwoSumTargetUpGrade(int[] nums, int start, int target)
        {
            // nums 数组必须有序
            Array.Sort(nums);
            int lo = start, hi = nums.Length - 1;
            List<List<int>> res = new List<List<int>>();

            while (lo < hi)
            {
                int sum = nums[lo] + nums[hi];
                int left = nums[lo], right = nums[hi];
                if (sum < target)
                {
                    while (lo < hi && nums[lo] == left) lo++;

                }
                else if (sum > target)
                {
                    while (lo < hi && nums[hi] == right) hi--;

                }
                else if (sum == target)
                {
                    res.Add(new List<int>() { lo, hi });
                    while (lo < hi && nums[lo] == left)
                    {
                        lo++;
                    }
                    while (lo < hi && nums[hi] == right)
                    {
                        hi--;
                    }
                }
            }

            return res;
        }

        public List<List<int>> ThreeSumTarget(int[] nums, int target)
        {
            Array.Sort(nums);
            int n = nums.Length;
            List<List<int>> res = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                List<List<int>> tuples = TwoSumTargetUpGrade(nums, i + 1, target - nums[i]);
                foreach (var tuple in tuples)
                {
                    tuple.Add(nums[i]);
                    res.Add(tuple);
                }
                while (i < n - 1 && nums[i] == nums[i + 1])
                {
                    i++;
                }
            }
            return res;
        }


        public List<List<int>> ThreeSumTarget(int[] nums, int start, int target)
        {
            Array.Sort(nums);
            int n = nums.Length;
            List<List<int>> res = new List<List<int>>();

            for (int i = start; i < n; i++)
            {
                List<List<int>> tuples = TwoSumTargetUpGrade(nums, i + 1, target - nums[i]);
                foreach (var tuple in tuples)
                {
                    tuple.Add(nums[i]);
                    res.Add(tuple);
                }
                while (i < n - 1 && nums[i] == nums[i + 1])
                {
                    i++;
                }
            }
            return res;
        }

        public List<List<int>> FourSumTarget(int[] nums, int target)
        {

            Array.Sort(nums);
            int n = nums.Length;
            List<List<int>> res = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                List<List<int>> tuples = ThreeSumTarget(nums, i + 1, target - nums[i]);
                foreach (var tuple in tuples)
                {
                    tuple.Add(nums[i]);
                    res.Add(tuple);
                }

                while (i < n - 1 && nums[i] == nums[i + 1])
                    i++;
            }
            return res;

        }
    }

}