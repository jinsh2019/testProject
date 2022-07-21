using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.daily
{
    public class day1
    {
        #region findKthLargest
        public int findKthLargest(int[] nums, int k)
        {
            shuffle(nums);
            int lo = 0, hi = nums.Length - 1;
            k = nums.Length - k;
            while (lo <= hi)
            {
                int p = partition(nums, lo, hi);

                if (p < k)
                {
                    lo = p + 1;
                }
                else if (p > k)
                {
                    hi = p - 1;
                }
                else
                {
                    return nums[p];
                }
            }
            return -1;
        }

        private void shuffle(int[] nums)
        {
            Random random = new Random();
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + random.Next(n - i);
                swap(nums, i, r);
            }
        }

        private int partition(int[] nums, int lo, int hi)
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
                    j--;
                if (i >= j)
                    break;

                swap(nums, i, j);
            }
            swap(nums, lo, j);
            return j;
        }

        private void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;

        }

        public int findKthLargest1(int[] nums, int k)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
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
        #endregion

        #region 无向图中连通分量数
        public int CountComponents(int n, int[][] edges)
        {
          algorithms.@base.UnionFind uf = new algorithms.@base.UnionFind(n);

            int m = edges.Length;
            for (int i = 0; i < m; i++)
                uf.union(edges[i][0], edges[i][1]);

            return uf.count;

        }
        #endregion


        #region 二叉搜索树的最小绝对差
        int res = int.MaxValue;
        TreeNode prev = null;
        public int GetMinimumDifference(TreeNode root)
        {
            traverse(root);
            return res;
        }
        void traverse(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            traverse(root.left); //leaf
                                 // in-order
            if (prev != null)
            {
                res = Math.Min(res, root.val - prev.val);
            }
            prev = root;
            traverse(root.right);
        }
        #endregion

        #region 二叉树中第二小的节点
        public int FindSecondMinimumValue(TreeNode root)
        {
            if (root.left == null && root.right == null)
            {
                return -1;
            }

            int left = root.left.val, right = root.right.val;
            if (root.val == root.left.val)
            {
                left = FindSecondMinimumValue(root.left);
            }

            if (root.val == root.right.val)
            {
                right = FindSecondMinimumValue(root.right);
            }

            if (left == -1)
            {
                return right;
            }
            if (right == -1)
            {
                return right;
            }

            return Math.Min(left, right);
        }
        #endregion
    }
}
