using CDS;
using System.Collections;

namespace July.daily
{
    internal class day12
    {
        Dictionary<string, int> memo = new Dictionary<string, int>();
        IList<TreeNode> res = new List<TreeNode>();
        // 问题分解
        public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
        {
            traverse(root);
            return res;
        }

        string traverse(TreeNode root)
        {
            if (root == null)
                return "#";

            // 迭代关系
            string left = traverse(root.left);
            string right = traverse(root.right);

            string subTree = left + "," + right + "," + root.val;// #,#,4
            Console.Write(subTree);
            int freq = 0;
            if (memo.ContainsKey(subTree))
                freq = memo[subTree];
            if (freq == 1)
            {
                res.Add(root); // 3|2
            }
            memo[subTree] = freq + 1;
            return subTree;
        }

        // 105,106,1008
        // 
        public TreeNode BstFromPreOrder(int[] preorder)
        {
            return Buildbst(preorder, 0, preorder.Length - 1);
        }

        TreeNode Buildbst(int[] preorder, int start, int end)
        {
            if (start > end)
                return null;

            int rootVal = preorder[start];
            TreeNode root = new TreeNode(rootVal);

            // 根据SBT的性质确定p的位置
            int p = start + 1;
            while (p <= end && preorder[p] < rootVal)
                p++;

            root.left = Buildbst(preorder, start + 1, p - 1);
            root.right = Buildbst(preorder, p, end);

            return root;
        }
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if (root == null) return null;
            if (root.val == key)
            {
                // 1. 叶子节点
                if (root.left == null && root.right == null)
                    return null;
                // 2. 有一个节点
                if (root.left == null) return root.right;
                if (root.right == null) return root.left;
                // 3. 两个节点
                //TreeNode minnode = GetMinNode(root.right);
                //root.val = minnode.val;
                //root.right = DeleteNode(root.right, minnode.val);

                // 获得右子树最小的节点
                TreeNode minNode = GetMinNode(root.right);
                // 删除右子树最小的节点
                root.right = DeleteNode(root.right, minNode.val);
                // 用右子树最小的节点替换 root 节点
                minNode.left = root.left;
                minNode.right = root.right;
                root = minNode;
            }
            else if (root.val > key)
            {
                root.left = DeleteNode(root.left, key);
            }
            else if (root.val < key)
            {
                root.right = DeleteNode(root.right, key);
            }
            return root;
        }

        private TreeNode GetMinNode(TreeNode node)
        {
            while (node.left != null)
                node = node.left;
            return node;
        }
        IList<IList<string>> res1 = new List<IList<string>>();
        public IList<IList<string>> SolveNQueens(int n)
        {
            // '.' 表示空，'Q' 表示皇后，初始化空棋盘。
            IList<string> board = new List<string>();
            for (int i = 0; i < n; i++)
            {
                board.Add(new string('.', n));
            }
            backtrack(board, 0);
            return res1;
        }
        // 路径：board 中小于 row 的那些行都已经成功放置了皇后
        // 选择列表：第 row 行的所有列都是放置皇后的选择
        // 结束条件：row 超过 board 的最后一行
        void backtrack(IList<string> board, int row)
        {
            // 触发结束条件
            if (row == board.Count)
            {
                res1.Add(new List<string>(board));
                return;
            }
            int n = board[row].Length;
            for (int col = 0; col < n; col++)
            {
                // 排除不合法选择
                if (!isValid(board, row, col))
                {
                    continue;
                }
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
    }
    public class mycompare<T> : IComparer<T>
    {
        public int Compare(T? x, T? y)
        {
            throw new NotImplementedException();
        }
    }
}
