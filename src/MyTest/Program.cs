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

        #region day3
        // BFS 算法解题
        // 111. 二叉树的最小深度
        public int MinDepth(TreeNode root)
        {
            if (root == null)
                return 0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            // root 本身就是一层, depth 初始化为1
            int depth = 1;

            while (q.Count != 0)
            {
                int sz = q.Count;
                /* 将当前队列中的所有结点向四周扩散 */
                for (int i = 0; i < sz; i++)
                {
                    TreeNode cur = q.Dequeue();
                    /* 判断是否达到终点 */
                    if (cur.left == null && cur.right == null)
                    {
                        return depth;
                    }
                    /* 将 cur 的相邻结点加入队列 */
                    if (cur.left != null)
                        q.Enqueue(cur.left);
                    if (cur.right != null)
                        q.Enqueue(cur.right);
                }
                /* 这里增加深度 */
                depth++;
            }
            return depth;
        }

        // 752. 打开转盘锁
        // 将 s[j] 向上拨动一次
        string PlusOne(string s, int j)
        {
            char[] ch = s.ToCharArray();
            if (ch[j] == '9')
                ch[j] = '0';
            else
                ch[j] = (char)(ch[j] + 1);
            return new string(ch);
        }
        // 将 s[j] 向下拨动一次
        string minusOne(string s, int j)
        {
            char[] ch = s.ToCharArray();
            if (ch[j] == '0')
                ch[j] = '9';
            else
                ch[j] = (char)(ch[j] - 1);
            return new string(ch);
        }
        // BFS 框架， 打印出所有可能的密码
        void BFS(string target)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue("0000");

            while (q.Count != 0)
            {
                int sz = q.Count;
                /* 将当前队列中的所有结点向周围扩散 */
                for (int i = 0; i < sz; i++)
                {
                    string cur = q.Dequeue();

                    /* 判断是否到达终点 */
                    Console.WriteLine(cur);

                    /* 将一个结点的相邻结点加入队列 */
                    for (int j = 0; j < 4; j++)
                    {
                        string up = PlusOne(cur, j);
                        string down = minusOne(cur, j);
                        q.Enqueue(up);
                        q.Enqueue(down);
                    }
                }
                /* 在这里增加步数 */
            }
        }
        int openLock(string[] deadends, string target)
        {
            // 记录需要跳过的死亡密码
            HashSet<string> deads = new HashSet<string>();
            foreach (var s in deadends)
            {
                deads.Add(s);
            }
            // 记录已经穷举过的密码，防止走回头路
            HashSet<string> visited = new HashSet<string>();
            Queue<string> q = new Queue<string>();
            // 从起点开始启动广度优先搜索
            int step = 0;
            q.Enqueue("0000");
            visited.Add("0000");

            while (q.Count != 0)
            {
                int sz = q.Count;
                /* 将当前队列中的所有结点向周围扩散 */
                for (int i = 0; i < sz; i++)
                {
                    string cur = q.Dequeue();

                    /* 判断是否达到终点 */
                    if (deads.Contains(cur))
                        continue;
                    if (cur.Equals(target))
                        return step;

                    /* 将一个结点的未遍历相邻结点加入队列 */
                    for (int j = 0; j < 4; j++)
                    {
                        string up = PlusOne(cur, j);
                        if (!visited.Contains(up))
                        {
                            q.Enqueue(up);
                            visited.Add(up);
                        }
                        string down = minusOne(cur, j);
                        if (!visited.Contains(down))
                        {
                            q.Enqueue(down);
                            visited.Add(down);
                        }
                    }
                }
                /* 在这里增加步数 */
                step++;
            }
            // 如果穷举完都没有找到目标密码，那就是找不到了
            return -1;
        }


        // 二叉树
        // 迭代遍历数组
        void travers(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {

            }
        }
        // 递归遍历数组
        void traverse(int[] arr, int i)
        {
            if (i == arr.Length)
                return;
            // 前序位置
            traverse(arr, i + 1);
            // 后序位置
        }

        // 迭代遍历单链表
        void traverse(ListNode head)
        {
            for (ListNode p = head; p != null; p = p.next)
            {

            }
        }
        // 递归遍历单链表
        void traverse1(ListNode head)
        {
            if (head != null)
                return;
            // 前序位置
            traverse1(head);
            // 后序位置
        }

        void traverse(TreeNode root)
        {
            if (root == null)
            {
                return;
            }
            // 前序位置
            traverse(root.left);
            // 中序位置
            traverse(root.right);
            // 后序位置
        }



        // 104. 二叉树的最大深度
        // 记录最大深度
        static int res_MaxDepth = 0;
        // 记录遍历到的节点的深度 （使用外部变量获取结果,有回溯那味了）
        static int depth = 0;
        static int maxDepth_traverse(TreeNode root)
        {
            traverse_maxDepth(root);
            return res_MaxDepth;
        }
        private static void traverse_maxDepth(TreeNode root)
        {
            if (root == null)
            {
                // 到达叶子节点，更新最大深度
                res_MaxDepth = Math.Max(res_MaxDepth, depth);
                return;
            }

            // 前序位置
            depth++;
            traverse_maxDepth(root.left);
            traverse_maxDepth(root.right);
            // 后序位置
            depth--;
        }

        // 二叉树遍历框架 (使用返回值获取结果)
        int maxDepth_recur(TreeNode root)
        {
            if (root == null)
                return 0;

            int leftMax = maxDepth_recur(root.left);
            int rightMax = maxDepth_recur(root.right);

            int res = Math.Max(leftMax, rightMax) + 1;
            return res;
        }
        #endregion

        #region day4
        // 定义: 输入一棵二叉树的根节点，返回这棵树的前序遍历结果
        List<int> preorderTraverse(TreeNode root)
        {
            List<int> res = new List<int>();
            if (root == null)
                return res;
            // 前序遍历的结果，root.val 在第一个
            res.Add(root.val);
            // 利用函数定义，后面接着左子树的前序遍历结果
            res.AddRange(preorderTraverse(root.left));
            // 利用函数定义，最后接着柚子树的前序遍历结果
            res.AddRange(preorderTraverse(root.right));
            return res;
        }
        // 二叉树遍历函数
        void traverse_level(TreeNode root, int level)
        {
            if (root == null)
                return;
            // 前序位置
            System.Console.WriteLine($"Node {root} is on level {level}");
            traverse_level(root.left, level + 1);
            traverse_level(root.right, level + 1);
        }
        // 定义: 输入一棵二叉树，返回这颗二叉树的节点总数
        int count(TreeNode root)
        {
            if (root == null)
                return 0;
            int leftCount = count(root.left);
            int rightCount = count(root.right);
            // 后序位置
            WriteLine($"节点{root} 的左子树有{leftCount}个节点，右子树有{rightCount}个节点");

            return leftCount + rightCount + 1;
        }
        // 记录最大直径的长度
        int maxDiameter = 0;

        public int diameterOfBinaryTree(TreeNode root)
        {
            // 对每个节点计算直径，求最大直径
            traverse_diameter(root);
            return maxDiameter;

        }
        // 遍历二叉树
        private void traverse_diameter(TreeNode root)
        {
            if (root == null)
                return;

            // 对每个节点计算直径
            //int leftMax = maxDepth_traverse(root.left);
            //int rightMax = maxDepth_traverse(root.right);
            int leftMax = maxDepth_recur(root.left);
            int rightMax = maxDepth_recur(root.right);
            int myDiameter = leftMax + rightMax;
            // 更新全局最大直径
            maxDiameter = Math.Max(maxDiameter, myDiameter);

            traverse_diameter(root.left);
            traverse_diameter(root.right);
        }

        int maxDepth_postTraverse(TreeNode root)
        {
            if (root == null)
                return 0;

            int leftMax = maxDepth_postTraverse(root.left);
            int rightMax = maxDepth_postTraverse(root.right);
            // 后序位置，顺便计算最大直径
            int myDiameter = leftMax + rightMax;
            maxDiameter = Math.Max(maxDiameter, myDiameter);

            return 1 + Math.Max(leftMax, rightMax);
        }
        // 输入一棵二叉树的根节点，层序遍历这颗二叉树
        void levelTraverse(TreeNode root)
        {
            if (root == null) return;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            // 从上到下遍历二叉树的每一层
            while (q.Count > 0)
            {
                int sz = q.Count;
                for (int i = 0; i < sz; i++)
                {
                    // 从左到右遍历每一层的每个节点
                    TreeNode cur = q.Dequeue();
                    // 将下一层节点放入队列
                    if (cur.left != null)
                        q.Enqueue(cur.left);
                    if (cur.right != null)
                        q.Enqueue(cur.right);
                }
            }

        }
        #endregion

        #region day5
        // 排列、组合、子集问题汇总
        IList<IList<int>> res = new List<IList<int>>();
        Stack<int> track = new Stack<int>();

        public IList<IList<int>> subSets(int[] nums)
        {
            backtrack(nums, 0);
            return res;
        }
        //  回溯算法核心函数，遍历子集问题的回溯树
        private void backtrack(int[] nums, int start)
        {
            // 前序位置，每个节点的值都是一个子集
            res.Add(new List<int>(track));

            // 回溯算法标准框架
            for (int i = start; i < nums.Length; i++)
            {
                // 做选择
                track.Push(nums[i]);
                // 通过 start 参数控制树枝的遍历，避免产生重复的子集
                backtrack(nums, i + 1);
                // 撤销选择
                track.Pop();
            }
        }

        public IList<IList<int>> combine(int n, int k)
        {
            backtrack_combine(1, n, k);
            return res;
        }

        private void backtrack_combine(int start, int n, int k)
        {
            // base case
            if (k == track.Count)
            {
                // 遍历到了第k层，收集当前结点的值
                res.Add(new List<int>(track));
                return;
            }
            for (int i = start; i <= n; i++)
            {
                // 选择
                track.Push(i);
                // 通过 start 参数控制树枝的遍历，避免产生重复的子集
                backtrack_combine(i + 1, n, k);
                // 撤销选择
                track.Pop();
            }

        }

        #endregion

        #region day6
        // 46. 全排列
        IList<IList<int>> res_permute = new List<IList<int>>();
        // 记录回溯算法的递归路径
        Stack<int> track_permute = new Stack<int>();
        // track  中的元素会被标记为 true
        bool[] used;
        // 主函数， 输入一组不重复的数字，返回他们的全排列
        public IList<IList<int>> Permute1(int[] nums)
        {
            used = new bool[nums.Length];
            backtrack_Permute(nums);
            return res;
        }
        // 回溯算法核心函数
        private void backtrack_Permute(int[] nums)
        {
            // base case, 到达叶子结点
            if (track_permute.Count == nums.Length)
            {
                // 收集叶子结点上的值
                res.Add(new List<int>(track_permute));
                return;
            }

            // 回溯算法标准框架
            for (int i = 0; i < nums.Length; i++)
            {
                // 已经存在 track 中的元素， 不能重复选择
                if (used[i])
                    continue;

                // 做选择
                used[i] = true;
                track_permute.Push(nums[i]);
                // 进入下一层回溯树
                backtrack_Permute(nums);
                // 取消选择
                track_permute.Pop();
                used[i] = false;
            }
        }


        /* 仅收集第 k 层的节点值 */
        //// 回溯算法核心函数
        //void backtrack(int[] nums, int k)
        //{
        //    // base case，到达第 k 层，收集节点的值
        //    if (track.size() == k)
        //    {
        //        // 第 k 层节点的值就是大小为 k 的排列
        //        res.add(new LinkedList(track));
        //        return;
        //    }

        //    // 回溯算法标准框架
        //    for (int i = 0; i < nums.length; i++)
        //    {
        //        // ...
        //        backtrack(nums, k);
        //        // ...
        //    }
        //}

        /*  */
        // 90. 子集 II
        IList<IList<int>> res_dup = new List<IList<int>>();
        LinkedList<int> track_dup = new LinkedList<int>();
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            // 先排序， 让相同的元素靠在一起
            Array.Sort(nums);
            backtrack_Dup(nums, 0);
            return res_dup;
        }

        private void backtrack_Dup(int[] nums, int start)
        {
            // 前序位置， 每个节点的值都是一个子集
            res_dup.Add(new List<int>(track_dup));

            for (int i = start; i < nums.Length; i++)
            {
                // 剪枝逻辑，值相同的相邻树枝，只遍历一条
                if (i > start && nums[i] == nums[i - 1])
                {
                    continue;
                }

                track_dup.AddLast(nums[i]);
                backtrack_Dup(nums, i + 1);
                track_dup.RemoveLast();
            }
        }

        // 40. 组合总和 II
        IList<IList<int>> res_CS2 = new List<IList<int>>();
        // 记录回溯路径
        LinkedList<int> track_CS2 = new LinkedList<int>();
        // 记录track中的元素之和
        int trackSum = 0;
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            if (candidates.Length == 0)
                return res_CS2;

            // 先排序，让相同的元素靠在一起
            Array.Sort(candidates);
            backtrack_CS2(candidates, 0, target);
            return res_CS2;
        }
        // 回溯算法主函数
        private void backtrack_CS2(int[] nums, int start, int target)
        {
            // base case， 到达目标和，找到符合条件的组合
            if (trackSum == target)
            {
                res_CS2.Add(new List<int>(track_CS2));
                return;
            }
            // base case, 超过目标和，直接结束
            if (trackSum > target)
                return;

            // 回溯算法标准框架
            for (int i = start; i < nums.Length; i++)
            {
                // 剪枝逻辑，值相同的树枝，只遍历第一条
                if (i > start && nums[i] == nums[i - 1])
                {
                    continue;
                }
                // 做选择
                track_CS2.AddLast(nums[i]);
                trackSum += nums[i];
                // 递归遍历下一层回溯树
                backtrack_CS2(nums, i + 1, target);
                // 撤销选择
                track_CS2.RemoveLast();
                trackSum -= nums[i];
            }
        }
        #endregion

        #region day7
        // 47. 全排列 II
        IList<IList<int>> res_PUnique = new List<IList<int>>();
        LinkedList<int> track_PUnique = new LinkedList<int>();

        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            // 先排序， 让相同的元素靠在一起
            Array.Sort(nums);
            used = new bool[nums.Length];
            backtrack_Punique(nums);
            return res_PUnique;

        }

        private void backtrack_Punique(int[] nums)
        {
            if (track.Count == nums.Length)
            {
                res.Add(new List<int>(track_PUnique));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i])
                {
                    continue;
                }
                // 新添加的剪枝逻辑，固定相同的元素在排列中的相对位置
                if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1])
                {
                    // 如果前面的相邻相等元素没有用过，则跳过
                    continue;
                }
                // 选择 nums[i]
                track_PUnique.AddLast(nums[i]);
                used[i] = true;
                // 递归遍历下一层回溯树
                backtrack_Punique(nums);
                // 撤销选择
                track_PUnique.RemoveLast();
                used[i] = false;
            }
        }
        // 39. 组合总和
        IList<IList<int>> res_cSum = new List<IList<int>>();
        // 记录回溯的路径
        LinkedList<int> track_cSum = new LinkedList<int>();
        // 记录 track中的路径和
        int trackSum_cSum = 0;

        public IList<IList<int>> CombinationSum1(int[] candidates, int target)
        {
            if (candidates.Length == 0)
                return res;
            backtrack_cSum(candidates, 0, target);
            return res_cSum;
        }
        // 回溯算法主函数
        private void backtrack_cSum(int[] nums, int start, int target)
        {
            // base case, 找到目标和，记录结果
            if (trackSum == target)
            {
                res_cSum.Add(new List<int>(track_cSum));
                return;
            }
            // base case, 超过目标和，停止向下遍历
            if (trackSum > target)
                return;

            // 回溯算法标准框架
            for (int i = start; i < nums.Length; i++)
            {
                // 选择 nums[i]
                trackSum_cSum += nums[i];
                track_cSum.AddLast(nums[i]);
                // 递归遍历下一层回溯树
                // 同一元素可重复使用，注意参数
                backtrack_cSum(nums, i, target);
                // 撤销选择 nums[i]
                trackSum_cSum -= nums[i];
                track_cSum.RemoveLast();
            }
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

            day9 _day9 = new day9();
            _day9.minWindow("ADOBECODEBANC", "ABC");

            day10 _day10 = new day10();
            _day10.TwoSum(new int[] { 3, 2, 4 }, 6);
            WriteLine("Hello World!");


            NumArray numArray = new NumArray(new int[] { -2, 0, 3, -5, 2, -1 });
            numArray.SumRange(2, 5);

            NumMatrix numMatrix = new NumMatrix(new int[,] {
            { 3,0,1,4,2},
            { 5,6,3,2,1},
            { 1,2,0,1,5},
            { 4,1,0,1,7},
            { 1,0,3,0,5},
            });
            var res = numMatrix.sumRegion(2, 1, 4, 3);

            int[] p = { 1, 2, 3 };
            testArray(ref p);



            day13 day13 = new day13();
            int[][] matrix = new int[][] {
            new int[] { 1, 2, 3 },
            new int[]{ 4,5,6},
            new int[]{ 7, 8, 9 }
            };
            day13.SpiralOrder(matrix);

            day13.GenerateMatrix(3);

            day14 day14 = new day14();
            TreeNode node1 = new TreeNode(1);
            TreeNode node3 = new TreeNode(3);
            TreeNode node2 = new TreeNode(2, node1, node3);
            TreeNode node6 = new TreeNode(6);
            TreeNode node9 = new TreeNode(9);
            TreeNode node7 = new TreeNode(7, node6, node9);
            TreeNode node4 = new TreeNode(4, node2, node7);
            day14.InvertTree1(node4);

            day14.constructMaximumBinaryTree(new int[] { 3, 2, 1, 6, 0, 5 });

            day16 day16 = new day16();
            day16.GenerateTrees(3);
            // day16.NumTree(3);

            day17 day17 = new day17();
            int[][] graph = new int[4][] {
            new int[]{ 1,2},
            new int[]{ 3},
            new int[]{ 3},
            new int[]{ }
            };
            day17.AllPathsSourceTarget(graph);
        }
        public static void testArray(ref int[] nums)
        {
            int[] tmp = { 4, 5, 6 };
            nums = tmp;
        }

        #region 总览 11天
        // 数组遍历 链表遍历  二叉树遍历  N 叉树的遍历
        // 动态规划解题套路框架 day1

        // 回溯算法 day2

        // BFS 算法解题 day3

        // 二叉树 day3

        // 回溯算法 day4 day5 day6

        // 双指针 day8

        // 二分搜索 day9

        // 滑动窗口 day9

        // 股票买卖 打家劫舍 

        // nSum 问题 day10

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
