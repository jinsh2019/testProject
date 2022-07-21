using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuneProject.cls
{
    internal class QuickSort
    {
        public void sort(int[] nums)
        {
            shuffle(nums);
            sort(nums, 0, nums.Length - 1);
        }

        private void sort(int[] nums, int lo, int hi)
        {
            if (lo >= hi)
                return;

            int p = partition(nums, lo, hi);

            sort(nums, lo, p - 1);
            sort(nums, p + 1, hi);
        }

        // 对 nums[lo..hi] 进行切分
        private int partition(int[] nums, int lo, int hi)
        {
            // 枢值
            int pivot = nums[lo];

            // 关于区间的边界控制需格外小心，稍有不慎就会出错
            // 我这里把 i, j 定义为开区间，同时定义：
            // [lo, i) <= pivot；(j, hi] > pivot
            // 之后都要正确维护这个边界区间的定义
            int i = lo + 1, j = hi; // 左右指针

            // 二路快排
            // 当 i > j 时结束循环，以保证区间 [lo, hi] 都被覆盖
            while (i <= j)
            {
                while (i < hi && nums[i] <= pivot)
                    i++;  // 此 while 结束时恰好 nums[i] > pivot

                while (j > lo && nums[j] > pivot)
                    j--; // 此 while 结束时恰好 nums[j] <= pivot

                if (i >= j)
                    break; // 就第一趟排序，左，右部全部排好之后，会达到i>=j

                // 此时 [lo, i) <= pivot && (j, hi] > pivot
                // 交换 nums[j] 和 nums[i]
                swap(nums, i, j);
                // 此时 [lo, i] <= pivot && [j, hi] > pivot
            }
            // 最后将 pivot 放到合适的位置，即 pivot 左边元素较小，右边元素较大
            swap(nums, lo, j); // lo 是pivot的位置， j右侧已经排好
            return j;
        }

        private void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        // 将nums打乱
        private void shuffle(int[] nums)
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


        #region basic quick sort
        public void sort2(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return;
            }
            sort1(nums, 0, nums.Length - 1);

        }
        public void sort1(int[] nums, int lo, int hi)
        {
            if (lo >= hi)
            {
                return;
            }
            int m = partition1(nums, lo, hi);
            sort1(nums, lo, m - 1);
            sort1(nums, m + 1, hi);
        }

        private int partition1(int[] nums, int lo, int hi)
        {
            int j = lo;
            int target = nums[lo];
            for (int i = lo + 1; i <= hi; i++)
            {
                // 交换后保证 arr[l, j] <= target
                if (nums[i] <= target)
                {
                    swap(nums, j + 1, i);
                    j++;
                }
            }
            swap(nums, j, lo);
            return j;
        }
        #endregion

    }
}
