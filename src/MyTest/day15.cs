using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class day15
    {

        // 315. 计算右侧小于当前元素的个数
        public IList<int> CountSmaller(int[] nums)
        {
            throw new NotImplementedException();
        }

    }

    // 912. 排序数组
    // 归并排序
    class Merge
    {
        // 定义： 排序nums[lo..hi]
        private static int[] temp;
        public static void sort(int[] nums)
        {
            temp = new int[nums.Length];
            sort(nums, 0, nums.Length - 1);
        }

        private static void sort(int[] nums, int lo, int hi)
        {
            if (lo == hi)
                return;// 单个元素不用排序

            int mid = lo + (hi - lo) / 2;
            // 利用定义，排序[lo..mid]
            sort(nums, lo, mid);
            // 利用定义，排序[mid+1..hi]
            sort(nums, mid + 1, hi);

            // 后序位置
            // 此时两部分子数组已经被排序好
            // 合并两个有序数组，使nums[lo..hi]有序
            merge(nums, lo, mid, hi);
        }

        // 将有序数组nums[lo..mid]和有序数组nums[mid+1..hi]
        // 合并为有序数组 nums[lo..hi]
        private static void merge(int[] nums, int lo, int mid, int hi)
        {
            // 先把 nums[lo..hi]复制到辅助数组中
            // 以便合并后的结果能够直接存入nums
            for (int k = lo; k <= hi; k++)
                temp[k] = nums[k];

            // 数组双指针技巧，合并两个有序数组
            int i = lo, j = mid + 1;
            for (int p = lo; p <= hi; p++)
            {
                if (i == mid + 1)
                {  // 左半边数组已全部被合并
                    nums[p] = temp[j++];
                }
                else if (j == hi + 1)
                {  // 右版本数组已全部被合并
                    nums[p] = temp[i++];
                }
                else if (temp[i] > temp[j])
                {
                    nums[p] = temp[j++];
                }
                else if(temp[i] < temp[j])
                {
                    nums[p] = temp[i++];
                }
            }
        }
    }
}
