using System.Collections.Concurrent;

namespace July.algorithms.day22;

public class QuickSort
{
    public void sort(int[] nums)
    {
        shuttle(nums);
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
        swap(nums, lo, j); // lo 是pivot的index
        return j;
    }

    private void shuttle(int[] nums)
    {
        int n = nums.Length;
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            int r = rand.Next(n - i); // 0~n
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