using CDS;

namespace July.algorithms.day2
{
    internal class BST
    {
        // 108. 将有序数组转换为二叉搜索树
        public TreeNode SortedArrayToBST(int[] nums)
        {
            return build(nums, 0, nums.Length - 1);
        }

        private TreeNode build(int[] nums, int left, int right)
        {
            if (left > right)
                return null;

            int mid = left + (right - left) / 2;
            TreeNode root = new TreeNode(nums[mid]);
            root.left = build(nums, left, mid - 1);
            root.right = build(nums, mid + 1, right);
            return root;
        }

        // 235. 二叉搜索树的最近公共祖先
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
                return null;

            // 确定左小右大，进入迭代
            if (p.val > q.val)
            {
                return LowestCommonAncestor(root, q, p);
            }
            // 具有同一个root满足以下条件时
            if (root.val >= p.val && root.val <= q.val)
            {
                return root;
            }
            // 找最近的公共祖先， 如果root.val > q.val时，往左找，直到root.val < q.val,往右找
            if (root.val > q.val)
            {
                return LowestCommonAncestor(root.left, p, q);
            }
            else
            {
                return LowestCommonAncestor(root.right, p, q);
            }

        }

        // 897. 递增顺序搜索树
        public TreeNode IncreasingBST(TreeNode root) { 
            if(root == null)
                return null;

            TreeNode left = IncreasingBST(root.left); // 
            root.left = null;
            TreeNode right = IncreasingBST(root.right);
            root.right = right;

            if (left == null)
                return root;

            TreeNode p = left;
            while (p != null && p.right != null) {
                p = p.right;
            }

            p.right = root;
            return left; 
        }
    }
}
