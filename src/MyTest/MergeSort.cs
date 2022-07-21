using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class MergeSort
    {
        private static int[] temp;
        public static void sort(int[] nums)
        {
            temp = new int[nums.Length];
            sort(nums, 0, nums.Length - 1);
        }

        private static void sort(int[] nums, int lo, int hi)
        {
            // base Case
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
                temp[k] = nums[k];

            int i = lo, j = mid + 1;
            for (int p = lo; p <= hi; p++)
            {
                if (i == mid + 1)
                    nums[p] = temp[j++]; // 左半边数组已全部被合并
                else if (j == hi + 1)
                    nums[p] = temp[i++]; // 右半边数组已全部被合并
                else if (temp[i] > temp[j])
                    nums[p] = temp[j++];
                else if (temp[i] <= temp[j])
                    nums[p] = temp[i++];
            }
        }
    }
}
