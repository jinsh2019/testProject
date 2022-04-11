using System;
using static System.Console;

namespace MyTest
{
    class Program
    {

        #region 一句话的代码
        // 704. 二分查找
        public static int Search(int[] nums, int target)
        {
            // { -1,0,3,5,9,12}
            int left = 0;
            int right = nums.Length - 1;
            int mid = 0;
            while (left <= right) //  相等 1. 使得 left可以走到最后； 2. 使得right可以走到第一个；3.在中间位置重合，获取target
            {
                mid = (right - left) / 2 + left;
                if (nums[mid] == target)
                    return mid;
                else if (nums[mid] > target)
                    right = mid - 1;
                else
                    left = mid + 1;
            }
            return -1;
        }

        // 1,2,3,4,5 => 2
        // a. left=1, right=5,mid= 3; b. left=1 right=2, mid=1; c left=2, right=2 break;
        // 1,2,3,4,5 => 4
        // a. left=1, right=5,mid= 3; b. left= 4 right 5, mid =
        public static int FirstBadVersion(int n)
        {
            int left = 1;
            int right = n;
            int mid = 0;
            while (left < right)
            {
                mid = (right - left) / 2 + left;
                if (isbad(mid)) // bad versoin is in [left...Mid]
                {
                    right = mid;
                }
                else // bad version is in [mid+1...right]
                {
                    left = mid + 1;
                }
            }
            return left;
        }

        private static bool isbad(int version)
        {
            if (version >= 2) return true;
            return false;
        }

        public static int SearchInsert(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left <= right)
            {
                int mid = (right - left) / 2 + left;
                if (nums[mid] == target)
                {
                    return mid;
                }
                else
                {
                    if (nums[mid] > target)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
            }

            return left;
        }

        public static int[] RunningSum(int[] nums)
        {
            int t = 0;
            for (int i = 0; i < nums.Length; i++)
                nums[i] = (t += nums[i]);
            return nums;
        }

        public int[] BuildArray(int[] nums)
        {
            int n = nums.Length;
            int[] ans = new int[n];
            for (int i = 0; i < n; i++)
            {
                ans[i] = nums[nums[i]];
            }
            return ans;
        }


        public static int[] GetConcatenation(int[] nums)
        {
            int n = nums.Length;
            int[] ans = new int[2 * n];
            for (int i = 0; i < n; i++)
            {
                ans[i] = nums[i];
                ans[n + i] = nums[i];
            }
            return ans;
        }

        public static int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;

        }
        #endregion

        static void Main(string[] args)
        {
            #region 一句话的代码
            int[] t1 = { -1, 0, 3, 5, 9, 12 };// { 5 };//{ -1, 0, 3, 5, 9, 12 };
            FirstBadVersion(5);
            Search(t1, -5);

            int[] t2 = { 1, 3, 5, 6 };
            SearchInsert(t2, 5);
            int[] t3 = { 1, 3, 5, 6 };
            SearchInsert(t3, 2); ;

            int[] t4 = { 1, 3, 5, 6 };
            SearchInsert(t4, 7);

            int[] t5 = { 1 };
            SearchInsert(t5, 0);

            int[] t6 = { 1, 1, 1, 1, 1, 1 };
            RunningSum(t6);

            int[] t7 = { 1, 2, 1 };
            GetConcatenation(t7);

            TreeNode node20 = new TreeNode(20, new TreeNode(15), new TreeNode(7));
            TreeNode root = new TreeNode(3, new TreeNode(9), node20);
            WriteLine("MaxDepth:" + MaxDepth(root));
            #endregion

            WriteLine("Hello World!");
        }

        #region private Method
        private static int[,] BuildTo2D(int[][] my2DArray)
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
        #endregion

    }


    #region public class
    //Definition for a binary tree node.
    public class TreeNode
    {
        public int val { get; set; }
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }
        TreeNode() { }
        public TreeNode(int val) { this.val = val; }
        public TreeNode(int val, TreeNode left, TreeNode right)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    #endregion

}
