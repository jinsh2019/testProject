namespace July.algorithms.day5
{
    internal class QuickSort
        {
            public void sort(int[] nums)
            {
                shuttle(nums);
                sort(nums, 0, nums.Length-1);
            }

            private void shuttle(int[] nums)
            {
                int n = nums.Length;
                Random random = new Random();
                for (int i = 0; i < n; i++)
                {
                    int r = i + random.Next(n - i);
                    swap(nums, i, r);
                }
            }

            private void swap(int[] nums, int i, int j)
            {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }

            private void sort(int[] nums, int lo, int hi)
            {
                if (lo >= hi) return;


                int p = partition(nums, lo, hi);
                sort(nums, lo, p - 1); // error point
                sort(nums, p + 1, hi); // error point
            }

            private int partition(int[] nums, int lo, int hi)
            {
                int pivot = nums[lo];

                int i = lo + 1, j = hi;
                // error point 后期+1可能相等
                while (i <= j)
                { 
                    while (i < hi && nums[i] <= pivot) // 直到大于pivot才停下来
                        i++;
                    while (j > lo && nums[j] > pivot) // 直到小于pivot才停下来
                        j--;

                    if (i >= j)
                        break;

                    swap(nums, i, j);
                }

                swap(nums, lo, j);
                return j;// 返回交换点
            }
        }
}
