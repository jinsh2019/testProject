using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuneProject.cls
{


    internal class MergeSort
    {
        private int[] temp;

        public void sort(int[] nums)
        {
            temp = new int[nums.Length];
            sort(nums, 0, nums.Length - 1);
        }
        private void sort(int[] nums, int lo, int hi)
        {
            if (lo == hi)
                return;
            int mid = lo + (hi - lo) / 2;
            sort(nums, lo, mid);
            sort(nums, mid + 1, hi);
            merge(nums, lo, mid, hi);

        }

        private void merge(int[] nums, int lo, int mid, int hi)
        {
            // 先把 nums[lo..hi] 复制到辅助数组中
            // 以便合并后的结果能够直接存入 nums
            for (int k = lo; k <= hi; k++)
                temp[k] = nums[k];

            // 数组双指针技巧，合并两个有序数组
            int i = lo, j = mid + 1;
            for (int p = lo;p <= hi; p++)
            {
                if (i == mid + 1)
                {
                    // 左半边数组已全部被合并
                    nums[p] = temp[j++];
                }
                else if (j == hi + 1)
                {
                    // 右半边数组已全部被合并
                    nums[p] = temp[i++];
                }
                else if (temp[i] > temp[j])
                {
                    nums[p] = temp[j++];
                }
                else if (temp[i] < temp[j])
                {
                    nums[p] = temp[i++];
                }
            }
        }
    }
}
