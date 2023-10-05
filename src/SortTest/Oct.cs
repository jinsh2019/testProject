using CDS;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

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
            if (head == null) return head;
            FlattenList(head);
            return head;
        }

        private Node FlattenList(Node head)
        {
            Node tmp = head;
            Node res = null;
            while (tmp != null)                         // 退出循环是为了，处理最深的层级
            {
                if (tmp.child != null)
                {
                    Node pHead = tmp.child;
                    Node pTail = FlattenList(pHead);

                    // 处理 pTail 节点
                    pTail.next = tmp.next;
                    if (tmp.next != null)
                    {
                        tmp.next.prev = pTail;
                    }
                    // 处理 node.child != null 节点
                    tmp.next = pHead;
                    pHead.prev = tmp;
                    // 处理child
                    tmp.child = null;
                }
                if (tmp.next == null)
                {
                    res = tmp;
                }
                tmp = tmp.next;


            }

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
            if (col == n) return backtrack(board, row + 1, 0);

            // 行越界时，找到有效解
            if (row == m) return true;

            // 非空，递归下一列
            if (board[row][col] != '.')
                return backtrack(board, row, col + 1);

            // 
            for (char ch = '1'; ch <= '9'; ch++)
            {
                if (!IsValid(board, row, col, ch)) continue;

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
                if (board[(r / 3) * 3 + i / 3][(c / 3) * 3 + i % 3] == n)
                    return false;
                // cube 中[0..8][0..8]
            }
            return true;
        }



        IList<IList<string>> res = new List<IList<string>>();

        /// <summary>
        /// 51. N 皇后
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

            int n = board[row].Length;
            for (int col = 0; col < n; col++)
            {
                if (!isValid(board, row, col))
                    continue;
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
            {
                if (board[i][col] == 'Q') return false;
            }
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
        public void RecursionSum(int n)
        {
            Console.WriteLine(DoAdd(n));
        }

        private int DoAdd(int n)
        {
            // Base Case 
            if (n == 1)
                return 1;

            int child = DoAdd(n - 1);
            int cur = n;
            return cur + child;
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
            // 子问题的返回值为 nex.next
            ListNode nexnext = SwapPairs(nex.next);
            // 两两交换，不影响父问题
            now.next = nexnext;
            nex.next = now;
            // 当前栈的nex，是下一层栈的nexnex
            return nex;
        }

        public void PrintReverse(char[] str)
        {
            helper(0, str);
        }

        void helper(int index, char[] str)
        {
            // base Case
            if (str == null || index >= str.Length)
                return;

            // pre
            Console.Write(str[index]);
            // 子问题
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
            {
                stack.Push(arr[i]);
            }

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
    }// end class

} // end namespace
