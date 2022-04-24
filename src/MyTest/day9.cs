using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class day9
    {
        // 704. 二分查找
        public int Search(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1; // 注意

            // 循环终止条件:
            // 1. left == right + 1;
            // 2. nums[mid] == target
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] == target)
                    return mid;
                else if (nums[mid] < target)
                    left = mid + 1; // 注意
                else if (nums[mid] > target)
                    right = mid - 1; // 注意
            }
            return -1;
        }

        int left_bound(int[] nums, int target)
        {
            if (nums.Length == 0)
                return -1;
            int left = 0;
            int right = nums.Length; // 注意
            // 循环终止条件:
            // left == right
            // [left, left)
            while (left < right) // 注意
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] == target)
                    right = mid;
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else if (nums[mid] > target)
                {
                    right = mid; // 注意
                }
            }
            return left;

            //// target 比所有数都大
            //if(left == nums.Length) return -1;
            //// 类似之前算法的处理方式
            //return nums[left] == target ? left : -1; // 左边界，不然找不到
        }
        // 求的是左边界
        int left_bound1(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else if (nums[mid] > target)
                {
                    right = mid - 1;
                }
                else if (nums[mid] == target)
                {
                    // 右边界收缩
                    right = mid - 1;
                }
            }
            // 这里改为检查 left 越界的情况
            if (left >= nums.Length || nums[left] != target)
                return -1;

            return left;
        }

        int right_bound(int[] nums, int target)
        {
            if (nums.Length == 0) return -1;
            int left = 0, right = nums.Length;
            // left == left+1
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] == target)
                {
                    left = mid + 1; // 注意
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else if (nums[mid] > target)
                {
                    right = mid;
                }
            }
            //return left - 1; // 注意
            if (left == 0) return -1;
            return nums[left - 1] == target ? (left - 1) : -1;
        }

        // 求的是右边界
        int right_bound1(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else if (nums[mid] > target)
                {
                    right = mid - 1;
                }
                else if (nums[mid] == target)
                {
                    // 左边界收缩
                    left = mid + 1;
                }
            }
            // 这里改为检查 right 越界的情况，
            if (right < 0 || nums[right] != target)
                return -1;
            return right;
        }

        //  34. 在排序数组中查找元素的第一个和最后一个位置
        public int[] SearchRange(int[] nums, int target)
        {
            int rs_left = left_bound1(nums, target);
            int rs_right = right_bound1(nums, target);

            return new int[] { rs_left, rs_right };

        }


        // 滑动窗口算法框架 
        // 76. 最小覆盖子串
        public string minWindow(string s, string t)
        {
            Dictionary<char, int> need = new Dictionary<char, int>();
            Dictionary<char, int> window = new Dictionary<char, int>();
            foreach (var c in t)
            {
                if (need.ContainsKey(c))
                    need[c]++;
                else
                    need.Add(c, 1);
            }

            int left = 0, right = 0, valid = 0;
            // 记录最小覆盖子串的起始索引及长度
            int start = 0, len = int.MaxValue;
            while (right < s.Length)
            {
                // c 是将移入窗口的字符
                char c = s[right];
                // 扩大窗口
                right++;
                // 进行窗口内数据的一系列更新
                if (need.ContainsKey(c))
                {
                    #region 更新窗口
                    if (window.ContainsKey(c))
                        window[c]++;
                    else
                        window.Add(c, 1);
                    #endregion

                    if (window[c] == need[c])
                        valid++;
                }
                // debug 输出的位置
                Console.WriteLine($"window:[{left},{right})");

                // 判断左侧窗口是否要收缩
                while (valid == need.Count)
                {
                    // 在这里更新最小覆盖子串
                    if (right - left < len)
                    {
                        start = left;
                        len = right - left;
                    }
                    // d 是将移出窗口的字符
                    char d = s[left];
                    // 所有窗口
                    left++;
                    // 进行窗口内数据的一系列更新
                    if (need.ContainsKey(d))
                    {
                        if (window[d] == need[d])
                            valid--;
                        window[d]--;
                    }
                }

            }
            return len == int.MaxValue ? "" : s.Substring(start, len);
        }
        // 567. 字符串的排列
        public bool CheckInclusion(string t, string s)
        {
            Dictionary<char, int> need = new Dictionary<char, int>();
            Dictionary<char, int> window = new Dictionary<char, int>();
            foreach (var c in t)
            {
                if (need.ContainsKey(c))
                {
                    need[c]++;
                }
                else
                {
                    need.Add(c, 1);
                }
            }
            int left = 0, right = 0;
            // valid 表示窗口中满足 need 条件的字符个数，
            // 如果 valid 和 need.size 的大小相同，
            // 则说明窗口已满足条件，已经完全覆盖了串 T。
            int valid = 0;
            // 迭代结束条件
            // right == s.count()
            while (right < s.Count())
            {
                char c = s[right];
                right++;
                // 进行窗口内数据的一系列更新
                if (need.ContainsKey(c))
                {
                    if (window.ContainsKey(c))
                        window[c]++;
                    else
                        window.Add(c, 1);
                    if (window[c] == need[c])
                        valid++;
                }

                // 判断左侧窗口是否要收缩
                while (right - left >= t.Count())
                {
                    // 在这里判断是否找到了合法的子串
                    if (valid == need.Count())
                        return true;

                    char d = s[left];
                    left++;
                    // 进行窗口内数据的一系列更新
                    if (need.ContainsKey(d))
                    {
                        if (window[d] == need[d])
                            valid--;
                        window[d]--;
                    }
                }
            }

            return false;
        }

        // 438. 找到字符串中所有字母异位词
        public IList<int> FindAnagrams(string s, string t)
        {
            Dictionary<char, int> need = new Dictionary<char, int>();
            Dictionary<char, int> window = new Dictionary<char, int>();
            foreach (var c in t)
            {
                if (need.ContainsKey(c))
                    need[c]++;
                else
                    need.Add(c, 1);
            }


            int left = 0, right = 0;
            int valid = 0;
            List<int> res = new List<int>(); // 记录结果

            while (right < s.Count())
            {
                char c = s[right];
                right++;
                // 进行窗口内数据的一系列更新
                if (need.ContainsKey(c))
                {
                    if (window.ContainsKey(c))
                        window[c]++;
                    else
                        window.Add(c, 1);
                    if (window[c] == need[c])
                        valid++;
                }
                // 判断是否要收缩
                while (right - left >= t.Count())
                {
                    // 符合提条件时，把起始索引加入res
                    if (valid == need.Count())
                        res.Add(left);
                    char d = s[left];
                    left++;
                    // 进行窗口内数据的一系列更新
                    if (need.ContainsKey(d))
                    {
                        if (window[d] == need[d])
                            valid--;
                        window[d]--;
                    }
                }
            }
            return res;
        }

        // 3. 无重复字符的最长子串
        public int LengthOfLongestSubstring(string s)
        {
            Dictionary<char, int> window = new Dictionary<char, int>();

            int left = 0, right = 0;
            int res = 0;
            // 结束条件 right == s.count()
            while (right < s.Count())
            {
                char c = s[right];
                right++;
                // 进行窗口内数据的一系列更新
                if (window.ContainsKey(c))
                    window[c]++;
                else
                    window.Add(c, 1);

                // 判断左侧窗口是否要收缩
                while (window[c] > 1)
                {
                    char d = s[left];
                    left++;
                    // 进行窗口内数据的一系列更新
                    window[d]--;
                }
                // 在这里更新答案
                res = Math.Max(res, right - left);
            }

            return res;
        }

        // 319. 灯泡开关
        public int BulbSwitch(int n)
        {
            return (int)Math.Sqrt(n);
        }
    }
}
