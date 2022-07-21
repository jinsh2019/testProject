using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuneProject
{
    internal class Class3
    {

        // 利用二叉搜索树的性质： 中序遍历是升序
        TreeNode first = null, second = null;
        TreeNode prev = new TreeNode(int.MinValue);
        // 只有两个节点有问题，交换两个节点的值
        public void RecoverTree(TreeNode root)
        {
            // 3,2,1 目标1,2,3
            inorderTraverse(root);

            int temp = first.val;
            first.val = second.val;
            second.val = temp;
        }

        // 中序遍历
        void inorderTraverse(TreeNode root)
        {
            if (root == null)
                return;

            inorderTraverse(root.left);
            if (root.val < prev.val) // 根节点的值小于前一个节点的值
            {
                if (first == null)
                {
                    first = prev;
                }
                second = root;
            }
            prev = root; // 不停更新prev 节点
            inorderTraverse(root.right);

        }


        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST(root, null, null);
        }
        bool IsValidBST(TreeNode root, TreeNode min, TreeNode max)
        {
            if (root == null)
                return true;
            if (min != null && root.val <= min.val) return false;
            if (max != null && root.val >= max.val) return false;
            return IsValidBST(root.left, min, root) // root --> stack: 5,2,1
                                                    // (root.left: 1, min: null, max: 2)
              && IsValidBST(root.right, root, max); // (root.right: 4, min: 2, max: null)



        }

        // 用于辅助合并有序数组
        private static int[] temp;

        public static void sort(int[] nums)
        {
            temp = new int[nums.Length];
            sort(nums, 0, nums.Length - 1);
        }

        private static void sort(int[] nums, int lo, int hi)
        {
            if (lo == hi)
                return;

            int mid = lo + (hi - lo) / 2;
            sort(nums, lo, mid);
            sort(nums, mid + 1, hi);

            merge(nums, lo, mid, hi);

        }

        private static void merge(int[] nums, int lo, int mid, int hi)
        {
            for (int k = lo; k <= hi; k++)
            {
                temp[k] = nums[k];
            }

            int i = lo, j = mid + 1;
            for (int p = lo; p <= hi; p++)
            {
                if (i == mid + 1)
                {
                    nums[p] = temp[j++];
                }
                else if (j == hi + 1)
                {
                    nums[p] = temp[i++];
                }
                else if (temp[i] > temp[j])
                {
                    nums[p] = temp[j++];
                }
                else
                {
                    nums[p] = temp[i++];
                }
            }
        }
    }
}
