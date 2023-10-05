namespace July.algorithms.day22
{
    // parent:  (i-1)/2
    // lson:    i*2+1
    // rson:    i*2+2
    internal class HeapSort
    {
        /// <summary>
        /// 堆排序
        /// </summary>
        /// <param name="nums">all nums</param>
        /// <param name="n">rest of length that need to be sorted</param>
        /// <param name="i">index needed heapify</param>
        public void heapify(int[] nums, int n, int i)
        {
            int pIdx = i;
            int lson = i * 2 + 1;
            int rson = i * 2 + 2;

            // 判断三者最大 max(root, left, right)
            if (lson < n && nums[pIdx] < nums[lson])
                pIdx = lson;

            if (rson < n && nums[pIdx] < nums[rson])
                pIdx = rson;

            if (pIdx != i)
            {
                swap(nums, i, pIdx);
                heapify(nums, n, pIdx);
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="nums">需要处理的数组</param>
        public void sort(int[] nums)
        {
            int len = nums.Length;
            //  建堆 parent:  (i-1)/2；
            //  从len-1开始heapify
            //  一直到父节点为0
            for (int i = (len - 1 - 1) / 2; i >= 0; i--)
                heapify(nums, len, i);

            // 排序
            for (int i = len - 1; i > 0; i--)
            {
                swap(nums, i, 0);   // 不断的交换第0个元素与第i个元素
                heapify(nums, i, 0);// 维护堆顶元素
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
