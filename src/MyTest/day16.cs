using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class day16
    {


        //230. 二叉搜索树中第K小的元素

        // 记录结果
        int res = 0;
        // 记录当前元素的排名
        int rank = 0;
        public int KthSmallest(TreeNode root, int k)
        {
            // 利用BST的中序遍历特性
            traverse(root, k);
            return res;
        }

        private void traverse(TreeNode root, int k)
        {
            if (root == null)
                return;

            traverse(root.left, k);
            // 中序遍历代码位置
            rank++;
            if (k == rank)
            {
                // 找到第k小的元素
                res = root.val;
                return;
            }
            traverse(root.right, k);
        }

        // 538. 把二叉搜索树转换为累加树
        // Greater Sum Tree
        // 记录累加和
        int sum = 0;
        public TreeNode ConvertBST(TreeNode root)
        {
            traverse(root);
            return root;
        }

        private void traverse(TreeNode root)
        {
            if (root == null)
                return;

            traverse(root.right);
            // 维护累加和
            sum += root.val;
            // 将BST转换成累加树
            root.val = sum;
            traverse(root.left);
        }

        // 判断是否是BST
        bool isValidBST(TreeNode root)
        {
            return isValidBST(root, null, null);
        }

        // 限定以 root 为根的子树节点必须满足
        // max.val > root.val > min.val
        private bool isValidBST(TreeNode root, TreeNode min, TreeNode max)
        {
            // base case
            if (root == null) return true;
            // 若 root.val 不符合 max和min的限制，说明不是合法BST
            if (min != null && root.val <= min.val) return false;
            if (min != null && root.val >= max.val) return false;
            // 限定左子树的最大值是 root.val,
            // 右子树的最小值是 root.val
            return isValidBST(root.left, min, root)
                && isValidBST(root.right, root, max);
        }

        // 700. 二叉搜索树中的搜索
        public TreeNode SearchBST(TreeNode root, int target)
        {
            if (root == null)
                return null;
            // 去左子树搜索
            if (root.val > target)
            {
                return SearchBST(root.left, target);
            }
            // 去右子树搜索
            if (root.val < target)
            {
                return SearchBST(root.right, target);
            }

            return root;
        }

        //701. 二叉搜索树中的插入操作
        public TreeNode InsertIntoBST(TreeNode root, int val)
        {
            // 找到空位置插入新节点
            if (root == null) return new TreeNode(val);

            //if (root.val == val)
            //    BST 中一般不会插入已存在元素
            if (root.val < val)
                root.right = InsertIntoBST(root.right, val);
            if (root.val > val)
                root.left = InsertIntoBST(root.left, val);
            return root;

        }


        //450. 删除二叉搜索树中的节点
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if (root == null) return null;
            if (root.val == key)
            {
                // 这两个if 把情况1 和情况2 都正确处理了
                if (root.left == null) return root.right;
                if (root.right == null) return root.left;

                // 处理情况 3
                // 获得右子树最小的节点
                TreeNode minNode = getMin(root.right);
                // 删除右子树最小的节点
                root.right = DeleteNode(root.right, minNode.val);
                // 用右子树最小的节点替换root 节点
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

        private TreeNode getMin(TreeNode root)
        {
            // BST 最左边的就是最小的
            while (root.left != null) root = root.left;
            return root;
        }

        // 96. 不同的二叉搜索树
        // 备忘录
        int[][] memo;
        public int NumTree(int n)
        {
            memo = new int[n][];
            for (int i = 0; i < memo.Length; i++)
            {
                memo[i] = new int[n];
            }
            return count(1, n);
        }

        private int count(int lo, int hi)
        {
            if (lo > hi)
                return 1;

            // 查备忘录
            if (memo[lo][hi] != 0)
            {
                return memo[lo][hi];
            }

            int res = 0;
            for (int i = lo; i <= hi; i++)
            {
                int left = count(lo, i - 1);
                int right = count(i + 1, hi);
                res += left * right;
            }
            // 将结果存入备忘录
            memo[lo][hi] = res;

            return res;
        }

        // 95. 不同的二叉搜索树 II
        public IList<TreeNode> GenerateTrees(int n)
        {
            if (n == 0) return new List<TreeNode>();

            return build(1, n);

        }

        private IList<TreeNode> build(int lo, int hi)
        {
            List<TreeNode> res = new List<TreeNode>();

            if (lo > hi)
            {
                res.Add(null);
                return null;
            }

            // 1. 穷举 root 节点的所有可能
            for (int i = lo; i <= hi; i++)
            {
                // 2. 递归构造出左右子树的所有合法 BST
                IList<TreeNode> leftTree = build(lo, i - 1);
                IList<TreeNode> rightTree = build(i + 1, hi);
                // 3. 给 root 节点穷举所有左右子树的组合
                if (leftTree != null && rightTree != null)
                {
                    foreach (var left in leftTree)
                    {
                        foreach (var right in rightTree)
                        {
                            TreeNode root = new TreeNode(i);
                            root.left = left;
                            root.right = right;
                            res.Add(root);
                        }
                    }
                }
            }

            return res;
        }

        // 215. 数组中的第K个最大元素
        public int FindKthLargest(int[] nums, int k)
        {
            // 小顶堆，堆顶是最小元素
            PriorityQueue<int, int>
                pq = new PriorityQueue<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(nums[i], nums[i]);
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
           
            return pq.Peek();
        }
    }

    public class Quick
    {
        public static void sort(int[] nums)
        {
            shuffle(nums);
            sort(nums, 0, nums.Length - 1);
        }

        private static void sort(int[] nums, int lo, int hi)
        {
            if (lo >= hi)
                return;

            int p = partition(nums, lo, hi);
            sort(nums, lo, p - 1);
            sort(nums, p + 1, hi);
        }

        private static int partition(int[] nums, int lo, int hi)
        {
            int pivot = nums[lo];

            int i = lo + 1, j = hi;

            while (i <= j)
            {
                while (i < hi && nums[i] <= pivot)
                {
                    i++;
                }
                while (j > lo && nums[j] > pivot)
                {
                    j--;
                }
                if (i >= j)
                {
                    break;
                }
                swap(nums, i, j);
            }

            swap(nums, lo, hi);
            return j;
        }

        private static void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        private static void shuffle(int[] nums)
        {
            Random rand = new Random();
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                // 生成 [i, n - 1] 的随机数
                int r = i + rand.Next(n - i);
                swap(nums, i, r);
            }
        }
    }
}
