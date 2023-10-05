namespace CDS
{
    // 树
    //Definition for a binary tree node.
    public class TreeNode
    {
        public int val { get; set; }
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }
        public TreeNode() { }
        public TreeNode(int val) { this.val = val; }
        public TreeNode(int val, TreeNode left, TreeNode right)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
        public int myIndex { get; set; }
    }

    public static class TreeNodeExtention
    {
        /// <summary>
        /// 按层级遍历初始化一个树
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode Build(this TreeNode root, int[] nums)
        {
            if (nums.Length == 0)
                return null;

            root = new TreeNode(nums[0]);

            root.left = Build(root.left, nums);
            root.right = Build(root.right, nums);
            return root;

        }
    }
}
