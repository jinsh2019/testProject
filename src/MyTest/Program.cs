using CDS;
using System;
using System.Collections.Generic;
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

        // 22. 括号生成
        public IList<string> GenerateParenthesis(int n)
        {
            List<string> res = new List<string>();
            if (n == 0) return res;
            helper(res, "", n, n);

            return res;
        }

        private void helper(List<string> res, string s, int left, int right)
        {
            if (left > right)// 轮到右括号了  (=> () 
            {
                return;
            }
            if (left == 0 && right == 0)
            {
                res.Add(s);
                return;
            }
            if (left > 0)
            {
                helper(res, s + "(", left - 1, right);
            }
            if (right > 0)
            {
                helper(res, s + ")", left, right - 1);
            }
        }

        // 79. 单词搜索
        public bool Exist(char[][] board, string word)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    if (Exist(board, i, j, word, 0))
                        return true;
                }
            }
            return false;
        }

        private bool Exist(char[][] board, int i, int j, string word, int start)
        {
            if (start >= word.Length) return true;
            if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length) return false;
            if (board[i][j] == word[start++])
            {
                char c = board[i][j];
                board[i][j] = '#'; // marked
                bool res = Exist(board, i - 1, j, word, start) ||
                    Exist(board, i + 1, j, word, start) ||
                    Exist(board, i, j - 1, word, start) ||
                    Exist(board, i, j + 1, word, start);

                board[i][j] = c; // backing
                return res;
            }

            return false;
        }
        // house rober
        public int rob(int[] nums)
        {
            if (nums.Length == 0)
                return 0;
            // 子问题
            // f(k) = 偷[0..k)房间中的最大金额

            // f(0) = 0
            // f(1)= nums[0]
            // f(k) = max{rob(k-1), nums[k-1]+ rob(k-2)};
            int N = nums.Length;
            int[] dp = new int[N + 1];
            dp[0] = 0;
            dp[1] = nums[0];

            for (int k = 2; k <= N; k++)
            {
                // 套用子问题的递推关系
                dp[k] = Math.Max(dp[k - 1], nums[k - 1] + dp[k - 2]);
            }

            return dp[N];
        }

        // 213. 打家劫舍 II
        public int rob2(int[] nums)
        {
            int N = nums.Length;
            if (N == 1) return nums[0];

            // 动态规划数组
            int[] f = new int[N + 1];
            int[] g = new int[N + 1];
            // 定义起始位置
            f[1] = nums[0]; // 第一个房间作为起点
            g[2] = nums[1]; // 第二个房子作为起点

            for (int i = 2; i <= N - 1; i++)
            {
                f[i] = Math.Max(f[i - 1], f[i - 2] + nums[i - 1]);
            }

            for (int i = 3; i <= N; i++)
            {
                g[i] = Math.Max(g[i - 1], g[i - 2] + nums[i - 1]);
            }

            return Math.Max(f[N - 1], g[N]);
        }

        // 55. 跳跃游戏
        public bool CanJump(int[] nums)
        {
            // dp[i] 表示i位置可以到达
            bool[] dp = new bool[nums.Length];
            dp[0] = true;

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    // 1. 达到j位置；
                    // 2. nums[j] 的步长 + j位置 如果可以到达i位置，则dp[i]为true
                    if (dp[j] && nums[j] + j >= i)
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }
            return dp[nums.Length - 1];
        }


        public bool CanJump_greedy(int[] nums)
        {
            if (nums.Length == 1)
            {
                return true;
            }

            // 取最大覆盖跨度
            int cover = nums[0];
            for (int i = 1; i <= cover; i++)
            {
                cover = Math.Max(cover, i + nums[i]);
                if (cover >= nums.Length - 1)
                    return true;
            }

            return false;
        }

        // 201. 数字范围按位与
        public int RangeBitwiseAnd(int m, int n)
        {
            //m 要赋值给 i，所以提前判断一下
            if (m == int.MaxValue)
            {
                return m;
            }
            int res = m;
            for (int i = m + 1; i <= n; i++)
            {
                res &= i;
                if (res == 0 || i == int.MaxValue)
                {
                    break;
                }
            }
            return res;

        }
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
    }
}
