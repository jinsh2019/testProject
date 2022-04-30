using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class day12
    {
        // 370 区间加法
        int[] getModifiedArray(int length, int[][] updates)
        {
            // nums 初始化全0
            int[] nums = new int[length];
            // 构造差分解法
            Difference df = new Difference(nums);

            foreach (var update in updates)// 遍历多维数组
            {
                int i = update[0];
                int j = update[1];
                int val = update[2];
                df.increment(i, j, val);
            }

            return df.result();
        }

        // 1109. 航班预订统计
        public int[] CorpFlightBookings(int[][] bookings, int n)
        {
            int[] nums = new int[n];

            Difference df = new Difference(nums);

            foreach (var booking in bookings)
            {
                int i = booking[0] - 1;
                int j = booking[1] - 1;
                int val = booking[2];
                df.increment(i, j, val);

            }
            return df.result();
        }

        // 1094. 拼车
        public bool CarPooling(int[][] trips, int capacity)
        {
            int[] nums = new int[1001];

            Difference df = new Difference(nums);

            foreach (var trip in trips)
            {
                int val = trip[0];
                //  第 trip[1] 站乘客上车
                int i = trip[1];
                // 第 trip[2] 站乘客已经下车
                // 即乘客在车上的区间是[trip[1],trip[2]-1]
                int j = trip[2] - 1;
                // 进行区间操作
                df.increment(i, j, val);
            }

            int[] res = df.result();

            //  客车乘客自始至终都不应该超载
            for (int i = 0; i < res.Length; i++)
            {
                if (capacity < res[i])
                {
                    return false;
                }
            }

            return true;
        }

        //  26. 删除有序数组中的重复项
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int slow = 0, fast = 0;
            while (fast < nums.Length)
            {
                if (nums[fast] != nums[slow])
                {
                    slow++;
                    // 维护 nums[0..slow] 无重复
                    nums[slow] = nums[fast];
                }
                fast++;
            }
            // 数组长度为索引 + 1 
            return slow + 1;
        }

        // 83. 删除排序链表中的重复元素
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null) return null;
            ListNode slow = head, fast = head;
            while (fast != null)
            {
                if (fast.val != slow.val)
                {
                    // nums[slow] = nums[fast]
                    slow.next = fast;
                    // slow++
                    slow = slow.next;
                }
                // fast++
                fast = fast.next;
            }

            slow.next = null;
            return head;
        }

        //27. 移除元素
        public int RemoveElement(int[] nums, int val)
        {
            int fast = 0, slow = 0;
            while (fast < nums.Length)
            {
                if (nums[fast] != val)
                {
                    nums[slow] = nums[fast];
                    slow++;
                }
                fast++;
            }
            return slow;
        }

        // 283. 移动零
        public void MoveZeroes(int[] nums)
        {
            int p = RemoveElement(nums, 0);
            for (; p < nums.Length; p++)
            {
                nums[p] = 0;
            }
        }

        //167. 两数之和 II - 输入有序数组
        public int[] TwoSum(int[] nums, int target)
        {
            // 一左一右两个指针相向而行
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                int sum = nums[left] + nums[right];
                if (sum == target)
                {
                    // 题目要求的索引是从1开始的
                    return new int[] { left + 1, right + 1 };
                }
                else if (sum < target)
                {
                    left++; // 让 sum 大一点
                }
                else if (sum > target)
                {
                    right--; // 让 sum 小一点
                }
            }

            return new int[] { -1, -1 };
        }

        // 344. 反转字符串
        public void ReverseString(char[] s)
        {
            // 一左一右两个指针相向而行
            int left = 0, right = s.Length - 1;
            while (left < right)
            {
                // 交换 s[left] 和 s[right]
                char temp = s[left];
                s[left] = s[right];
                s[right] = temp;
                left++;
                right--;
            }
        }

        // 5. 最长回文子串
        public String LongestPalindrome(String s)
        {
            string res = "";
            for (int i = 0; i < s.Length; i++)
            {
                string s1 = palindrome(s, i, i);
                string s2 = palindrome(s, i, i + 1);

                res = res.Length > s1.Length ? res : s1;
                res = res.Length > s2.Length ? res : s2;
            }

            return res;

        }

        // 在s中寻找以s[l]和s[r]为中心的最长回文串
        public string palindrome(string s, int l, int r)
        {
            // 防止索引越界
            while (l >= 0 && r < s.Length
                && s[l] == s[r])
            {
                // 向两边展开
                l--; r++;
            }
            // 返回以s[l] 和 s[r] 为中心的最长回文串
            return s.Substring(l + 1, r);
        }
    }
    // 差分数组工具类
    class Difference
    {
        private int[] diff;

        // 输入一个初始数组，区间操作将在这个数组上进行
        public Difference(int[] nums)
        {
            diff = new int[nums.Length];
            // 根据初始数组构造差分数组
            diff[0] = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                diff[i] = nums[i] - nums[i - 1];
            }
        }
        // 给闭区间[i,j] 增加 val（可以是负数）
        public void increment(int i, int j, int val)
        {
            diff[i] += val;
            if (j + 1 < diff.Length)
            {
                diff[j + 1] -= val;
            }
        }
        // 返回结果数组
        public int[] result()
        {
            int[] res = new int[diff.Length];
            // 根据差分数组构造结果数组
            res[0] = diff[0];
            for (int i = 1; i < diff.Length; i++)
            {
                res[i] = res[i - 1] + diff[i];
            }
            return res;
        }
    }

}
