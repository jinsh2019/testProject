﻿namespace August
{
    internal class QuickSort
    {
        public void sort(int[] nums)
        {
            sort(nums, 0, nums.Length - 1);
        }
        public void sort(int[] nums, int lo, int hi)
        {
            if (lo >= hi)
                return;

            int p = partition(nums, lo, hi);
            sort(nums, lo, p - 1);
            sort(nums, p + 1, hi);
        }

        private int partition(int[] nums, int lo, int hi)
        {
            int pivot = nums[lo];
            int i = lo + 1;
            int j = hi;
            while (i <= j)
            {
                while (i < hi && nums[i] <= pivot)
                    i++;
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
    }
}
