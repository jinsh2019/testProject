using CDS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Formats.Asn1;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;

namespace SortTest
{
    internal class Oct
    {
        /// <summary>
        /// 70. 爬楼梯
        /// 1. 设计状态        f[i] 指爬到i阶的方法有多少个
        /// 2. 写出状态转移方程 f[i] 等于 f[i-1] 与 f[i-2]之和个方法
        /// 3. 设定初始状态     爬到0阶的方法有几个 1个,爬到1阶的方法有几个 1个
        /// 4. 执行状态转移     [2...n]
        /// 5. 返回结果         f[n]
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int ClimbStairs(int n)
        {
            int[] f = new int[n + 1];            // 1. 2. 
            f[0] = 1;                           // 3. 
            f[1] = 1;
            for (int i = 2; i <= n; ++i)
            {
                f[i] = f[i - 1] + f[i - 2];     // 4. 
            }

            return f[n];                        // 5. 
        }

        /// <summary>
        /// 509. 斐波那契数
        /// 1. 设计状态                  dp[i] 是指i时的斐波那契额数的数值
        /// 2. 写出状态转移方程           dp[i] = dp[i-1] + dp[i-2]
        /// 3. 设定初始状态               dp[0] = 0, dp[1] = 1
        /// 4. 执行状态转移               [2...n]
        /// 5. 返回最终的解               dp[n]
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Fib(int n)
        {
            // if(n == 0) return 0;
            // if(n == 1) return 1;

            // return Fib(n-1)+ Fib(n-2);

            if (n == 0) return 0;

            int[] dp = new int[n + 1];              // 1
            dp[0] = 0;                              // 3
            dp[1] = 1;
            for (int i = 2; i <= n; i++)            // 4
            {
                dp[i] = dp[i - 1] + dp[i - 2];      // 2
            }

            return dp[n];                           // 5
        }

        /// <summary>
        /// 1137. 第 N 个泰波那契数
        /// 1. 设计状态         dp[i] 指在i的情况下dp[i]的数值是多少
        /// 2. 写出状态转移方程  dp[i] = dp[i - 3] + dp[i - 2] + dp[i - 1];
        /// 3. 设定初始化值
        /// 4. 执行状态转移
        /// 5. 返回最终结果
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Tribonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 1;

            int[] dp = new int[n + 1];                      // 1
            dp[0] = 0;                                      // 3
            dp[1] = 1;
            dp[2] = 1;
            for (int i = 3; i <= n; i++)                    // 4
            {
                dp[i] = dp[i - 3] + dp[i - 2] + dp[i - 1];  // 2
            }

            return dp[n];                                   // 5
        }

        /// <summary>
        /// 2006. 差的绝对值为 K 的数对数目
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int CountKDifference(int[] nums, int k)
        {
            int ans = 0;
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(nums[i] - nums[j]) == k)
                    {
                        ans++;
                    }
                }
            }
            return ans;
        }
        /// <summary>
        /// LCP 01. 猜数字
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public int Game(int[] guess, int[] answer)
        {
            int tmp = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == answer[i])
                {
                    tmp++;
                }
            }
            return tmp;
        }


        /// <summary>
        /// LCP 06. 拿硬币
        /// </summary>
        /// <param name="coins"></param>
        /// <returns></returns>
        public int MinCount(int[] coins)
        {
            int ans = 0;
            for (int i = 0; i < coins.Length; i++)
            {
                if ((coins[i] & 1) == 1)
                {
                    ans += (coins[i] / 2) + 1;
                }
                else
                {
                    ans += coins[i] / 2;
                }
            }
            return ans;
        }

        /// <summary>
        /// LCR 069. 山脉数组的峰顶索引
        /// 求索引
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public int PeakIndexInMountainArray(int[] arr)
        {
            int iMax = -1;
            int max = int.MinValue;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    iMax = i;
                }
            }
            return iMax;
        }

        /// <summary>
        /// 1464. 数组中两元素的最大乘积
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaxProduct(int[] nums)
        {
            int max = nums[0], subMax = nums[1];
            if (max < subMax)
            {
                int x = max;
                max = subMax;
                subMax = x;
            }

            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] > max)
                {
                    subMax = max;
                    max = nums[i];

                }
                else if (nums[i] > subMax)
                {
                    subMax = nums[i];
                }

            }

            return (max - 1) * (subMax - 1);
        }

        /// <summary>
        /// 485. 最大连续 1 的个数
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int FindMaxConsecutiveOnes(int[] nums)
        {
            int pre = 0;
            int res = 0;
            foreach (var num in nums)
            {
                if (num == 0)
                {
                    pre = 0;
                }
                else
                {
                    pre += 1;
                    if (pre > res)
                    {
                        res = pre;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// 2057. 值相等的最小索引
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int SmallestEqual(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if ((i % 10) == nums[i])
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 27. 移除元素
        /// Case: [3,2,2,3]
        /// [3,2,2,3] n=3
        /// [3,2,2,3] n=2
        /// [2,2,3,3] n=1
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int RemoveElement(int[] nums, int val)
        {
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                while (i < n && nums[i] == val)         // 1
                {
                    int x = nums[i];
                    nums[i] = nums[n - 1];
                    nums[n - 1] = x;
                    n--;
                }
            }

            return n;
        }

        /// <summary>
        /// 665. 非递减数列 >=
        /// 2,3,4,5,[6,3],9
        /// 2,3,4,5,[3,3],9  X
        /// 2,3,4,5,[6,9],9  Y
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool CheckPossibility(int[] nums)
        {
            int pair = 0;
            int pos = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] > nums[i + 1])
                {
                    pair++;
                    pos = i;
                }
            }
            if (pair >= 2) { return false; }
            if (pair == 0) { return true; }

            if (pos == 0 || nums[pos - 1] <= nums[pos + 1]) { return true; }            // pos后面的数大于pos前面的数

            if (pos == nums.Length - 2 || nums[pos] <= nums[pos + 2]) { return true; }  // pos后面的数大于pos前面的数

            return false;
        }

        /// <summary>
        /// 2656. K 个元素的最大和
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int MaximizeSum(int[] nums, int k)
        {
            int ans = 0;
            int maxIndex = 0;
            for (int i = 1; i < nums.Length; i++)
                if (nums[i] > nums[maxIndex]) maxIndex = i;

            while (k-- != 0) ans += nums[maxIndex]++;

            return ans;
        }

        /// <summary>
        /// 2535. 数组元素和与数字和的绝对差
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int DifferenceOfSum(int[] nums)
        {

            int sum1 = 0;
            int sum2 = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum1 += nums[i];

                int temp = nums[i];
                while (temp > 0)
                {
                    sum2 += temp % 10;
                    temp /= 10;
                }
            }
            return Math.Abs(sum1 - sum2);
        }

        /// <summary>
        /// 26. 删除有序数组中的重复项
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int RemoveDuplicates(int[] nums)
        {
            int n = nums.Length;
            int fast = 1;
            int slow = 0;
            while (fast < n)
            {
                if (nums[fast] != nums[slow])
                {
                    nums[slow + 1] = nums[fast];
                    slow++;
                }
                fast++;
            }
            return slow + 1;
        }
        /// <summary>
        /// LCR 122. 路径加密
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string PathEncryption(string path)
        {
            return path.Replace(".", " ");
        }

        /// <summary>
        /// 1876. 长度为三且各字符不同的子字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int CountGoodSubstrings(string s)
        {
            int ans = 0;
            for (int i = 2; i < s.Length; i++)
            {
                if (s[i] != s[i - 1] && s[i - 1] != s[i - 2] && s[i - 2] != s[i])
                {
                    ans++;
                }
            }

            return ans;
        }

        /// <summary>
        /// 面试题 01.03. URL化
        /// </summary>
        /// <param name="S"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string ReplaceSpaces(string S, int length)
        {
            var result = new char[length * 3];
            var index = 0;
            for (var i = 0; i < length; i++)
            {
                if (S[i] == ' ')
                {
                    result[index] = '%';
                    index++;
                    result[index] = '2';
                    index++;
                    result[index] = '0';
                    index++;
                }
                else
                {
                    result[index] = S[i];
                    index++;
                }
            }

            return new string(result, 0, index);
        }


        /// <summary>
        /// 430. 扁平化多级双向链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public Node Flatten(Node head)
        {
            if (head == null)
                return head;
            FlattenList(head);
            return head;
        }
        /// <summary>
        /// 返回尾节点
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        private Node FlattenList(Node head)
        {
            // tNode指向头节点
            Node tNode = head;
            Node res = null;

            while (tNode != null)
            {
                if (tNode.child != null)
                {
                    // 子链表头和尾
                    Node cHead = tNode.child;
                    Node cTail = FlattenList(cHead);    // 递归

                    // 处理 cTail 节点
                    cTail.next = tNode.next;
                    if (tNode.next != null)
                        tNode.next.prev = cTail;
                    // 拐弯链表与子链头节点
                    tNode.next = cHead;
                    cHead.prev = tNode;
                    tNode.child = null;
                }
                // 找到尾节点
                if (tNode.next == null)
                    res = tNode;
                tNode = tNode.next;
            }
            // 返回尾节点
            return res;
        }

        /// <summary>
        /// 快速排序
        /// 912. 排序数组
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void QuickSort(int[] nums, int start, int end)
        {
            if (start >= end) return;

            int pivot = QuickSortByPivot(nums, start, end);
            QuickSort(nums, start, pivot - 1);
            QuickSort(nums, pivot + 1, end);

        }
        /// <summary>
        /// 按基准划分
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int QuickSortByPivot(int[] nums, int start, int end)
        {
            Random r = new Random();
            int rIndex = r.Next(start, end + 1);
            int t = nums[start];
            nums[start] = nums[rIndex];
            nums[rIndex] = t;

            int pivot = start;
            int j = start + 1;

            for (int i = start + 1; i <= end; i++)
            {
                if (nums[i] <= nums[pivot])
                {
                    int tmp = nums[j];
                    nums[j] = nums[i];
                    nums[i] = tmp;

                    j++;
                }
            }

            int x = nums[pivot];
            nums[pivot] = nums[j - 1];
            nums[j - 1] = x;

            pivot = j - 1;
            return pivot;
        }

        /// <summary>
        /// 归并排序
        /// 912. 排序数组
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void MergeSort(int[] nums, int start, int end)
        {
            if (start == end) return;

            int mid = (start + end) / 2;
            MergeSort(nums, start, mid);
            MergeSort(nums, mid + 1, end);

            Merge(nums, start, mid, end);
        }

        /// <summary>
        /// 合并两个有序数列[start..mid]与[mid+1, end]
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="start"></param>
        /// <param name="mid"></param>
        /// <param name="end"></param>
        private void Merge(int[] nums, int start, int mid, int end)
        {
            // 申请一个临时数组,确定排序范围
            int[] tmp = new int[end - start + 1];
            int p = 0;
            int l = start;
            int r = mid + 1;

            while (l <= mid && r <= end)
            {
                if (nums[l] <= nums[r])
                {
                    tmp[p] = nums[l];
                    p++;
                    l++;
                }
                else
                {
                    tmp[p] = nums[r];
                    p++;
                    r++;
                }
            }
            // 处理剩余部分
            while (l <= mid)
            {
                tmp[p] = nums[l];
                p++;
                l++;
            }
            while (r <= end)
            {
                tmp[p] = nums[r];
                p++;
                r++;
            }
            // 将临时数组的元素赋值到原数组中
            for (int i = start; i < end + 1; i++)
            {
                nums[i] = tmp[i - start];
            }
        }

        /// <summary>
        /// 36. 有效的数独
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsValidSudoku(char[][] board)
        {
            // 对第i行(列)第index个数字计数            
            int[,] rows = new int[9, 9];
            int[,] cols = new int[9, 9];
            // [0,0,0..8] [0,1,0..8] [0,2,0..8] 
            // [1,0,0..8] [1,1,0..8] [1,2,0..8]
            // [2,0,0..8] [2,1,0..8] [2,2,0..8]
            // 统计任意小cube内的数字个数
            int[,,] subboxes = new int[3, 3, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char c = board[i][j];
                    if (c != '.')
                    {
                        int index = c - '0' - 1;
                        rows[i, index]++;
                        cols[j, index]++;
                        subboxes[i / 3, j / 3, index]++;
                        if (rows[i, index] > 1 || cols[j, index] > 1 || subboxes[i / 3, j / 3, index] > 1)
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 37. 解数独
        /// </summary>
        /// <param name="board"></param>
        public void SolveSudoku(char[][] board)
        {
            backtrack(board, 0, 0);
        }
        private bool backtrack(char[][] board, int row, int col)
        {
            int m = 9, n = 9;
            // 列越界
            if (col == n)
                return backtrack(board, row + 1, 0);

            // 行越界，找到有效解
            if (row == m)
                return true;

            // 非空，递归下一列
            if (board[row][col] != '.')
                return backtrack(board, row, col + 1);

            // 
            for (char ch = '1'; ch <= '9'; ch++)
            {
                if (!IsValid(board, row, col, ch))
                    continue;

                board[row][col] = ch;

                if (backtrack(board, row, col + 1))
                    return true;

                board[row][col] = '.';
            }

            return false;
        }

        private bool IsValid(char[][] board, int r, int c, char n)
        {
            for (int i = 0; i < 9; i++)
            {
                // 所在行有n
                if (board[r][i] == n) return false;
                // 所在列有n
                if (board[i][c] == n) return false;
                // 所在cube有n
                int startRow = (r / 3) * 3;
                int startCol = (c / 3) * 3;
                if (board[startRow + i / 3][startCol + i % 3] == n)
                    return false;
                // cube 中[0..8][0..8]
                //  i [0..3]   i: [3-5]
                // [0][0..2]   [1][]
                // [1][0..2]
                // [2][0..2]
            }
            return true;
        }



        IList<IList<string>> res = new List<IList<string>>();

        /// <summary>
        /// 51. N 皇后
        /// 52. N 皇后 II
        /// 输入棋盘边长 n，返回所有合法的放置
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IList<IList<string>> SolveNQueens(int n)
        {
            // '.' 表示空，'Q' 表示皇后，初始化空棋盘。
            IList<string> board = new List<string>();
            for (int i = 0; i < n; i++)
            {
                board.Add(new string('.', n));
            }
            backtrack(board, 0);
            return res;
        }
        // 路径：board 中小于 row 的那些行都已经成功放置了皇后
        // 选择列表：第 row 行的所有列都是放置皇后的选择
        // 结束条件：row 超过 board 的最后一行
        void backtrack(IList<string> board, int row)
        {
            // 触发结束条件
            if (row == board.Count)
            {
                res.Add(new List<string>(board));
                return;
            }

            for (int col = 0; col < board[row].Length; col++)
            {
                if (!isValid(board, row, col)) continue;

                board[row] = board[row].Substring(0, col) + 'Q' + board[row].Substring(col + 1);
                backtrack(board, row + 1);
                board[row] = board[row].Substring(0, col) + '.' + board[row].Substring(col + 1);
            }
        }

        bool isValid(IList<string> board, int row, int col)
        {
            int n = board.Count;
            // 检查正上
            for (int i = 0; i <= row; i++)
                if (board[i][col] == 'Q') return false;
            // 检查右上
            for (int i = row - 1, j = col + 1;
                    i >= 0 && j < n; i--, j++)
            {
                if (board[i][j] == 'Q') return false;

            }
            // 检查左上
            for (int i = row - 1, j = col - 1;
                     i >= 0 && j >= 0; i--, j--)
            {
                if (board[i][j] == 'Q') return false;
            }

            return true;
        }

        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int[] ans = null;
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], i);
                }
                else
                {
                    dic[nums[i]] = i;
                }
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(target - nums[i]) && dic[target - nums[i]] != i)
                {
                    ans = new int[2];
                    ans[0] = i;
                    ans[1] = dic[target - nums[i]];
                }
            }

            return ans;
        }

        public List<int[]> TwoSumWithoutDuplicateAns(int[] nums, int target)
        {
            Array.Sort(nums);
            List<int[]> res = new List<int[]>();
            int l = 0, r = nums.Length - 1;
            while (l < r)
            {
                if (nums[l] + nums[r] > target)
                {
                    while (l < r && nums[r] == nums[r - 1]) r--;
                    r--;
                }
                else if (nums[l] + nums[r] < target)
                {
                    while (l < r && nums[l] == nums[l + 1]) l++;
                    l++;
                }
                else
                {
                    while (l < r && nums[l] == nums[l + 1])
                        l++;
                    while (l < r && nums[r] == nums[r - 1])
                        r--;
                    res.Add(new int[] { nums[l], nums[r] });
                    l++;
                    r--;
                }
            }

            return res;
        }

        public IList<int> GetDuplicateNums(int[] nums)
        {
            Console.WriteLine("RemoveDuplicate");
            nums = new int[] { 1, 1, 1, 5, 5, 7, 8, 6, 6, 4, 4, 4 };
            int l = 0, r = nums.Length - 1;
            List<int> res = new List<int>();
            while (l < r)
            {
                if (l < r && nums[l] == nums[l + 1])
                {
                    res.Add(nums[l]);
                    while (nums[l] == nums[l + 1]) l++;

                }
                if (l < r && nums[r] == nums[r - 1])
                {
                    res.Add(nums[r]);
                    while (nums[r] == nums[r - 1]) r--;
                }
                l++;
                r--;

            }

            return res;
        }

        public IList<int> RemoveDuplicateNums(int[] nums)
        {

            Console.WriteLine("RemoveDuplicateNums");
            nums = new int[] { 1, 1, 1, 5, 5, 7, 8, 6, 6, 4, 4, 4 };
            int i = 0, j = -1;
            while (j < nums.Length - 1)
            {
                j++;
                if (nums[i] == nums[j])
                    continue;
                else
                {
                    nums[i + 1] = nums[j];
                    i++;
                }
            }
            int[] ans = new int[i + 1];
            for (int k = 0; k < i + 1; k++)
            {
                ans[k] = nums[k];
            }

            return ans;
        }

        /// <summary>
        /// 15. 三数之和
        /// LCR 007. 三数之和
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            IList<IList<int>> res = new List<IList<int>>();

            // 留 nums.Length -1 给i, i = k+1
            for (int k = 0; k < nums.Length - 2; k++)
            {
                if (k > 0 && nums[k] == nums[k - 1])
                    continue;
                int i = k + 1;
                int j = nums.Length - 1;
                while (i < j)
                {
                    int sum = nums[k] + nums[i] + nums[j];
                    if (sum < 0)
                    {
                        while (i < j && nums[i] == nums[++i]) ;
                    }
                    else if (sum > 0)
                    {
                        while (i < j && nums[j] == nums[--j]) ;
                    }
                    else
                    {
                        res.Add(new List<int>() { nums[k], nums[i], nums[j] });
                        while (i < j && nums[i] == nums[++i]) ;
                        while (i < j && nums[j] == nums[--j]) ;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// 面试题 02.02. 返回倒数第 k 个节点
        /// [1,2,3,4,5]
        ///      s     f 
        /// </summary>
        /// <param name="head"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int KthToLast(ListNode head, int k)
        {
            ListNode slow = head, fast = head;
            for (int i = 0; i < k; i++)
            {
                fast = fast.next;
                if (fast == null)
                {

                }
            }
            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next;
            }

            return slow.val;
        }
        /// <summary>
        /// 19. 删除链表的倒数第 N 个结点
        /// </summary>
        /// <param name="head"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode slow = head, fast = head;
            for (int i = 0; i < n; i++)
            {
                fast = fast.next;
                // [1,2] 2 删除倒数第2个，返回倒数一个
                // [1] 1   删除倒数第1个，返回head.next null
                // [1,2] 4 删除倒数第3个，因为没有，head.next
                if (fast == null)
                    return head.next;
            }
            // 倒数n-1个节点
            fast = fast.next;
            while (fast != null)
            {
                fast = fast.next;
                slow = slow.next;
            }
            // 删除第n个节点
            slow.next = slow.next.next;
            return head;
        }

        /// <summary>
        /// 206. 反转链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode ReverseList(ListNode head)
        {
            ListNode newHead = null;
            DoReverse(head, ref newHead);

            return newHead;
        }

        /// <summary>
        /// DoReverse
        /// </summary>
        /// <param name="head">老头节点</param>
        /// <param name="newHead">新的头节点</param>
        /// <returns>返回尾节点</returns>
        /// <exception cref="NotImplementedException"></exception>
        private ListNode DoReverse(ListNode head, ref ListNode newHead)
        {
            // []
            if (head == null)
            {
                newHead = head;
                return head;
            }
            // [1]
            if (head.next == null)
            {
                newHead = head;
                return head;
            }
            // [1,2]
            ListNode tail = DoReverse(head.next, ref newHead);
            tail.next = head;
            head.next = null;

            return tail;
        }

        /// <summary>
        /// iteration 迭代加法
        /// recursion 递归加法
        /// </summary>
        /// <param name="n"></param>
        public int dfs_DoAdd(int n)
        {
            // Base Case 
            if (n == 1)
                return 1;

            return n + dfs_DoAdd(n - 1);
        }

        /// <summary>
        /// iteration 递推加法
        /// recursion 递归加法
        /// </summary>
        /// <param name="n"></param>
        public void IterationAdd(int n)
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                sum += i;
            }
            Console.WriteLine(sum);
        }

        /// <summary>
        /// 237. 删除链表中的节点
        /// </summary>
        /// <param name="node"></param>
        public void DeleteNode(ListNode node)
        {
            node.val = node.next.val;
            node.next = node.next.next;
        }

        /// <summary>
        /// 24. 两两交换链表中的节点
        /// 1,2,3,4
        /// 1,2,[4,3]
        /// 2,1,4,3
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode SwapPairs(ListNode head)
        {
            // base case [1],[1,2]
            if (head == null || head.next == null)
                return head;
            // now->nex->nexnex
            ListNode now = head;
            ListNode nex = head.next;
            // 子问题的返回值为 newHead
            ListNode newHead = SwapPairs(nex.next);
            // 两两交换，不影响父问题
            now.next = newHead;
            nex.next = now;
            // 当前栈的nex，是下一层栈的nexnex
            return nex;
        }
        /// <summary>
        /// 逆序打印
        /// </summary>
        /// <param name="str"></param>
        public void PrintReverse(char[] str)
        {
            helper(0, str);
        }

        void helper(int index, char[] str)
        {
            // bottom Case
            if (str == null || index >= str.Length)
                return;

            // 递归子问题
            helper(index + 1, str);
            // post
            Console.Write(str[index]);
        }
        /// <summary>
        /// 151. 反转字符串中的单词
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReverseWords(string s)
        {
            //string[] arr = Regex.Split(s,"\\s+");
            string[] arr = s.Split(" ");
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < arr.Length; i++)
                stack.Push(arr[i]);

            StringBuilder sb = new StringBuilder();
            while (stack.Count > 0)
            {
                string word = stack.Pop();
                if (word == "")
                    continue;
                sb.Append(word).Append(" ");
            }

            return sb.ToString().Trim();
        }
        /// <summary>
        /// 172. 阶乘后的零
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int TrailingZeroes(int n)
        {
            if (n < 5)
                return 0;

            return n / 5 + TrailingZeroes(n / 5);
        }
        /// <summary>
        /// 1342. 将数字变成 0 的操作次数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int NumberOfSteps(int num)
        {
            if (num == 0)
                return 0;

            if (num % 2 == 1)
            {
                // 否则，减去 1
                return NumberOfSteps(num - 1) + 1;
            }
            else
            {
                // 如果当前数字是偶数，你需要把它除以 2
                return NumberOfSteps(num / 2) + 1;
            }
        }
        /// <summary>
        /// 222. 完全二叉树的节点个数
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int CountNodes(TreeNode root)
        {
            if (root == null)
                return 0;
            return CountNodes(root.left) + CountNodes(root.right) + 1;
        }

        /// <summary>
        /// LCP 44. 开幕式焰火
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int NumColor(TreeNode root)
        {
            HashSet<int> set = new HashSet<int>();
            dfs_numColor(root, set);

            return set.Count;
        }

        private void dfs_numColor(TreeNode root, HashSet<int> set)
        {
            if (root == null)
                return;
            set.Add(root.val);
            if (root.left != null)
                dfs_numColor(root.left, set);
            if (root.right != null)
                dfs_numColor(root.right, set);
        }

        /// <summary>
        /// 938. 二叉搜索树的范围和
        /// </summary>
        /// <param name="root"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        int sum = 0;
        public int RangeSumBST(TreeNode root, int low, int high)
        {
            if (root == null) return 0;
            traverse(root, low, high);
            return sum;
        }

        private void traverse(TreeNode root, int low, int high)
        {
            if (root == null) return;

            if (root.val < low)
            {
                traverse(root.right, low, high);
            }
            else if (root.val > high)
            {
                traverse(root.left, low, high);
            }
            else
            {
                sum += root.val;
                traverse(root.left, low, high);
                traverse(root.right, low, high);
            }
        }
        /// <summary>
        /// 397. 整数替换
        /// 记忆化剪枝
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        Dictionary<int, int> memo = new Dictionary<int, int>();
        public int IntegerReplacement(int n)
        {
            if (n == 1)
                return 0;

            if (!memo.ContainsKey(n))
            {
                if (n % 2 == 0)
                {
                    memo.Add(n, 1 + IntegerReplacement(n / 2));
                }
                else
                {
                    memo.Add(n, 2 + Math.Min(IntegerReplacement(n / 2), IntegerReplacement(n / 2 + 1)));
                }
            }

            return memo[n];
        }

        /// <summary>
        /// 797. 所有可能的路径
        /// LCR 110. 所有可能的路径
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            IList<IList<int>> res = new List<IList<int>>();
            IList<int> path = new List<int>();
            dfs_AllPathsSourceTarget(graph, 0, path, res);

            return res;
        }

        private void dfs_AllPathsSourceTarget(int[][] graph, int s, IList<int> path, IList<IList<int>> res)
        {
            path.Add(s);

            int n = graph.Length;
            if (s == n - 1)
            {
                res.Add(new List<int>(path));
            }

            foreach (var v in graph[s])
            {
                dfs_AllPathsSourceTarget(graph, v, path, res);
            }

            path.RemoveAt(path.Count - 1);
        }

        /// <summary>
        /// LCR 123. 图书整理 I
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int[] ReverseBookList(ListNode head)
        {
            Stack<int> stack = new Stack<int>();
            ListNode p = head;
            while (p != null)
            {
                stack.Push(p.val);
                p = p.next;
            }
            List<int> res = new List<int>();
            while (stack.Count != 0)
            {
                res.Add(stack.Pop());
            }

            return res.ToArray();

        }
        /// <summary>
        /// 1614. 括号的最大嵌套深度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int MaxDepth(string s)
        {
            Stack<int> stack = new Stack<int>();
            int ans = 0;
            foreach (var c in s)
            {
                if (c == '(')
                {
                    stack.Push(c);
                    if (stack.Count > ans)
                        ans = stack.Count;
                }
                else if (c == ')')
                {
                    stack.Pop();
                }
            }
            return ans;
        }

        /// <summary>
        /// 234. 回文链表
        /// LCR 027. 回文链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public bool IsPalindrome(ListNode head)
        {
            var p = head;
            var stack = new Stack<int>();
            while (p != null)
            {
                stack.Push(p.val);
                p = p.next;
            }

            var q = head;
            while (q != null)
            {
                if (q.val != stack.Pop())
                    return false;
                q = q.next;
            }
            return true;
        }

        /// <summary>
        /// 71. 简化路径
        /// 分析：需要特殊考虑..和//
        /// 0. 拆分path, 正则表达式为：一个或多个"/"
        /// 1. 遇到..时，需要回退一格：当stack.Count == 0时，不需要回退
        /// 2. 遇到非. 且 非 空字符串，Push到Stack中
        /// 3. 特殊情况处理，如果没有任何stack 需要返回"/"
        /// 4. 一般情况，新弹出路径作为父路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string SimplifyPath(string path)
        {
            string[] arr = Regex.Split(path, "/+");         // 0
            Stack<string> stack = new Stack<string>();
            string ans = "";

            foreach (var s in arr)
            {
                if (s == "..")                              // 1
                {
                    if (stack.Count != 0)
                        stack.Pop();
                }
                else if (s != "." && s != "")               // 2
                {
                    stack.Push(s);
                }
            }

            if (stack.Count == 0)                           // 3
            {
                return "/";
            }

            while (stack.Count != 0)                        // 4
            {
                ans = "/" + stack.Pop() + ans;
            }

            return ans;
        }

        /// <summary>
        /// 20. 有效的括号
        /// 分析：使用栈对字符串逐一分析
        /// 1. ([{ 进栈
        /// 2. 注意判空 对栈操作时需要
        /// 3. 如果时)]}则需要pop出来对应得([{
        /// 4. 注意特殊情况直接给)]}时， 返回false 不合法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsValidB(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (var c in s)
            {
                if (c == '(' || c == '[' || c == '{')                   // 1
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count != 0)                               // 2
                    {
                        if (c == ')' && stack.Peek() != '(')            // 3
                        {
                            return false;
                        }
                        if (c == ']' && stack.Peek() != '[')
                        {
                            return false;
                        }
                        if (c == '}' && stack.Peek() != '{')
                        {
                            return false;
                        }
                        stack.Pop();
                    }
                    else                                               // 4                     
                    {
                        return false;
                    }
                }
            }

            if (stack.Count != 0)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 150. 逆波兰表达式求值s
        /// 分析：遇到运算符，pop出数字进行计算, / pop (13/5), 运算结果压入栈，遇到+ 4+（13/5） 运算结果压入栈 
        /// tokens = ["4","13","5","/","+"]
        /// 4+（13/5） = ？
        /// 1. 遇到运算符,pop出数字进行计算
        /// 2. 根据运算符得出结果
        /// 3. 入栈作为a
        /// 4. 非运算符， 强制转换后入栈
        /// 5. 返回运算结果
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            foreach (var s in tokens)
            {

                if (s == "+")                       // 1
                {
                    int b = stack.Pop();
                    int a = stack.Pop();
                    int res = a + b;                // 2
                    stack.Push(a + b);                // 3
                }
                else if (s == "-")
                {
                    int b = stack.Pop();
                    int a = stack.Pop();
                    stack.Push(a - b);
                }
                else if (s == "*")
                {
                    int b = stack.Pop();
                    int a = stack.Pop();
                    stack.Push(a * b);
                }
                else if (s == "/")
                {
                    int b = stack.Pop();
                    int a = stack.Pop();
                    stack.Push(a / b);
                }
                else
                {
                    stack.Push(int.Parse(s));       // 4
                }
            }

            return stack.Peek();                    // 5
        }

        /// <summary>
        /// 224. 基本计算器
        /// 分析： 
        ///  a. +，- 使用连加模式（num1）+(-num2), 
        ///  b. */   取前一个pre进行运算 pre*num1 pre/num1
        ///  c. 括号，使用递归方式解决子问题后，再进行后续计算
        /// 步骤
        /// 1. 递归，将参数类型换为queue，操作更方便
        /// 2. 存储数字
        /// 3. 符号
        /// 4. 出队，将字符数字转正int类型
        /// 5. 遇到左括号进入递归计算 子问题得res
        /// 6. 获取子问题得res， 进行下一步计算
        /// 7. 根据符号位进行四则运算
        /// 8. 遇到右括号 结束循环，准备计算结果
        /// 9. 计算结果
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int Calculate(string s)
        {

            Queue<char> q = new Queue<char>();
            foreach (var c in s)
            {
                q.Enqueue(c);
            }
            int res = helper(q);
            return res;
        }

        static int helper(Queue<char> queue)                                // 1
        {
            Stack<int> stack = new Stack<int>();                            // 2
            int num = 0;
            char sign = '+';                                                // 3

            while (queue.Count != 0)
            {
                char c = queue.Dequeue();                                   // 4
                if (char.IsDigit(c))
                {
                    num = num * 10 + (c - '0');
                }

                if (c == '(')                                               // 5
                    num = helper(queue);                                    // 6

                if ((!char.IsDigit(c) && c != ' ') || queue.Count == 0)     // 7 
                {
                    int pre = 0;
                    switch (sign)
                    {
                        case '+':
                            stack.Push(num);
                            break;
                        case '-':
                            stack.Push(-num);
                            break;
                        case '*':
                            pre = stack.Peek();
                            stack.Pop();
                            stack.Push(pre * num);
                            break;
                        case '/':
                            pre = stack.Peek();
                            stack.Pop();
                            stack.Push(pre / num);
                            break;
                    }

                    sign = c;
                    num = 0;
                }

                if (c == ')')                                               // 8
                    break;
            }

            int res = 0;
            while (stack.Count != 0)                                        // 9
            {
                res += stack.Peek();
                stack.Pop();
            }
            return res;
        }

        /// <summary>
        /// 36. 有效的数独
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsValidSudoku1(char[][] board)
        {
            int[,] rows = new int[9, 9];
            int[,] cols = new int[9, 9];
            int[,,] cube = new int[3, 3, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char c = board[i][j];
                    if (c != '.')
                    {
                        int index = c - '0' - 1;
                        rows[i, index]++;
                        cols[j, index]++;
                        cube[i / 3, j / 3, index]++;

                        if (rows[i, index] > 1 || cols[j, index] > 1 || cube[i / 3, j / 3, index] > 1)
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 54. 螺旋矩阵
        /// 分析：定义上，下，左，右，边界后，进行枚举
        /// 1. 定上 从左到右
        ///     1.1 上边界++
        ///     1.1 上边界大于下边界 break
        /// 2. 定右right 从上到下
        ///     2.1 右边界--
        ///     2.2 右边界小于左边界 break
        /// 3. 定下down 从右到左
        ///     3.1 下边界--
        ///     3.2 下边界小于上边界 break
        /// 4. 定左left，从下向上
        ///     4.1 左边界++
        ///     4.2 左边界大于右边界 break
        /// 5. 返回结果集
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public IList<int> SpiralOrder(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            int up = 0, down = m - 1;                   // 定义上下边界
            int left = 0, right = n - 1;                // 定义左右边界
            IList<int> res = new List<int>();           // 结果集
            while (true)
            {
                for (int i = left; i <= right; i++)     // 1
                {
                    res.Add(matrix[up][i]);
                }
                up++;                                   // 1.1
                if (up > down) break;                   // 1.2


                for (int i = up; i <= down; i++)        // 2
                {
                    res.Add(matrix[i][right]);
                }
                right--;                                // 2.1
                if (right < left) break;                // 2.2


                for (int i = right; i >= left; i--)     // 3
                {
                    res.Add(matrix[down][i]);
                }
                down--;                                 // 3.1
                if (down < up) break;                   // 3.2


                for (int i = down; i >= up; i++)        // 4
                {
                    res.Add(matrix[i][left]);
                }
                left++;                                 // 4.1
                if (left > right) break;                // 4.2
            }
            return res;                                 // 5
        }

        /// <summary>
        /// 48. 旋转图像
        /// 分析： 先主对角线对调， 然后左右对调
        /// </summary>
        /// <param name="matrix"></param>
        public void Rotate(int[][] matrix)
        {
            int m = matrix.Length;
            for (int i = 0; i < m; i++)
            {
                for (int j = i; j < m; j++)         // 从i开始到m，否则会被覆盖
                {
                    int tmp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = tmp;
                }
            }

            foreach (var row in matrix)
            {
                int l = 0, r = row.Length;
                while (l < r)
                {
                    int tmp = row[l];
                    row[l] = row[r];
                    row[r] = tmp;
                    l++;
                    r--;
                }
            }
        }
        /// <summary>
        /// 73. 矩阵置零
        /// </summary>
        /// <param name="matrix"></param>
        public void SetZeroes(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            bool[] rows = new bool[m];
            bool[] cols = new bool[n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        rows[i] = true;
                        cols[j] = true;
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (rows[i] == true || cols[j] == true)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }
        }
        /// <summary>
        /// 383. 赎金信
        /// 分析：
        /// 1. 定义hashmap , 对 magazine 字符进行计数
        /// 2. ransomNote 对字符做减法，小于0返回false
        /// </summary>
        /// <param name="ransomNote"></param>
        /// <param name="magazine"></param>
        /// <returns></returns>
        public bool CanConstruct(string ransomNote, string magazine)
        {
            char[] hash = new char[26];

            foreach (var c in magazine)
            {
                hash[c - 'a']++;
            }

            foreach (var c in ransomNote)
            {
                if (hash[c - 'a'] > 0)
                {
                    hash[c - 'a']--;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 205. 同构字符串
        /// 分析：
        /// 1. 建立s字符映射到t字符关系
        /// 2. 建立t字符映射到s字符关系
        /// 3. 对字符进行判断，每个x映射y
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, char> s2t = new Dictionary<char, char>();
            Dictionary<char, char> t2s = new Dictionary<char, char>();
            for (int i = 0; i < s.Length; i++)
            {
                char x = s[i], y = t[i];

                // x->y
                if (s2t.ContainsKey(x) && s2t[x] != y)
                {
                    return false;
                }

                if (!s2t.ContainsKey(x))
                {
                    s2t.Add(x, y);
                }
                else
                {
                    s2t[x] = y;
                }


                // y->x
                if (t2s.ContainsKey(y) && t2s[y] != x)
                {
                    return false;
                }

                if (!t2s.ContainsKey(y))
                {
                    t2s.Add(y, x);
                }
                else
                {
                    t2s[y] = x;
                }
            }

            return true;
        }
        /// <summary>
        /// 290. 单词规律
        /// 分析：
        /// 1. 建立pattern字符映射到s字符串关系
        /// 2. 建立s字符串映射到pattern字符关系
        /// 3. 对映射关系逐一进行判断，检查每个x映射到y和y映射到x
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool WordPattern(string pattern, string s)
        {
            string[] arr = Regex.Split(s, @"\s+");
            if (arr.Length != pattern.Length)
                return false;
            Dictionary<char, string> p2s = new Dictionary<char, string>();
            Dictionary<string, char> s2p = new Dictionary<string, char>();
            for (int i = 0; i < arr.Length; i++)
            {
                char x = pattern[i];
                string y = arr[i];
                if (p2s.ContainsKey(x) && p2s[x] != y)
                {
                    return false;
                }
                if (!p2s.ContainsKey(x))
                {
                    p2s.Add(x, y);
                }

                if (s2p.ContainsKey(y) && s2p[y] != x)
                {
                    return false;
                }
                if (!s2p.ContainsKey(y))
                {
                    s2p.Add(y, x);
                }
            }

            return true;
        }
        /// <summary>
        /// 242. 有效的字母异位词
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;

            int[] hash = new int[26];
            foreach (char c in s)
            {
                hash[c - 'a']++;
            }
            foreach (var c in t)
            {
                if (hash[c - 'a'] > 0)
                {
                    hash[c - 'a']--;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 49. 字母异位词分组
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Stack<string> stack1 = new Stack<string>();
            Stack<string> stack2 = new Stack<string>();
            IList<IList<string>> res = new List<IList<string>>();
            foreach (var s in strs)                                 // 初始化stack1
                stack1.Push(s);
            while (stack1.Count != 0)
            {
                IList<string> tmp = new List<string>();
                string x = stack1.Pop();                            // 选取一个x
                tmp.Add(x);

                while (stack1.Count != 0)                           // 直到stack1 == 0
                {
                    string y = stack1.Peek();
                    if (IsAnagram(x, y))                            // 判断是否是异位词
                        tmp.Add(stack1.Pop());
                    else
                        stack2.Push(stack1.Pop());
                }
                res.Add(tmp);                                       // 结束
                while (stack2.Count != 0)
                {
                    stack1.Push(stack2.Pop());
                }
            }

            return res;
        }

        /// <summary>
        /// 1. 两数之和
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum1(int[] nums, int target)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]] = i;
                }
                else
                {
                    dic.Add(nums[i], i);
                }
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(target - nums[i]) && i != dic[target - nums[i]])
                {
                    return new int[] { i, dic[target - nums[i]] };
                }
            }

            return new int[2];
        }

        /// <summary>
        /// 202. 快乐数
        /// 1. 使用HashSet判断是否重复
        /// 2. 求平方和
        /// 3. 平方和等于0返回true
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsHappy(int n)
        {
            HashSet<int> set = new HashSet<int>();

            while (set.Add(n))                          // 1
            {
                int squareSum = 0;                      // 2
                while (n != 0)
                {
                    int remain = n % 10;
                    squareSum += remain * remain;
                    n = n / 10;
                }
                if (squareSum == 1)                    // 3
                {
                    return true;
                }
                else
                {
                    n = squareSum;
                }
            }

            return false;
        }

        /// <summary>
        /// 219. 存在重复元素 II
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], i);
                }
                else
                {
                    if (Math.Abs((dic[nums[i]] - i)) <= k)
                    {
                        return true;
                    }
                    else
                    {
                        dic[nums[i]] = i;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 128. 最长连续序列
        /// 分析，
        /// 1. 使用hashmap进行计数
        /// 2. l-1，base, r+1 判断是否连续 [ l = 0,r= 5 ] [l=99, r=101]
        /// 3. r-l-1算出长度 r-l-1 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int LongestConsecutive(int[] nums)
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)               // 1
            {
                set.Add(nums[i]);
            }
            int ans = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int left = nums[i] - 1, right = nums[i] + 1;    // 2 

                while (set.Contains(left))
                {
                    set.Remove(left);
                    left--;
                }
                while (set.Contains(right))
                {
                    set.Remove(right);
                    right++;
                }

                ans = Math.Max(ans, right - left - 1);          // 3
            }
            return ans;
        }

        /// <summary>
        /// 88. 合并两个有序数组
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="m"></param>
        /// <param name="nums2"></param>
        /// <param name="n"></param>
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int[] tmp = new int[m];
            int i;
            for (i = 0; i < m; i++)
            {
                tmp[i] = nums1[i];
            }

            int p = 0;
            i = 0;
            int j = 0;
            while (i < m && j < n)
            {
                if (tmp[i] < nums2[j])
                {
                    nums1[p] = tmp[i];
                    p++;
                    i++;
                }
                else
                {
                    nums1[p] = nums2[j];
                    p++;
                    j++;
                }
            }

            while (i < m)
            {
                nums1[p] = tmp[i];
                p++;
                i++;
            }
            while (j < n)
            {
                nums1[p] = nums2[j];
                p++;
                j++;
            }
        }
        /// <summary>
        /// 189. 轮转数组
        /// [1,2,3,4,5,6,7]
        ///     i=0+3
        ///        1,2,3,4
        ///  7,6,5      
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        public void Rotate(int[] nums, int k)
        {
            int[] tmp = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                tmp[(i + k) % nums.Length] = nums[i];       // 从(i+k)开始，将i放入(i+k)
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                nums[i] = tmp[i];
            }
        }
        /// <summary>
        /// 121. 买卖股票的最佳时机
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfit(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }
                else if (prices[i] - minPrice > maxProfit)
                {
                    maxProfit = prices[i] - minPrice;
                }
                // else 小于最大价格不进行操作，等待下一个最大价格
            }

            return maxProfit;
        }

        /// <summary>
        /// 80. 删除有序数组中的重复项
        /// 
        /// 1,1,1,2,2,3
        ///     2   3
        ///           c
        ///             i
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int RemoveDuplicates1(int[] nums)
        {
            int count = 2;
            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] != nums[count - 2])
                {
                    nums[count] = nums[i];
                    count++;
                }
            }
            return count;
        }

        // 1,2,2,2,2,3,4,5
        //       3 4 5 c
        //               i



        /// <summary>
        /// 169. 多数元素
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MajorityElement(int[] nums)
        {
            int res = 0;
            int cnt = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (cnt == 0)
                {
                    res = nums[i];
                    cnt++;
                }
                else if (res != nums[i])
                    cnt--;
                else
                    cnt++;
            }
            return res;
        }

        /// <summary>
        /// 55. 跳跃游戏
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool CanJump(int[] nums)
        {
            int fathest = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                fathest = Math.Max(i + nums[i], fathest);
                if (i == fathest)                           // 遍历nums，i == fathest 到不了最后一个下标
                    return false;
            }

            return false;
        }
        /// <summary>
        /// 45. 跳跃游戏 II
        /// 贪心算法
        /// 
        /// 2,3,1,1,4
        ///     c   f
        ///          i    
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Jump(int[] nums)
        {
            int ans = 0;
            int curEnd = 0;
            int nextEnd = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                nextEnd = Math.Max(i + nums[i], nextEnd);   // 不断取下一个最远距离，就是最小跳跃次数
                if (i == curEnd)                            // curEnd等于0，表示第一次跳时 ans++
                {
                    ans++;
                    curEnd = nextEnd;
                }
            }

            return ans;
        }

        /// <summary>
        /// 274. H 指数
        /// 分析： 引用论文次数和发表论文数的关系
        /// 最好时写得多，被引用的也多
        /// 如果每一篇论文引用100次，发表了100篇，证明H指数很高
        /// 少量论文引用次数多，写了很多篇引用次数少的论文的H指数很低
        /// </summary>
        /// <param name="citations"></param>
        /// <returns></returns>
        public int HIndex(int[] citations)
        {
            Array.Sort(citations);
            int i = citations.Length - 1, h = 0;

            while (i >= 0 && citations[i] > h)
            {
                h++;                            // 被引用次数多的收录进去，直到被引用次数小于论文数，或者写的质量低H指数停止增加
                i--;                            // 下一篇
            }

            return h;
        }


        /// <summary>
        ///238. 除自身以外数组的乘积
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int[] ProductExceptSelf(int[] nums)
        {
            int n = nums.Length;
            int[] L = new int[n];
            int[] R = new int[n];

            L[0] = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                L[i] = nums[i - 1] * L[i - 1];
            }

            R[n - 1] = 1;
            for (int i = n - 2; i >= 0; i--)
            {
                R[i] = nums[i + 1] * R[i + 1];
            }

            int[] ans = new int[n];
            for (int i = 0; i < n; i++)
            {
                ans[i] = L[i] * R[i];
            }

            return ans;
        }


        /// <summary>
        /// 134. 加油站
        /// </summary>
        /// <param name="gas"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int totalSum = 0;
            int curSum = 0;
            int start = 0;
            for (int i = 0; i < gas.Length; i++)
            {
                totalSum += gas[i] - cost[i];           // 总收入大于总支出，可以环绕一圈
                curSum += gas[i] - cost[i];
                if (curSum < 0)                         // 从start出发，若入不敷出，出发点start=i+1 换掉
                {
                    start = i + 1;
                    curSum = 0;
                }
            }
            if (totalSum < 0)
                return -1;
            return start;
        }

        /// <summary>
        /// 135. 分发糖果
        /// </summary>
        /// <param name="ratings"></param>
        /// <returns></returns>
        public int Candy(int[] ratings)
        {
            int[] candies = new int[ratings.Length];
            Array.Fill(candies, 1);

            for (int i = 1; i < ratings.Length; i++)
            {
                if (candies[i] > candies[i - 1])
                {
                    candies[i]++;
                }
            }

            for (int i = ratings.Length - 2; i >= 0; i--)
            {
                if (candies[i] > candies[i + 1])
                {
                    candies[i] = Math.Max(candies[i], candies[i + 1] + 1);
                }
            }
            int sum = 0;
            foreach (var candy in candies)
            {
                sum += candy;
            }
            return sum;
        }
        /// <summary>
        /// 58. 最后一个单词的长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLastWord(string s)
        {
            string[] strs = Regex.Split(s.Trim(), @"\s+");
            return strs[strs.Length - 1].Length;
        }
        /// <summary>
        /// 14. 最长公共前缀
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public string LongestCommonPrefix(string[] strs)
        {
            for (int i = 0; i < strs[0].Length; i++)
            {
                char ch = strs[0][i];
                for (int j = 1; j < strs.Length; j++)
                {
                    if (i == strs[j].Length || i == strs[j].Length)
                    {
                        return strs[0].Substring(0, i + 1);
                    }
                }
            }

            return strs[0];
        }

        /// <summary>
        /// 28. 找出字符串中第一个匹配项的下标
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public int StrStr(string haystack, string needle)
        {
            return haystack.IndexOf(needle);
        }

        public double MyPow(double x, int n)
        {
            return Math.Pow(x, n);
        }
        /// <summary>
        /// 263. 丑数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsUgly(int n)
        {
            if (n < 1) return false;

            while (n % 2 == 0)
                n /= 2;
            while (n % 5 == 0)
                n /= 5;
            while (n % 3 == 0)
                n /= 3;

            return n == 1;
        }
        /// <summary>
        /// 264. 丑数 II
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int NthUglyNumber(int n)
        {
            long[] factors = { 2, 3, 5 };
            HashSet<long> set = new HashSet<long>();
            PriorityQueue<long, long> pq = new PriorityQueue<long, long>();
            pq.Enqueue(1, 1);
            long ugly = 0;
            for (int i = 0; i < n; i++)
            {
                long cur = pq.Dequeue();
                ugly = cur;
                // 生成丑数
                foreach (var factor in factors)
                {
                    long next = cur * factor;
                    // 去重
                    if (set.Add(next))
                    {
                        pq.Enqueue(next, next);

                    }
                }
            }

            return (int)ugly;
        }

        /// <summary>
        /// 215. 数组中的第K个最大元素
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int FindKthLargest(int[] nums, int k)
        {
            int[] count = new int[10000 * 2 + 5];
            for (int i = 0; i < nums.Length; i++)
            {
                count[nums[i] + 10000]++;
            }

            for (int i = count.Length - 1; i >= 0; i--)
            {
                while (count[i] > 0)
                {
                    k--;
                    count[i]--;
                    if (k == 0)
                        return i - 10000;
                }
            }

            return -1;
        }

        /// <summary>
        /// 373. 查找和最小的 K 对数字
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
        {
            if (nums1.Length == 0 || nums2.Length == 0 || k == 0)
                return null;

            PriorityQueue<pix, int> pq = new PriorityQueue<pix, int>();
            for (int i = 0; i < nums1.Length; i++)
            {
                for (int j = 0; j < nums2.Length; j++)
                {
                    var sum = nums1[i] + nums2[j];
                    pq.Enqueue(new pix(nums1[i], nums2[j], sum), sum);
                }
            }
            IList<IList<int>> res = new List<IList<int>>();
            while (k != 0 && pq.Count != 0)
            {
                var tPix = pq.Dequeue();
                res.Add(new List<int>() { tPix.x, tPix.y });
                k--;
            }
            return res;
        }
        /// <summary>
        /// 9. 回文数
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;
            int cur = 0;
            int nums = x;
            while (nums != 0)
            {
                cur = cur * 10 + nums % 10; //  翻转正数后进行判断  cur == x
                nums /= 10;
            }
            return cur == x;
        }
        /// <summary>
        /// 66. 加一
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public int[] PlusOne(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (digits[i] == 9)
                {
                    digits[i] = 0;
                }
                else
                {
                    digits[i] += 1;
                    return digits;
                }
            }

            digits = new int[digits.Length + 1];
            digits[0] = 1;
            return digits;
        }


        /// <summary>
        /// 67. 二进制求和
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public String AddBinary(String a, String b)
        {
            StringBuilder sb = new StringBuilder();
            int reminder = 0;                           // 进位
            int i = a.Length - 1;
            int j = b.Length - 1;
            while (i >= 0 || j >= 0)
            {
                int sum = reminder;
                if (i >= 0)
                {
                    sum += a[i] - '0';                  // a[i]
                    i--;
                }
                if (j >= 0)
                {
                    sum += b[j] - '0';                  // b[i]
                    j--;
                }
                sb.Append(sum % 2);                     // (a[i]+b[i])%2
                reminder = sum / 2;                     // reminder
            }
            if (reminder != 0)
                sb.Append(reminder);

            char[] r = sb.ToString().ToCharArray();
            Array.Reverse(r);
            return new string(r);
        }
        /// <summary>
        /// 190. 颠倒二进制位
        /// 与颠倒10进制位一样
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public uint reverseBits(uint n)
        {
            uint ans = 0;
            for (int i = 0; i < 32; i++)
            {
                ans = (ans << 1) | (n & 1);             // ans = ans/2 + a%2
                n >>= 1;
            }

            return ans;
        }

        /// <summary>
        /// 125. 验证回文串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsPalindrome(string s)
        {
            if (s == null || s.Length < 2)
                return true;
            int i = 0, j = s.Length - 1;
            while (i < j)
            {
                while (i < j && !char.IsLetterOrDigit(s[i]))
                {
                    i++;
                }
                while (i < j && !char.IsLetterOrDigit(s[j]))
                {
                    j--;
                }
                if (s[i].ToString().ToLower() != s[j].ToString().ToLower())
                    return false;

                i++;
                j--;
            }

            return true;
        }

        /// <summary>
        /// 11. 盛最多水的容器
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int MaxArea(int[] height)
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
        /// 15. 三数之和
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<IList<int>> ThreeSum1(int[] nums)
        {
            Array.Sort(nums);
            IList<IList<int>> res = new List<IList<int>>();

            // 留 nums.Length -1 给i, i = k+1
            for (int k = 0; k < nums.Length - 2; k++)
            {
                if (k > 0 && nums[k] == nums[k - 1])
                    continue;
                int i = k + 1;
                int j = nums.Length - 1;
                while (i < j)
                {
                    int sum = nums[k] + nums[i] + nums[j];
                    if (sum < 0)
                    {
                        while (i < j && nums[i] == nums[++i]) ;
                    }
                    else if (sum > 0)
                    {
                        while (i < j && nums[j] == nums[--j]) ;
                    }
                    else
                    {
                        res.Add(new List<int>() { nums[k], nums[i], nums[j] });
                        while (i < j && nums[i] == nums[++i]) ;
                        while (i < j && nums[j] == nums[--j]) ;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// 209. 长度最小的子数组
        /// 
        /// 2,3,1,2,4,3
        /// 2 5 6 6 4 7 sum
        /// 1 2 3 3 1 2 min
        ///           j
        ///         i
        /// </summary>
        /// <param name="target"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MinSubArrayLen(int target, int[] nums)
        {
            int i = 0;
            int j = -1;
            int min = int.MaxValue;
            int sum = 0;
            while (j < nums.Length - 1)
            {
                j++;
                sum += nums[i];
                if (sum < target)           //  不停的扩大窗口
                    continue;

                while (sum >= target)       // 缩小窗口
                {
                    if (min > j - i + 1)    // 最小数组大于j-i+1
                        min = j - i + 1;
                    sum -= nums[i];
                    i++;
                }
            }
            return min == int.MaxValue ? 0 : min;
        }

        /// <summary>
        /// 35. 搜索插入位置
        /// 红绿
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int SearchInsert(int[] nums, int target)
        {
            int r = BinarySearch(nums, target);

            if (r == 0)
                return 0;
            if (r == nums.Length)
                return nums.Length;
            return r;
        }
        /// <summary>
        /// 返回的是右边界
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int BinarySearch(int[] nums, int target)
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
        // 比较函数
        public bool IsGreen(int val, int x)
        {
            return val >= x;
        }


        /// <summary>
        /// 141. 环形链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null)
                return false;
            ListNode slow = head, fast = head;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if (slow == fast)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 2. 两数相加
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode p1 = l1;
            ListNode p2 = l2;
            ListNode p = new ListNode(-1);
            ListNode dummp = p;
            int carry = 0;                              // 进位数
            while (p1 != null || p2 != null)
            {
                int sum = carry;
                if (p1 != null)
                {
                    sum += p1.val;
                    p1 = p1.next;
                }
                if (p2 != null)
                {
                    sum += p2.val;
                    p2 = p2.next;
                }

                carry = sum / 10;
                int reminder = sum % 10;                // 余数
                p.next = new ListNode(reminder);
                p = p.next;
            }
            if (carry != 0)
            {
                p.next = new ListNode(carry);
            }

            return dummp.next;
        }

        /// <summary>
        /// 21. 合并两个有序链表
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null && list2 == null)
                return null;
            if (list1 == null || list2 == null)
                return list1 == null ? list2 : list1;
            ListNode p1 = list1;
            ListNode p2 = list2;
            ListNode p = new ListNode(-1);
            ListNode dummy = p;
            while (p1 != null && p2 != null)
            {
                if (p1.val < p2.val)
                {
                    p.next = p1;
                    p1 = p1.next;
                }
                else
                {
                    p.next = p2;
                    p2 = p2.next;
                }
                p = p.next;
            }

            if (p1 != null)
                p.next = p1;
            if (p2 != null)
                p.next = p2;

            return dummy.next;
        }

        /// <summary>
        /// 138. 随机链表的复制
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public Node CopyRandomList(Node head)
        {
            if (head == null)
                return head;

            // node1->n1->node2->n2->node3->n3->null
            for (Node node = head; node != null; node = node.next.next)
            {
                Node nodeNew = new Node(node.val);
                nodeNew.next = node.next;
                node.next = nodeNew;
            }
            // 初始化random指针
            for (Node node = head; node != null; node = node.next.next)
            {
                Node nodeNew = node.next;
                nodeNew.random = (node.random != null) ? node.random.next : null;
            }

            // 分离两个链表
            Node headNew = head.next;
            for (Node node = head; node != null; node = node.next)
            {
                Node nodeNew = node.next;
                node.next = node.next.next;
                nodeNew.next = (nodeNew.next != null) ? nodeNew.next.next : null;
            }

            return headNew;
        }
        // 1,2,3
        //p  n
        // p c
        // p   n
        //   p c
        //       n
        //     p c 
        // 迭代方式来反转
        public ListNode ReverseList2(ListNode head)
        {
            var cur = head;
            ListNode pre = null;
            ListNode nxt = null;
            while (cur != null)
            {
                nxt = cur.next;
                cur.next = pre;

                pre = cur;
                cur = nxt;
            }
            return pre;
        }
        public ListNode ReverseList1(ListNode head)
        {
            ListNode newHead = null;
            DoReverse1(head, ref newHead);

            return newHead;
        }

        private ListNode DoReverse1(ListNode head, ref ListNode newHead)
        {
            // bottom case
            if (head == null || head.next == null)
            {  // 1. ref 是newhead；2. return 是tail节点
                newHead = head;
                return head;
            }
            // 递归入栈
            ListNode tail = DoReverse1(head.next, ref newHead);

            tail.next = head;
            head.next = null;

            return head;
        }


        /// <summary>
        /// 92. 反转链表 II
        /// </summary>
        /// <param name="head"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            if (m == 1)
            {
                return reverseN(head, n);                                 // n-m (2,4)=>(1,3)           
            }

            head.next = ReverseBetween(head.next, m - 1, n - 1);         // 相对位置减1
            return head;

        }


        /// <summary>
        /// 翻转前N个节点
        /// </summary>
        ListNode sucessor = null;
        ListNode reverseN(ListNode head, int n)
        {
            // base case 
            if (n == 1)
            {
                // 记录后继节点
                sucessor = head.next;
                // 返回尾节点
                return head;
            }

            ListNode last = reverseN(head.next, n - 1);
            head.next.next = head;                  // 指针指向的节点的指针指向head
            head.next = sucessor;                   // 不停的指向sucessor
            return last;                            // 返回同一个last

        }

        /// <summary>
        /// 86. 分隔链表
        /// </summary>
        /// <param name="head"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public ListNode Partition(ListNode head, int x)
        {
            // 占位符
            ListNode dummy1 = new ListNode();
            ListNode dummy2 = new ListNode();
            ListNode p1 = dummy1, p2 = dummy2;
            ListNode p = head;
            while (p != null)
            {
                if (p.val < x)
                {
                    p1.next = p;
                    p1 = p1.next;
                }
                else
                {
                    p2.next = p;
                    p2 = p2.next;
                }
                ListNode temp = p.next;
                p.next = null;
                p = temp;
                //p = p.next;
            }
            p1.next = dummy2.next;
            return dummy1.next;
        }

        public bool IsSymmetric(TreeNode root)
        {
            if (root == null) return true;

            return DFSCheck(root.left, root.right);
        }
        private bool DFSCheck(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)
                return true;

            if (left == null || right == null)
                return false;

            if (left.val != right.val)
                return false;

            return DFSCheck(left.left, right.right) && DFSCheck(left.right, right.left);
        }

        /// <summary>
        /// 117. 填充每个节点的下一个右侧节点指针 II
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public Node connect(Node root)
        {
            if (root == null)
                return root;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                //每一层的数量
                int size = queue.Count;
                //前一个节点
                Node prev = null;
                for (int i = 0; i < size; i++)
                {
                    //出队
                    Node node = queue.Dequeue();
                    //如果pre为空就表示node节点是这一行的第一个，
                    //没有前一个节点指向他，否则就让前一个节点指向他
                    if (prev != null)
                    {
                        prev.next = node;
                    }
                    //然后再让当前节点成为前一个节点
                    prev = node;
                    //左右子节点如果不为空就入队
                    if (node.left != null)
                        queue.Enqueue(node.left);
                    if (node.right != null)
                        queue.Enqueue(node.right);
                }
            }
            return root;
        }


        /// <summary>
        /// LCR 053. 二叉搜索树中的中序后继
        /// </summary>
        /// <param name="root"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
        {
            if (root == null) return null;
            // 求每一层迭代的后继节点
            TreeNode successor = null;

            if (root.val > p.val)
            { // 到左边找
                successor = InorderSuccessor(root.left, p);
                if (successor == null) // 右节点为NULL时，返回根节点 [11,9,null,8]
                    successor = root;
            }
            if (root.val < p.val)
            { // 到右边找
                successor = InorderSuccessor(root.right, p);
            }
            if (root.val == p.val)
            { // 找到该节点后，该节点的后继节点是： 右子树最小值
                successor = root.right;
                while (successor != null && successor.left != null)
                {
                    successor = successor.left;
                }
            }
            return successor;
        }

        /// <summary>
        /// 17. 电话号码的字母组合
        /// </summary>
        IList<string> res1 = new List<string>();
        Dictionary<char, string> dic = new Dictionary<char, string>();
        public IList<string> LetterCombinations(string digits)
        {
            if (digits == null || digits.Length == 0)
            {
                return res1;
            }
            dic.Add('2', "abc");
            dic.Add('3', "def");
            dic.Add('4', "ghi");
            dic.Add('5', "jkl");
            dic.Add('6', "mno");
            dic.Add('7', "pqrs");
            dic.Add('8', "tuv");
            dic.Add('9', "wxyz");

            backtrack_LetterCombinations(digits, 0, new StringBuilder());
            return res1;

        }
        private void backtrack_LetterCombinations(string digits, int start, StringBuilder sb)
        {
            if (sb.Length == digits.Length)
            {
                res1.Add(sb.ToString());
                return;
            }

            for (int i = start; i < digits.Length; i++)             // 遍历数字 使用start 控制组合
            {
                char curDig = digits[i];
                char[] curDigStr = dic[curDig].ToCharArray();
                foreach (var c in curDigStr)                        // 遍历数字中的字符         
                {
                    sb.Append(c);
                    Console.WriteLine("starts:" + start + ":" + sb.ToString());

                    backtrack_LetterCombinations(digits, i + 1, sb);// 递归下一个数字
                    sb.Remove(sb.Length - 1, 1);

                }
            }
        }

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
            dfs_GenerateParenthesis(n, n, "", res);
            return res;
        }

        private void dfs_GenerateParenthesis(int l, int r, string path, IList<string> res)
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
                dfs_GenerateParenthesis(l - 1, r, path + "(", res);          // 2
            }
            if (r > 0)
            {
                dfs_GenerateParenthesis(l, r - 1, path + ")", res);           // 3
            }
        }
        /// <summary>
        /// 56. 合并区间
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public int[][] Merge(int[][] intervals)
        {
            List<int[]> res = new List<int[]>();
            Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

            res.Add(intervals[0]);
            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i][0] > res[res.Count - 1][1])
                {   // 开始时间大于最后一组的结束时间，添加区间
                    res.Add(intervals[i]);
                }
                else
                {   // 开始时间小于最后一组时间，进行合并，取结束时间最大的
                    res[res.Count - 1][1] = Math.Max(res[res.Count - 1][1], intervals[i][1]);
                }
            }

            return res.ToArray();
        }

        /// <summary>
        /// 57. 插入区间
        /// </summary>
        /// <param name="intervals"></param>
        /// <param name="newInterval"></param>
        /// <returns></returns>
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {

            List<int[]> total = new List<int[]>();
            foreach (int[] interval in intervals)
            {
                total.Add(interval);
            }
            total.Add(newInterval);
            int[][] intervalsTotal = total.ToArray();
            // 加入 newInterval 后进行排序
            Array.Sort(intervalsTotal, (a, b) => a[0].CompareTo(b[0]));

            List<int[]> res = new List<int[]>();
            for (int i = 0; i < intervalsTotal.Length; i++)
            {
                if (res.Count == 0 || res[res.Count - 1][1] < intervalsTotal[i][0])
                {
                    res.Add(intervalsTotal[i]);
                }
                else
                {
                    res[res.Count - 1][1] = Math.Max(intervalsTotal[i][1], res[res.Count - 1][1]);
                }
            }

            return res.ToArray();
        }

        public int bSearch_left_bound(int[] nums, int target)
        {
            int left = 0, right = nums.Length;
            while (left < right)                 // 终止条件 left == right 
            {
                int mid = left + (right - left) / 2;
                if (target == nums[mid])
                {
                    right = mid;                // 右指针移动， mid给right
                }
                else if (target > nums[mid])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid; // [left,mid)
                }
            }
            //  大于target的个数为n
            if (left >= nums.Length)            // left 到右边界还没有找到target
                return -1;
            return nums[left] == target ? left : -1;

        }
        public int bSearch_right_bound(int[] nums, int target)
        {
            int left = 0, right = nums.Length;

            while (left < right)                        // 终止条件，left == right
            {
                int mid = left + (right - left) / 2;
                if (target == nums[mid])
                {
                    left = mid + 1;                     // 左指针移动, mid+1给left，让left前进一位             
                }
                else if (target > nums[mid])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            // 小于target的个数 < 0 或 大于target的个数>n
            if (left - 1 < 0 || left - 1 >= nums.Length)    // 反过来想，1. 如果在0位置找到了，left=1， 如果left-1=n，说明到右边界也没有找到
                return -1;
            return nums[left - 1] == target ? left - 1 : -1; // 左都是+1的，所以需要left-1来确定target
        }


        /// <summary>
        /// 162. 寻找峰值
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int FindPeakElement(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;             // [left,len-2] 为了下面的判断
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] > nums[mid + 1])      // right去高处：等待left==right
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;                 // left 去高处
                }
            }
            return left;
        }

        /// <summary>
        /// 200. 岛屿数量
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int NumIslands(char[][] grid)
        {
            int res = 0;
            int m = grid.Length, n = grid[0].Length;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        res++;
                        dfs_Fill_Color(grid, i, j);
                    }
                }
            }

            return res;
        }

        private void dfs_Fill_Color(char[][] grid, int i, int j)
        {
            int m = grid.Length, n = grid[0].Length;
            if (i < 0 || i > m || j < 0 || j > n)
            {
                return;
            }
            if (grid[i][j] == '0')
            {
                return;
            }

            grid[i][j] = '0';
            dfs_Fill_Color(grid, i + 1, j);
            dfs_Fill_Color(grid, i - 1, j);
            dfs_Fill_Color(grid, i, j + 1);
            dfs_Fill_Color(grid, i, j - 1);
        }

        /// <summary>
        /// 130. 被围绕的区域
        /// </summary>
        /// <param name="board"></param>
        public void solve(char[][] board)
        {
            if (board == null || board.Length == 0)
                return;
            int m = board.Length, n = board[0].Length;

            for (int i = 0; i < m; i++)
            {
                dfs_Solve(board, i, 0);
                dfs_Solve(board, i, n - 1);

            }

            for (int j = 0; j < n; j++)
            {
                dfs_Solve(board, 0, j);
                dfs_Solve(board, m - 1, j);
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i][j] == 'O')
                    {
                        board[i][j] = 'X';
                    }
                    if (board[i][j] == '-')
                    {
                        board[i][j] = 'O';
                    }
                }
            }
        }

        private void dfs_Solve(char[][] board, int i, int j)
        {
            int m = board.Length, n = board[0].Length;
            if (i < 0 || i >= m || j < 0 || j >= n)
                return;
            if (board[i][j] != 'O')
                return;
            board[i][j] = '-';
            dfs_Solve(board, i - 1, j);
            dfs_Solve(board, i + 1, j);
            dfs_Solve(board, i, j - 1);
            dfs_Solve(board, i, j + 1);
        }

        /// <summary>
        /// 133. 克隆图
        /// </summary>
        HashSet<Node> visited = new HashSet<Node>();
        Dictionary<Node, Node> dicO2C = new Dictionary<Node, Node>();
        public Node CloneGraph(Node node)
        {
            if (node == null)
                return node;
            traverse_diagram(node);
            return dicO2C[node];
        }

        private void traverse_diagram(Node node)
        {
            if (node == null)
                return;

            if (visited.Contains(node))
                return;

            visited.Add(node);

            if (!dicO2C.ContainsKey(node))
                dicO2C[node] = new Node(node.val);    // 在此处clone出来节点

            Node cloneNode = dicO2C[node];            // 拿出节点，准备初始化neighbors
            foreach (var neighbor in node.neighbors)
            {
                traverse_diagram(neighbor);

                Node cloneNeighbor = dicO2C[neighbor];  // 把clone出来的neighbor添加到cloneNode中
                cloneNode.neighbors.Add(cloneNeighbor);
            }
        }
        /// <summary>
        /// 127. 单词接龙
        /// </summary>
        /// <param name="beginWord"></param>
        /// <param name="endWord"></param>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> set = new HashSet<string>(wordList);
            if (set.Contains(beginWord))
            {
                set.Remove(beginWord);
            }

            Queue<string> queue = new Queue<string>();
            int level = 1;
            int curNum = 1;
            int nextNum = 0;
            queue.Enqueue(beginWord);
            while (queue.Count != 0)
            {
                string word = queue.Dequeue();
                curNum--;
                for (int i = 0; i < word.Length; i++)
                {
                    char[] wordUnit = word.ToCharArray();
                    for (char j = 'a'; j <= 'z'; j++)
                    {
                        wordUnit[i] = j;
                        string temp = new string(wordUnit);
                        if (set.Contains(temp))
                        {
                            if (temp == endWord)
                            {
                                return level + 1;
                            }
                        }
                        nextNum++;
                        queue.Enqueue(temp);
                        set.Remove(temp);
                    }
                }
                if (curNum == 0)
                {
                    curNum = nextNum;
                    nextNum = 0;
                    level++; ;
                }
            }

            return level;
        }

        /// <summary>
        /// 98. 验证二叉搜索树
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST(root, null, null);
        }

        private bool IsValidBST(TreeNode root, TreeNode min, TreeNode max)
        {
            if (root == null)                                       // 根节点为null时，返回为true
                return true;
            // Base Case 2
            if (min != null && root.val <= min.val)                 // 递归判断右子树，将父节点root作为最小值
                return false;
            if (max != null && root.val >= max.val)                 // 递归判断左子树，将父节点root作为最大值
                return false;

            return IsValidBST(root.right, root, max) && IsValidBST(root.left, min, root);
        }

        /// <summary>
        /// 310. 最小高度树
        /// 1. 转化成邻接表结构
        /// 2. 找到叶子节点
        /// 3. 不断删除叶子节点，直到剩下的节点数小于等于 2 个 ， 有可能两个节点作为根节点
        /// 4. 删除当前叶子节点， 计算新的叶子叶子节点
        /// 5. 将被删除的叶子节点的邻接节点的度减 1
        /// 6. 剩下的节点就是根节点
        /// </summary>
        /// <param name="n"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        public IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            if (n == 1)
            {
                return new List<int>() { 0 };
            }
            List<int>[] graph = new List<int>[n];                       // 1. 
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            foreach (int[] edge in edges)
            {
                int from = edge[1];
                int to = edge[0];
                graph[from].Add(to);
                graph[to].Add(from);
            }

            List<int> leaves = new List<int>();                         // 2. 
            for (int i = 0; i < n; i++)
            {
                if (graph[i].Count == 1)
                {
                    leaves.Add(i);
                }
            }

            int remainNodeNum = n;
            while (remainNodeNum > 2)                                   // 3. 
            {

                remainNodeNum -= leaves.Count;                          // 4. 
                List<int> newLeaves = new List<int>();
                foreach (var leaf in leaves)
                {
                    int neighbor = graph[leaf][0];                      // 5. 
                    graph[neighbor].Remove(leaf);
                    if (graph[neighbor].Count == 1)
                    {
                        newLeaves.Add(neighbor);
                    }
                }

                leaves.Clear();
                leaves.AddRange(newLeaves);
            }
            return leaves;                                              // 6. 
        }


        /// <summary>
        /// 239. 滑动窗口最大值
        /// 大小为k的滑动窗口内的最大值
        /// 1. 定义单调栈，三个方法 enqueue, dequeue,getmax
        /// 2. 进入k-1个
        /// 3. 进入第k个值
        /// 4. 获取单调栈最大值
        /// 5. 弹出第i-k+1个元素
        /// 6. 使用linkedlist来构建MonotonicQueue
        /// 7. 核心方法：留大数
        /// 8. 因为入队时有可能把最大数remove掉，所以对删除进行判断，已删除就跳过
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            Mq window = new Mq();                           // 1
            IList<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i < k - 1)                              // 2 
                {
                    window.Enqueue(nums[i]);
                }
                else
                {
                    window.Enqueue(nums[i]);                // 3. 
                    res.Add(window.GetMax());               // 4. 
                    window.Dequeue(nums[i - k + 1]);        // 5. 
                }
            }

            return res.ToArray();
        }

        /// <summary>
        /// LCR 183. 望远镜中最高的海拔
        /// </summary>
        /// <param name="heights"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int[] MaxAltitude(int[] nums, int k)
        {
            Mq window = new Mq();                           // 1
            IList<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i < k - 1)                              // 2 
                {
                    window.Enqueue(nums[i]);
                }
                else
                {
                    window.Enqueue(nums[i]);                // 3. 
                    res.Add(window.GetMax());               // 4. 
                    window.Dequeue(nums[i - k + 1]);        // 5. 
                }
            }

            return res.ToArray();
        }

        /// <summary>
        /// 1438. 绝对差不超过限制的最长连续子数组
        /// 1. 定义最大/小单调队列
        /// 2. 最大单调队列的初始化
        /// 3. 业务逻辑，绝对值小于等于limit
        /// 4. 取最大值
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int LongestSubarray(int[] nums, int limit)
        {
            LinkedList<int> maxQueue = new LinkedList<int>();                       //1. 
            LinkedList<int> minQueue = new LinkedList<int>();
            int left = 0, right = 0, ans = 0;
            int n = nums.Length;
            while (right < n)
            {
                while (maxQueue.Count != 0 && maxQueue.Last.Value < nums[right])    // 2. 
                {
                    maxQueue.RemoveLast();
                }

                while (minQueue.Count != 0 && minQueue.Last.Value > nums[right])
                {
                    minQueue.RemoveLast();
                }
                maxQueue.AddLast(nums[right]);
                minQueue.AddLast(nums[right]);

                while (maxQueue.Count != 0 && minQueue.Count != 0 &&                // 3. 
                    Math.Abs(maxQueue.First.Value - minQueue.First.Value) > limit)
                {
                    if (nums[left] == minQueue.First.Value)
                    {
                        minQueue.RemoveFirst();
                    }
                    if (nums[left] == maxQueue.First.Value)
                    {
                        maxQueue.RemoveFirst();
                    }

                    left++;
                }
                ans = Math.Max(ans, right - left + 1);                              // 4. 取最大值
                right++;
            }
            return ans;
        }


        /// <summary>
        /// 99. 恢复二叉搜索树
        /// Case1
        ///    1
        ///  3
        ///    2
        /// 1,2,3
        /// f   s
        /// 
        /// Caes2
        ///   3
        /// 1    5
        ///     4
        ///    2
        /// 1,3,2,4，5
        ///   f s
        ///   
        /// Case3  
        ///   3
        ///  8  1
        /// 2  6 7
        /// 
        /// 2,8,3,6,1,7
        ///   f     s 
        /// </summary>
        TreeNode prev = new TreeNode(int.MinValue);
        TreeNode first, second;
        public void RecoverTree(TreeNode root)
        {
            InorderTraverse(root);
            // 第一个节点与第二个节点进行交换
            int temp = first.val;
            first.val = second.val;
            second.val = temp;
        }

        private void InorderTraverse(TreeNode root)
        {
            if (root == null)
                return;

            InorderTraverse(root.left);
            if (root.val < prev.val)
            {
                if (first == null)          // 记录第一个不健康节点f
                    first = prev;
                second = root;              // 不停更新，找到第二个不健康节点
            }
            prev = root;                    // 不停更新前一个
            InorderTraverse(root.right);
        }

        /// <summary>
        /// 783. 二叉搜索树节点最小距离
        /// 530. 二叉搜索树的最小绝对差
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        TreeNode prev1 = null;
        int ans = int.MaxValue;
        public int MinDiffInBST(TreeNode root)
        {
            traverseBST(root);
            return ans;
        }

        private void traverseBST(TreeNode root)
        {
            if (root == null)
                return;

            traverseBST(root.left);
            if (prev1 != null)
            {
                ans = Math.Min(ans, Math.Abs(root.val - prev1.val));
            }
            prev1 = root;
            traverseBST(root.right);
        }


        /// <summary>
        /// 108. 将有序数组转换为二叉搜索树(BST的反序列化)
        /// 105,106,108,1008
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public TreeNode SortedArrayToBST(int[] nums)
        {
            var ans = BuildBST(nums, 0, nums.Length - 1);
            return ans;
        }

        private TreeNode BuildBST(int[] nums, int left, int right)
        {
            if (left > right)                                   // Base Case
                return null;

            int mid = left + (right - left) / 2;
            TreeNode root = new TreeNode(nums[mid]);            // 建root 从中间值开始建树结构
            root.left = BuildBST(nums, left, mid - 1);          // 递归建左树， 返回左数root
            root.right = BuildBST(nums, mid + 1, right);        // 递归建右数， 在已知的mid与right，递归建立右树

            return root;
        }

        /// <summary>
        /// 1008. 前序遍历构造二叉搜索树(BST的反序列化)
        /// 105,106,108,109,1008
        /// </summary>
        /// <param name="preorder"></param>
        /// <returns></returns>
        public TreeNode BstFromPreorder(int[] preorder)
        {
            return BuildBSTFromPreOrder(preorder, 0, preorder.Length - 1);

        }

        private TreeNode BuildBSTFromPreOrder(int[] preorder, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            int rootVal = preorder[start];
            TreeNode root = new TreeNode(rootVal);

            int p = start + 1;
            while (p <= end && preorder[p] < rootVal)
            {
                p++;
            }

            root.left = BuildBSTFromPreOrder(preorder, start + 1, p - 1);
            root.right = BuildBSTFromPreOrder(preorder, p, end);

            return root;
        }


        /// <summary>
        /// 105. 从前序与中序遍历序列构造二叉树
        /// </summary>
        Dictionary<int, int> valToIndex = new Dictionary<int, int>();
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            for (int i = 0; i < inorder.Length; i++)
            {
                valToIndex.Add(inorder[i], i);
            }

            return BuildTree(preorder, 0, preorder.Length - 1,
                             inorder, 0, inorder.Length - 1);
        }

        private TreeNode BuildTree(int[] preorder, int preStart, int preEnd,
                                    int[] inorder, int inStart, int inEnd)
        {
            if (preStart > preEnd)
            {
                return null;
            }

            int rootVal = preorder[preStart];
            int index = valToIndex[rootVal];
            int leftSize = index - inStart;

            TreeNode root = new TreeNode(rootVal);

            root.left = BuildTree(preorder, preStart + 1, preStart + leftSize,
                                    inorder, inStart, index - 1);
            root.right = BuildTree(preorder, preStart + leftSize + 1, preEnd,
                                    inorder, index + 1, inEnd);

            return root;
        }

        /// <summary>
        /// 701. 二叉搜索树中的插入操作
        /// </summary>
        /// <param name="root"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public TreeNode InsertIntoBST(TreeNode root, int val)
        {
            if (root == null)
                return new TreeNode(val);

            if (root.val < val)
                root.right = InsertIntoBST(root.right, val);
            if (root.val > val)
                root.left = InsertIntoBST(root.left, val);

            return root;
        }

        /// <summary>
        /// 84. 柱状图中最大的矩形
        /// </summary>
        /// <param name="heights"></param>
        /// <returns></returns>
        public int LargestRectangleArea(int[] heights)
        {
            int len = heights.Length;                               // base case
            if (len == 0)
                return 0;
            if (len == 1)
                return heights[0];


            int area = 0;
            Stack<int> stack = new Stack<int>();                    // 单调栈（非递减）
            for (int i = 0; i < len; i++)
            {   // i位置的值小于栈顶元素，准备计算面积
                while (stack.Count != 0 && heights[i] < heights[stack.Peek()])
                {
                    int curHeight = heights[stack.Pop()];
                    // 把相等的元素弹出来
                    while (stack.Count != 0 && heights[stack.Peek()] == curHeight)
                    {
                        stack.Pop();
                    }

                    int curWidth;
                    if (stack.Count == 0)                           // 栈为空，当前宽度是i
                        curWidth = i;
                    else
                        curWidth = i - stack.Peek() - 1;            // 栈非空，设置当前宽度

                    area = Math.Max(area, curWidth * curHeight);
                }
                stack.Push(i);
            }

            while (stack.Count != 0)                                // 栈不为空的情况，最小的那一个
            {
                int curHeight = heights[stack.Pop()];
                while (stack.Count != 0 && heights[stack.Peek()] == curHeight)
                {
                    stack.Pop();
                }
                int curWidth;
                if (stack.Count == 0)                               // 栈为空，当前宽度是len                        
                    curWidth = len;
                else
                    curWidth = len - stack.Peek() - 1;
                area = Math.Max(area, curHeight * curWidth);
            }

            return area;
        }

        /// <summary>
        /// LCR 159. 库存管理 III
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public int[] InventoryManagement(int[] stock, int cnt)
        {
            if (stock == null)
                return null;

            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var item in stock)
            {
                pq.Enqueue(item, item);
            }

            List<int> res = new List<int>();
            for (int i = 0; i < cnt; i++)
            {
                if (pq.Count != 0)
                    res.Add(pq.Dequeue());
            }

            return res.ToArray();
        }


        /// <summary>
        /// 347. 前 K 个高频元素
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int[] TopKFrequent(int[] nums, int k)
        {
            if (nums == null)
                return null;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (var num in nums)
            {
                if (!dic.ContainsKey(num))
                {
                    dic.Add(num, 1);
                }
                else
                {
                    dic[num]++;
                }
            }

            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var item in dic)
            {
                pq.Enqueue(item.Key, item.Value);
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }

            List<int> list = new List<int>();
            while (pq.Count != 0)
            {
                list.Add(pq.Dequeue());
            }

            return list.ToArray();
        }
        /// <summary>
        /// 65. 有效数字
        /// 分析：
        /// 模拟题： 先看看有没有e，然后再判断e左右两边是否合法
        /// 右边必须为正数（只要有小数点就返回false）；左边如果有非法数字则不过
        /// 1. 获取 e/E的位置
        /// 2. 没有e，正数或小数都可以
        /// 3. mustInt true 必须为正数； false 任何都可以
        /// 4. "0e"
        /// 5. 多余一个点或是正数
        /// 6.  非.非数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsNumber(string s)
        {
            int idx = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'e' || s[i] == 'E')                     // 1. 
                {
                    if (idx == -1)
                        idx = i;
                    else
                        return false;
                }
            }
            if (idx == -1)
            {
                return check(s, 0, s.Length - 1, false);            // 2. 
            }
            // 有e，判断idx是有效数，e后面是正数
            return check(s, 0, idx - 1, false) && check(s, idx + 1, s.Length - 1, true);
        }

        bool check(string s, int start, int end, bool mustInt)      // 3. 
        {
            if (start > end)                                        // 4. 
                return false;
            if (s[start] == '+' || s[start] == '-')
                start++;
            bool point = false;
            bool digit = false;
            for (int i = start; i <= end; i++)
            {
                if (s[i] == '.')
                {
                    if (point || mustInt)                           // 5. 
                        return false;
                    else
                        point = true;
                }
                else if (char.IsDigit(s[i]))
                {
                    digit = true;
                }
                else
                {
                    return false;                                   // 6.
                }
            }
            return digit;
        }


        /// <summary>
        /// 918. 环形子数组的最大和
        /// 1. 定义dp含义 dp[i]
        /// 2. 状态转移 dp[i] = max(dp[i-1]+nums[i],nums[i])
        /// 3. 初始状态 dp[0] = nums[0]
        /// 4. 执行状态 1..n-1
        /// 5. 返回值 dp[n-1] 在 n-1 位置的最大值
        /// 
        /// 需要定义一个 maxAns，用来获取dp中的最大值, maxAns == 初始值为nums[0]
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaxSubarraySumCircular(int[] nums)
        {
            int n = nums.Length;
            int[] dpMax = new int[n];
            int[] dpMin = new int[n];
            dpMax[0] = nums[0];                             // dp[i] i 位置的最大值
            dpMin[0] = nums[0];
            int totalSum = nums[0];
            int maxAns = dpMax[0];                          // 用来获取dp中的最大值        
            int minAns = dpMin[0];
            for (int i = 1; i < n; i++)                     // 状态转移
            {
                totalSum += nums[i];
                dpMax[i] = Math.Max(dpMax[i - 1] + nums[i], nums[i]);
                maxAns = Math.Max(maxAns, dpMax[i]);
                dpMin[i] = Math.Min(dpMin[i - 1] + nums[i], nums[i]);
                minAns = Math.Min(minAns, dpMin[i]);
            }
            // 
            // 1. 最大子数组头尾不相连与53题一致；[i..j]
            // 2. 最大子数组头尾相连，则 逆向思维， [0..i   j..n-1] 找到i .. j的最小值 用total减去即可
            return maxAns >= 0 ? Math.Max(maxAns, totalSum - minAns) : maxAns;
        }

        public void testS() { 
            
        }

    }// end class


    /// <summary>
    /// 703. 数据流中的第 K 大元素
    /// </summary>
    public class KthLargest
    {
        private PriorityQueue<int, int> pq;
        private int k;
        public KthLargest(int k, int[] nums)
        {
            pq = new PriorityQueue<int, int>();
            foreach (var item in nums)
            {
                pq.Enqueue(item, item);
                if (pq.Count > k)
                    pq.Dequeue();
            }
            this.k = k;
        }

        public int Add(int val)
        {
            pq.Enqueue(val, val);
            if (pq.Count > k)
            {
                pq.Dequeue();
            }

            return pq.Peek();
        }
    }



    // enqueue, dequeue,getmax
    class Mq
    {
        LinkedList<int> linkedList = new LinkedList<int>();         // 6. 
        public void Enqueue(int val)                                // 7. 
        {

            while (linkedList.Count != 0 && linkedList.Last.Value < val)
            {
                linkedList.RemoveLast();
            }
            linkedList.AddLast(val);
        }

        public void Dequeue(int val)                                // 8.
        {
            if (val == linkedList.First.Value)
                linkedList.RemoveFirst();
        }

        public int GetMax()
        {
            return linkedList.First.Value;

        }
    }
    class pix
    {
        int sum;
        public int x { get; }
        public int y { get; }

        public pix(int x, int y, int sum)
        {
            this.x = x;
            this.y = y;
            this.sum = sum;
        }
    }
} // end namespace
