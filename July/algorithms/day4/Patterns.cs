using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.day4
{
    internal class Patterns
    {

        #region 二叉树的前后中遍历

        #region 正常递归二叉树-前中后遍历 preorder/inorder/postorder
        IList<int> res = new List<int>();
        public IList<int> PreorderTraversal(TreeNode root)
        {
            if (root == null)
                return res;

            res.Add(root.val);
            PreorderTraversal(root.left);
            PreorderTraversal(root.right);

            return res;
        }

        public IList<int> InorderTraversal(TreeNode root)
        {
            if (root == null)
                return res;

            InorderTraversal(root.left);
            res.Add(root.val);
            InorderTraversal(root.right);

            return res;
        }

        public IList<int> PostorderTraversal(TreeNode root)
        {
            if (root == null)
                return res;

            PostorderTraversal(root.left);
            PostorderTraversal(root.right);
            res.Add(root.val);

            return res;
        }
        #endregion

        #region 分治法递归二叉树-前中后遍历
        public IList<int> PreorderTraversal_P(TreeNode root)
        {
            List<int> res = new List<int>();

            if (root == null)
                return res;

            res.Add(root.val);
            res.AddRange(PreorderTraversal(root.left));
            res.AddRange(PreorderTraversal(root.right));

            return res;
        }
        public IList<int> InorderTraversal_P(TreeNode root)
        {
            List<int> res = new List<int>();
            if (root == null)
                return res;

            res.AddRange(InorderTraversal_P(root.left));
            res.Add(root.val);
            res.AddRange(InorderTraversal_P(root.right));

            return res;

        }

        public IList<int> PostorderTraversal_P(TreeNode root)
        {
            List<int> res = new List<int>();
            if (root == null)
                return res;

            res.AddRange(PostorderTraversal_P(root.left));
            res.AddRange(PostorderTraversal_P(root.right));
            res.Add(root.val);

            return res;
        }

        #endregion

        #endregion

        #region 二叉树的层序遍历
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null)
                return res;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int sz = q.Count;

                List<int> level = new List<int>();
                for (int i = 0; i < sz; i++)
                {
                    TreeNode cur = q.Dequeue();
                    level.Add(cur.val);
                    if (cur.left != null)
                        q.Enqueue(cur.left);
                    if (cur.right != null)
                        q.Enqueue(cur.right);
                }
                res.Add(level);
            }
            return res;
        }

        #endregion

        #region 112. 路径总和
        // 用子问题的结果推导出大问题的结果
        public bool HasPathSum(TreeNode root, int target)
        {
            // base Case
            if (root == null)
                return false;

            if (root.left == null && root.right == null && root.val == target)
            {
                return true;
            }

            return HasPathSum(root.left, target - root.val)
                || HasPathSum(root.right, target - root.val);

        }
        // backtracking
        int target;
        bool found = false;
        int curSum = 0;

        public bool HasPathSum_B(TreeNode root, int targetSum)
        {
            if (root == null)
                return false;
            this.target = targetSum;
            traverse(root);
            return found;
        }
        // backtracking 的鼻祖
        private void traverse(TreeNode root)
        {
            if (root == null)
                return;

            curSum += root.val; // track

            if (root.left == null && root.right == null)
            {
                if (curSum == target)
                {
                    found = true;
                }
            }
            traverse(root.left);
            traverse(root.right);

            curSum -= root.val; // back
        }

        #endregion

        #region 单调栈 (单调递减)
        public int[] NextGreaterElement(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];

            Stack<int> s = new Stack<int>();
            for (int i = n - 1; i >= 0; i--)
            {
                while (s.Count != 0 && s.Peek() <= nums[i]) // pop出来所有大于 nums[i] 的元素
                    s.Pop();
                res[i] = s.Count == 0 ? -1 : s.Peek(); // 没有元素时给-1； 有元素时Peek()出来： 距离最近的元素
                s.Push(nums[i]);
            }
            return res;
        }

        public int[] NextGreaterElementCircleNums(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];

            Stack<int> s = new Stack<int>();
            for (int i = 2 * n - 1; i >= 0; i--)
            {
                while (s.Count != 0 && s.Peek() <= nums[i % n]) // pop出来所有大于 nums[i] 的元素
                    s.Pop();
                res[i % n] = s.Count == 0 ? -1 : s.Peek(); // 没有元素时给-1； 有元素时Peek()出来： 距离最近的元素
                s.Push(nums[i % n]);
            }
            return res;
        }
        #endregion
    }
   
}
