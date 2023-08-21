namespace August.algorithms.day26
{
    internal class QuickSort
    {
        public void sort(int[] nums)
        {
            shuttle(nums);
            sort(nums, 0, nums.Length - 1);
        }

        private void sort(int[] nums, int lo, int hi)
        {
            if (lo >= hi) return;

            int p = partition(nums, lo, hi);
            sort(nums, lo, p - 1);
            sort(nums, p + 1, hi);
        }

        /// <summary>
        /// 分治
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="lo">low 的位置</param>
        /// <param name="hi">high的职位</param>
        /// <returns>枢值的索引</returns>
        private int partition(int[] nums, int lo, int hi)
        {
            int pivot = nums[lo];
            int i = lo + 1, j = hi;
            while (i <= j)
            {
                while (i < hi && nums[i] <= pivot)
                {// 小于pivot的放左边
                    i++;
                }
                while (j > lo && nums[j] > pivot)
                { // 大于pivot的放右边
                    j--;
                }
                // i,j已经相交, 表示遍历完成
                if (i >= j)
                    break;
                // 交换i,j的值
                swap(nums, i, j);
            }
            // lo 是pivot的index
            swap(nums, lo, j);
            return j;
        }

        private void shuttle(int[] nums)
        {
            int n = nums.Length;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                int r = rand.Next(n - i);
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
