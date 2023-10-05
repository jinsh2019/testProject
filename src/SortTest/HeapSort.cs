namespace SortTest
{
    internal class HeapSort
    {
        /// <summary>
        /// https://www.bilibili.com/video/BV1fp4y1D7cj/?spm_id_from=333.337.search-card.all.click&vd_source=dce47f89f00f0607ace1b2e214093c17
        /// lson i*2+1
        /// rson i*2+2
        /// parent (i-1)/2
        /// 维护堆的性质: 上浮和下沉
        /// 时间复杂度O(nlogn)
        /// Case:   input {2,3,8,1,4,9,10,7,16,14}
        /// </summary>
        public HeapSort(int[] nums)
        {
            // 无序数组
            int len = nums.Length;
            // 建堆: 找到 n-1 的父节点 (n-1-1)/2 => (n/2-1) 
            // 维护n-1的父节点
            for (int i = len / 2 - 1; i >= 0; i--)
            {
                heapify(nums, len, i);
            }
            // 排序
            for (int i = len - 1; i > 0; i--)
            {
                // 交换第一个数与最后一个数
                swap(nums, i, 0);
                // 维护堆的性质
                heapify(nums, i, 0);
            }
        }

        /// <summary>
        /// 维护堆性质
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="len">数组长度</param>
        /// <param name="i">维护节点</param>
        private void heapify(int[] nums, int len, int i)
        {
            int largest = i;
            int lson = i * 2 + 1;
            int rson = i * 2 + 2;
            // 找到最大下标 max(p,lson,rson) 
            if (lson < len && nums[largest] < nums[lson])
                largest = lson;
            if (rson < len && nums[largest] < nums[rson])
                largest = rson;

            // 不相等后，有一个下沉过程
            if (largest != i)
            {
                swap(nums, i, largest);
                // 递归维护 pIdx
                heapify(nums, len, largest);                 
            }
        }

        private void swap(int[] nums, int i, int j)
        {
            int tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }
    }
}
