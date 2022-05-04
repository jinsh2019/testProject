using CDS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class day14
    {
        //226. 翻转二叉树
        public TreeNode InvertTree(TreeNode root)
        {
            traverse(root);
            return root;
        }

        private void traverse(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            // 每一个结点都需做的事情就是交换它的左右子节点
            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;

            traverse(root.left);
            traverse(root.right);
        }
        // 递归
        public TreeNode InvertTree1(TreeNode root)
        {
            if (root == null)
                return null;
            // 利用函数定义，先翻转左右子树
            TreeNode left = InvertTree1(root.left);
            TreeNode right = InvertTree1(root.right);

            // 然后交换左右子节点
            root.left = right;
            root.right = left;

            // 和定义逻辑自洽，以root 为根的这棵二叉树已经被翻转，返回root
            return root;
        }

        // 116. 填充每个节点的下一个右侧节点指针
        public Node Connect(Node root)
        {
            if (root == null) return null;
            // 遍历三叉树 ， 连接相邻结点
            traverseConnect(root.left, root.right);
            return root;
        }

        // 三叉树遍历框架
        private void traverseConnect(Node node1, Node node2)
        {
            if (node1 == null || node2 == null)
                return;

            // 前序位置
            // 将传入的两个结点串起来
            node1.right = node2;

            // 连接相同父节点的两个子节点
            traverseConnect(node1.left, node1.right);
            traverseConnect(node2.left, node2.right);

            // 连接跨越父节点的两个子节点
            traverseConnect(node1.right, node2.left);
        }

        // 114. 二叉树展开为链表
        public void Flatten(TreeNode root)
        {
            if (root == null) return;

            // 利用定义，把左右子树拉平
            Flatten(root.left);
            Flatten(root.right);

            // 1. 左右子树已经被拉平成一条链表
            TreeNode left = root.left;
            TreeNode right = root.right;

            // 2. 将左子树作为右子树
            root.left = null;
            root.right = left;

            // 3. 将原先的右子树接到当前右子树的末端
            TreeNode p = root;
            while (p.right != null)
            {
                p = p.right;
            }
            p.right = right;
        }

        // 654. 最大二叉树
        public TreeNode constructMaximumBinaryTree(int[] nums)
        {
            return build(nums, 0, nums.Length - 1);
        }

        // 定义: 将num[low..high] 的构造成符合条件的树，返回根节点
        TreeNode build(int[] nums, int low, int high)
        {
            if (low > high)
                return null;

            // 找到数组中的最大值和对应的索引
            int index = -1, maxVal = int.MinValue;
            for (int i = low; i <= high; i++)
            {
                if (maxVal < nums[i])
                {
                    index = i;
                    maxVal = nums[i];
                }
            }
            // 先构造出根节点
            TreeNode root = new TreeNode(maxVal);
            // 递归调用构造左右子树
            root.left = build(nums, low, index - 1);
            root.right = build(nums, index + 1, high);

            return root;
        }
        // 105. 从前序与中序遍历序列构造二叉树
        Dictionary<int, int> valToIndex = new Dictionary<int, int>();
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            for (int i = 0; i < inorder.Length; i++)
            {
                valToIndex.Add(inorder[i], i);
            }

            return buildTree1(preorder, 0, preorder.Length - 1,
                inorder, 0, inorder.Length - 1);
        }
        /*
            build 函数的定义：
            若前序遍历数组为 preorder[preStart..preEnd]，
            中序遍历数组为 inorder[inStart..inEnd]，
            构造二叉树，返回该二叉树的根节点 
        */
        private TreeNode buildTree1(int[] preorder, int preStart, int preEnd, int[] inorder, int inStart, int inEnd)
        {
            if (preStart > preEnd)
            {
                return null;
            }
            // root 节点对应的值就是前序遍历数组的第一个元素
            int rootVal = preorder[preStart];
            // rootVal 在中序遍历数组中的索引
            int index = valToIndex[rootVal];

            int leftSize = index - inStart;

            // 先构造出当前根节点
            TreeNode root = new TreeNode(rootVal);
            // 递归构造左右子树
            root.left = buildTree1(preorder, preStart + 1, preStart + leftSize,
                                   inorder, inStart, index - 1);

            root.right = buildTree1(preorder, preStart + leftSize + 1, preEnd,
                                    inorder, index + 1, inEnd);

            return root;
        }

        // 106. 从中序与后序遍历序列构造二叉树
        Dictionary<int, int> valToIndex2 = new Dictionary<int, int>();
        public TreeNode BuildTree2(int[] inorder, int[] postorder)
        {
            for (int i = 0; i < inorder.Length; i++)
            {
                valToIndex2.Add(inorder[i], i);
            }

            return BuildTree2(inorder, 0, inorder.Length - 1,
                               postorder, 0, postorder.Length - 1);
        }

        /*
             build 函数的定义:
            后序遍历数组为 postorder[postStart..postEnd],
            中序遍历数组为 inorder[inStart..inEnd]
            构造二叉树，返回二叉树的根节点
         */
        private TreeNode BuildTree2(int[] inorder, int inStart, int inEnd, int[] postorder, int postStart, int postEnd)
        {
            if (inStart > inEnd)
                return null;
            // root 节点对应的值就是后序遍历数组的最后一个元素
            int rootVal = postorder[postEnd];
            // rootVal 在中序遍历数组中的索引
            int index = valToIndex2[rootVal];
            // 左子树的节点个数
            int leftSize = index - inStart;
            TreeNode root = new TreeNode(rootVal);
            // 递归构造左右子树
            root.left = BuildTree2(inorder, inStart, index - 1,
                                    postorder, postStart, postStart + leftSize - 1);
            root.right = BuildTree2(inorder, index + 1, inEnd,
                                    postorder, postStart + leftSize, postEnd - 1);

            return root;
        }


        //889. 根据前序和后序遍历构造二叉树
        // 存储 postorder 中值到索引的映射
        Dictionary<int, int> valToIndex3 = new Dictionary<int, int>();
        public TreeNode ConstructFromPrePost(int[] preorder, int[] postorder)
        {
            for (int i = 0; i < postorder.Length; i++)
            {
                valToIndex3.Add(postorder[i], i);
            }
            return BuildTree3(preorder, 0, preorder.Length - 1,
                              postorder, 0, postorder.Length - 1);
        }

        // 定义：根据 preorder[preStart..preEnd] 和
        // postorder[postStart..postEnd]
        // 构建二叉树，并返回根节点。
        private TreeNode BuildTree3(int[] preorder, int preStart, int preEnd,
                                    int[] postorder, int postStart, int postEnd)
        {
            if (preStart > preEnd)
                return null;

            if (preStart == preEnd)
            {
                return new TreeNode(preorder[preStart]);
            }

            // root 节点对应的值就是前序遍历数组的第一个元素
            int rootVal = preorder[preStart];
            // root.left 的值是前序遍历第二个元素
            // 通过前序和后序遍历构造二叉树的关键在于通过左子树的根节点
            // 确定 preorder 和 postorder 中左右子树的元素区间
            int leftRootVal = preorder[preStart + 1];
            // leftRootVal 在后序遍历数组中的索引
            int index = valToIndex3[leftRootVal];
            // 左子树的元素个数
            int leftSize = index - postStart + 1;

            // 先构造出当前根节点
            TreeNode root = new TreeNode(rootVal);
            // 递归构造左右子树
            // 根据左子树的根节点索引和元素个数推导左右子树的索引边界
            root.left = BuildTree3(preorder, preStart + 1, preStart + leftSize,
                    postorder, postStart, index);
            root.right = BuildTree3(preorder, preStart + leftSize + 1, preEnd,
                    postorder, index + 1, postEnd - 1);

            return root;
        }

        //652. 寻找重复的子树
        // 记录所有子树
        Dictionary<string, int> memo = new Dictionary<string, int>();
        // 记录重复的子树根节点
        List<TreeNode> res = new List<TreeNode>();
        public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
        {
            traverse1(root);
            return res;
        }

        string traverse1(TreeNode root)
        {
            if (root == null)
                return "#";

            string left = traverse1(root.left);
            string right = traverse1(root.right);
            string subTree = left + "," + right + "," + root.val;
            if (memo.TryGetValue(subTree, out int freq))
            {
                if (freq == 1)
                    res.Add(root); // 有人和我重复，把自己加入结果列表
                memo[subTree] = freq + 1;
            }
            else
            {
                memo.Add(subTree, 1); // 暂时没人跟我重复，把自己加入集合
            }

            #region 原始思路
            //if (memo.ContainsKey(subTree))
            //    res.Add(root); // 有人和我重复，把自己加入结果列表
            //else
            //    memo.Add(subTree, 1); // 暂时没人跟我重复，把自己加入集合
            #endregion

            return subTree;
        }
    }

    // 297. 二叉树的序列化与反序列化
    public class Codec
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

        private void serialize(TreeNode root, StringBuilder sb)
        {
            if (root == null)
            {
                sb.Append(NULL).Append(SEP);
                return;
            }
            // 前序遍历位置
            sb.Append(root.val).Append(SEP);

            serialize(root.left, sb);
            serialize(root.right, sb);
        }

        // Decodes your encoded data to tree.
        /* 主函数，将字符串反序列化为二叉树结构 */
        public TreeNode deserialize(string data)
        {
            // 将字符串转化成列表
            LinkedList<string> nodes = new LinkedList<string>();
            foreach (var s in data.Split(SEP))
            {
                nodes.AddLast(s);
            }
            return deserialize(nodes);
        }

        /* 辅助函数，通过 nodes 列表构造二叉树 */
        private TreeNode deserialize(LinkedList<string> nodes)
        {
            if (nodes.Count == 0)
                return null;

            // 前序遍历位置
            // 列表最左侧就是根节点
            string first = nodes.First();
            nodes.RemoveFirst();
            if (first.Equals(NULL)) return null;
            TreeNode root = new TreeNode(int.Parse(first));

            root.left = deserialize(nodes);
            root.right = deserialize(nodes);

            return root;
        }

        public int[] SortArray(int[] nums)
        {
            Array.Sort(nums, new Comparison<int>((i1, i2) => i1.CompareTo(i2)));
            return nums;
        }
    }
}
