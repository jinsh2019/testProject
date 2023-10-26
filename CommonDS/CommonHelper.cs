using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

// Common Data Structure
namespace CDS
{
    public static class CommonHelper
    {
        // 构造链表 1-> 2 -> 3 -> 4
        public static ListNode BuildLinkNode(int[] arr)
        {
            if (arr == null)
                return null;

            ListNode lnode = null;
            for (int i = arr.Length - 1; i >= 0; i--)
                lnode = lnode == null ? new ListNode(arr[i]) : new ListNode(arr[i], lnode);
            return lnode;
        }


        /// <summary>
        /// 构造二维数组
        /// </summary>
        /// <param name="my2DArray">输入多维数组</param>
        /// <returns></returns>
        public static int[,] BuildTo2D(int[][] my2DArray)
        {
            if (my2DArray == null)
                return null;

            int m = my2DArray.GetLength(0);
            int n = my2DArray[0].GetLength(0);
            int[,] ans = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ans[i, j] = my2DArray[i][j];
                }
            }
            return ans;
        }

        public static Node BuildNode(int[] arr)
        {
            if (arr == null)
                return null;

            Node node = null;
            for (int i = arr.Length - 1; i >= 0; i--)
                node = node == null ? new Node(arr[i]) : new Node(arr[i], null, null, null);
            return node;
        }

        /// <summary>
        /// 打印二维数组
        /// </summary>
        /// <param name="board"></param>
        public static void Print2DMatrix(char[][] board)
        {
            int m = board.Length;

            for (int i = 0; i < m; i++)
            {
                int n = board[i].Length;
                for (int j = 0; j < n; j++)
                {
                    Write(board[i][j] + ",");
                }
                WriteLine();
            }
        }

        /// <summary>
        /// 打印二维数组
        /// </summary>
        /// <param name="board"></param>
        public static void Print2DMatrix(int[][] board)
        {
            int m = board.Length;
            for (int i = 0; i < m; i++)
            {
                int n = board[i].Length;
                for (int j = 0; j < n; j++)
                {
                    Write(board[i][j] + ",");
                }
                WriteLine();
            }
        }

        /// <summary>
        /// 108. 将有序数组转换为二叉搜索树(BST的反序列化)
        /// 105,106,108,1008
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            var root = BuildBST(nums, 0, nums.Length - 1);
            return root;
        }

        private static TreeNode BuildBST(int[] nums, int left, int right)
        {
            if (left > right)                                   // Base Case
                return null;

            int mid = left + (right - left) / 2;
            TreeNode root = new TreeNode(nums[mid]);            // 建root 从中间值开始建树结构
            root.left = BuildBST(nums, left, mid - 1);          // 递归建左树， 返回左数root
            root.right = BuildBST(nums, mid + 1, right);        // 递归建右数， 在已知的mid与right，递归建立右树

            return root;
        }

        public static IList<IList<int>> LevelOrder(TreeNode root)
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
    }
}
