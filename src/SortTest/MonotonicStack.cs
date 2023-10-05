using System;
using System.Collections.Generic;

namespace SortTest
{
    /// <summary>
    /// 496. 下一个更大元素 I
    /// 503. 下一个更大元素 II
    /// 739. 每日温度
    /// </summary>
    public class MonotonicStack
    {
        /// <summary>
        /// 单调栈模板
        /// [2,1,2,4,3]
        /// [4,2,4,-1,-1]
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int[] MonotonicStackTemplate(int[] nums)
        {
            Console.WriteLine();
            int n = nums.Length;

            int[] res = new int[n];
            Stack<int> s = new Stack<int>();
            // FILO先进最后一个
            for (int i = n - 1; i >= 0; i--)
            {
                while (s.Count > 0 && s.Peek() <= nums[i])
                {
                    s.Pop();
                }
                res[i] = s.Count == 0 ? -1 : s.Peek();
                s.Push(nums[i]);

                Console.WriteLine(string.Join(",", s));
            }

            return res;
        }
        /// <summary>
        /// 496. 下一个更大元素 I
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            // 预处理1 : 根据单调栈模板，返回下一个最大数的结果集
            int[] greater = MonotonicStackTemplate(nums2);
            // 预处理2:  使用map预处理O(1)
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums2.Length; i++)
            {
                if (!map.ContainsKey(nums2[i]))
                {
                    map.Add(nums2[i], greater[i]);
                }
            }
            // 处理返回结果
            int[] res = new int[nums1.Length];
            for (int i = 0; i < nums1.Length; i++)
            {
                res[i] = map[nums1[i]];
            }

            return res;
        }

        /// <summary>
        /// 503. 下一个更大元素 II
        /// 循环数组
        /// [1,2,1]: n
        /// [2,-1,2]: 输出
        /// [1,2,1，1，2，1]: 2*n
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int[] NextGreaterElements(int[] nums)
        {
            int n = nums.Length;

            int[] res = new int[n];
            Stack<int> s = new Stack<int>();
            // FILO先进最后一个
            for (int i = 2 * n - 1; i >= 0; i--)
            {
                while (s.Count > 0 && s.Peek() <= nums[i % n])
                {
                    s.Pop();
                }
                res[i % n] = s.Count == 0 ? -1 : s.Peek();
                s.Push(nums[i % n]);
            }

            return res;
        }
        /// <summary>
        /// 739. 每日温度
        /// LCR 038. 每日温度
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int[] DailyTemperatures(int[] nums)
        {
            int n = nums.Length;

            int[] res = new int[n];
            Stack<int> s = new Stack<int>();
            for (int i = n - 1; i >= 0; i--)
            {
                // 弹出小数
                while (s.Count > 0 && nums[s.Peek()] <= nums[i])
                    s.Pop();
                // 存距离
                res[i] = s.Count == 0 ? 0 : (s.Peek() - i);
                // 压入大数索引
                s.Push(i);
            }

            return res;
        }
    }
}
