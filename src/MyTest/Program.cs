using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace MyTest
{
    class Program
    {

        #region 一句话的代码
        // 704. 二分查找
        public static int Search(int[] nums, int target)
        {
            // { -1,0,3,5,9,12}
            int left = 0;
            int right = nums.Length - 1;
            int mid = 0;
            while (left <= right) //  相等 1. 使得 left可以走到最后； 2. 使得right可以走到第一个；3.在中间位置重合，获取target
            {
                mid = (right - left) / 2 + left;
                if (nums[mid] == target)
                    return mid;
                else if (nums[mid] > target)
                    right = mid - 1;
                else
                    left = mid + 1;
            }
            return -1;
        }

        // 1,2,3,4,5 => 2
        // a. left=1, right=5,mid= 3; b. left=1 right=2, mid=1; c left=2, right=2 break;
        // 1,2,3,4,5 => 4
        // a. left=1, right=5,mid= 3; b. left= 4 right 5, mid =
        public static int FirstBadVersion(int n)
        {
            int left = 1;
            int right = n;
            int mid = 0;
            while (left < right)
            {
                mid = (right - left) / 2 + left;
                if (isbad(mid)) // bad versoin is in [left...Mid]
                {
                    right = mid;
                }
                else // bad version is in [mid+1...right]
                {
                    left = mid + 1;
                }
            }
            return left;
        }

        private static bool isbad(int version)
        {
            if (version >= 2) return true;
            return false;
        }

        public static int SearchInsert(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left <= right)
            {
                int mid = (right - left) / 2 + left;
                if (nums[mid] == target)
                {
                    return mid;
                }
                else
                {
                    if (nums[mid] > target)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
            }

            return left;
        }

        public static int[] RunningSum(int[] nums)
        {
            int t = 0;
            for (int i = 0; i < nums.Length; i++)
                nums[i] = (t += nums[i]);
            return nums;
        }

        public int[] BuildArray(int[] nums)
        {
            int n = nums.Length;
            int[] ans = new int[n];
            for (int i = 0; i < n; i++)
            {
                ans[i] = nums[nums[i]];
            }
            return ans;
        }


        public static int[] GetConcatenation(int[] nums)
        {
            int n = nums.Length;
            int[] ans = new int[2 * n];
            for (int i = 0; i < n; i++)
            {
                ans[i] = nums[i];
                ans[n + i] = nums[i];
            }
            return ans;
        }

        public static int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;

        }
        #endregion

        #region 大象编程 链表部分
        //  27. 移除元素
        public int RemoveElement(int[] nums, int val)
        {
            int left = 0;
            for (int right = 0; right < nums.Length; right++)
            {
                if (nums[right] != val)
                {
                    nums[left] = nums[right];
                    left++;
                }
            }
            return left;
        }

        // 链表框架
        public void linkListBasic(ListNode head)
        {
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                // 进行操作， pre 表示前一个结点， curr 表示当前结点
                if (prev == null)
                {
                    // curr 是头结点时的操作
                }
                else
                {
                    // curr 不是头结点时的操作
                }
                prev = curr;
                curr = curr.next;
            }
        }
        // 206. 反转链表
        public static void reverseList(ListNode head)
        {
            // 使用递归的方式也可以做得到
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                ListNode next = curr.next; // next pointer as temp varia
                if (prev == null)
                {
                    curr.next = null;
                }
                else
                {
                    curr.next = prev;
                }

                prev = curr;
                curr = next;
            }
        }
        // 递归方式
        public static ListNode reverseList1(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            ListNode newHead = reverseList1(head.next); // pop the item from 3 to 1
            head.next.next = head;  // make next point to point head
            head.next = null;       // make head point to point null
            return newHead;         // return the listnode that its next is null
        }

        // 92. 反转链表 II
        public static ListNode ReverseBetween(ListNode head, int left, int right)
        {
            ListNode dummyNode = new ListNode(-1);
            dummyNode.next = head;

            ListNode pre = dummyNode;
            for (int i = 0; i < left - 1; i++)
            {
                pre = pre.next; // 指向链表翻转的前驱指针
            }

            ListNode rightNode = pre; // 求出右节点的位置
            for (int i = 0; i < right - left + 1; i++)
            {
                rightNode = rightNode.next;
            }

            ListNode leftNode = pre.next; // 左节点
            ListNode curr = rightNode.next; // 右节点

            pre.next = null; // 断开连接
            rightNode.next = null; // 断开连接

            reverseLinkedList(leftNode); // 翻转链表

            pre.next = rightNode; // 前驱结点接上翻转链表
            leftNode.next = curr; // 翻转链表的尾结点连接上当前结点

            return dummyNode.next;
        }

        private static void reverseLinkedList(ListNode head)
        {
            ListNode pre = null;
            ListNode cur = head;
            while (cur != null)
            {
                ListNode next = cur.next;
                cur.next = pre;
                pre = cur;
                cur = next;
            }
        }

        #endregion

        // 看算法小炒21天
        #region day1
        // 数组遍历 链表遍历  二叉树遍历  N 叉树的遍历
        // 动态规划解题套路框架

        // 124. 二叉树中的最大路径和
        public int MaxPathSum(TreeNode root)
        {
            int[] arr = { int.MinValue };
            MaxPathSum(root, arr);

            return arr[0];
        }

        /// <summary>
        /// 从root出发，更新全局最大值
        /// </summary>
        /// <param name="root">根节点</param>
        /// <param name="arr">全局最大值</param>
        /// <returns>返回：两个方向，二选一，选择最大</returns>
        private int MaxPathSum(TreeNode root, int[] arr)
        {
            if (root == null) return 0;

            int left_max = Math.Max(0, MaxPathSum(root.left, arr));
            int right_max = Math.Max(0, MaxPathSum(root.right, arr));

            arr[0] = Math.Max(root.val + left_max + right_max, arr[0]); // 更新全局最大值

            return Math.Max(left_max, right_max) + root.val;// 两个方向，二选一，选择最大
        }

        // 322. 零钱兑换
        public int CoinChange(int[] coins, int amount)
        {
            // 题目要求的最终结果是dp(amount)
            return dp(coins, amount);
        }
        // 定义： 要凑出金额n,至少要dp(coins,n)个硬币
        private int dp(int[] coins, int amount)
        {
            // base case
            if (amount == 0) return 0;
            if (amount < 0) return -1;

            int res = int.MaxValue;
            foreach (var coin in coins)
            {
                // 计算子问题的结果
                int subProblem = dp(coins, amount - coin);
                // 子问题无解则跳过
                if (subProblem == -1) continue;
                // 在子问题中选择最优，然后加1
                res = Math.Min(res, subProblem + 1);
            }

            return res == int.MaxValue ? -1 : res;
        }
        // withMemo
        int CoinChangeWithMemo(int[] coins, int amount)
        {
            int[] memo = new int[amount + 1];
            // 备忘录初始化一个不会被取到的特殊值，代表还未被计算
            Array.Fill(memo, -666);

            return dpWithMemo(coins, amount, memo);
        }
        // 定义： 要凑出金额m,至少要dp(coins,m)个硬币
        int dpWithMemo(int[] coins, int amount, int[] memo)
        {
            if (amount == 0) return 0;
            if (amount < 0) return -1;
            // 查备忘录，防止重复计算
            if (memo[amount] != -666)
                return memo[amount];

            int res = int.MaxValue;
            foreach (var coin in coins)
            {
                // 计算子问题的结果
                int subProblem = dpWithMemo(coins, amount - coin, memo);
                // 子问题无解则跳过
                if (subProblem == -1) continue;
                // 在子问题中选择最优解，然后加一
                res = Math.Min(res, subProblem + 1);
            }
            // 把计算结果存入备忘录
            memo[amount] = (res == int.MaxValue) ? -1 : res;
            return memo[amount];
        }

        int CoinChangeWithDP(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
            // 数组大小为amount+1, 初始值也为 amount + 1
            Array.Fill(dp, amount + 1);

            // base case
            dp[0] = 0;
            // 外层 for 循环在遍历所有状态的所有取值
            for (int i = 0; i < dp.Length; i++)
            {
                // 内层 for 循环在所有选择的最小值
                foreach (var coin in coins)
                {
                    // 子问题无解，跳过
                    if (i - coin < 0)
                        continue;
                    dp[i] = Math.Min(dp[i], 1 + dp[i - coin]);
                }

            }
            return (dp[amount] == amount + 1) ? -1 : dp[amount];
        }
        #endregion

        #region day2
        // 46. 全排列
        // 回溯算法
        public IList<IList<int>> Permute(int[] nums)
        {
            // 结果
            IList<IList<int>> res = new List<IList<int>>();
            // 记录「路径」
            List<int> track = new List<int>();
            // 「路径」中的元素会被标记为 true，避免重复使用
            bool[] used = new bool[nums.Length];

            backtrack(nums, track, used, res);
            return res;
        }

        // 路径：记录在 path 中
        // 选择列表：nums 中不存在于 path 的那些元素（used[i] 为 false）
        // 结束条件：nums 中的元素全都在 path 中出现
        private void backtrack(int[] nums, List<int> path, bool[] used, IList<IList<int>> res)
        {
            // 触发结束条件
            if (path.Count == nums.Length)
            {
                res.Add(new List<int>(path));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                // 排除不合法的选择
                if (used[i])
                {
                    // nums[i] 已经在track中，跳过
                    continue;
                }
                // 做选择
                path.Add(nums[i]);
                used[i] = true;
                // 进入下一层决策树
                backtrack(nums, path, used, res);
                // 取消选择
                path.RemoveAt(path.Count - 1);
                used[i] = false;
            }
        }

        // 51. N 皇后
        /* 输入棋盘边长 n，返回所有合法的放置 */
        public IList<IList<string>> SolveNQueens(int n)
        {
            IList<IList<string>> res = new List<IList<string>>();
            // '.' 表示空，'Q' 表示皇后，初始化空棋盘。
            List<StringBuilder> board = new List<StringBuilder>(Enumerable.Repeat(new StringBuilder("."), n));
            backtrack_NQueue(board, n, res);
            return res;
        }

        // 路径：board 中小于 row 的那些行都已经成功放置了皇后
        // 选择列表：第 row 行的所有列都是放置皇后的选择
        // 结束条件：row 超过 board 的最后一行
        private void backtrack_NQueue(List<StringBuilder> board, int row, IList<IList<string>> res)
        {
            // 触发结束条件
            if (row == board.Count)
            {
                List<string> path = board.Select(x => x.ToString()).ToList();
                res.Add(path);
                return;
            }

            int n = board[row].Length;
            for (int col = 0; col < n; col++)
            {
                // 排除不合法选择
                if (!IsValid(board, row, col))
                {
                    continue;
                }
                // 做选择
                board[row][col] = 'Q';
                // 进入下一行决策
                backtrack_NQueue(board, row + 1, res);
                // 撤销选择
                board[row][col] = '.';
            }
        }

        /* 是否可以在 board[row][col] 放置皇后？ */
        private bool IsValid(List<StringBuilder> board, int row, int col)
        {
            int n = board.Count;
            // 检查列是否有皇后互相冲突
            for (int i = 0; i < n; i++)
            {
                if (board[i][col] == 'Q')
                    return false;
            }

            // 检查右上方是否有皇后互相冲突
            for (int i = row - 1, j = col + 1;
                i >= 0 && j < n; i--, j++)
            {
                if (board[i][j] == 'Q')
                    return false;
            }
            // 检查左上方是否有皇后互相冲突
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0;
                i--, j--)
            {
                if (board[i][j] == 'Q')
                    return false;
            }

            return true;
        }
        #endregion

        static void Main(string[] args)
        {
            #region 一句话的代码
            int[] t1 = { -1, 0, 3, 5, 9, 12 };// { 5 };//{ -1, 0, 3, 5, 9, 12 };
            FirstBadVersion(5);
            Search(t1, -5);

            int[] t2 = { 1, 3, 5, 6 };
            SearchInsert(t2, 5);
            int[] t3 = { 1, 3, 5, 6 };
            SearchInsert(t3, 2); ;

            int[] t4 = { 1, 3, 5, 6 };
            SearchInsert(t4, 7);

            int[] t5 = { 1 };
            SearchInsert(t5, 0);

            int[] t6 = { 1, 1, 1, 1, 1, 1 };
            RunningSum(t6);

            int[] t7 = { 1, 2, 1 };
            GetConcatenation(t7);

            TreeNode node20 = new TreeNode(20, new TreeNode(15), new TreeNode(7));
            TreeNode root = new TreeNode(3, new TreeNode(9), node20);
            WriteLine("MaxDepth:" + MaxDepth(root));
            #endregion

            reverseList1(CommonHelper.BuildLinkNode(new int[] { 1, 2, 3 }));

            WriteLine("Hello World!");
        }

        #region 总览 11天
        // 数组遍历 链表遍历  二叉树遍历  N 叉树的遍历
        // 动态规划解题套路框架

        // 回溯算法

        // BFS 算法解题

        // 二叉树

        // 回溯算法

        // 双指针

        // 二分搜索

        // 滑动窗口

        // 股票买卖 打家劫舍 

        // nSum 问题

        // 空间复杂度和时间复杂度分析
        #endregion

        #region Chapter 1 5天
        // 链表算法

        // 数组算法

        // 二叉树算法

        // 图

        // 综合
        #endregion

        #region Chapter 2 5天
        // dp 基本技巧

        // 子序列问题

        // 背包问题

        // 综合游戏

        // 贪心问题
        #endregion

        #region Chapter 3 3天
        // 暴力搜索

        // 数学运算

        // 经典面试

        #endregion

        #region Chapter 4 5天
        // 杂项
        #endregion

        #region Review 10天

        #endregion

    }
}
