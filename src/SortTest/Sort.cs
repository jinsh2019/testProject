using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace SortTest
{
    /// <summary>
    /// 参考url  https://www.bilibili.com/video/BV1Zs4y1X7mN/?share_source=copy_web&vd_source=80759d120fd4dd7db0c92ba90a5d7c27&t=27
    /// </summary>
    public static class Sort
    {
        /// <summary>
        /// 选择排序 将选择出最小值的位置，并交换位置
        /// </summary>
        /// <param name="nums"></param>
        public static void SelectorSort(int[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] < nums[min])
                    {
                        min = j;
                    }
                }

                int temp = nums[i];
                nums[i] = nums[min];
                nums[min] = temp;

            }
        }


        /// <summary>
        /// 冒泡排序 将最大数放在最后面
        /// </summary>
        /// <param name="nums"></param>
        public static void BubbleSort(int[] nums)
        {
            int n = nums.Length;
            for (int i = n - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int tmp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = tmp;
                    }
                }
            }
        }

        /// <summary>
        /// 插入排序 对于前i-1个已经有序的情况下，将i插入合适的位置
        /// </summary>
        /// <param name="nums"></param>
        public static void InsertSort(int[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                int x = nums[i];
                int j = i - 1; // j 是 i前面的一个数
                while (j >= 0 && x <= nums[j])
                {
                    nums[j + 1] = nums[j];
                    j--;
                }
                nums[j + 1] = x;
            }
        }

        /// <summary>
        ///  归并排序 使用分治的思想进行排序
        ///  使用到了递归+合并
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void MergeSort(int[] nums, int start, int end)
        {
            if (start == end)
                return;

            int mid = (start + end) / 2;
            MergeSort(nums, start, mid);
            MergeSort(nums, mid + 1, end);

            Merge(nums, start, mid, end);
        }

        private static void Merge(int[] nums, int start, int mid, int end)
        {
            int[] tmp = new int[end - start + 1];
            int p = 0;
            int l = start;
            int r = mid + 1;

            while ((l <= mid) && (r <= end))
            {
                if (nums[l] <= nums[r])
                {
                    tmp[p] = nums[l];
                    p++;
                    l++;
                }
                else
                {
                    tmp[p] = nums[r];
                    p++;
                    r++;
                }
            }
            // 将剩余部分加入tmp中
            while (l <= mid)
            {
                tmp[p] = nums[l];
                p++;
                l++;

            }
            // 将剩余部分加入tmp中
            while (r <= end)
            {
                tmp[p] = nums[r];
                p++;
                r++;
            }
            // 把大集合的元素复制回原数组
            for (int i = start; i < end + 1; i++)
            {
                nums[i] = tmp[i - start];
            }
        }

        public static void BucketSort(int[] nums)
        {
            //int minV = nums.Min();
            //int maxV = nums.Max();
            //int bucketCount = 3;

        }


        public static void QuickSort(int[] nums, int l, int r)
        {
            if (l >= r)
                return;

            int pivot = QuickSortPivot(nums, l, r); //  2 5 6 8 9
            QuickSort(nums, l, pivot - 1);
            QuickSort(nums, pivot + 1, r);
        }

        /// <summary>
        /// 1. before j is the values that all will be less and equal than nums[pivot]
        /// 2. put the less value on left and more value on right
        /// 3. exchange nums[j] with nums[i] if nums[i] < nums[pivot]
        /// 4. after exchanging then j++
        /// 5. exchange the less value with pivot value
        /// 6. calculate new pivot index
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="start"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static int QuickSortPivot(int[] nums, int start, int r)
        {
            Random random = new Random();
            start = start + random.Next(r + 1 - start);
            int pivot = start;
            int j = start + 1;                          // 1

            for (int i = start + 1; i <= r; i++)        // 2 
            {
                if (nums[i] <= nums[pivot])
                {
                    int y = nums[j];                // 3
                    nums[j] = nums[i];
                    nums[i] = y;

                    j++;                            // 4
                }
            }

            int x = nums[pivot];                    // 5
            nums[pivot] = nums[j - 1];
            nums[j - 1] = x;

            pivot = j - 1;                          // 6
            return pivot;
        }

        // put nums`s value into arr[value] then count the quantity of value 
        public static void CountingSort(int[] nums)
        {
            int[] cnt = new int[100];                       // 声明count 数组，取值范围[0..99]
            for (int i = 0; i < nums.Length; i++)
            {
                cnt[nums[i]]++;                             // 对nums[i]在对应的index上进行计数
            }

            int top = 0; // the position of nums
            for (int i = 0; i < 100; i++)
            {
                while (cnt[i] != 0)
                {
                    nums[top++] = i;                        // 对倒数字
                    --cnt[i];                               // count 数字自减
                }
            }
        }


    }
}
