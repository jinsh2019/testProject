using CDS;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace SortTest
{
    public static class Sep
    {
        // select the min then exchange the postion 
        public static void SelectionSort(int[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)         
                {
                    if (nums[j] < nums[min])
                    {
                        min = j;
                    }
                }
                int x = nums[i];
                nums[i] = nums[min];
                nums[min] = x;
            }
        }

        /// <summary>
        /// 396. 旋转函数
        /// F(0)=             0×nums[0]+1×nums[1]+…+(n-2)*nums[n-2]+(n−1)×nums[n−1]
        /// F(1)=0*nums[n-1]+ 1×nums[0]+2×nums[1]+…+(n-1)×nums[n−2]
        /// F(1)-F(0) = nums[0]+ nums[1]+nums[2]+...+nums[n-1]+n*nums[n-1] - (n-1+1)*nums[n-1]
        ///      F(1) = F(0)+ numSum−n×nums[n−1] 
        /// F(k)=F(k−1)+numSum−n×nums[n−k]
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxRotateFunction(int[] nums)
        {
            int f = 0, n = nums.Length, numSum = nums.Sum();
            for (int i = 0; i < n; i++)
                f += i * nums[i];
            int res = f;
            for (int i = n - 1; i > 0; i--)
            {
                f += numSum - n * nums[n - (n - i)];
                res = Math.Max(res, f);
            }
            return res;
        }
        // like a Bubble goes up, move bigger value go up
        public static void BubbleSort(int[] nums)
        {
            int n = nums.Length;
            for (int i = n - 1; i > 0; --i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int x = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = x;
                    }
                }
            }
        }

        // insert a value into sortedList on right position
        public static void InsertSort(int[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i < n; ++i)
            {
                int x = nums[i];
                int j = i - 1;                              // 第一次不走while循环，j不断靠近i
                while (j >= 0 && x < nums[j])
                {
                    nums[j + 1] = nums[j];                  // 逐一交换比x大的数字后，将x插入停止位置
                    j--;
                }
                nums[j + 1] = x;
            }
        }

        //public static int MaxProduct(int[] nums)
        //{

        //    int max = nums[0], submax = nums[1];
        //    if (max < submax)
        //    {
        //        int tmp = max;
        //        max = submax;
        //        submax = tmp;
        //    }
        //    for (int i = 2; i < nums.Length; i++)
        //    {
        //        if (nums[i] > max)
        //        {
        //            submax = max;
        //            max = nums[i];
        //        }
        //        else if (nums[i] > submax)
        //        {
        //            submax = nums[i];
        //        }
        //    }

        //    return (max - 1) * (submax - 1);
        //}

        public static int FindFinalValue(int[] nums, int original)
        {
            for (int i = 0; i < nums.Length;)
            {
                if (original == nums[i])
                {
                    original *= 2;
                    i = 0;
                    continue;
                }
                i++;
            }
            return original;
        }
        public static ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            ListNode newHead = ReverseList(head.next);
            // 靠栈来维持状态,不容易理解
            head.next.next = head;
            head.next = null;

            return newHead;
        }

        public static ListNode DoReverse(ListNode head, ref ListNode newhead)
        {
            if (head == null || head.next == null)
            {
                newhead = head;// 最后一次赋值
                return head;
            }

            ListNode tail = DoReverse(head.next, ref newhead);
            tail.next = head;
            head.next = null;

            return head;
        }

        public static IList<string> PrintVertically(string s)
        {
            List<Queue<char>> list = new List<Queue<char>>();
            string[] arr = s.Split(' ');
            int m = arr.Length;

            for (int i = 0; i < m; i++)
            {
                Queue<char> q = new Queue<char>();
                for (int j = 0; j < arr[i].Length; j++)
                {
                    q.Enqueue(arr[i][j]);
                }
                list.Add(q);
            }
            List<string> ans = new List<string>();
            List<Queue<char>> Zlist = new List<Queue<char>>();
            while (true)
            {
                if (list.Count() == 0)
                    break;
                if (Zlist.Count() == list.Count())
                    break;
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i].Count() != 0)
                    {
                        sb.Append(list[i].Dequeue());
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                    if (list[i].Count == 0 && !Zlist.Contains(list[i]))
                    {
                        Zlist.Add(list[i]);
                    }
                }

                ans.Add(sb.ToString().TrimEnd());
            }

            return ans.ToArray();
        }

        public static string Multiply(string num1, string num2)
        {
            int b = int.Parse(num2);
            int a = int.Parse(num1);
            int ans = 0;
            int w = 1;
            while (a > 0)
            {
                int tmp = (a % 10) * w;
                ans += tmp * b;
                a = a / 10;
                w *= 10;
            }

            return ans.ToString();
        }


        public static void GameOfLife(int[][] board)
        {
            int m = board.Length;
            int n = board[0].Length;
            int[][] matrix = new int[m][];

            for (int i = 0; i < m; i++)
            {
                matrix[i] = new int[n];
            }

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    matrix[i][j] = board[i][j];
                }
            }

            for (int i = 0; i < m; i++)
            {

                for (int j = 0; j < n; j++)
                {
                    int cnt = 0;
                    //board[i+1][j]
                    // 左上，上，右上 (i-1,j-1)，(i-1,j) ,(i-1,j+1)
                    // 左，右        (i,j-1) (i,j+1)
                    // 左下，下，右下  (i+1,j-1)，(i+1,j) ,(i+1,j+1)
                    if (i - 1 >= 0 && i - 1 < m && j - 1 >= 0 && j - 1 < n && board[i - 1][j - 1] == 1)
                    {
                        cnt++;
                    }

                    if (i - 1 >= 0 && i - 1 < m && j >= 0 && j < n && board[i - 1][j] == 1)
                    {
                        cnt++;
                    }

                    if (i - 1 >= 0 && i - 1 < m && j + 1 >= 0 && j + 1 < n && board[i - 1][j + 1] == 1)
                    {
                        cnt++;
                    }

                    if (i >= 0 && i < m && j - 1 >= 0 && j - 1 < n && board[i][j - 1] == 1)
                    {
                        cnt++;
                    }

                    if (i >= 0 && i < m && j + 1 >= 0 && j + 1 < n && board[i][j + 1] == 1)
                    {
                        cnt++;
                    }

                    if (i + 1 >= 0 && i + 1 < m && j - 1 >= 0 && j - 1 < n && board[i + 1][j - 1] == 1)
                    {
                        cnt++;
                    }

                    if (i + 1 >= 0 && i + 1 < m && j >= 0 && j < n && board[i + 1][j] == 1)
                    {
                        cnt++;
                    }

                    if (i + 1 >= 0 && i + 1 < m && j + 1 >= 0 && j + 1 < n && board[i + 1][j + 1] == 1)
                    {
                        cnt++;
                    }
                    //.. 

                    if (cnt < 2)
                    {
                        matrix[i][j] = 0;
                    }
                    if (cnt > 3)
                    {
                        matrix[i][j] = 0;
                    }
                    if (cnt == 3)
                    {
                        matrix[i][j] = 1;
                    }
                }
            }

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    board[i][j] = matrix[i][j];
                }
            }
        }


        public static int Massage(int[] nums)
        {
            /*
                分两种情况进行讨论
                1. 在i不接的情况下最长, i-1 不接 dp[i] = dp[i-1]
                2. 在i  接的情况下最长, i-1 不接 i-2 不知道接不接则： dp[i] = dp[i-2]+ nums[i]
                dp[i] = max(dp[i-1],dp[i-2]+nums[i])
            */
            int n = nums.Length;
            if (n == 0) return 0;
            int[] dp = new int[n];
            dp[0] = nums[0];
            for (int i = 1; i < n; ++i)
            {
                if (i == 1)
                {
                    dp[1] = Math.Max(nums[0], nums[1]);
                }
                else
                {
                    dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
                }
            }

            return dp[n - 1];
        }
        /*
         1. 将num各个位上的数存储在nums数组中
         2. 从高位向低位找，找到第一个为6的数，并更新为9
         3. 从高位开始，重新组装这个数；每次循环将数据都X10
         */
        public static int Maximum69Number(int num)
        {
            int[] nums = new int[5];
            int top = 0;
            while (num != 0)                     // 1
            {
                nums[top++] = num % 10;
                num /= 10;
            }

            for (int i = top - 1; i >= 0; --i)       // 2
            {
                if (nums[i] == 6)
                {
                    nums[i] = 9;
                    break;
                }
            }

            int ans = 0;                        // 3
            while (top-- != 0)
            {
                ans = ans * 10 + nums[top];
            }

            return ans;

        }
        /*
         1. 申请hash表，对每个字符进行计数
         2. 当已经记录过，清空hash表， ans++
         3. 一共分的段数
         */
        public static int PartitionString(string s)
        {
            int[] hash = new int[26];               // 1
            int ans = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (hash[s[i] - 'a'] == 1)          // 2
                {
                    hash = new int[26];
                    ans++;
                }
                hash[s[i] - 'a'] = 1;
            }
            return ans + 1;                        // 3
        }

        /*
         * 2078. 两栋颜色不同且距离最远的房子
           分析: n^2 遍历数组元素，找到距离最大距离
         1. max 为全局变量，用来记录最大值
         2. 当遇到不同的颜色时， 则判断一下最大最大距离时多少
         */
        public static int MaxDistance(int[] colors)
        {
            int max = 0;                                    // 1
            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = 1; j < colors.Length; j++)
                {
                    if (colors[i] != colors[j])             // 2
                    {
                        max = Math.Max(j - i, max);
                    }
                }
            }
            return max;

        }

        /*
         * 1903. 字符串中的最大奇数
         * 分析：从个位开始找，找到第一个为奇数的数字，然后进行切分
         * 
         * 知识库：
         * s[i] - '0' 转为数字类型
         * Substring(int startIndex, int length);截取的长度
         */
        public static string LargestOddNumber(string s)
        {
            int i;
            for (i = s.Length - 1; i >= 0; --i)
            {
                if (((s[i] - '0') & 1) == 1)
                {
                    return s.Substring(0, i + 1);
                }
            }
            return "";
        }

        /*
         * 807. 保持城市天际线
         * 
         * 考虑欠缺：算出了所有行和列的最大值，应该考虑的时每个节点所在行和列的最大值
         * 1. 定义行最大，列最大数组
         * 2. 对与每一个item，针对每行和每列的最大值中取最小
         * 
         * **/
        public static int MaxIncreaseKeepingSkyline(int[][] grid)
        {
            int n = grid.Length;
            int[] rowMax = new int[n];                                      // 1
            int[] colMax = new int[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    rowMax[i] = Math.Max(rowMax[i], grid[i][j]);
                    colMax[j] = Math.Max(colMax[j], grid[i][j]);
                }
            }
            int ans = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ans += Math.Min(rowMax[i], colMax[j]) - grid[i][j];     // 2
                }
            }
            return ans;
        }

        /*
         * 1221. 分割平衡字符串
         * 分析：
         * 声明两个计数器 cnt, p 对LR，进行统计，如果p归零， 则cnt++
         * 
         * 1. 遇到   'L'  p 自增1
         * 2. 遇到非 'L'  p 自减1
         * 3. p为0 则添加分割符
         * **/
        public static int BalancedStringSplit(string s)
        {
            int n = s.Length;
            int cnt = 0;
            int p = 0;
            for (int i = 0; i < n; i++)
            {
                if (s[i] == 'L')                    // 1
                {
                    p++;
                }
                else                                // 2
                {
                    p--;
                }
                if (p == 0)
                {                       // 3
                    cnt++;
                }
            }
            return cnt;
        }

        /*
         * 1827. 最少操作使数组递增
         * 分析：
         * 从低位向高位检查，遇到小于低位的进行(+1)操作
         * 1. 寻找最大数,并更新最大数的下标
         * 2. 如果坐标小于最大数，则
         *  2.1 添加增量操作
         *  2.2 更新i位置的值
         *  2.3 使用当前坐标更新最大值索引
         * **/
        public static int MinOperations(int[] nums)
        {
            int cnt = 0;
            int maxIndex = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[maxIndex] < nums[i])                       // 1 
                {
                    maxIndex = i;
                }
                else                                                // 2
                {
                    int increased = nums[maxIndex] - nums[i] + 1;
                    cnt += increased;
                    nums[i] += increased;
                    maxIndex = i;
                }
            }

            return cnt;
        }

        /*
         * 1974. 使用特殊打字机键入单词的最少时间
         * 
         * 知识点： 
         * 1. word[i] - 'a'; 相对于a的位置
         * 2.  到达cur的距离
         *      2.1 abs(cur - pre)    : 步数差
         *      2.1 26- abs(cur - pre): 26 - 步数差 = 反向走 类似于取补集
         * **/
        public static int MinTimeToType(string word)
        {
            int n = word.Length;
            int pre = 0;
            int cnt = 0;
            for (int i = 0; i < n; ++i)
            {
                int cur = word[i] - 'a';
                cnt += 1 + Math.Min(Math.Abs(cur - pre), 26 - Math.Abs(cur - pre));
                pre = cur;
            }

            return cnt;
        }

        /*
         * 969. 煎饼排序
         * 分析: leetcode 的举例有一些问题
         *  原始数据
            3241
            1. 选4的索引2
                4231
                1324
            2. 选3的索引1
                3124
                2134
            3. 选2的索引0
                1234
         * 选取最大的数字的索引，进行反转子数组，然后再反转到最后一个数字中
         * 1. 选择值最大的索引，
         * 2. 如果索引是最后一个，则忽略统计
         * 3. 反转子数组索引[0...maxIndex]
         * 4. 再次反转子数组索引[0...n-1]
         * 5. 添加   k值序列
         * 6. 再添加 k值序列
         * **/
        public static IList<int> PancakeSort(int[] nums)
        {
            IList<int> ret = new List<int>();
            for (int n = nums.Length; n > 1; --n)
            {
                int maxIndex = 0;
                for (int i = 1; i < n; i++)                 // 1
                {
                    if (nums[i] >= nums[maxIndex])
                    {
                        maxIndex = i;
                    }
                }
                if (maxIndex == n - 1)                      // 2
                {
                    continue;
                }

                Reverse(nums, maxIndex);                    // 2
                Reverse(nums, n - 1);
                ret.Add(maxIndex + 1);                      // 4
                ret.Add(n);                                 // 5
            }
            return ret;
        }

        private static void Reverse(int[] nums, int end)    // 6
        {
            for (int i = 0, j = end; i < j; i++, j--)
            {
                int tmp = nums[i];
                nums[i] = nums[j];
                nums[j] = tmp;
            }
        }
        /*
         * 561. 数组拆分
         * 分析
         * 22配对的最小数相加，得到最大的数
         * 将nums 进行排序，取奇数位置和
         * 
         * **/

        public static int ArrayPairSum(int[] nums)
        {
            int ans = 0;
            for (int i = 0; i < nums.Length; i += 2)
            {
                ans += nums[i];
            }

            return ans;
        }
        /**
         * 942. 增减字符串匹配
         * 分析
         * "IDID"
         * 前一个字符小于后一个字符时，I
         * 前一个字符大于后一个字符时，D
         * 只向后比较，则第一个最大的数可以设置为字符的长度，依次取l++,r++
         * 0,4,1,3,2
         * 1. 最大数一个数设置为length
         * 2. 第一个数设置为0
         * 3. 因为先i++，后判断<；因此还差一个数字
         * **/
        public static int[] DiStringMatch(string s)
        {
            int l = 0;
            int r = s.Length;                           // 1
            IList<int> list = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'I')                        // 2
                {
                    list.Add(l++);
                }
                else
                {
                    list.Add(r--);
                }
            }
            list.Add(l);                                // 3

            return list.ToArray();
        }

        /**
         * 2027. 转换字符串的最少操作次数
         * 分析：
         * 遇到‘X’的时且i> covered时
         * 1.cnt进行计数，
         * 2.coverted+2的操作，作为下一次的跳跃
         * 
         * **/
        public static int MinimumMoves(string s)
        {
            int covered = -1, cnt = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'X' && i > covered)
                {
                    cnt++;
                    covered = i + 2;
                }
            }

            return cnt;
        }
        /**
         * 605. 种花问题
         * 分析：
         * 遍历数组nums, 遇到第一个0开始计数，
         * 如果出现两个0时，则返回true
         * 如果出现1时，cnt重置为-1
         * 
         * 
         * 
         * **/
        public static bool CanPlaceFlowers(int[] nums, int n)
        {
            int cnt = -1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i == 0 && nums[i] == 0)
                {
                    cnt = 0;
                }
                if (nums[i] == 0)
                {
                    cnt++;
                    if (cnt == 2)
                    {
                        n--;
                        if (n == 0)
                            return true;
                        cnt = 0;
                    }
                    if (i == nums.Length - 1 && cnt == 1)
                        n--;
                    continue;
                }
                else
                {
                    cnt = -1;
                }
            }
            if (n <= 0) return true;
            return false;
        }
        // 1,0,0,0,1
        // 1,0,0,0,0,1


        public static bool CanThreePartsEqualSum(int[] nums)
        {
            int leave = nums.Sum() % 3;
            if (leave != 0) return false;
            int three = nums.Sum() / 3;
            int t1 = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                t1 += nums[i];
                if (t1 == three)
                {
                    t1 = 0;
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        t1 += nums[j];
                        if (t1 == three && j != nums.Length - 1)
                        {
                            t1 = 0;
                            for (int k = j + 1; k < nums.Length; k++)
                            {
                                t1 += nums[k];
                            }
                            if (t1 == three)
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        /**
         * 921. 使括号有效的最少添加
         * 分析：
         *  使用栈进行统计
         *  1. 如果时（，进栈
         *  2. 如果是）查看是否有对应的（，没有则进栈
         *  3. 统计进栈的)(的数量
         * **/
        public static int MinAddToMakeValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Push(s[i]);
                }
                else
                {
                    if (stack.Count != 0 && stack.Peek() == '(')
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(s[i]);
                    }
                }
            }

            return stack.Count;
        }

        public static int MinFlips(string target)
        {
            int cnt = 0;
            char[] s = new char[target.Length];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = '0';
            }
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] != s[i])
                {
                    for (int j = i; j < target.Length; j++)
                    {
                        s[j] = (s[j] == '0') ? '1' : '0';
                    }
                    cnt++;
                }
            }
            return cnt;
        }

        /// <summary>
        /// 1877. 数组中最大数对和的最小值
        /// 分析：
        /// 题目要求返回最小的最大对数和，
        /// 因此取【nums】中，最大值与最小值组合的值为最小的对，在这些对中找到最大的对
        /// 1. 排序
        /// 2. 组合对
        /// 
        /// 3,5,4,2,4,6
        /// 2,3,4,4,5,6
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinPairSum(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            int pairSum = int.MinValue;
            for (int i = 0, j = n - 1; i < j; ++i, --j)
            {
                if (pairSum < (nums[i] + nums[j]))
                {
                    pairSum = nums[i] + nums[j];
                }
            }

            return pairSum;
        }

        /// <summary>
        /// 976. 三角形的最大周长
        /// 分析：
        /// 先排序， 从大到小找，找到第一个a+b<![CDATA[<]]>c的数即可
        /// 1. 排序
        /// 2. a+b<![CDATA[<]]>c
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int LargestPerimeter(int[] nums)
        {
            Array.Sort(nums);                                   // 1
            int n = nums.Length;
            for (int i = n - 1; i >= 2; --i)
            {
                if (nums[i] < nums[i - 1] + nums[i - 2])        // 2
                {
                    return nums[i] + nums[i - 1] + nums[i - 2];
                }
            }
            return 0;

        }

        /// <summary>
        /// 455. 分发饼干
        /// 分析：
        /// 对孩子胃口，和饼干尺寸数组进行排序后，找到第一个满足孩子胃口i的饼干尺寸j，
        /// i+1后，找到下下一个匹配到的饼干尺寸
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int FindContentChildren(int[] g, int[] s)
        {
            Array.Sort(g);
            Array.Sort(s);

            int ans = 0;
            int j = 0;
            for (int i = 0; i < g.Length; i++)
            {
                for (; j < s.Length; j++)
                {
                    if (s[j] >= g[i])
                    {
                        ans++;
                        j++;
                        break;
                    }
                }
            }

            return ans;
        }

        /// <summary>
        /// 680. 验证回文串 II
        /// LCR 019. 验证回文串 II
        /// 给定一个非空字符串 s，请判断如果 最多 从字符串中删除一个字符能否得到一个回文字符串。
        /// 
        /// 分析
        /// 题目要求删除一个，
        /// 1. 将validate函数提取出来，判断删除i++ 或 j-- 只要其中一种情况为回文串，则返回true
        /// 2. 如果本身是回文串，则返回true
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ValidPalindrome(string s)
        {
            int n = s.Length;
            int low = 0, high = n - 1;
            while (low < high)
            {
                if (s[low] == s[high])
                {
                    low++;
                    high--;
                }
                else
                {
                    return validPalindrome(s, low, high - 1) || validPalindrome(s, low + 1, high); // 2
                }
            }
            return true;
        }

        private static bool validPalindrome(string s, int low, int high)                // 1
        {
            for (int i = low, j = high; i < j; ++i, --j)
            {
                if (s[i] != s[j])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 409. 最长回文串
        /// 给定一个包含大写字母和小写字母的字符串 s ，返回 通过这些字母构造成的 最长的回文串 。
        /// 在构造过程中，请注意 区分大小写 。比如 "Aa" 不能当做一个回文字符串。
        /// 
        /// 1. 使用cnt数组对字母进行统计
        /// 2. 如果cnt/2的个数*2作为ans的个数
        /// 3. 如果是奇数个且ans是偶数时，则加上一个中心位置
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LongestPalindrome(string s)
        {
            int[] cnt = new int[128];                   // 1
            int n = s.Length;
            for (int i = 0; i < n; ++i)
            {
                cnt[s[i]]++;
            }

            int ans = 0;
            foreach (int v in cnt)
            {
                ans += v / 2 * 2;                       // 2
                if (v % 2 == 1 && ans % 2 == 0)         // 3
                {
                    ans++;
                }
            }
            return ans;
        }


        /// <summary>
        /// 881. 救生艇
        /// people存储的每个人的重量， limit 有两个条件限制1.人数限制2人；2重量限制
        /// 1. 排序
        /// 2. 大+小 小于limit的值
        /// 3. 不满足2条件下，大重量小于limit时
        /// 4. i==j只剩下一个人时 
        ///     进行计数
        /// </summary>
        /// <param name="people"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static int NumRescueBoats(int[] people, int limit)
        {
            Array.Sort(people);                                     // 1
            int n = people.Length;
            int cnt = 0;
            int i, j;
            for (i = 0, j = n - 1; i < j;)
            {
                if (people[i] + people[j] <= limit)                 // 2
                {
                    cnt++;
                    i++;
                    j--;
                }
                else if (people[j] <= limit)                        // 3
                {
                    cnt++;
                    j--;
                }
                if (i == j) cnt++;                                  // 4
            }
            return cnt;
        }
        // 1,2,2,3 3

        /// <summary>
        /// 945. 使数组唯一的最小增量
        /// 排个序 
        /// 3,2,1,2,1,7
        /// 1. 声明cnt[] 记录每一个数字的个数
        /// 2. 遍历cnt[] 数组，如果>1, 向后查找计数为0的位置并记录一下距离
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinIncrementForUnique(int[] nums)
        {
            int[] cnt = new int[200100];
            int ans = 0;
            for (int i = 0; i < nums.Length; i++)           // 1
            {
                cnt[nums[i]]++;
            }

            for (int i = 0; i < 200100; ++i)
            {
                if (cnt[i] > 1)                             // 2   
                {
                    while (cnt[i] != 1)
                    {
                        for (int j = i; j < 200100; j++)    // 3
                        {
                            if (cnt[j] == 0)
                            {
                                ans += j - i;               // 4
                                cnt[j] = 1;
                                break;
                            }
                        }
                        cnt[i]--;
                    }
                }

            }

            return ans;
        }
        // 1,1,2,2,3,7

        /// <summary>
        /// 344. 反转字符串
        /// 1. 使用i=0, j = n-1分别指向数组的0，和n-1位置
        /// 2. 交换i,j指向的值
        /// </summary>
        /// <param name="s"></param>
        public static void ReverseString(char[] s)
        {
            int i = 0;
            int j = s.Length - 1;                   // 1
            while (i < j)
            {
                char x = s[i];                      // 2
                s[i] = s[j];
                s[j] = x;
                i++;
                j--;
            }
        }

        /// <summary>
        /// 392. 判断子序列
        /// 1. 使用i,j两个变量分别指向s和t
        /// 2. 当s[i] == t[j]时， i++
        /// 3. 持续j++
        /// 当s首先遍历结束则为子数组
        /// 
        /// ahbgdc
        /// abc
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsSubsequence(string s, string t)
        {
            int i = 0, j = 0;                       // 1
            while (i < s.Length && j < t.Length)
            {
                if (s[i] == t[j])
                    i++;
                j++;                                // 2
            }
            return i == s.Length;                   // 3
        }

        /// <summary>
        /// 1. 两数之和
        /// 因为要求出原来nums所在索引
        /// 1. 拷贝出一个arr
        /// 2. 先对nums进行排序
        /// 3. 声明i,j分别指向arr的头和尾，进行相加
        /// 4. 如果大于target时，j--, 如果小于target时，i++, 等于target 返回
        /// 5. 声明p,q，获取原数组所在位置
        /// 6.  p != k 不能取相同的索引[3,3]
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            int n = nums.Length;
            int[] arr = new int[n];
            for (int k = 0; k < n; k++)                     // 1   
            {
                arr[k] = nums[k];
            }
            Array.Sort(arr);                                // 2
            int i = 0, j = arr.Length - 1;                  // 3
            while (i < j)
            {
                if (arr[i] + arr[j] > target)               // 4
                {
                    j--;
                }
                else if (arr[i] + arr[j] < target)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }
            int p = 0, q = 0;                                // 5
            for (int k = 0; k < nums.Length; k++)
            {
                if (nums[k] == arr[i])
                {
                    p = k;
                }
            }
            for (int k = 0; k < nums.Length; k++)
            {
                if (nums[k] == arr[j] && p != k)            // 6
                {
                    q = k;
                }
            }
            return new int[2] { p, q };
        }

        /// <summary>
        /// 3. 无重复字符的最长子串
        /// 根据题目意思，需要统计字符的数量，
        ///
        /// 1. 定义一个cnt数组进行统计
        /// 2.i,j分别指向s和-1
        /// 3. 判断cnt>1，则j++,cnt[p]--,i++ cnt[w]--,
        ///     pwwkew  p进cnt，w进cnt，w再进cnt，
        /// 4. 对j所在的位置的数量进行裁剪，直到新增数值的数量=1为止
        /// 5. 计算长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            int ans = 0;
            int[] cnt = new int[256];                   // 1
            int i = 0, j = -1;                          // 2
            while (i < s.Length)
            {
                cnt[s[i]]++;
                while (cnt[s[i]] > 1 && j < i)          // 3
                {
                    ++j;
                    cnt[s[j]]--;                        // 4
                }

                ans = Math.Max(ans, i - j);            // 5
                i++;
            }
            return ans;
        }

        /// <summary>
        /// 2563. 统计公平数对的数目
        /// 1. nums[i]+ nums[j]<= upper 的数目a，以及nums[i]+nums[j]< lower的数目b
        /// 2. 声明l,r分别指向nums[]的头和尾
        /// 3. 统计小于cnt的数目， ++l 和越大，--r 和越小； 运行条件为 l<r
        /// 4. 返回ans
        /// 0,1,7,4,4,5
        /// 0,1,4,4,5,7
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static long CountFairPairs(int[] nums, int lower, int upper)
        {
            if (nums.Length < 2) return 0;
            Array.Sort(nums);
            long a = lessEqualThanCnt(nums, upper);                      // 1
            long b = lessEqualThanCnt(nums, lower - 1);

            return a - b;                                               // 4
        }
        private static long lessEqualThanCnt(int[] nums, int upper)
        {
            long ans = 0;
            int l = 0, r = nums.Length - 1;                             // 2
            while (l < r)
            {
                if (nums[r] + nums[l] <= upper)                        // 3 
                {
                    ans += r - l;
                    ++l;
                }
                else
                    --r;

            }
            return ans;
        }

        /// <summary>
        /// 977. 有序数组的平方
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortedSquares(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = (int)Math.Pow(nums[i], 2);
            }
            Array.Sort(nums);
            return nums;
        }

        /// <summary>
        /// 2108. 找出数组中的第一个回文字符串
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string FirstPalindrome(string[] words)
        {
            int n = words.Length;
            for (int i = 0; i < n; i++)
            {
                int len = words[i].Length;
                int k = len - 1;
                int j = 0;
                while (j < k)
                {
                    if (words[i][j] == words[i][k])
                    {
                        j++; k--;
                    }
                    else
                    {
                        break;
                    }
                }

                if (j + k == len - 1 || j == k)
                {
                    return words[i];
                }
            }
            return "";
        }

        /// <summary>
        /// 面试题 16.24. 数对和
        /// 思路：
        ///     排序nums+左右指针 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IList<IList<int>> PairSums(int[] nums, int target)
        {
            Array.Sort(nums);
            int l = 0, r = nums.Length - 1;
            List<IList<int>> list = new List<IList<int>>();
            while (l < r)
            {
                if (nums[l] + nums[r] > target)
                {
                    r--;
                }
                else if (nums[l] + nums[r] < target)
                {
                    l++;
                }
                else
                {
                    list.Add(new List<int>() { nums[l], nums[r] }); ;
                    r--;
                    l++;
                }
            }
            return list;
        }

        /// <summary>
        /// 2000. 反转单词前缀
        /// 思路：
        /// 左右指针
        /// </summary>
        /// <param name="word"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string ReversePrefix(string word, char ch)
        {
            int l = 0, r;
            for (r = 0; r < word.Length; r++)
            {
                if (word[r] == ch)
                {
                    break;
                }
            }
            char[] arr = word.ToArray();
            while (l < r && r < word.Length)
            {
                char tmp = arr[l];
                arr[l] = word[r];
                arr[r] = tmp;
                l++;
                r--;
            }

            return new string(arr);
        }
        /// <summary>
        /// 434. 字符串中的单词数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CountSegments(string s)
        {
            int cnt = 0;
            string[] arr = s.Split(' ');
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                if (arr[i].Length != 0) cnt++;
            }
            return cnt;
        }
        /// <summary>
        /// 283. 移动零
        /// 
        /// </summary>
        /// <param name="nums"></param>
        public static void MoveZeroes(int[] nums)
        {
            List<int> list = new List<int>();
            foreach (var v in nums)
            {
                if (v != 0)
                {
                    list.Add(v);
                }
            }
            list.Sort();
            int n = nums.Length - list.Count;
            list.AddRange(new int[3]);
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = list[i];
            }
        }
        /// <summary>
        /// 141. 环形链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool HasCycle(ListNode head)
        {
            if (head.next == null) return false;
            ListNode fast = head;
            ListNode slow = head;
            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
                if (fast == slow)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// LCR 022. 环形链表 II
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DetectCycle(ListNode head)
        {
            ListNode fast = head, slow = head;
            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
                if (fast == slow)
                    break;
            }
            if (fast == null && slow == null)
                return null;

            fast = head;
            while (fast != slow)
            {
                fast = fast.next;
                slow = slow.next;
            }

            return fast;
        }

        //public static ListNode RemoveNthFromEnd(ListNode head, int n)
        //{


        //}

        /// <summary>
        /// 1679. K 和数对的最大数目
        /// 排序，两数之和等于k的个数
        /// 
        /// 3,1,3,4,3
        /// 1,3,3,3,4
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int MaxOperations(int[] nums, int k)
        {
            int cnt = 0;
            Array.Sort(nums);
            int i = 0, j = nums.Length - 1;
            while (i < j)
            {
                if (nums[i] + nums[j] > k)
                {
                    j--;
                }
                else if (nums[i] + nums[j] < k)
                {
                    i++;
                }
                else
                {
                    i++;
                    j--;
                    cnt++;
                }
            }

            return cnt;
        }
        /// <summary>
        /// 11. 盛最多水的容器
        /// 1,8,6,2,5,4,8,3,7
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int MaxArea(int[] height)
        {
            int i = 0, j = height.Length - 1;
            int res = 0;
            while (i < j)
            {
                if (height[i] < height[j])
                {
                    res = Math.Max(res, Math.Min(height[i], height[j]) * (j - i));
                    i++;
                }
                else
                {
                    res = Math.Max(res, Math.Min(height[i], height[j]) * (j - i));
                    j--;
                }
            }
            return res;
        }

        /// <summary>
        /// 1963. 使字符串平衡的最小交换次数
        /// 
        /// 1. cnt进行统计
        /// 2. 当cnt等于0时, cnt++ 后面肯定有一个对应的']',同时ans++增加一个交换操作
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int MinSwaps(string s)
        {
            int n = s.Length;
            int cnt = 0, ans = 0;

            for (int i = 0; i < n; i++)
            {
                if (s[i] == '[')                    // 1
                {
                    cnt++;
                }
                else if (cnt > 0)
                {
                    cnt--;
                }
                else
                {                              // 2
                    cnt++;
                    ans++;
                }
            }

            return ans;
        }

        /// <summary>
        /// 524. 通过删除字母匹配到字典里最长单词
        /// "ale","apple","monkey","plea"
        /// abpcplea
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string FindLongestWord(string s, IList<string> dictionary)
        {
            string res = "";
            foreach (string t in dictionary)
            {
                int i = 0, j = 0;
                while (i < t.Length && j < s.Length)
                {
                    if (t[i] == s[j])
                    {
                        ++i;
                    }
                    ++j;
                }
                if (i == t.Length)                      // 删除一些元素后且匹配完成后，j是否结束不管
                {
                    if (t.Length > res.Length || (t.Length == res.Length && t.CompareTo(res) < 0))
                    {
                        res = t;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 167. 两数之和 II - 输入有序数组
        /// 本身有序，定义l,r指向数组头和尾
        /// (numbers[r]+ numbers[l]) > target
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum2(int[] numbers, int target)
        {
            int l = 0, r = numbers.Length - 1;
            while (l < r)
            {
                if (numbers[l] + numbers[r] > target)
                {
                    r--;
                }
                else if (numbers[l] + numbers[r] < target)
                {
                    l++;
                }
                else
                {
                    break;
                }
            }
            int[] ans = new int[2];
            ans[0] = l + 1;
            ans[1] = r + 1;

            return ans;
        }

        /// <summary>
        /// 1493. 删掉一个元素以后全为 1 的最长子数组
        /// 1. 左指针，右指针
        ///     1.1 j 小于len-1 因为进去就j++，len-2+1 = len-1 数组的最后一个数
        /// 2. 右滑动
        /// 3. 左滑动
        /// 4. 算出最大距离
        /// 5. 因为带上了一个0的长度，因此需要-1
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int LongestSubarray(int[] nums)
        {
            int i = 0, j = -1;                          // 1
            int zeroCnt = 0;
            int maxLen = 0;
            while (j < nums.Length - 1)                 // 1.1
            {
                j++;                                    // 2
                if (nums[j] == 0) zeroCnt++;

                while (zeroCnt > 1)
                {
                    if (nums[i] == 0) zeroCnt--;
                    ++i;                                // 3
                }
                if (j - i + 1 > maxLen)                 // 4
                {
                    maxLen = j - i + 1;
                }
            }
            return maxLen - 1;                          // 5
        }

        /// <summary>
        /// 1004. 最大连续1的个数 III
        /// 定长滑动窗口
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int LongestOnes(int[] nums, int k)
        {
            int i = 0, j = -1;
            int zeroCount = 0;
            int max = 0;
            while (j < nums.Length - 1)
            {
                j++;
                if (nums[j] == 0) zeroCount++;
                while (zeroCount > k)
                {
                    if (nums[i] == 0) zeroCount--;
                    i++;
                }

                if (max < j - i + 1)
                {
                    max = j - i + 1;
                }
            }

            return max;

        }

        /// <summary>
        /// 1456. 定长子串中元音的最大数目
        /// 请返回字符串 s 中长度为 k 的单个子字符串中可能包含的最大元音字母数
        /// 定长的滑动窗口
        /// abciiidef
        /// 3
        /// 1. 初始化元音字母 
        /// 2. i,j 座位窗口的两个指针
        /// 3. j 不能越界
        /// 4. j++ 扩大窗口的逻辑
        /// 5. i-- 缩小窗口的逻辑
        /// 6. 取最大值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int MaxVowels(string s, int k)
        {
            List<char> listv = new List<char>() { 'a', 'e', 'i', 'o', 'u' };    // 1
            int i = 0, j = -1;                                                  // 2
            int maxV = 0;
            int cnt = 0;
            while (j < s.Length - 1)                                            // 3
            {
                j++;
                if (listv.Contains(s[j]))                                       // 4
                    cnt++;
                if (j - i + 1 > k)                                              // 5
                {
                    if (listv.Contains(s[i])) cnt--;
                    i++;
                }
                if (maxV < cnt)                                                 // 6
                {
                    maxV = cnt;
                }
            }

            return maxV;
        }

        /// <summary>
        /// 209. 长度最小的子数组
        /// 滑动窗口
        /// i,j窗口的边界
        /// </summary>
        /// <param name="target"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinSubArrayLen(int target, int[] nums)
        {
            int i = 0, j = -1;
            int min = int.MaxValue;
            int sum = 0;
            while (j < nums.Length - 1)
            {
                j++;                            // 1
                sum += nums[j];
                if (sum < target) continue;

                while (sum >= target)
                {
                    if (min > j - i + 1) min = j - i + 1;
                    sum -= nums[i];
                    i++;
                }
            }
            return min == int.MaxValue ? 0 : min;
        }

        /// <summary>
        /// 567. 字符串的排列
        /// LCR 014. 字符串的排列
        /// 比滑动窗口多一步
        /// 需要计数
        /// [eidbaooo]
        ///    [ab]
        /// 1. 统计s2的数量
        ///     1.1 等于s1长度时，滑动窗口开始移动
        /// 2. 两数组相等则返回true 使用系统函数
        /// 3. 缩小左窗口，准备扩大右窗口
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool CheckInclusion(string s1, string s2)
        {
            int[] cnts1 = new int[26];
            int[] cnts2 = new int[26];
            for (int k = 0; k < s1.Length; k++)
            {
                cnts1[s1[k] - 'a']++;
            }

            int i = 0; int j = -1;
            while (j < s2.Length - 1)
            {
                j++;
                cnts2[s2[j] - 'a']++;               // 1
                if (j - i + 1 < s1.Length)          // 1.1 
                {
                    continue;
                }

                if (cnts1.SequenceEqual(cnts2))     // 2
                    return true;

                cnts2[s2[i] - 'a']--;               // 3
                i++;
            }

            return false;
        }

        /// <summary>
        /// LCR 015. 找到字符串中所有字母异位词
        /// 滑动窗口+计数
        /// 1. 统计p
        /// 2. 统计s
        /// 3. 缩窗口
        /// 4. 数组相等
        /// 5.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IList<int> FindAnagrams(string s, string p)
        {
            int[] cntS = new int[26];
            int[] cntP = new int[26];
            List<int> list = new List<int>();
            for (int k = 0; k < p.Length; k++)              // 1
            {
                cntP[p[k] - 'a']++;
            }
            int i = 0, j = -1;
            while (j < s.Length - 1)
            {
                j++;
                cntS[s[j] - 'a']++;                         // 2
                if (j - i + 1 < p.Length) continue;


                if (cntP.SequenceEqual(cntS))               // 4
                {
                    list.Add(i);
                }

                cntS[s[i] - 'a']--;                         // 3
                i++;
            }

            return list;
        }
        /// <summary>
        /// 713. 乘积小于 K 的子数组
        /// 滑动窗口
        /// 1,2,3,4,5: j-i+1
        /// i=0,j=2 [1],[1,2],[1,3]   以i开头，j结尾的子数组个数
        /// i=1,j=2 [2],[2,3]           
        /// i=2,j=2 [3]
        /// 
        /// 1. 左右指针作为窗口
        /// 2. 乘法逻辑                          扩大窗口
        /// 3. 除法逻辑,严格大于k时[i]能走到的位置   缩小窗口
        /// 4. 以i为头，j为尾的子数组个数           统计数据
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            int i = 0, j = -1;                              // 1
            int x = 1;
            int ans = 0;
            while (j < nums.Length - 1)
            {
                j++;
                x *= nums[j];                               // 2
                while (i <= j && x >= k)                    // 3
                {
                    x = x / nums[i];
                    i++;
                }
                ans += j - i + 1;                           // 4
            }

            return ans;
        }
        /// <summary>
        /// 1480. 一维数组的动态和
        /// 前缀和
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] RunningSum(int[] nums)
        {
            for (int i = 1; i < nums.Length; i++)
            {
                nums[i] += nums[i - 1];
            }
            return nums;
        }
        /// <summary>
        /// 1991. 找到数组的中间位置
        /// 724. 寻找数组的中心下标
        /// 前缀和 算法
        /// 找到i 使得sum(i-1) == sum(n-1) - sum(i)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindMiddleIndex(int[] nums)
        {
            for (int i = 1; i < nums.Length; i++)
            {
                nums[i] += nums[i - 1];
            }
            if (nums[0] == nums[nums.Length - 1])
                return 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i - 1] == nums[nums.Length - 1] - nums[i])
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 1310. 子数组异或查询
        /// 知识点:
        /// 1^0 = 1
        /// 1^0^1 = 0
        /// 1^0^1^0 = 1
        /// 两次异或等于还原数据
        /// 
        /// 1. 前缀异或
        /// 2. [l,r]
        /// 3. l==0 则 ans = arr[r]
        /// 4. l!=0 则 ans = arr[r]^arr[l-1] 求出[l,r]的异或值
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="queries"></param>
        /// <returns></returns>
        public static int[] XorQueries(int[] arr, int[][] queries)
        {
            for (int i = 1; i < arr.Length; i++)                    // 1
            {
                arr[i] ^= arr[i - 1];
            }

            IList<int> list = new List<int>();
            for (int i = 0; i < queries.Length; i++)
            {
                var l = queries[i][0];                              // 2
                var r = queries[i][1];
                int ans = 0;
                if (l == 0)
                {
                    ans = arr[r];
                }
                else
                {
                    ans = arr[r] ^ arr[l - 1];
                }
                list.Add(ans);
            }

            return list.ToArray();
        }
        /// <summary>
        /// 974. 和可被 K 整除的子数组
        /// 523. 连续的子数组和
        /// (sum[r] - sum[l])%k = 0
        ///  sum[r]%k ，sum[l]%k 只要同余则可以被k整除：
        ///  求前缀和的模相等的个数
        ///  1. 获取第一个前缀和的模（模为负数则+k，也视为模）
        ///  2. 获取后面前缀和的模  （模为负数则+k，也视为模）
        ///  3. 声明h数组，进行计数统计；初始化h[0]=1,即模为0时可以被看成除尽
        ///  4. 进行计数统计 ans+=h[sum[j]] 取子集数量
        ///  知识点：
        ///  4. 子集数量
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SubarraysDivByK(int[] nums, int k)
        {
            int[] sum = new int[nums.Length];                   // 1
            if ((nums[0] % k) < 0)
                sum[0] = (nums[0] % k) + k;
            else
                sum[0] = (nums[0] % k);

            for (int i = 1; i < nums.Length; i++)               // 2     
            {
                sum[i] = (sum[i - 1] + nums[i]) % k;
                if (sum[i] < 0) sum[i] += k;
            }

            int[] h = new int[30001];
            h[0] = 1;                                           // 3
            int ans = 0;
            for (int j = 0; j < sum.Length; j++)
            {
                ans += h[sum[j]];                               // 4
                h[sum[j]]++;
            }
            return ans;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="t"></param>
        public static void printCycleNTimes(int[] nums, int t)
        {
            int n = nums.Length;
            int k = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write(nums[i]);
                if ((i % n == n - 1) && k < t)
                {
                    i = -1;
                    k++;
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// 2134. 最少交换次数来组合所有的 1 II
        /// 滑动窗口+ 计数
        /// 
        ///  1, 1, 0, 0, 1 x 1, 1, 0, 0, 1
        ///              i      j   
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinSwaps(int[] nums)
        {
            int[] copy = new int[20010];
            int i, n = nums.Length, ans = 0, sum = 0;
            int windows = 0;
            for (i = 0; i < n; i++)
            {
                //统计1的个数
                if (nums[i] == 1) sum++;
            }
            for (i = 0; i < 2 * n; i++)
            {
                //数组扩容，%实现循环
                copy[i] = nums[i % n];
            }
            for (i = 0; i < sum; i++)
            {
                //初始化窗口
                windows += copy[i];
            }
            ans = windows;
            for (i = 0; i < n; i++)
            {
                windows -= copy[i];
                windows += copy[i + sum];
                if (windows > ans)
                {
                    ans = windows;
                }
            }

            return sum - ans;
        }

        /// <summary>
        /// 697. 数组的度
        /// 1. 初始化cnt数组
        /// 2. 统计最大度的个数
        /// 3. 收集度的值
        /// 4. 滑动窗口获取度的长度
        /// 5. 计算度的最小距离
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindShortestSubArray(int[] nums)
        {
            int[] cnt = new int[50001];
            for (int i = 0; i < nums.Length; i++) cnt[nums[i]]++;       // 1

            int maxCnt = 0;
            List<int> list = new List<int>();
            for (int j = 0; j < 50001; j++)                             // 2
            {
                if (maxCnt < cnt[j])
                {
                    maxCnt = cnt[j];
                }
            }
            for (int k = 0; k < 50001; k++)                             // 3
            {
                if (cnt[k] == maxCnt)
                {
                    list.Add(k);
                }
            }
            int minAns = int.MaxValue;
            foreach (var maxValue in list)
            {
                int p = 0, q = -1;
                int ans = 0;
                while (q < nums.Length - 1)                             // 4
                {
                    q++;
                    if (nums[q] == maxValue) maxCnt--;

                    if (maxCnt != 0) continue;

                    while (maxCnt == 0)
                    {
                        if (nums[p] != maxValue)
                            p++;
                        else
                            maxCnt++;
                    }
                    ans = q - p + 1;
                }
                minAns = Math.Min(minAns, ans);                       // 5
            }

            return minAns;
        }
        /// <summary>
        /// 238. 除自身以外数组的乘积
        /// 1. 除  0  外所有乘积suffix[1],   后缀积第 1 项
        /// 2. 除 n-1 外所有乘积prefix[n-2], 前缀积第n-2项
        /// 3. 除  i  外所有乘积
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] ConstructArr(int[] nums)
        {
            int n = nums.Length;
            if (n == 0)
            {
                return new int[0];
            }
            int[] L = new int[n];
            L[0] = nums[0];
            for (int i = 1; i < n; i++)
            {
                L[i] = L[i - 1] * nums[i];
            }

            int[] R = new int[n];
            R[n - 1] = nums[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                R[i] = R[i + 1] * nums[i];
            }

            int[] ans = new int[n];
            ans[0] = R[1];                                 // 1  
            ans[n - 1] = L[n - 2];                         // 2
            for (int i = 1; i < n - 1; i++)
            {
                ans[i] = L[i - 1] * R[i + 1];              // 3
            }

            return ans;
        }

        /// <summary>
        /// 1732. 找到最高海拔
        /// </summary>
        /// <param name="gain"></param>
        /// <returns></returns>
        public static int LargestAltitude(int[] gain)
        {
            int n = gain.Length;
            int[] nums = new int[n + 1];
            nums[0] = 0;
            int max = int.MinValue;
            for (int i = 1; i < nums.Length; i++)
            {
                nums[i] = nums[i - 1] + gain[i - 1];
                max = Math.Max(max, nums[i]);
            }

            return Math.Max(max, nums[0]);

        }

        /// <summary>
        /// 1413. 逐步求和得到正数的最小值
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinStartValue(int[] nums)
        {
            int[] sum = new int[nums.Length];
            sum[0] = nums[0];
            int min = Math.Min(sum[0], int.MaxValue);
            for (int i = 1; i < nums.Length; i++)
            {
                sum[i] = sum[i - 1] + nums[i];
                min = Math.Min(min, sum[i]);
            }

            return 1 - min <= 0 ? 1 : 1 - min;
        }

        /// <summary>
        /// 1588. 所有奇数长度子数组的和
        /// 
        /// 1. 确定开始索引
        /// 2. 设置步长: 1,3,5...
        /// 3. 确定边界
        ///     4. [start,end]：[0,0],[0,2],[0,4],... 求sum
        /// 4. 奇数个数组的前缀和 - 以start为起始位置的前缀和  
        ///     prefixSum[1] - prefixSum[0], prefixSum[3] - prefixSum[0] ...;
        ///     prefixSum[2] - prefixSum[1], prefixSum[4] - prefixSum[1] ...;
        ///     prefixSum[3] - prefixSum[2], prefixSum[5] - prefixSum[2] ...;
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int SumOddLengthSubarrays(int[] arr)
        {
            int sum = 0;
            int n = arr.Length;
            for (int start = 0; start < n; start++)                           // 1
            {
                for (int step = 1; start + step <= n; step += 2)              // 2
                {
                    int end = start + step - 1;                               // 3
                    for (int j = start; j <= end; j++)                        // 4 
                    {
                        sum += arr[j];
                    }
                }
            }

            return sum;
        }

        /// <summary>
        /// LCR 010. 和为 K 的子数组
        /// 560. 和为 K 的子数组
        /// 1. 通用做法，前缀和为0。map 存储的是前缀差
        /// 2. 找到对应的k值
        /// 3. 取子集
        /// 4. 新增或更新个数
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SubarraySum(int[] nums, int k)
        {
            int ans = 0, pre = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, 1);                                              // 1
            for (int i = 0; i < nums.Length; i++)
            {
                pre += nums[i];
                if (map.ContainsKey(pre - k))                           // 2
                    ans += map[pre - k];                                // 3

                if (map.ContainsKey(pre))                               // 4
                    map[pre]++;
                else
                    map.Add(pre, 1);
            }
            return ans;
        }

        /// <summary>
        /// 523. 连续的子数组和
        /// 974. 和可被 K 整除的子数组
        /// 给你一个整数数组 nums 和一个整数 k ，编写一个函数来判断该数组是否含有同时满足下述条件的连续子数组：
        /// 
        /// 子数组大小 至少为 2 ，且
        /// 子数组元素总和为 k 的倍数。
        /// 如果存在，返回 true ；否则，返回 false 。
        /// 
        /// 如果存在一个整数 n ，令整数 x 符合 x = n * k ，则称 x 是 k 的一个倍数。0 始终视为 k 的一个倍数。
        /// 
        /// 
        /// 分析：
        /// 前缀余数问题，与前缀和类似
        /// 同余问题，整除问题
        /// a%k = p...q
        /// b%k = r...q
        /// (a-b) 可以被k整除 
        /// 
        /// 1. 余数为0，索引-1
        /// 2. 计算[0, n-1]的余数 
        /// 3. 具有相同的余数 &&  i-preIndex >1
        /// 4.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool CheckSubarraySum(int[] nums, int k)
        {
            int n = nums.Length;
            if (n < 2) return false;

            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, -1);                                             //  1
            int remainder = 0;
            for (int i = 0; i < n; i++)
            {
                remainder = (remainder + nums[i]) % k;                  //  2            
                if (map.ContainsKey(remainder))                         //  3
                {
                    int prevIndex = map[remainder];
                    if (i - prevIndex > 1)
                        return true;
                }
                else
                {
                    map.Add(remainder, i);
                }
            }
            return false;
        }

        /// <summary>
        /// 1508. 子数组和排序后的区间和
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="n"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int RangeSum(int[] nums, int n, int left, int right)
        {
            List<int> list = new List<int>();
            for (int start = 0; start < n; start++)
            {
                int sum = 0;
                for (int end = start; end < n; end++)
                {
                    sum = (sum + nums[end]) % 1000000007;
                    list.Add(sum);
                }
            }
            list.Sort();
            int ans = 0;
            for (int i = left - 1; i < right; i++)
            {
                ans = ((ans + list[i]) % 1000000007);
            }
            return ans;
        }
        /// <summary>
        /// 1423. 可获得的最大点数
        /// 1,2,3,4,5,6,1{}1,2,3,4,5,6,1
        /// 1,1000,1{}1,1001,1
        /// 1,79,80,1,1,1,200,1{}1,79,80,1,1,1,200,1
        /// 分析： 取窗口大小等于n-k个数的最小值即为从左右两边取得最大值
        /// 1. 设置windowSize
        /// 2. 初始化SumW的值
        /// 3. 遍历数组， 取window和的最小值
        /// </summary>
        /// <param name="cardPoints"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int MaxScore(int[] cardPoints, int k)
        {
            int n = cardPoints.Length;
            int windowSize = n - k;                                     // 1
            int sumW = 0, min;

            for (int i = 0; i < windowSize; i++)
                sumW += cardPoints[i];
            min = sumW;                                                 // 2
            for (int i = windowSize; i < n; i++)                        // 3
            {
                sumW += cardPoints[i] - cardPoints[i - windowSize];
                min = Math.Min(sumW, min);
            }
            int ans = cardPoints.Sum() - min;
            return ans;

        }
        /// <summary>
        /// 1685. 有序数组中差绝对值之和
        /// 1. nums[i] -nums[0] + nums[i] -nums[1]+...+ num[i]-nums[i-1] = i*nums[i] - (nums[0]+nums[1]+...+nums[i-1])
        ///  = i*nums[i] - prefixSum[i-1]
        /// 2. nums[i+1]-nums[i]+nums[i+2]-nums[i]+...+nums[n]-nums[i] = (nums[i+1]+...+nums[n]) - (n-i-1)*nums[i]
        ///  = prefixSum[n-1] - prefixSum[i] - (n-i-1)*nums[i]
        ///  分析：因为非递减,[i]位置中，左边差的绝对值为1.；右边差的绝对值为2.
        ///  根据前缀和，对左右两边进行计算，求出数组返回值
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] GetSumAbsoluteDifferences(int[] nums)
        {
            int sum = 0;
            int[] prefixSum = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                prefixSum[i] = sum;
            }
            int[] output = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                int sumOfLeftDifferences = (i + 1) * nums[i] - prefixSum[i];
                int sumOfRightDifferences = prefixSum[nums.Length - 1] - prefixSum[i] - (nums.Length - 1 - i) * nums[i];
                output[i] = sumOfLeftDifferences + sumOfRightDifferences;
            }

            return output;
        }
        /// <summary>
        /// 2207. 字符串中最多数目的子字符串
        /// aabb
        /// ab
        /// 分析: 统计cur子序列的个数，以及作为a,b的cnt，大者作为新增子集数量： b的cnt大则在最左侧添加a
        /// 1. 当前子序列个数
        /// 2. 遇到front则统计curSum
        /// 3. 统计fcnt和bcnt
        /// 4. 大者作为新增子集数量
        /// 5. curSum + increasedNums 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static long MaximumSubsequenceCount(string text, string pattern)
        {
            char front = pattern[0], back = pattern[1];
            long curSum = 0;                                        // 1
            int fcnt = 0, bcnt = 0;
            for (int i = text.Length - 1; i >= 0; i--)
            {
                char c = text[i];
                if (c == front)
                {
                    curSum += bcnt;                                 // 2
                    fcnt++;                                         // 3
                }
                if (c == back)
                {
                    bcnt++;
                }
            }
            int increasedNums = Math.Max(fcnt, bcnt);               // 4                 
            return curSum + increasedNums;                          // 5

        }

        /// <summary>
        /// 930. 和相同的二元子数组
        /// 560. 和为 K 的子数组
        /// 1,0,1,0,1
        /// 2
        /// 1,1,2,2,3  Sum[0..i]
        /// dic[-1，-1，0，0，1]
        /// 分析: 使用前缀和+hash，如果存在与goal的差值相同的前缀和Sum[i], Sum[j] 则， Sum[i] - Sum[j] = 0
        ///
        /// 1. 查看是否存在
        /// 2. 存在加上子集
        /// 3. 未统计新增
        /// 4. 取子集
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public static int NumSubarraysWithSum(int[] nums, int goal)
        {

            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add(0, 1);
            int prefixSum = 0, ans = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                prefixSum = prefixSum + nums[i];
                if (dic.ContainsKey(prefixSum - goal))              // 1
                {
                    ans += dic[prefixSum - goal];                   // 2
                }

                if (dic.ContainsKey(prefixSum))                     // 3
                {
                    dic[prefixSum]++;
                }
                else
                {
                    dic.Add(prefixSum, 1);                           // 4
                }

            }

            return ans;
        }
        /// <summary>
        /// 2121. 相同元素的间隔之和
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static long[] GetDistances(int[] arr)
        {
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (dic.ContainsKey(arr[i]))
                {
                    dic[arr[i]].Add(i);
                }
                else
                {
                    dic.Add(arr[i], new List<int>() { i });
                }
            }
            long[] ret = new long[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                int sum = 0;
                var list = dic[arr[i]];
                foreach (var item in list)
                {
                    sum += Math.Abs(i - item);
                }
                ret[i] = sum;
            }

            return ret;
        }

        /// <summary>
        /// 560. 和为 K 的子数组
        /// prefixSum[i...j]
        /// 分析： dic 存的是prefixSum的值
        /// prefixSum[j] - prefixSum[i] = Sum[i+1...j]
        /// 1. 声明dic key:prefixSum[i] value:count
        ///     1.1 前缀和0，默认是1，因为prefix=k的情况下，本身就可以作为统计
        /// 2. 如果有前缀和等于prefixSum-k，添加子集进去
        /// 3. 更新前缀和
        /// 4. 添加前缀和
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SubarraySum1(int[] nums, int k)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();   // 1
            dic.Add(0, 1);                                           // 1.1
            int ans = 0, prefixSum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                prefixSum += nums[i];
                if (dic.ContainsKey(prefixSum - k))                  // 2
                    ans += dic[prefixSum - k];

                if (dic.ContainsKey(prefixSum))                     // 3
                    dic[prefixSum]++;
                else
                    dic.Add(prefixSum, 1);                          // 4
            }

            return ans;
        }
        /// <summary>
        /// 525. 连续数组
        /// 0,1,0,1,0,0,0,1,1,1
        ///         i         j
        ///   0看成-1； 1为1； 之和为0的最长距离
        ///   prefixSum[j] - prefixSum[i]
        ///  1. 声明dic key: 1,-1之和 value: index
        ///  2. -1位置记录的和记录为0
        ///  3. dic包含这个计数器，则使用i-preIndex 找到最大值
        ///  4. dic不包含这个计数器，则添加这个计数器
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int FindMaxLength(int[] nums)
        {
            int max = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();  // 1 
            dic.Add(0, -1);                                         // 2
            int counter = 0;
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                int num = nums[i];
                if (num == 1)
                    counter++;
                else
                    counter--;

                if (dic.ContainsKey(counter))                       // 3
                {
                    int prevIndex = dic[counter];
                    max = Math.Max(max, i - prevIndex);
                }
                else
                {
                    dic.Add(counter, i);
                }
            }
            return max;

        }


        /// <summary>
        /// 1248. 统计「优美子数组」
        /// 分析：根据题意即分析奇数的个数，将每一项&1，则变成统计1的个数
        /// prefixSum[j] - prefixSum[i] = k
        /// 前缀和+hash表
        /// 1. 定义字典表，key: 前缀和，value： 次数
        /// 2. 字典包含差值，取子集
        /// 3. 维护字典
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int NumberOfSubarrays(int[] nums, int k)
        {
            int[] arr = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
                arr[i] = nums[i] & 1;

            int prefixSum = 0, ans = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();          // 1
            dic.Add(0, 1);
            for (int i = 0; i < arr.Length; i++)
            {
                prefixSum += arr[i];
                if (dic.ContainsKey(prefixSum - k))                         // 2
                {
                    ans += dic[prefixSum - k];
                }

                if (dic.ContainsKey(prefixSum))                             // 3
                {
                    dic[prefixSum]++;
                }
                else
                {
                    dic.Add(prefixSum, 1);
                }
            }
            return ans;
        }

        public static int BinarySearch(int[] nums, int x)
        {
            int l = -1, r = nums.Length;
            int mid;
            while (l + 1 < r)
            {
                mid = l + (r - l) / 2;
                if (IsGreen(nums[mid], x))
                {
                    r = mid;
                }
                else
                {
                    l = mid; ;
                }
            }
            return r;
        }
        /// 红红红红红红绿绿绿绿绿绿绿绿绿绿
        private static bool IsGreen(int val, int x)
        {
            return val >= x;
        }
        /// <summary>
        /// 704. 二分查找
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Search(int[] nums, int target)
        {
            int r = BinarySearch(nums, target);
            if (r == nums.Length || nums[r] != target)
            {
                return -1;
            }

            return r;
        }
        /// <summary>
        /// LCR 072. x 的平方根
        /// 0...x 找到一个数val 使得 val*val > x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int MySqrt(int x)
        {
            return 0;
        }

        /// <summary>
        /// 322. 零钱兑换
        /// 贪心
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static int CoinChange(int[] coins, int amount)
        {
            Array.Sort(coins, (a, b) => b.CompareTo(a));
            int ans = int.MaxValue;
            dfs(coins, amount, 0, 0, ref ans);
            return ans == int.MaxValue ? -1 : ans;
        }

        private static void dfs(int[] coins, int amount, int i, int count, ref int ans)
        {
            if (amount == 0)
            {
                ans = Math.Min(ans, count);
                return;
            }

            if (i == coins.Length) return;
            for (int k = amount / coins[i]; k >= 0 && k + count < ans; k--)
            {
                dfs(coins, amount - k * coins[i], i + 1, count + k, ref ans);
            }
        }
        /// <summary>
        /// 275. H 指数 II
        /// </summary>
        /// <param name="citations"></param>
        /// <returns></returns>

        public static int HIndex(int[] citations)
        {
            int n = citations.Length;
            int l = -1, r = citations.Length;
            int mid;
            while (l + 1 < r)
            {
                mid = l + (r - l) / 2;
                if (IsGreen1(citations[mid], n - mid))
                {
                    r = mid;
                }
                else
                {
                    l = mid;
                }
            }
            return n - r;
        }
        // a. n-mid 论文数量
        // b. citations[i] 引用次数
        public static bool IsGreen1(int val, int x)
        {
            return val >= x;
        }
        /// <summary>
        /// 2643. 一最多的行
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>

        public static int[] RowAndMaximumOnes(int[][] mat)
        {
            int m = mat.Length;
            int n = mat[0].Length;
            int MaxCount = 0, i, j;
            int[] ans = new int[2];
            for (i = 0; i < m; i++)
            {
                int count = 0;
                for (j = 0; j < n; j++)
                {
                    if (mat[i][j] == 1)
                    {
                        count++;
                    }
                }
                if (count > MaxCount)
                {
                    ans[0] = i;
                    ans[1] = count;
                    MaxCount = count;
                }
            }
            return ans;
        }
        /// <summary>
        /// 832. 翻转图像
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static int[][] FlipAndInvertImage(int[][] image)
        {
            int m = image.Length;
            int n = image[0].Length;
            for (int i = 0; i < m; i++)
            {
                int p = 0, q = n - 1;
                while (p < q)
                {
                    int x = image[i][p];
                    image[i][p] = image[i][q];
                    image[i][q] = x;
                    p++;
                    q--;
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (image[i][j] == 1)
                    {
                        image[i][j] = 0;
                    }
                    else
                    {
                        image[i][j] = 1;
                    }
                }
            }

            return image;
        }

        /// <summary>
        /// 54. 螺旋矩阵
        /// 
        /// 1. 上部up   ， up++   超下break
        /// 2. 右部right， right--超左break
        /// 3. 下部 down,  down-- 超上break
        /// 4. 左部 left， left++ 超右 break
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static IList<int> SpiralOrder(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            int t = 0, b = m - 1;
            int l = 0, r = n - 1;
            IList<int> ans = new List<int>();

            while (true)
            {
                for (int i = l; i <= r; i++)
                    ans.Add(matrix[t][i]);
                t++;
                if (t > b) break;                   // 1

                for (int i = t; i <= b; i++)
                    ans.Add(matrix[i][r]);
                r--;
                if (r < l) break;                   // 2

                for (int i = r; i >= l; i--)
                    ans.Add(matrix[b][i]);
                b--;
                if (b < t) break;                   // 3

                for (int i = b; i >= t; i--)
                    ans.Add(matrix[i][l]);
                l++;
                if (l > r) break;                   // 4
            }
            return ans;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static IList<int> LuckyNumbers(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            int[] minRow = new int[m];
            int[] maxcol = new int[n];
            Array.Fill(minRow, int.MaxValue);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    minRow[i] = Math.Min(minRow[i], matrix[i][j]); // 填充一个数
                    maxcol[j] = Math.Max(maxcol[j], matrix[i][j]); // 填充一行数
                }
            }

            IList<int> ans = new List<int>();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == minRow[i] && matrix[i][j] == maxcol[j])
                        ans.Add(matrix[i][j]);

                }
            }

            return ans;
        }
        /// <summary>
        /// 766. 托普利茨矩阵
        ///  1,2,3,4
        ///  5,1,2,3
        ///  9,5,1,2
        /// 1. 第一次遍历 1,1;2,2;3,3
        /// 2. 第二次遍历 5,5;1,1,2,2
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool IsToeplitzMatrix(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (matrix[i][j] != matrix[i - 1][j - 1])
                    {
                        return false;
                    }
                }
            }
            return true;

        }

        /// <summary>
        /// 463. 岛屿的周长
        /// 分析
        /// 岛屿个数*4-Conn
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int IslandPerimeter(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            int cnt = 0, conn = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        cnt++;
                        if (i > 0 && grid[i - 1][j] == 1) conn++;
                        if (i < m - 1 && grid[i + 1][j] == 1) conn++;
                        if (j > 0 && grid[i][j - 1] == 1) conn++;
                        if (j < n - 1 && grid[i][j + 1] == 1) conn++;
                    }
                }
            }

            return 4 * cnt - conn;
        }
        /// <summary>
        /// 867. 转置矩阵
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int[][] Transpose(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            int[][] ans = new int[n][];
            for (int i = 0; i < n; i++)
            {
                ans[i] = new int[m];
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ans[j][i] = matrix[i][j];
                }
            }
            return ans;
        }

        /// <summary>
        /// 2022. 将一维数组转变成二维数组
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[][] Construct2DArray(int[] original, int m, int n)
        {
            int total = original.Length; ;
            if (total != m * n) return new int[0][];
            int[][] ans = new int[m][];
            for (int i = 0; i < n; i++)
            {
                ans[i] = new int[n];
            }
            for (int i = 0; i < original.Length; i += n)
            {
                Array.Copy(original, i, ans[i / n], 0, n);  // [0..n-1],[n...2n-1],[2n...3n-1]
            }

            return ans;
        }

        /// <summary>
        /// 2133. 检查是否每一行每一列都包含全部整数
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool CheckValid1(int[][] matrix)
        {
            int n = matrix.Length;
            for (int i = 0; i < n; i++)
            {
                Array.Sort(matrix[i]);
                //Console.WriteLine(string.Join(',', matrix[i]));
            }

            for (int i = 0; i < n; i++)
            {
                Array.Sort(matrix[i]);
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (j + 1 != matrix[i][j])
                    {
                        Console.WriteLine(i);
                        Console.WriteLine(j);
                        return false;
                    }

                }
            }
            return true;
        }


        /// <summary>
        /// 面试题 01.07. 旋转矩阵
        /// 48. 旋转图像
        /// 暴力解法:
        /// 时间复杂度o(n^2), 空间复杂度o(n^2)
        /// 1. matrix[i,j] 置换后的位置为： j行倒数第i列 即 matrix[j,n-1-i]
        /// 2. 还原matrix
        /// </summary>
        /// <param name="matrix"></param>
        public static void Rotate(int[][] matrix)
        {
            int n = matrix.Length;
            int[][] matrix_new = new int[n][];
            for (int i = 0; i < n; i++)
            {
                matrix_new[i] = new int[n];
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix_new[j][n - i - 1] = matrix[i][j];    // 1
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i][j] = matrix_new[i][j];            // 2
                }
            }
        }

        /// <summary>
        /// 面试题 01.07. 旋转矩阵
        /// 分析： 原地旋转
        /// 1. 水平翻转
        /// 2. 将[i,j] 与倒数第i个元素互换 [n-i-1, j]
        /// 3. 主对角线翻转
        /// 4. 沿主对角线互换元素 [i,j] [j,i]
        /// 知识点：
        /// 对角线翻转时， j需要小于i；不然会再次翻转回去
        /// </summary>
        /// <param name="matrix"></param>
        public static void Rotate1(int[][] matrix)
        {
            int n = matrix.Length;
            for (int i = 0; i < n / 2; i++)                     // 1
            {
                for (int j = 0; j < n; j++)
                {
                    int x = matrix[i][j];
                    matrix[i][j] = matrix[n - i - 1][j];        // 2
                    matrix[n - i - 1][j] = x;
                }
            }
            for (int i = 0; i < n; i++)                         // 3
            {
                for (int j = 0; j < i; j++)
                {
                    int x = matrix[i][j];
                    matrix[i][j] = matrix[j][i];                // 4
                    matrix[j][i] = x;
                }
            }
        }

        /// <summary>
        /// 59. 螺旋矩阵 II
        /// 1. 从左到右 fix row, move col
        ///     1.1 top下移
        /// 2. 从上到下 fix col, move row
        ///     2.1 right 左移
        /// 3. 从右到左 fix row, move col
        ///     3.1 buttom 上移
        /// 4. 从下到上 fix col, move row
        ///     4.1 left 右移
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[][] GenerateMatrix(int n)
        {
            int[][] matrix = new int[n][];
            for (int i = 0; i < n; i++)
                matrix[i] = new int[n];
            int l = 0, r = n - 1;
            int t = 0, b = n - 1;
            int num = 1, tar = n * n;
            while (num <= tar)
            {
                for (int i = l; i <= r; i++)    //1 
                {
                    matrix[t][i] = num;
                    num++;
                }
                t++;                            // 1.1
                for (int i = t; i <= b; i++)    //2 
                {
                    matrix[i][r] = num;
                    num++;
                }
                r--;                            // 2.1
                for (int i = r; i >= l; i--)    //3 
                {
                    matrix[b][i] = num;
                    num++;
                }
                b--;                            // 3.1
                for (int i = b; i >= t; i--)    // 4 
                {
                    matrix[i][l] = num;
                    num++;
                }
                l++;                            // 4.1
            }
            return matrix;
        }


        /// <summary>
        /// 191. 位1的个数
        /// 2 0b10
        /// 5 0b110
        /// 8 0b1000
        /// 
        /// 0b10 & 0b01= 0b00
        /// 0b110 & 0b101 = 0b100
        /// 0b100 & 0b011 = 0b000
        /// 通过不停的进行 n&(n-1) 消除1的来统计1的个数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int HammingWeight(uint n)
        {
            int cnt = 0;
            while (n != 0)
            {
                n &= (n - 1);
                cnt++;
            }

            return cnt;
        }
        /// <summary>
        /// 136. 只出现一次的数字
        ///  异或^
        ///  110 ^ 110 = 000
        ///  相同的数异或后等于0
        ///  110^0 = 110
        ///  一个数与0异或后不变
        ///  
        /// 根据题目仅有一个数出现了奇数次，使用异或运算可以把偶数次设置为0
        /// 然后算出ans
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber(int[] nums)
        {
            int x = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                x ^= nums[i];
            }

            return x;
        }
        /// <summary>
        /// 201. 数字范围按位与
        /// 分析： 按与运算，因为是连续的数字，低位的数字一定右奇偶
        /// 通过偶数是0，做与运算后为0
        /// 5, 7
        /// 101      10      1
        /// 110  =>  11  =>  1
        /// 111      11      1
        /// 一直做到高位相同的时候，然后再进行右移即可
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int RangeBitwiseAnd(int left, int right)
        {
            int offset = 0;
            while (left != right)
            {
                left >>= 1;
                right >>= 1;
                offset++;
            }
            left <<= offset;

            return left;
        }
        /// <summary>
        /// 137. 只出现一次的数字 II
        /// 出现三次，如果用hashmap来做是最简单的
        /// 1. hashmap 用来记录次数
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber2(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();      // 1 
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]]++;
                }
                else
                {
                    dic.Add(nums[i], 1);

                }
            }
            int ans = 0;
            foreach (var item in dic)
            {
                if (item.Value == 1)
                {
                    ans = item.Key;
                    break;
                }
            }

            return ans;
        }

        /// <summary>
        /// 461. 汉明距离
        /// 1. 异或运算后的n的二进制数，记录两数1，0的不同
        /// 2. 进行与运算，获取1的个数ans
        /// 111 & 110 = 110 1
        /// 110 & 101 = 100 2
        /// 100 & 011 = 000 3
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int HammingDistance(int x, int y)
        {
            int ans = 0;
            int n = x ^ y;                          // 1
            while (n != 0)
            {
                n &= (n - 1);                       // 2 
                ans++;
            }
            return ans;
        }

        /// <summary>
        /// 477. 汉明距离总和
        /// 1. 10^9 < 2^30 移动的位置
        /// 2. 求各个数字每一位的和
        /// 3. 总和=1的总数*0的总数
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int TotalHammingDistance(int[] nums)
        {
            int ans = 0, n = nums.Length;
            for (int i = 0; i < 30; ++i)                    // 1
            {
                int c = 0;
                for (int j = 0; j < n; ++j)
                {
                    c += (nums[i] >> i) & 1;                // 2
                }
                ans += c * (n - c);                         // 3
            }

            return ans;
        }

        /// <summary>
        /// 318. 最大单词长度乘积
        /// 1. 把各个字符移位或操作后，放入i位置
        /// 2. 各个字母都不相同
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>

        public static int MaxProduct(string[] words)
        {
            int length = words.Length;
            int[] masks = new int[length];
            for (int i = 0; i < length; ++i)
            {
                string word = words[i];
                for (int j = 0; j < word.Length; ++j)
                {
                    masks[i] |= 1 << (word[j] - 'a');                                       // 1
                }
            }
            int maxProd = 0;
            for (int i = 0; i < length; ++i)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if ((masks[i] & masks[j]) == 0)                                         // 2
                    {
                        maxProd = Math.Max(maxProd, words[i].Length * words[j].Length);
                    }
                }
            }

            return maxProd;
        }

        /// <summary>
        /// 746. 使用最小花费爬楼梯
        /// 1. f[i] 从第i个台阶爬的最小花费 cost[n]是从第n个台阶爬的花费数：到达顶部
        /// 2. 可以从第0或1个台阶开始爬
        /// 3. 状态转移方程
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static int MinCostClimbingStairs(int[] cost)
        {
            int n = cost.Length;
            int[] f = new int[n + 1];                                               // 1
            f[0] = 0;                                                               // 2                         
            f[1] = 0;
            for (int i = 2; i <= n; i++)
            {
                f[i] = Math.Min(f[i - 1] + cost[i - 1], f[i - 2] + cost[i - 2]);    // 3
            }

            return f[n];
        }
        /// <summary>
        /// 198. 打家劫舍
        /// 1. 设计状态
        /// 2. 写出状态转移方程
        /// 3. 设定初始状态
        /// 4. 执行状态转移
        /// 5. 返回最终的解
        /// 
        /// 1. 设计 dp[i] 时获得的金额最大
        /// 2. 状态转移方程 dp[i] = max(dp[i-1],dp[i-2]+nums[i])
        /// 3. 设定初始状态 dp[0] = nums[0],dp[1] = max(nums[0],nums[1])
        /// 4. for语句，执行
        /// 5. return 语句
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Rob(int[] nums)
        {
            if (nums.Length == 1) return nums[0];
            int[] dp = new int[nums.Length];                        // 1
            dp[0] = nums[0];                                        // 3
            dp[1] = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < nums.Length; i++)
            {
                dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);   // 2,4
            }

            return dp[nums.Length - 1];                             // 5
        }

        /// <summary>
        /// 213. 打家劫舍 II
        /// 选0，则不选n-1
        /// 选n-1 不选0
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Rob2(int[] nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Max(nums[0], nums[1]);
            int x1 = robRange(nums, 0, nums.Length - 2);
            int x2 = robRange(nums, 1, nums.Length - 1);

            return Math.Max(x1, x2);
        }

        private static int robRange(int[] nums, int start, int end)
        {
            int first = nums[start];
            int second = Math.Max(nums[start], nums[start + 1]);
            for (int i = start + 2; i <= end; i++)
            {
                int temp = second;
                second = Math.Max(first + nums[i], second);
                first = temp;
            }

            return second;
        }

        /// <summary>
        /// 740. 删除并获得点数
        /// 1. 设计状态
        /// 2. 写出状态转移方程
        /// 3. 设定初始状态
        /// 4. 执行状态转移
        /// 5. 返回最终的解
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int DeleteAndEarn(int[] nums)
        {
            if (nums.Length == 1) return nums[0];

            int[] hash = new int[10001];
            for (int i = 0; i < nums.Length; i++)
            {
                hash[nums[i]] += nums[i];
            }

            int[] dp = new int[hash.Length];                        // 1
            dp[0] = hash[0];                                        // 3
            dp[1] = Math.Max(hash[0], hash[1]);
            for (int i = 2; i < hash.Length; i++)
            {
                dp[i] = Math.Max(dp[i - 1], dp[i - 2] + hash[i]);   // 2,4
            }

            return dp[hash.Length - 1];                             // 5

        }

        /// <summary>
        /// 717. 1 比特与 2 比特字符
        /// 10,11 +2
        /// 0     +1
        /// i == n-1 为第一个比特符
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static bool IsOneBitCharacter(int[] bits)
        {
            int n = bits.Length, i = 0;
            while (i < n - 1)
            {
                i += bits[i] + 1;
            }

            return i == n - 1;
        }

        /// <summary>
        /// 8. 把字符串转换成整数 (atoi)
        /// LCR 192. 把字符串转换成整数 (atoi)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StrToInt(string str)
        {
            if (str == null || str.Length == 0)
                return 0;
            str = str.Trim();
            int start = 0, sign = 1;
            if (str[0] == '+')
            {
                sign = 1;
                start++;
            }
            else if (str[0] == '-')
            {
                sign = -1;
                start++;
            }
            long res = 0;
            for (int i = start; i < str.Length; i++)
            {
                if (!char.IsNumber(str[i]))
                    return (int)res * sign;
                res = res * 10 + str[i] - '0';
                if (sign == 1 && res > int.MaxValue) return int.MaxValue;
                if (sign == -1 && res > int.MaxValue) return int.MinValue;
            }

            return (int)res * sign;
        }
        /// <summary>
        /// 91. 解码方法
        /// 1. 设计状态
        /// 2. 写出状态转移方程
        /// 3. 设置初始状态
        /// 4. 执行状态转移
        /// 5. 返回最终结果
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>

        public static int numDecodings(string s)
        {
            int len = s.Length;
            int[] dp = new int[len + 1];                    // dp[i] 第i个位置上，解码的方案数
            dp[0] = 1;                                      // dp[0] 如果是空字符串，解析方案1个
            dp[1] = s[0] != '0' ? 1 : 0;                    // dp[1] 取第一个位置为0时，没有解析方案，解析失败，如果是其他的话，则有一个解决方案
            for (int i = 2; i <= len; i++)
            {
                int first = int.Parse(s.Substring(i - 1, 1));
                int second = int.Parse(s.Substring(i - 2, 2));
                if (first >= 1 && first <= 9)
                {
                    dp[i] = dp[i - 1];                      // dp[i] 上的方案数等于dp[i-1]上的方案数
                }
                if (second >= 10 && second <= 26)
                {
                    dp[i] = dp[i] + dp[i - 2];              // dp[i] 上的方案数等于dp[i]+dp[i-1]上的方案数
                }
            }

            return dp[len];
        }

        /// <summary>
        /// 139. 单词拆分
        /// ["leet","code"]
        /// leetcode
        ///     i=5
        /// j=0
        ///     j=4 i=8
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDict"></param>
        /// <returns></returns>
        public static bool WordBreak(string s, IList<string> wordDict)
        {
            HashSet<string> hash = new HashSet<string>(wordDict);
            bool[] dp = new bool[s.Length + 1];                         // dp[i] 取第i个字符是否可以在wordDict中匹配
            dp[0] = true;                                               // dp[0] 不取任何字符时，默认设置可以匹配
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (dp[j] && hash.Contains(s.Substring(j, i - j)))  // dp[j] 代表了在上一次为true的情况下，hash包含对应的值
                    {
                        dp[i] = true;                                   // 设置当前i位置时true
                        break;
                    }
                }
            }
            return dp[s.Length];
        }

        /// <summary>
        /// 45. 跳跃游戏 II
        /// Greedy 贪心算法
        /// 1. 定义  当前可移动到的终点位置 （Fathest）
        /// 2. 定义  下一个可以移动到的终点位置 (nextFathest)
        /// 3. 更新  下一个可以移动到的终点位置 (nextFathest)
        /// 4. i到达 当前可移动到的终点位置 (Fathest)
        /// 5. 更新  当前可移动到的终点位置 （Fathest）
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int Jump(int[] nums)
        {
            int ans = 0;
            int Fathest = 0;                                        // 1  
            int nextFathest = 0;                                    // 2
            for (int i = 0; i < nums.Length - 1; i++)
            {
                nextFathest = Math.Max(nextFathest, i + nums[i]);   // 3
                if (i == Fathest)                                   // 4
                {
                    ans++;
                    Fathest = nextFathest;                          // 5
                }
            }

            return ans;
        }

        /// <summary>
        /// 55. 跳跃游戏
        /// 1. 终点位置必须大于 n-1的位置
        /// 2. farthest
        /// 
        /// 3,2,1,0,4
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool CanJump(int[] nums)
        {
            int farthest = 0;                                 // 1
            for (int i = 0; i < nums.Length - 1; i++)
            {
                farthest = Math.Max(i + nums[i], farthest);
                if (i == farthest)                            // 2
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 1043. 分隔数组以得到最大和
        /// 从前往后推， 前面时后面的基础
        /// 
        /// 1. dp[i] 截取到第i个元素时，子数组和最大
        /// 2. dp[0] 什么的元素都不选，值为0
        /// 3. 取[i..j]的中的最大值 m
        /// 4. 状态转移 dp[i-j]的最大值 + m*j, 与dp[i] 取最大值 
        /// 
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int MaxSumAfterPartitioning(int[] nums, int k)
        {
            int n = nums.Length;
            int[] dp = new int[n + 1];                              // 1. 
            dp[0] = 0;                                              // 2. 
            for (int i = 1; i <= n; i++)
            {
                int m = int.MinValue;
                for (int j = 1; j <= Math.Min(i, k); j++)
                {
                    m = Math.Max(m, nums[i - j]);                   // 3. 
                    dp[i] = Math.Max(dp[i], dp[i - j] + m * j);     // 4. 
                }
            }

            return dp[n];
        }

        /// <summary>
        /// 53. 最大子数组和
        /// 面试题 16.17. 连续数列
        /// 1. 设计状态 f[i] 以i结尾的最大子数
        /// 2. 写出状态转移方程  f[i] = Math.Max(f[i - 1] + nums[i], nums[i]);
        /// 3. 设置初始状态   f[0] = nums[0];
        /// 4. 执行状态转移
        /// 5. 返回最终结果
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxSubArray(int[] nums)
        {
            int n = nums.Length;
            int[] f = new int[nums.Length];
            f[0] = nums[0];
            int max = nums[0];
            for (int i = 1; i < n; i++)
            {
                f[i] = Math.Max(f[i - 1] + nums[i], nums[i]);
                max = Math.Max(max, f[i]);
            }
            return max;
        }

        /// <summary>
        /// 1186. 删除一次得到子数组最大和
        /// 1. 定义状态 dp[i][0.1] 代表在i位置时不删和删的状态
        /// 2. 0 位置初始化状态 不删/删除
        /// 3. 状态转移方程： 不删:: dp[i][0] = Math.Max(dp[i - 1][0], 0) + nums[i] a 代表之前没有删过，b 0 如果之前时负数， 取nums[i]
        ///                 删除:: Math.Max(dp[i - 1][0], dp[i - 1][1] + nums[i]) a 代表删除当前，b 代表之前已经删过了
        /// 4. 使用Max(a,b,c) 保留 ans， 因为dp[i][0或1] 不一定就时最大的，还要保留之前的值
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int MaximumSum(int[] nums)
        {
            int n = nums.Length;
            int[][] dp = new int[n][];                                      // 1
            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[2];
            }
            dp[0][0] = nums[0];                                             // 2
            dp[0][1] = 0;
            int ans = nums[0];
            for (int i = 1; i < n; ++i)
            {
                dp[i][0] = Math.Max(dp[i - 1][0], 0) + nums[i];             // 3
                dp[i][1] = Math.Max(dp[i - 1][0], dp[i - 1][1] + nums[i]);

                ans = Math.Max(ans, Math.Max(dp[i][0], dp[i][1]));          // 4
            }
            return ans;
        }

        /// <summary>
        /// 1749. 任意子数组和的绝对值的最大值
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxAbsoluteSum(int[] nums)
        {
            int maxRes = 0, minRes = 0;
            int tempMax = 0, tempMin = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (tempMax + nums[i] <= 0)
                    tempMax = 0;
                else
                    tempMax += nums[i];
                if (tempMin + nums[i] >= 0)
                    tempMin = 0;
                else
                    tempMin += nums[i];
                if (tempMin < minRes)
                    minRes = tempMin;
                if (tempMax > maxRes)
                    maxRes = tempMax;
            }

            return Math.Max(maxRes, -minRes);
        }
        /// <summary>
        /// 1567. 乘积为正数的最长子数组长度
        /// 使用动态规划解决问题
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int GetMaxLen(int[] nums)
        {
            int m = nums.Length;
            int[][] dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[2];
            }

            if (nums[0] > 0)
            {
                dp[0][1] = 1;
            }
            else if (nums[0] == 0)
            {
                //
            }
            else
            {
                dp[0][0] = 1;
            }

            int max = dp[0][1];
            for (int i = 1; i < m; i++)
            {
                if (nums[i] == 0)
                {
                    //
                }
                else if (nums[i] > 0)
                {
                    dp[i][1] = dp[i - 1][1] + 1;
                    dp[i][0] = dp[i - 1][0] == 0 ? 0 : dp[i - 1][0] + 1;
                }
                else
                {
                    dp[i][1] = dp[i - 1][0] == 0 ? 0 : dp[i - 1][0] + 1;
                    dp[i][0] = dp[i - 1][1] + 1;
                }
                max = Math.Max(max, dp[i][1]);
            }
            return max;
        }

        /// <summary>
        /// 152. 乘积最大子数组
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxProduct(int[] nums)
        {
            int m = nums.Length;
            int[][] dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[2];
            }

            if (nums[0] > 0)
            {
                dp[0][1] = nums[0];
            }
            else if (nums[0] == 0)
            {
                //
            }
            else
            {
                dp[0][0] = nums[0];
            }

            int max = nums[0];
            for (int i = 1; i < m; i++)
            {
                if (nums[i] == 0)
                {
                    //
                }
                else if (nums[i] > 0)
                {
                    dp[i][1] = dp[i - 1][1] == 0 ? nums[i] : dp[i - 1][1] * nums[i];
                    dp[i][0] = dp[i - 1][0] == 0 ? 0 : dp[i - 1][0] * nums[i];
                }
                else
                {
                    dp[i][1] = dp[i - 1][0] == 0 ? 0 : dp[i - 1][0] * nums[i];
                    dp[i][0] = dp[i - 1][1] == 0 ? nums[i] : dp[i - 1][1] * nums[i];
                }
                max = Math.Max(dp[i][0], Math.Max(max, dp[i][1]));
            }
            return max;
        }
        /// <summary>
        /// 300. 最长递增子序列
        /// 1. dp[i] 在i位置上的最长递增子序列 状态转移方程 when nums[i] > nums[j] then dp[i] = max(dp[j]+1, dp[i])
        /// 2. 设置每一个dp[0..n-1] 默认等于1，因为对于每一个元素都单独认为是单调递增
        /// 3. dp[0] 初始状态 
        /// 4. 状态转移方程
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int LengthOfLIS(int[] nums)
        {
            int n = nums.Length;
            int[] dp = new int[n];                              // 1
            Array.Fill(dp, 1);                                  // 2
            dp[0] = 1;                                          // 3
            int maxans = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])                      // 4 
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }
                maxans = Math.Max(maxans, dp[i]);
            }

            return maxans;
        }

        /// <summary>
        /// 674. 最长连续递增序列
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindLengthOfLCIS(int[] nums)
        {
            int n = nums.Length;
            if (n == 1) return 1;
            int maxans = 0;
            int start = 0;
            for (int i = 0; i < n; i++)
            {
                if (i > 0 && nums[i] <= nums[i - 1])
                {
                    start = i;
                }
                maxans = Math.Max(maxans, i - start + 1);
            }
            return maxans;
        }

        /// <summary>
        /// 300. 最长递增子序列
        /// 673. 最长递增子序列的个数
        /// 674. 最长连续递增序列
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindNumberOfLIS(int[] nums)
        {
            int n = nums.Length;
            int[] dp = new int[n + 1];                              // 1
            Array.Fill(dp, 1);                                      // 2
            dp[0] = 1;                                              // 3
            int maxans = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])                          // 4 
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }
                maxans = Math.Max(maxans, dp[i]);
            }
            int cnt = dp.Where(x => x == maxans).Count();
            return cnt;
        }

        /// <summary>
        /// LCR 101. 分割等和子集
        /// 416. 分割等和子集
        /// 分析：
        /// 定义状态： dp[i][j] 在选择第i个物品且j装满为有效
        /// 1. 只要有一个子集等于总和一半即可返回true
        /// 2. i范围[0...n]， j范围[0...sum] 
        /// 3. 包体积为0，认为满 true
        /// 4. 放不下  dp[0][1] =fasle;
        /// 5. 可以放下且容积j等于nums[i-1]
        /// 6. 容积j大于第nums[i-1]时， 
        ///     6.1 找到dp[i-1][j-nums[i-1]]是否放下，如果放下，则再放一个nums[i-1]
        ///     6.2 虽然大，但是放不下，不放了直接给false
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool CanPartition(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 == 1) return false;
            sum = sum / 2;                                          // 1

            int n = nums.Length;
            bool[][] dp = new bool[n + 1][];                        // 2
            for (int i = 0; i <= n; i++)
                dp[i] = new bool[sum + 1];

            for (int i = 0; i <= n; i++)                            // 3
                dp[i][0] = true;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= sum; j++)
                {
                    if (j < nums[i - 1])                            // 4
                    {
                        dp[i][j] = dp[i - 1][j];
                    }
                    //else if (j == nums[i - 1])                      // 5
                    //{
                    //    dp[i][j] = true;
                    //}
                    else                                            // 6
                    {
                        dp[i][j] = dp[i - 1][j - nums[i - 1]] || dp[i - 1][j];
                    }
                }
            }

            return dp[n][sum];
        }

        /// <summary>
        /// 1262. 可被三整除的最大和
        /// 分析：
        /// 模有三种情况{0,1,2}
        /// 0 直接返回sum
        /// 1，2 减去对应的值后返回sum
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxSumDivThree(int[] nums)
        {
            int sum = 0;
            List<int> a1 = new List<int>();
            List<int> a2 = new List<int>();
            foreach (var item in nums)
            {
                sum += item;
                if (item % 3 == 1)
                {
                    a1.Add(item);
                }
                else if (item % 3 == 2)
                {
                    a2.Add(item);
                }
            }

            if (sum % 3 == 0) return sum;

            a1.Sort();
            a2.Sort();

            int wantedDelete = int.MaxValue;

            if (sum % 3 == 1)                                               // 和的模为1时
            {
                if (a1.Count >= 1)
                    wantedDelete = Math.Min(wantedDelete, a1[0]);           // 减去一个模为1的数 1%3= 1
                if (a2.Count >= 2)
                    wantedDelete = Math.Min(wantedDelete, a2[0] + a2[1]);   // 减去两个模为2的数 4%3 = 1
            }

            if (sum % 3 == 2)                                               // 和的模为2时
            {
                if (a2.Count >= 1)
                    wantedDelete = Math.Min(wantedDelete, a2[0]);           // 减去一个模为2的数 2%3= 2
                if (a1.Count >= 2)
                    wantedDelete = Math.Min(wantedDelete, a1[0] + a1[1]);   // 减去两个模为1的数 (1+1)%3= 2
            }

            return sum - wantedDelete;
        }
        /// <summary>
        /// 1262. 可被三整除的最大和
        /// dp[i][j] i位置模为j的最大和
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaxSumDivThree1(int[] nums)
        {
            int n = nums.Length;
            int[][] dp = new int[n + 1][];
            for (int i = 0; i <= n; i++)
                dp[i] = new int[3];
            dp[0][0] = 0;                                               // 都不选的最大和为0
            dp[0][1] = 0;
            dp[0][2] = 0;
            for (int i = 0; i < n; i++)
            {
                int j = i + 1;
                for (int cnt = 0; cnt < 3; cnt++)
                {
                    dp[j][cnt] = dp[j - 1][cnt];                        // 不选择i位置，将状态转移
                    int sum = dp[j - 1][cnt] + nums[i];                 // 选择i位置，将状态转移
                    dp[j][sum % 3] = Math.Max(dp[j][sum % 3], sum);     // 更新dp[i][j]的值
                }
            }

            return dp[n][0];
        }
        /// <summary>
        /// 474. 一和零
        /// dp[i][j] 当i是0的个数，j是1的个数的最大子集的长度
        /// 1. 统计每个字符串0， 1的个数
        /// 2. 状态转移方程从dp[m][n] = 0 开始，取dp[i][j]的最大值
        ///  2.1 m-zeros所在行，n-ones所在列dp[5-1,4-1]+1 的字串值
        ///  2.2 
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int FindMaxForm(string[] strs, int m, int n)
        {
            int[][] dp = new int[m + 1][];
            for (int i = 0; i <= m; i++)
                dp[i] = new int[n + 1];

            foreach (var s in strs)
            {
                int zeros = s.Count(x => x == '0'), ones = s.Count(x => x == '1');          // 1

                for (int i = m; i >= zeros; i--)
                {
                    for (int j = n; j >= ones; j--)
                    {
                        dp[i][j] = Math.Max(dp[i][j], 1 + dp[i - zeros][j - ones]);         // 2
                    }
                }

            }
            return dp[m][n];
        }

        /// <summary>
        /// 1049. 最后一块石头的重量 II
        /// </summary>
        /// <param name="stones"></param>
        /// <returns></returns>
        public static int LastStoneWeightII(int[] stones)
        {
            int n = stones.Length;
            int total = stones.Sum();
            int half = stones.Sum() / 2;
            int[][] dp = new int[n + 1][];
            for (int i = 0; i < n + 1; i++)
                dp[i] = new int[half + 1];

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= half; j++)
                {
                    int cur = stones[i - 1];
                    if (cur > j)
                    {
                        dp[i][j] = dp[i - 1][j];
                    }
                    else
                    {
                        dp[i][j] = Math.Max(cur + dp[i - 1][j - cur], dp[i - 1][j]);
                    }
                }
            }

            return total - dp[n][half] * 2;
        }

        /// <summary>
        /// 面试题 08.11. 硬币
        /// 322. 零钱兑换
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int WaysToChange(int n)
        {
            int[] nums = { 1, 5, 10, 25 };
            int len = nums.Length;
            int[][] dp = new int[len + 1][];          // dp[i][j] [0...m]选择i方案时，当容量为j时的方案数
            for (int i = 0; i < len + 1; i++)
            {
                dp[i] = new int[n + 1];               // 容量[0...n] 共n+1中判断
            }
            for (int i = 0; i < len + 1; i++)
            {
                dp[i][0] = 1;                         // dp[i][0] 容量为0时的默认方案数是1
            }

            for (int i = 1; i <= len; i++)            // 有4种选择从(i-1)=>[1..4]
            {
                for (int j = 1; j <= n; j++)          // 容量从[1...n]开始算起
                {
                    if (j >= nums[i - 1])             // 状态转移 可选nums[i-1]
                    {
                        dp[i][j] = (dp[i - 1][j] + dp[i][j - nums[i - 1]]) % 1000000007; // 状态转移  上个币种的方法数 dp[i - 1][j] + 本币种的方法数 dp[i][j - nums[i - 1]]
                    }
                    else                              // 不选
                    {
                        dp[i][j] = dp[i - 1][j];
                    }
                }
            }

            return dp[len][n];
        }


        /// <summary>
        /// 518. 零钱兑换 II
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CoinChange1(int[] coins, int amount)
        {
            int n = amount;
            int[] nums = coins; // { 1, 5, 10, 25 };
            int len = nums.Length;
            int[][] dp = new int[len + 1][];          // dp[i][j] [0...m]选择i方案时，当容量为j时的方案数
            for (int i = 0; i < len + 1; i++)
            {
                dp[i] = new int[n + 1];               // 容量[0...n] 共n+1中判断
            }
            for (int i = 0; i < len + 1; i++)
            {
                dp[i][0] = 1;                         // dp[i][0] 容量为0时的默认方案数是1
            }

            for (int i = 1; i <= len; i++)            // 有4种选择从(i-1)=>[1..4]
            {
                for (int j = 1; j <= n; j++)          // 容量从[1...n]开始算起
                {
                    if (j >= nums[i - 1])             // 状态转移 可选nums[i-1]
                    {
                        dp[i][j] = (dp[i - 1][j] + dp[i][j - nums[i - 1]]) % 1000000007; // 状态转移  上个币种的方法数 dp[i - 1][j] + 本币种的方法数 dp[i][j - nums[i - 1]]
                    }
                    else                              // 不选
                    {
                        dp[i][j] = dp[i - 1][j];
                    }
                }
            }

            return dp[len][n];
        }

        /// <summary>
        /// 518. 零钱兑换 II
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="coins"></param>
        /// <returns></returns>
        public static int Change(int amount, int[] coins)
        {
            int[] dp = new int[amount + 1];
            dp[0] = 1;
            foreach (var coin in coins)
            {
                for (int j = 1; j <= amount; j++)
                {
                    if (j - coin >= 0)
                    {
                        dp[j] = dp[j] + dp[j - coin];
                    }
                }
            }

            return dp[amount];
        }

        /// <summary>
        /// LCR 168. 丑数
        /// 264. 丑数 II
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int NthUglyNumber(int n)
        {
            long[] factors = { 2, 3, 5 };
            HashSet<long> set = new HashSet<long>();
            PriorityQueue<long, long> pq = new PriorityQueue<long, long>();
            set.Add(1);
            pq.Enqueue(1, 1);
            long ugly = 0;
            for (int i = 0; i < n; i++)
            {
                long cur = pq.Dequeue();
                ugly = cur;
                foreach (var factor in factors)
                {
                    long next = (long)cur * factor;
                    if (set.Add(next))
                        pq.Enqueue(next, next);
                }
            }
            return (int)ugly;
        }

        /// <summary>
        /// 1981. 最小化目标值与所选元素的差
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int MinimizeTheDifference(int[][] mat, int target)
        {
            HashSet<int> cap = new HashSet<int>() { 0 };

            foreach (var row in mat)
            {
                HashSet<int> temp = new HashSet<int>() { 0 };
                int gTarget = -1;                                   // 大于target保留一个数字
                int lTarget = int.MaxValue;
                foreach (var x in cap)                              // 遍历Rows
                {
                    foreach (var y in row)                          // 遍历cols
                    {
                        if (x + y <= target)                        // 小于target添加到temp中
                        {
                            if (lTarget == int.MaxValue)
                            {
                                lTarget = x + y;
                            }
                            else
                            {
                                if (lTarget < x + y)
                                {
                                    lTarget = x + y;
                                }
                            }

                        }
                        else
                        {
                            if (gTarget == -1 || x + y < gTarget)   // 检索row小于gTarget的数字
                                gTarget = x + y;
                        }
                    }
                }
                if (lTarget != int.MaxValue)
                {
                    temp.Add(lTarget);
                }
                if (gTarget != -1)
                {
                    temp.Add(gTarget);
                }

                foreach (var item in temp)
                {
                    cap.Add(item);
                }
            }

            int diff = int.MaxValue;
            foreach (var item in cap)
            {
                diff = Math.Min(diff, Math.Abs(item - target));
            }

            return diff;
        }

        /// <summary>
        /// 435. 无重叠区间
        /// 452. 用最少数量的箭引爆气球
        /// 贪心
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public static int EraseOverlapIntervals(int[][] intervals)
        {
            Array.Sort(intervals, (a, b) => a[1].CompareTo(b[1]));
            int count = 1;                                  // 至少有一个不相交
            int end = intervals[0][1];                      // 最早结束
            foreach (var intv in intervals)
            {
                int start = intv[0];                        // 找到下一个比end大的intv
                if (start > end)
                {
                    count++;
                    end = intv[1];
                }
            }

            return intervals.Length - count;
        }

        /// <summary>
        /// 452. 用最少数量的箭引爆气球
        /// 435. 无重叠区间
        /// 贪心
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static int FindMinArrowShots(int[][] points)
        {
            Array.Sort(points, (a, b) => a[1].CompareTo(b[1]));
            int count = 1;
            int end = points[0][1];                      // 最早结束
            foreach (var intv in points)
            {
                int start = intv[0];                        // 找到下一个比end大的intv
                if (start > end)
                {
                    count++;
                    end = intv[1];
                }
            }

            return count;
        }
        /// <summary>
        /// 20. 有效的括号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (var c in s)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count != 0
                        && stackOf(c) == stack.Peek())
                    {
                        stack.Pop();
                    }
                    else
                        return false;
                }
            }

            return stack.Count == 0;
        }

        private static char stackOf(char c)
        {
            if (c == ')')
                return '(';
            if (c == '}')
                return '{';
            else
                return '[';
        }

        /// <summary>
        /// LCR 095. 最长公共子序列
        /// 1143. 最长公共子序列
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public static int LongestCommonSubsequence(string s1, string s2)
        {
            int m = s1.Length, n = s2.Length;
            int[][] dp = new int[m + 1][];                             // dp[i][j] 索引在i,j位置时的最长公共子序列
            for (int i = 0; i <= m; i++)                              // dp[0..m][0] = 0 s2什么都不取，公共子序列为0 
                dp[i] = new int[n + 1];                                // dp[0][0..n] = 0 s1什么都不取，公共子序列为0
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                        dp[i][j] = dp[i - 1][j - 1] + 1;
                    else
                        dp[i][j] = Math.Max(dp[i][j - 1], dp[i - 1][j]);
                }
            }


            return dp[m][n];
        }
    } // class end


    public class BackTrackQuestion
    {
        IList<IList<int>> res = new List<IList<int>>();
        LinkedList<int> track = new LinkedList<int>();
        /// <summary>
        /// 78. 子集
        ///  input: [1,2,3]
        /// output: [] [1] [1,2] [1,2,3] [1,3] [2],[2,3] [3]
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<IList<int>> Subsets(int[] nums)
        {
            return null;
        }

        /// <summary>
        /// 46. 全排列
        ///  input: [1,2,3]
        /// output: [[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public IList<IList<int>> Permute(int[] nums)
        {
            backtrack(nums);
            return res;
        }
        private void backtrack(int[] nums)
        {
            if (track.Count == nums.Length)
            {
                res.Add(new List<int>(track));
                return;
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (track.Contains(nums[i])) continue;

                track.AddLast(nums[i]);
                backtrack(nums);
                track.RemoveLast();
            }
        }

        IList<IList<int>> res_combine = new List<IList<int>>();
        /// <summary>
        /// 77. 组合
        /// LCR 080. 组合
        /// 
        /// 1. 路径等于k
        /// 2. 纵向控制 depth
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public IList<IList<int>> Combine(int n, int k)
        {
            IList<int> path = new List<int>();
            DFS_Combine(1, n, k, path);
            return res_combine;
        }

        /// <summary>
        /// 1. 路径等于k
        /// 2. 纵向控制 depth
        /// </summary>
        /// <param name="depth">深度</param>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <param name="path"></param>
        private void DFS_Combine(int depth, int n, int k, IList<int> path)
        {
            if (path.Count == k)                                // 1
            {
                res_combine.Add(path);
                return;
            }

            for (int i = depth; i <= n; ++i)                    // 2
            {
                path.Add(i);
                DFS_Combine(i + 1, n, k, path);
                path.RemoveAt(path.Count - 1);
            }
        }

        IList<IList<int>> res_Subsets = new List<IList<int>>();
        /// <summary>
        /// 78. 子集
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<IList<int>> Subsets1(int[] nums)
        {
            IList<int> path = new List<int>();
            dfs_Subsets1(0, nums, path);
            return res_Subsets;
        }

        private void dfs_Subsets1(int depth, int[] nums, IList<int> path)
        {
            res_Subsets.Add(new List<int>(path));

            for (int i = depth; i < nums.Length; ++i)
            {
                path.Add(nums[i]);
                dfs_Subsets1(i + 1, nums, path);
                path.RemoveAt(path.Count - 1);
            }
        }

        /// <summary>
        /// 39. 组合总和
        /// [2,3,6,7]
        /// 2222
        /// 2223
        /// 2226
        /// 2227
        /// 2233
        /// 2236
        /// 2237
        /// 26
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        IList<IList<int>> res_CombinationSum = new List<IList<int>>();
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<int> path = new List<int>();
            dfs_CombinationSum(0, candidates, target, 0, path);
            return res_CombinationSum;
        }

        private void dfs_CombinationSum(int depth, int[] nums, int target, int sum, IList<int> path)
        {
            if (sum > target)
                return;

            if (sum == target)
            {
                res_CombinationSum.Add(new List<int>(path));
                return;
            }

            for (int i = depth; i < nums.Length; ++i)
            {
                path.Add(nums[i]);                                  // 3. add
                sum += nums[i];                                     // 2. status changed
                dfs_CombinationSum(i + 1, nums, target, sum, path);
                sum -= nums[i];                                     // 2. status changed
                path.RemoveAt(path.Count - 1);                      // 4. remove
            }
        }


        /// <summary>
        /// 40. 组合总和 II
        /// LCR 082. 组合总和 II
        /// [10,1,2,7,6,1,5]
        /// 1,1,2,5,6,7,10
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        IList<IList<int>> res_CombinationSum2 = new List<IList<int>>();
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            IList<int> path = new List<int>();
            Array.Sort(candidates);
            dfs_CombinationSum2(0, candidates, target, 0, path);
            return res_CombinationSum2;
        }

        private void dfs_CombinationSum2(int depth, int[] nums, int target, int sum, IList<int> path)
        {
            if (sum > target)
                return;

            if (sum == target)
            {
                res_CombinationSum2.Add(new List<int>(path));
                return;
            }

            for (int i = depth; i < nums.Length; ++i)
            {
                if (i > depth && nums[i] == nums[i - 1])            // 同一深度不使用相同的数，因为纵向的已经要过了。会重复计算
                {
                    continue;
                }
                path.Add(nums[i]);                                  // 3. add
                sum += nums[i];                                     // 2. status changed
                dfs_CombinationSum2(i + 1, nums, target, sum, path);
                sum -= nums[i];                                     // 2. status changed
                path.RemoveAt(path.Count - 1);                      // 4. remove
            }
        }


        /// <summary>
        /// 216. 组合总和 III
        /// 
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        IList<IList<int>> res_CombinationSum3 = new List<IList<int>>();
        public IList<IList<int>> CombinationSum3(int k, int target)
        {
            IList<int> path = new List<int>();
            dfs_CombinationSum3(1, k, target, 0, path);
            return res_CombinationSum3;
        }

        private void dfs_CombinationSum3(int depth, int k, int target, int sum, IList<int> path)
        {
            if (sum > target)
                return;

            if (sum == target)
            {
                if (path.Count == k)                                // 1. 相加和为n且数量为k个
                    res_CombinationSum3.Add(new List<int>(path));
                return;                                             // 任何一个超限，返回
            }

            for (int i = depth; i <= 9; ++i)
            {
                path.Add(i);                                        // 3. add
                sum += i;                                           // 2. status changed
                dfs_CombinationSum3(i + 1, k, target, sum, path);
                sum -= i;                                           // 2. status changed
                path.RemoveAt(path.Count - 1);                      // 4. remove
            }
        }


        /// <summary>
        /// 377. 组合总和 Ⅳ
        /// LCR 104. 组合总和 Ⅳ
        /// 暴力解法导致内存过大，需要使用DP的方式来解决
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        IList<IList<int>> res_CombinationSum4 = new List<IList<int>>();
        public int CombinationSum4(int[] candidates, int target)
        {
            //IList<int> path = new List<int>();
            //Array.Sort(candidates);
            //dfs_CombinationSum4(0, candidates, target, 0, path);
            //return res_CombinationSum4.Count;

            /// 1. 设计状态                 dp[i] 选取方案之和等于i的方案数
            /// 2. 写出状态转移方程          dp[i] = dp[i]+dp[i-num]
            /// 3. 设定初始状态              dp[0] = 1, 和为0的方案数默认有一个即什么都不选
            /// 4. 执行状态转移              for[1...target]=> for[nums[0]...nums[n-1]]
            /// 5. 返回最终的解               return dp[target]
            /// 
            int[] dp = new int[target + 1];
            dp[0] = 1;
            for (int i = 1; i <= target; i++)
            {
                for (int j = 0; j < candidates.Length; j++)
                {
                    if (i > candidates[j])
                    {
                        dp[i] = dp[i] + dp[i - candidates[j]];
                    }
                }
            }


            return dp[target];
        }

        //private void dfs_CombinationSum4(int depth, int[] nums, int target, int sum, IList<int> path)
        //{
        //    if (sum > target)
        //        return;

        //    if (sum == target)
        //    {
        //        res_CombinationSum4.Add(new List<int>(path));
        //        return;
        //    }

        //    for (int i = depth; i < nums.Length; ++i)
        //    {
        //        path.Add(nums[i]);                                  // 3. add
        //        sum += nums[i];                                     // 2. status changed
        //        dfs_CombinationSum4(depth, nums, target, sum, path);
        //        sum -= nums[i];                                     // 2. status changed
        //        path.RemoveAt(path.Count - 1);                      // 4. remove
        //    }
        //}

        /// <summary>
        /// LCR 085. 括号生成
        /// 22. 括号生成
        /// 1. 左括号剩余的数量少于右括号了
        /// 2. 递归left
        /// 3. 递归right
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IList<string> GenerateParenthesis(int n)
        {
            IList<string> res = new List<string>();
            dfs_GenerateParenthesis(n, n, n, "", res);
            return res;
        }

        private void dfs_GenerateParenthesis(int l, int r, int n, string path, IList<string> res)
        {
            if (l > r)                                                          // 1
            {
                return;
            }
            if (l == 0 && r == 0)
            {
                res.Add(path);
                return;
            }

            if (l > 0)
            {
                dfs_GenerateParenthesis(l - 1, r, n, path + "(", res);          // 2
            }
            if (r > 0)
            {
                dfs_GenerateParenthesis(l, r - 1, n, path + ")", res);           // 3
            }
        }


        /// <summary>
        /// 46. 全排列
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public IList<IList<int>> Permute1(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            IList<int> path = new List<int>();
            dfs_Permute1(nums, path, res);

            return res;
        }

        private void dfs_Permute1(int[] nums, IList<int> path, IList<IList<int>> res)
        {
            // return
            if (path.Count == nums.Length)
            {
                res.Add(new List<int>(path));
                return;
            }
            // choice
            for (int i = 0; i < nums.Length; i++)
            {
                if (path.Contains(nums[i]))
                    continue;
                path.Add(nums[i]);
                dfs_Permute1(nums, path, res);
                path.RemoveAt(path.Count - 1);
            }
        }

        /// <summary>
        /// 47. 全排列 II
        /// LCR 084. 全排列 II
        /// 可包含重复数字的序列 nums
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            List<int> path = new List<int>();
            Array.Sort(nums);
            bool[] visited = new bool[nums.Length];
            dfs_PermuteUnique(nums, path, res, visited);

            return res;
        }

        private void dfs_PermuteUnique(int[] nums, List<int> path, IList<IList<int>> res, bool[] visited)
        {
            if (path.Count == nums.Length)
            {
                res.Add(new List<int>(path));
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (visited[i])
                    continue;
                if (i > 0 && nums[i] == nums[i - 1] && !visited[i - 1])
                    continue;

                path.Add(nums[i]);
                visited[i] = true;
                dfs_PermuteUnique(nums, path, res, visited);
                visited[i] = false;
                path.RemoveAt(path.Count - 1);
            }
        }
    }
    public class TreeQuestion
    {
        IList<int> res;
        public TreeQuestion()
        {
            res = new List<int>();
        }
        /// <summary>
        /// 144. 二叉树的前序遍历
        /// 根，左，右
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<int> PreorderTraversal(TreeNode root)
        {
            if (root == null)
                return res;

            res.Add(root.val);
            PreorderTraversal(root.left);
            PreorderTraversal(root.right);
            return res;
        }
        /// <summary>
        /// 94. 二叉树的中序遍历
        /// 左，根，右
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<int> InorderTraversal(TreeNode root)
        {
            if (root == null)
                return res;

            InorderTraversal(root.left);
            res.Add(root.val);
            InorderTraversal(root.right);

            return res;
        }
        /// <summary>
        /// 145. 二叉树的后序遍历
        /// 右，根，左
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<int> PostorderTraversal(TreeNode root)
        {
            if (root == null)
                return res;

            PostorderTraversal(root.left);
            PostorderTraversal(root.right);
            res.Add(root.val);

            return res;
        }

        /// <summary>
        /// 110. 平衡二叉树
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        bool isBalance = false;
        public bool IsBalanced(TreeNode root)
        {
            maxDepth(root);
            return isBalance;
        }

        private int maxDepth(TreeNode root)
        {
            if (root == null) return 0;

            int leftMaxDepth = maxDepth(root.left);
            int rightMaxDepth = maxDepth(root.right);
            if (Math.Abs(leftMaxDepth - rightMaxDepth) > 1)
                isBalance = false;

            return 1 + Math.Max(leftMaxDepth, rightMaxDepth);
        }

        /// <summary>
        /// 965. 单值二叉树
        /// 1. 父节点是叶子节点
        /// 2. 主逻辑值： 节点本身的值不相等
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public bool IsUnivalTree(TreeNode root)
        {
            return UnivalTree(root, root.val);
        }

        private bool UnivalTree(TreeNode root, int val)
        {
            if (root == null)                           // 1
                return true;

            if (root.val != val)                        // 2
                return false;

            return UnivalTree(root.left, val) && UnivalTree(root.right, val);
        }
        /// <summary>
        /// 100. 相同的树
        /// 1. 父节点是叶子节点
        /// 2. 其中一个叶子节点为空（另外一个不为空）
        /// 3. 值不相等
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)                 // 1
                return true;

            if (p == null || q == null)                 // 2
                return false;

            if (p.val != q.val)                         // 3
                return false;

            return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
        /// <summary>
        /// 257. 二叉树的所有路径
        /// 1. NULL 节点
        /// 2. 到达叶子节点
        /// 3. 回溯的时候使用了 context 中的 path，因此可以构造多条路径
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<string> BinaryTreePaths(TreeNode root)
        {
            IList<string> res = new List<string>();
            DFS_BinaryTreePaths(root, "", res);
            return res;
        }

        private void DFS_BinaryTreePaths(TreeNode root, string path, IList<string> res)
        {
            if (root == null)                                               // 1
                return;

            if (root.left == null && root.right == null)                    // 2
            {
                res.Add(path + root.val);
                return;
            }

            DFS_BinaryTreePaths(root.left, path + root.val + "->", res);    // 3
            DFS_BinaryTreePaths(root.right, path + root.val + "->", res);
        }


        /// <summary>
        /// 112. 路径总和
        /// 1. NULL 节点
        /// 2. 叶子节点与目标值相等
        /// 3. 递推l,r部分的路径和是否与目标节点一致
        /// </summary>
        /// <param name="root"></param>
        /// <param name="targetSum"></param>
        /// <returns></returns>
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)                                               // 1
                return false;

            if (root.left == null && root.right == null)                    // 2
                return root.val == targetSum;

            bool left = HasPathSum(root.left, targetSum - root.val);        // 3
            bool right = HasPathSum(root.right, targetSum - root.val);

            return left || right;
        }

        /// <summary>
        /// 226. 翻转二叉树
        /// 1. NULL 节点
        /// 2. 翻转左右节点
        /// 3. 递推l,r
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)                               // 1
                return null;

            TreeNode tmp = root.right;                      // 2
            root.right = root.left;
            root.left = tmp;

            InvertTree(root.left);                          // 3
            InvertTree(root.right);

            return root;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subRoot"></param>
        /// <returns></returns>
        public bool IsSubtree(TreeNode root, TreeNode subRoot)
        {
            if (root == null)
                return false;

            return dfs_IsSubtree(root, subRoot);
        }

        private bool dfs_IsSubtree(TreeNode s, TreeNode t)
        {
            if (s == null)
                return false;

            return check_IsSubtree(s, t) || dfs_IsSubtree(s.left, t) || dfs_IsSubtree(s.right, t);
        }

        private bool check_IsSubtree(TreeNode s, TreeNode t)
        {
            if (s == null && t == null)
            {
                return true;
            }
            if (s == null || t == null || s.val != t.val)
            {
                return false;
            }
            return check_IsSubtree(s.left, t.left) && check_IsSubtree(s.right, t.right);

        }

        /// <summary>
        /// 101. 对称二叉树
        /// 1. NULL 节点
        /// 2. 其中一个为空
        /// 3. 递推outter,inner
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsSymmetric(TreeNode root)
        {
            if (root == null) return true;

            return DFS_IsSymmetric(root.left, root.right);
        }

        /// <summary>
        /// 111. 二叉树的最小深度
        /// 1. 上一级别是叶子节点，深度0
        /// 2. 本级别是叶子节点，深度1
        /// 3. 左右树最小深度
        /// 4. 其中一个为null 返回带有叶子节点的深度（中间节点不算最小深度）
        /// 5. 去左右最小深度的值+1
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MinDepth(TreeNode root)
        {
            if (root == null)                               // 1
                return 0;

            if (root.left == null && root.right == null)    // 2
                return 1;

            int m1 = MinDepth(root.left);                   // 3
            int m2 = MinDepth(root.right);

            if (root.left == null || root.right == null)    // 4
                return m1 + m2 + 1;

            return Math.Min(m1, m2) + 1;                    // 5
        }

        private static bool DFS_IsSymmetric(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)                          // 1
                return true;

            if (left == null || right == null)                          // 2
                return false;

            if (left.val != right.val)
                return false;

            bool outter = DFS_IsSymmetric(left.left, right.right);       // 3
            bool inner = DFS_IsSymmetric(left.right, right.left);
            return outter && inner;
        }
        /// <summary>
        /// 102. 二叉树的层序遍历
        /// 1. 定义结果集
        /// 2. 队列
        /// 3. 本级元素
        /// 4. 准备子元素
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();             // 1
            if (root == null)
                return res;

            Queue<TreeNode> q = new Queue<TreeNode>();                  // 2
            q.Enqueue(root);
            while (q.Count > 0)
            {
                int size = q.Count;
                List<int> level = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    TreeNode cur = q.Dequeue();
                    level.Add(cur.val);                                 // 3

                    if (cur.left != null)                               // 4
                        q.Enqueue(cur.left);
                    if (cur.right != null)
                        q.Enqueue(cur.right);
                }
                res.Add(level);
            }
            return res;
        }
        /// <summary>
        /// 114. 二叉树展开为链表
        /// </summary>
        /// <param name="root"></param>
        public void Flatten(TreeNode root)
        {
            if (root == null)
                return;

            Flatten(root.left);
            Flatten(root.right);

            TreeNode left = root.left;
            TreeNode right = root.right;

            root.left = null;
            root.right = left;

            TreeNode p = root;
            while (p.right != null)
            {
                p = p.right;
            }
            p.right = right;
        }

        /// <summary>
        /// 513. 找树左下角的值
        /// LCR 045. 找树左下角的值
        /// 最底层 最左边 节点的值
        /// 1. 高度自增1
        /// 2. 递推左右子树
        /// 3. 高度自增1时， 取第一个val
        /// </summary>
        int curVal = 0;
        int curHeight = 0;
        public int FindBottomLeftValue(TreeNode root)
        {
            int curHeight = 0;
            DFS(root, 0);
            return curVal;
        }
        public void DFS(TreeNode root, int height)
        {
            if (root == null)
                return;

            height++;                               // 1

            DFS(root.left, height);                 // 2
            DFS(root.right, height);

            if (height > curHeight)                 // 3
            {
                curHeight = height;
                curVal = root.val;
            }
        }
        /// <summary>
        /// 872. 叶子相似的树
        /// 1. 找到叶子节点
        /// 2. 递推左右子树
        /// </summary>
        /// <param name="root1"></param>
        /// <param name="root2"></param>
        /// <returns></returns>
        public bool LeafSimilar(TreeNode root1, TreeNode root2)
        {
            IList<int> seq1 = new List<int>();
            if (root1 != null)
            {
                DFS_LeafSimilar(root1, seq1);
            }

            IList<int> seq2 = new List<int>();
            if (root2 != null)
            {
                DFS_LeafSimilar(root2, seq2);
            }

            return seq1.SequenceEqual(seq2);
        }

        private void DFS_LeafSimilar(TreeNode node, IList<int> seq)
        {
            if (node.left == null && node.right == null)                // 1
            {
                seq.Add(node.val);
            }
            else
            {
                if (node.left != null)
                {
                    DFS_LeafSimilar(node.left, seq);                    // 2
                }
                if (node.right != null)
                {
                    DFS_LeafSimilar(node.right, seq);
                }
            }
        }

        /// <summary>
        /// 404. 左叶子之和
        /// 1. 是node的左节点，【同时】是叶子节点，则开始计数；非叶子节点，递推左树
        /// 2. 是node的右节点，【同时】非叶子节点，则递推右子树
        /// 3. 判断叶子节点
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int SumOfLeftLeaves(TreeNode root)
        {
            if (root == null)
                return 0;


            return DFS_SumOfLeftLeaves(root); ;
        }

        private int DFS_SumOfLeftLeaves(TreeNode node)
        {
            int ans = 0;
            if (node.left != null)                          // 1
            {
                ans += IsLeafNode(node.left) ? node.left.val : DFS_SumOfLeftLeaves(node.left);
            }
            if (node.right != null && !IsLeafNode(node.right))
            {
                ans += DFS_SumOfLeftLeaves(node.right);     // 2
            }

            return ans;
        }

        private bool IsLeafNode(TreeNode node)
        {
            return node.left == null && node.right == null; // 3
        }

        /// <summary>
        /// 面试题 04.03. 特定深度节点链表
        /// 1. 弹出来size个node
        /// 2. 添加层级
        /// 3. 构造这个链表
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public ListNode[] ListOfDepth(TreeNode root)
        {
            List<List<TreeNode>> res = new List<List<TreeNode>>();

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                int size = q.Count;

                var level = new List<TreeNode>();
                for (int i = 0; i < size; i++)                  // 1
                {
                    TreeNode node = q.Dequeue();
                    level.Add(node);
                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }

                res.Add(level);                                  // 2
            }

            if (res.Count == 0) return new ListNode[0];


            ListNode[] ans = new ListNode[res.Count];           // 3
            for (int i = 0; i < res.Count; i++)
            {
                var head = new ListNode() { val = res[i][0].val };
                var p = head;
                for (int j = 1; j < res[i].Count; j++)
                {
                    var node = new ListNode() { val = res[i][j].val };
                    p.next = node;
                    p = node;
                }
                ans[i] = head;
            }

            return ans;
        }

        /// <summary>
        /// 113. 路径总和 II
        /// 1. 定义结果集
        /// 2. 定义递推函数
        /// 3. 是叶子节点且val == sum时,添加路径和 
        /// 4. 剩余的目标和
        /// 5. 递推左右子树
        /// 6. 路径和与目标和不一致，则退回
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            IList<IList<int>> res = new List<IList<int>>();             // 1
            DFS_PathSum2(root, sum, new List<int>(), res);              // 2
            return res;
        }

        private void DFS_PathSum2(TreeNode root, int sum, List<int> path, IList<IList<int>> res)
        {
            if (root == null) return;

            path.Add(root.val);
            if (root.left == null && root.right == null && root.val == sum)
            {
                res.Add(new List<int>(path));                          // 3
            }

            int target = sum - root.val;                               // 4   

            DFS_PathSum2(root.left, target, path, res);                // 5
            DFS_PathSum2(root.right, target, path, res);

            path.RemoveAt(path.Count - 1);                             // 6
        }

        /// <summary>
        /// 437. 路径总和 III
        /// </summary>
        /// <param name="root"></param>
        /// <param name="targetSum"></param>
        /// <returns></returns>
        public int PathSum3(TreeNode root, int targetSum)
        {
            if (root == null) return 0;

            int ret = RootSum(root, targetSum);
            ret += PathSum3(root.left, targetSum);
            ret += PathSum3(root.right, targetSum);

            return ret;
        }

        private int RootSum(TreeNode root, int targetSum)
        {
            int ret = 0;

            if (root == null)
                return 0;

            if (root.val == targetSum)
                ret++;

            ret += RootSum(root.left, targetSum - root.val);
            ret += RootSum(root.left, targetSum - root.val);

            return ret;
        }

        /// <summary>
        /// 1448. 统计二叉树中好节点的数目
        /// 根到好节点所经过的节点中，没有任何节点大于好节点： 也即好节点大于或等于路径最大值
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int GoodNodes(TreeNode root)
        {
            return DFS_GoodNodes(root, int.MinValue);
        }

        private int DFS_GoodNodes(TreeNode root, int pathMax)
        {
            if (root == null) return 0;

            int res = 0;
            if (root.val >= pathMax)                            // 
            {
                res++;
                pathMax = root.val;
            }
            res += DFS_GoodNodes(root.left, pathMax) + DFS_GoodNodes(root.right, pathMax);
            return res;
        }
    }// class end
    public static class BSearchRangeL
    {

        /// <summary>
        /// 34. 在排序数组中查找元素的第一个和最后一个位置
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] SearchRange(int[] nums, int target)
        {
            int l = BinarySearch(nums, target, true);
            int r = BinarySearch(nums, target, false);

            int[] ans = new int[2] { -1, -1 };

            if (r != l)                                 // 如果相等说不值不存在
            {
                ans[0] = l;
                ans[1] = r - 1;
            }

            return ans;
        }
        public static int BinarySearch(int[] nums, int target, bool lflag)
        {
            int l = -1, r = nums.Length;
            int mid;
            while (l + 1 < r)
            {
                mid = l + (r - l) / 2;
                if (IsGreen(nums[mid], target, lflag))
                {
                    r = mid;
                }
                else
                {
                    l = mid;
                }
            }
            return r;
        }

        public static bool IsGreen(int val, int x, bool lflag)
        {
            return lflag ? val >= x : val > x;
        }

    }

    public static class BSearchInsertL
    {
        /// <summary>
        /// LCR 068. 搜索插入位置
        /// 35. 搜索插入位置
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int SearchInsert(int[] nums, int target)
        {
            int r = BinarySearch(nums, target);

            if (r == nums.Length) { return nums.Length; }           // r 在nums外
            if (r == 0) return 0;                                   // r 在nums第一个位置

            return r;                                               //  nums[r] >=target 即是插入位置
        }

        private static int BinarySearch(int[] nums, int target)
        {
            int l = -1, r = nums.Length;
            int mid;
            while (l + 1 < r)
            {
                mid = l + (r - l) / 2;
                if (IsGreen(nums[mid], target))
                {
                    r = mid;
                }
                else
                {

                    l = mid;
                }
            }
            return r;
        }

        private static bool IsGreen(int val, int target)
        {
            return val >= target;
        }

    }
    //3,2,0,4
    // s:2 f:0
    // s:0 f:2
    // s:4 f:4

} // namespace end
