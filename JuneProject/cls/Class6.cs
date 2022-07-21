using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuneProject.cls
{
    internal class Class6
    {

        IList<IList<string>> res = new List<IList<string>>();

        /* 输入棋盘边长 n，返回所有合法的放置 */
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
                // 排除不合法选择
                if (!isValid(board, row, col))
                    continue;

                // 做选择
                // board[row][col] = 'Q';
                board[row] = board[row].Substring(0, col) + 'Q' + board[row].Substring(col + 1);
                // 进入下一行决策
                backtrack(board, row + 1);
                // 撤销选择
                // board[row][col] = '.';
                board[row] = board[row].Substring(0, col) + '.' + board[row].Substring(col + 1);
            }
        }

        /* 是否可以在 board[row][col] 放置皇后？*/
        bool isValid(IList<string> board, int row, int col)
        {
            int n = board.Count;
            // 检查列是否有皇后互相冲突
            for (int i = 0; i <= row; i++)
            {
                if (board[i][col] == 'Q')
                {
                    return false;
                }
            }
            // 检查右上方是否有皇后互相冲突
            for (int i = row - 1, j = col + 1;
                    i >= 0 && j < n; i--, j++)
            {
                if (board[i][j] == 'Q')
                {
                    return false;
                }
            }
            // 检查左上方是否有皇后互相冲突
            for (int i = row - 1, j = col - 1;
                     i >= 0 && j >= 0; i--, j--)
            {
                if (board[i][j] == 'Q')
                {
                    return false;
                }
            }
            return true;
        }

        public void testc()
        {
            StringBuilder sb = new StringBuilder();
            sb.ToString().Reverse().ToList().ToString();
            int[][] arr = new int[3][];
            List<int[]> res = new List<int[]>();
            res.ToArray();
            int[] nums = new int[3];
            //var dic =  nums.GroupBy(x => x).ToDictionary<int, int>(x => x, x => x);
            Array.Sort(arr, (x, y) => x[0].CompareTo(y[0]));


            Random r = new Random(); ;
            r.Next();

            LinkedList<int> q = new LinkedList<int>();
            q.Last();
            // q.Remove()
            //List<int> res = new List<int>();
            //res.Contains(nums);
        }

        public int MaxSubArray(int[] nums)
        {
            int n = nums.Length;
            if (n == 0) return 0;
            // 定义dp: dp[i] 代表在数组以i结尾，最大和的值
            int[] dp = new int[n];
            // base Case
            // 第一个元素前面没有子数组
            dp[0] = nums[0];
            // 状态转移方程
            for (int i = 1; i < n; i++)
            {
                // 递推公式： dp[i] from dp[i-1]
                // dp[i] = Max(nums[i],nums[i]+dp[i-1])
                dp[i] = Math.Max(nums[i], nums[i] + dp[i - 1]);
            }
            // 得到nums的最大子数组的和oiiyuh7
            int res = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                res = Math.Max(res, dp[i]);
            }
            return res;
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            int n = nums.Length;
            if (nums.Length <= 1)
                return nums;
            int[] dp = new int[n];
            dp[0] = 1;
            //  dp[1] = dp[0] * nums[0]
            //  dp[2] = dp[1] *  nums[1];

            // 左边乘积
            for (int i = 1; i < n; i++)
            {
                dp[i] = dp[i - 1] * nums[i - 1];
            }

            int tmp = 1;
            // dp[n-2] = dp[n-2] * nums[n-1]
            // dp[n-3] = dp[n-3] * nums[n-2] *nums[n-1]
            for (int i = n - 2; i >= 0; i--)
            {
                tmp = tmp * nums[i + 1];
                dp[i] = dp[i] * tmp;
            }
            return dp;
        }


        public int[] TwoSum(int[] nums, int target)
        {
            int n = nums.Length;

            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], i);
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (map.ContainsKey((target - nums[i])) && i != map[target - nums[i]])
                {
                    return new int[] { i, map[target - nums[i]] };
                }
            }

            throw new Exception("no right answer!");
        }

        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int len = nums1.Length - 1;
            m--;
            n--;
            while (n >= 0)
            {
                if (m >= 0 && nums1[m] > nums2[n])
                {
                    // 进行数组内部的转移
                    nums1[len--] = nums1[m--];
                }
                else
                {
                    // 外部数组赋值到num1
                    nums1[len--] = nums2[n--];
                }
            }
        }

        public void SortColors(int[] nums)
        {
            int n = nums.Length;
            if (n == 0) return;
            int start = 0, end = n - 1;
            while (start < end)
            {
                if (nums[start] > nums[end])
                {
                    swap(nums, start, end);
                    end--;
                }
                start++;
            }
        }

        public void swap(int[] nums, int a, int b)
        {
            int temp = nums[a];
            nums[a] = nums[b];
            nums[b] = temp;
        }
    }

    // BST的序列化与反序列化
    public class CodecBST
    {
        // 分隔符，区分每个节点的值
        private readonly static string SEP = ",";

        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            serialize(root, sb);
            return sb.ToString();
        }

        private void serialize(TreeNode root, StringBuilder sb)
        {
            if (root == null)
                return;

            // 前序遍历位置进行序列化
            sb.Append(root.val).Append(SEP);
            serialize(root.left, sb);
            serialize(root.right, sb);
        }

        // Decodes your encoded data to tree.
        public TreeNode? deserialize(string data)
        {
            if (data.Length == 0) return null;

            // 转化成前序遍历结果
            Queue<int> preorder = new Queue<int>();
            foreach (var s in data.Split(SEP))
            {
                if (int.TryParse(s, out int val))
                {
                    preorder.Enqueue(int.Parse(s));
                }
            }
            return deserialize(preorder, int.MinValue, int.MaxValue);
        }

        // 定义：将 nodes 中值在闭区间 [min, max] 的节点构造成一棵 BST
        private TreeNode? deserialize(Queue<int> nodes, int min, int max)
        {
            if (nodes.Count == 0) return null;

            // 前序遍历位置进行反序列化
            // 前序遍历结果第一个节点是根节点
            int rootVal = nodes.Peek();
            // 如果大于max时，返回null；这时表示换到右节点
            if (rootVal > max)
                return null;
            // 如果小于min时，返回null；这时表示换到右节点
            if (rootVal < min)
                return null;
            nodes.Dequeue();

            // 生成 root 节点
            TreeNode root = new TreeNode(rootVal);
            // 构建左右子树
            // BST 左子树都比根节点小，右子树都比根节点大
            root.left = deserialize(nodes, min, rootVal);
            root.right = deserialize(nodes, rootVal, max);

            return root;
        }
    }

    /**
     * Definition for a binary tree node.
     * public class TreeNode {
     *     public int val;
     *     public TreeNode left;
     *     public TreeNode right;
     *     public TreeNode(int x) { val = x; }
     * }
     */

    // 普通二叉树的序列化和反序列化 (serialize/ deserialize)
    public class CodecCommonTree
    {
        string SEP = ",";
        string NULL = "#";
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            serialize(root, sb);
            return sb.ToString();
        }

        void serialize(TreeNode root, StringBuilder sb)
        {
            if (root == null)
            {
                sb.Append(NULL).Append(SEP);
                return;
            }

            sb.Append(root.val).Append(SEP);

            serialize(root.left, sb);
            serialize(root.right, sb);
        }

        // Decodes your encoded data to tree.
        public TreeNode? deserialize(string data)
        {
            Queue<string> nodes = new Queue<string>();
            foreach (string s in data.Split(SEP))
            {
                nodes.Enqueue(s);
            }
            return deserialize(nodes);
        }

        TreeNode? deserialize(Queue<string> nodes)
        {
            // base Case 
            if (nodes.Count == 0)
                return null;

            // logic 
            string first = nodes.First();
            nodes.Dequeue();
            // 需要换到另外一个结点
            if (first == NULL)
                return null;

            TreeNode root = new TreeNode(int.Parse(first));

            root.left = deserialize(nodes);
            root.right = deserialize(nodes);

            return root;
        }
    }

}
