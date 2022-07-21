﻿namespace July.algorithms.day11
{
    internal class MergeSort
    {
        int[] temp;
        public void sort(int[] nums)
        {
            temp = new int[nums.Length];
            sort(nums, 0, nums.Length - 1);
        }

        private void sort(int[] nums, int lo, int hi)
        {
            if (hi == lo)
                return;

            int mid = lo + (hi - lo) / 2;
            sort(nums, lo, mid);
            sort(nums, mid + 1, hi);
            merge(nums, lo, mid, hi);
        }

        private void merge(int[] nums, int lo, int mid, int hi)
        {
            for (int k = 0; k <= nums.Length; k++)
            {
                temp[k] = nums[k];
            }

            int i = lo, j = mid + 1;
            for (int p = lo; i <= hi; p++)
            {
                if (i == mid + 1)
                    nums[p] = temp[j++];
                else if (j == hi + 1)
                    nums[p] = temp[i++];
                else if (temp[i] <= temp[j])
                    nums[p] = temp[i++];
                else
                    nums[p] = temp[j++];
            }
        }
    }
}
