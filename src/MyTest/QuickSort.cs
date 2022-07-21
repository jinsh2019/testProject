using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
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
            // base Case
            if (lo >= hi)
                return;

            int p = partition(nums, lo, hi);
            sort(nums, lo, p - 1);
            sort(nums, p + 1, hi);

        }

        private int partition(int[] nums, int lo, int hi)
        {
            int pivot = nums[lo];
            int i = lo + 1, j = hi;

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

        private void shuffle(int[] nums)
        {
            Random rnd = new Random();
            int n = nums.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int r = i + rnd.Next(n - i);
                swap(nums, i, r);
            }
        }

        private void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
}
