using CDS;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;
using System.Threading;
using static System.Console;

namespace SortTest
{
    class Program
    {
        #region Bubble

        #region 冒泡排序基础版

        // https://www.bilibili.com/video/BV1Zs4y1X7mN/?share_source=copy_web&vd_source=80759d120fd4dd7db0c92ba90a5d7c27&t=27
        //
        public static void BubbleSort(int[] nums)
        {
            int n = nums.Length;
            // 外层管范围，内层管最值
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
        #endregion

        #region 有序标记
        public static void BubbleSort2(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                // 有序标记， 每一轮的初始值都是true
                bool isSorted = true;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    int tmp = 0;
                    if (array[j] > array[j + 1])
                    {
                        tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                        // 因为有元素进行交换，所以不是有序的，标记为false
                        isSorted = false;
                    }
                }
                if (isSorted)
                {
                    break;
                }
            }
        }

        #endregion

        #region 部分有序的优化
        public static void BubbleSort3(int[] array)
        {
            // 记录最后一次交换的位置
            int lastExchangeIndex = 0;
            // 无序数列的边界，每次比较只需要比到这里为止
            int sortBorder = array.Length - 1;
            for (int i = 0; i < array.Length - 1; i++)
            {
                // 有序标记，每一轮的初始值都是true
                bool isSorted = true;
                for (int j = 0; j < sortBorder; j++)
                {
                    int tmp = 0;
                    if (array[j] > array[j + 1])
                    {
                        tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                        // 因为有元素进行交换，所以不是有序的，标记为false
                        isSorted = false;
                        // 更新为最后一次交换的位置
                        lastExchangeIndex = j;
                    }
                }
                sortBorder = lastExchangeIndex;
                if (isSorted)
                {
                    break;
                }

                isSorted = true;
            }
        }
        #endregion

        #region 鸡尾酒排序法
        public static void BubbleSort4(int[] array)
        {
            int tmp = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                bool isSorted = true;
                // 奇数轮，从左向右比较和交换
                for (int j = i; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                        isSorted = false;
                    }

                }
                if (isSorted)
                {
                    break;
                }
                // 在偶数轮之前，将isSorted重新标记为true
                isSorted = true;
                // 偶数轮，从右向左比较和交换
                for (int j = array.Length - i - 1; j > i; j--)
                {
                    if (array[j] < array[j - 1])
                    {
                        tmp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = tmp;
                        isSorted = false;
                    }
                }
                if (isSorted)
                {
                    break;
                }
            }
        }
        #endregion

        #endregion

        #region Selection Sort
        public static void SelectorSort1(int[] nums)
        {
            // 外层范围，内层取minIdx
            for (int i = 0; i < nums.Length - 1; i++)
            {// i from 0 end with 6: 7 numbers
                int minIdx = i;
                // j from 1 end with 7: 7 numbers
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] < nums[minIdx])
                    {
                        minIdx = j;
                    }
                }
                if (i != minIdx)
                {
                    int temp = nums[i];
                    nums[i] = nums[minIdx];
                    nums[minIdx] = temp;
                }
            }

        }
        #endregion

        #region Insert Sort
        /// <summary>
        /// 插入排序 对于前i-1个已经有序的情况下，将i插入合适的位置
        /// </summary>
        /// <param name="nums"></param>
        public static void InsertSort(int[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                int tmp = nums[i];
                int j = i - 1; // j 是 i前面的一个数
                while (j >= 0 && tmp <= nums[j])
                {
                    nums[j + 1] = nums[j];
                    j--;
                }
                nums[j + 1] = tmp;
            }
        }
        #endregion

        #region Shell Sort
        public static void ShellSort(int[] array)
        {
            // 希尔排序的增量
            int d = array.Length;
            while (d > 1)
            {
                // 使用希尔增量的方式，即每次折半
                d = d / 2;
                for (int x = 0; x < d; x++)
                {
                    for (int i = x + d; i < array.Length; i = i + d)
                    {
                        int temp = array[i];
                        int j;
                        for (j = i - d; (j >= 0) && (array[i] > temp); j = j - d)
                        {
                            array[j + d] = array[j];
                        }
                        array[j + d] = temp;
                    }
                }
            }
        }
        #endregion

        #region Merge Sort
        public static void MergeSort(int[] nums, int start, int end)
        {
            if (start == end) return;

            // 折半成两个独立的小集合
            int mid = (start + end) / 2;
            MergeSort(nums, start, mid);
            MergeSort(nums, mid + 1, end);
            // 把两个有序小集合，归并成一个大集合
            Merge(nums, start, mid, end);

        }

        private static void Merge(int[] nums, int start, int mid, int end)
        {
            // 开辟额外大集合，设置指针
            int[] tmp = new int[end - start + 1];
            int p = 0;
            int l = start;
            int r = mid + 1;

            // 比较两个小集合的元素，依次放入大集合
            while ((l <= mid) && (r <= end))
            {
                if (nums[l] <= nums[r]) tmp[p++] = nums[l++];

                else tmp[p++] = nums[r++];

            }
            // 处理剩余元素
            while (l <= mid) tmp[p++] = nums[l++];

            while (r <= end) tmp[p++] = nums[r++];

            // 把大集合的元素复制回原数组
            for (int i = start; i < end + 1; i++)
                nums[i] = tmp[i - start];
        }
        #endregion

        #region Quick Sort
        public static void QuickSort1(int[] nums, int start, int end)
        {
            if (start >= end) return;

            int pivot = Partition1(nums, start, end);
            QuickSort1(nums, start, pivot - 1);
            QuickSort1(nums, pivot + 1, end);
        }
        // 返回基点
        private static int Partition1(int[] nums, int start, int end)
        {
            // 基点
            int pivot = start;
            int j = start + 1;
            // i,j 前后指针，小于等于pivot的值，与nums[j]交换
            for (int i = start + 1; i <= end; i++)
            {
                if (nums[i] <= nums[pivot])
                {
                    int y = nums[j];
                    nums[j] = nums[i];
                    nums[i] = y;

                    j++;
                }
            }
            // 与基点交换值
            int x = nums[pivot];
            nums[pivot] = nums[j - 1];
            nums[j - 1] = x;
            // 基点位置
            pivot = j - 1;
            return pivot;
        }
        #endregion

        #region Heap Sort ???
        public static void downAdjust(int[] array, int parentIndex, int length)
        {
            // temp 保存父节点值，用于最后的赋值
            int temp = array[parentIndex];
            int childIndex = 2 * parentIndex + 1;
            while (childIndex < length)
            {
                // 如果有右孩子，且右孩子大于左孩子的值，则定位到右孩子
                if (childIndex + 1 < length && array[childIndex + 1] > array[childIndex])
                {
                    childIndex++;
                }
                // 如果父节点小于任何一个孩子的值的值，则直接跳出
                if (temp >= array[childIndex])
                    break;
                // 无需真正交换，单向赋值即可
                array[parentIndex] = array[childIndex];
                parentIndex = childIndex;
                childIndex = 2 * childIndex + 1;
            }
            array[parentIndex] = temp;

        }
        public static void HeapSort(int[] array)
        {
            // 从最后一个非叶子节点开始，依次下沉调整
            for (int i = (array.Length - 2) / 2; i >= 0; i--)
            {
                downAdjust(array, i, array.Length - 1);
            }
            WriteLine(string.Join(",", array));
            // 循环删除堆顶元素，移到集合尾部，调整堆产生新的堆顶
            for (int i = array.Length - 1; i > 0; i--)
            {
                // 最后1个元素和第1个元素交换
                int temp = array[i];
                array[i] = array[0];
                array[0] = temp;
                // 下沉 调整最大堆
                downAdjust(array, 0, i);
            }
            WriteLine(string.Join(",", array));
        }
        #endregion

        #region Count Sort

        public static int[] CountSort1(int[] array)
        {
            // 1. 得到数列的最大值
            int max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            // 2. 根据数列最大值确定统计数组的长度
            int[] countArray = new int[max + 1];
            // 3. 遍历数列， 填充统计数组
            for (int i = 0; i < array.Length; i++)
            {
                countArray[array[i]]++;
            }
            // 4. 遍历统计数组，输出结果
            int index = 0;
            int[] sortedArray = new int[array.Length];
            for (int i = 0; i < countArray.Length; i++)
            {
                for (int j = 0; j < countArray[i]; j++)
                {
                    sortedArray[index++] = i;
                }
            }
            return sortedArray;
        }

        // 计数排序的优化 1.增加偏移量，解决空间浪费问题； 2. 统计数组变形， 成为稳定排序
        public static int[] CountSort2(int[] array)
        {
            // 1. 得到数列的最大值和最小值，并计算差值d
            int max = array[0];
            int min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
                if (array[i] < min)
                {
                    min = array[i];
                }
            }

            int d = max - min;
            // 2. 创建统计数组并统计对应元素的个数
            int[] countArray = new int[d + 1];
            for (int i = 0; i < array.Length; i++)
            {
                countArray[array[i] - min]++;
            }

            // 3. 统计数组做变形，后面的元素等于前面的元素之和
            for (int i = 1; i < countArray.Length; i++)
            {
                countArray[i] += countArray[i - 1];
            }
            // 4. 倒序遍历原始数列，从统计数组找到正确位置，输出到结果数组
            int[] sortedArray = new int[array.Length];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                sortedArray[countArray[array[i] - min] - 1] = array[i];
                countArray[array[i] - min]--;
            }
            return sortedArray;
        }
        #endregion

        #region Bucket Sort
        public static double[] bucketSort(double[] array)
        {
            // 1. 得到数列的最大值和最小值，并算出差值d
            double max = array[0];
            double min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
                if (array[i] < min)
                {
                    min = array[i];
                }
            }
            double d = max - min;
            // 2. 初始化桶
            int bucketNum = array.Length;
            List<List<double>> bucketList = new List<List<double>>(bucketNum);
            for (int i = 0; i < bucketNum; i++)
            {
                bucketList.Add(new List<double>());
            }

            // 3. 遍历原始数组，将每个元素放入桶中
            for (int i = 0; i < array.Length; i++)
            {
                int num = (int)((array[i] - min) * (bucketNum - 1) / d);
                bucketList[num].Add(array[i]);
            }
            // 4. 对每个桶内部进行排序
            for (int i = 0; i < bucketList.Count; i++)
            {
                bucketList[i].Sort();
            }
            // 5. 输出全部元素
            double[] sortedArray = new double[array.Length];
            int index = 0;
            foreach (var list in bucketList)
            {
                foreach (var item in list)
                {
                    sortedArray[index] = item;
                    index++;
                }
            }

            return sortedArray;
        }
        #endregion

        #region RadixSort

        public const int ASCII_RANGE = 128;
        // 计数排序
        public static string[] radixSort(string[] array, int maxLength)
        {
            // 排序结果数组，用于存储每一次按位排序的临时结果
            string[] sortedArray = new string[array.Length];
            // 从个位开始比较，一致比较到最高位
            for (int k = maxLength - 1; k >= 0; k--)
            {
                // 计数排序的过程，分成3步:
                // 1. 创建辅助排序的统计数组，并把待排序的字符对号入座
                // 这里为了代码简洁，直接使用ASCII代码范围作为数组长度
                int[] count = new int[ASCII_RANGE];
                for (int i = 0; i < array.Length; i++)
                {
                    int index = getCharIndex(array[i], k);
                    count[index]++;
                }
                // 2. 统计数组做变形，后面的元素等于前面的元素之和
                for (int i = 1; i < count.Length; i++)
                {
                    count[i] = count[i] + count[i - 1];
                }
                // 3. 倒序遍历原始数列，从统计数组找到正确位置，输出到结果数组
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    int index = getCharIndex(array[i], k);
                    int sortedIndex = count[index] - 1;
                    sortedArray[sortedIndex] = array[i];
                    count[index]--;
                }
                // 下一轮排序需要以上一轮的排序结果为基础，因此把结果复制给array
                array = (string[])sortedArray.Clone();
            }
            return array;
        }
        // 获取字符串第k位字符锁对应的ASCII码序号
        private static int getCharIndex(string str, int k)
        {
            // 如果字符串长度小于k，直接返回0，相当于给不存在的位置补0
            if (str.Length < (k + 1))
                return 0;

            return str[k];
        }

        #endregion
        static void Main(string[] args)
        {
            #region Sep
            {
                // selection sort
                int[] nums = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                Sort.SelectorSort(nums);
                WriteLine("SelectionSort:" + string.Join(",", nums));
            }
            {
                // bubble sort
                int[] nums = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                Sort.BubbleSort(nums);
                WriteLine("BubbleSort: " + string.Join(",", nums));

            }
            {
                int[] nums = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                Sort.InsertSort(nums);
                WriteLine("InsertSort: " + string.Join(",", nums));
            }
            {
                int[] nums = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                Sort.MergeSort(nums, 0, nums.Length - 1);
                WriteLine("MergeSort: " + string.Join(",", nums));
            }
            {
                int[] nums = new int[] { 5, 6, 2, 7, 3, 1 };
                Sort.QuickSort(nums, 0, nums.Length - 1);
                WriteLine("QuickSort: " + string.Join(",", nums));
            }
            {
                //int[] nums = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                int[] nums = new int[] { 5, 2, 3, 1 };
                Sep.SelectionSort(nums);
                WriteLine("Sep.SelectionSort: " + string.Join(",", nums));
            }
            {
                int[] nums = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                // int[] nums = new int[] { 1, 2, 3, 4 };
                Sep.BubbleSort(nums);
                WriteLine("Sep.BubbleSort: " + string.Join(",", nums));
            }
            {
                int[] nums = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                // int[] nums = new int[] { 1, 2, 3, 4 };
                Sep.InsertSort(nums);
                WriteLine("Sep.InsertSort: " + string.Join(",", nums));
            }
            {

                int[] nums = new int[] { 2, 7, 4, 1, 8, 1 };
                // int[] nums = new int[] { 1, 2, 3, 4 };
                Sort.CountingSort(nums);
                WriteLine("Sort.CountingSort: " + string.Join(",", nums));
            }
            {
#if Release
                int[] array1 = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                // BubbleSort1(array);
                BubbleSort(array1);

                int[] array = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                // BubbleSort1(array);
                BubbleSort2(array);

                int[] array3 = new int[] { 3, 4, 2, 1, 5, 6, 7, 8 };
                BubbleSort3(array3);

                int[] array4 = new int[] { 2, 3, 4, 5, 6, 7, 8, 1 };
                BubbleSort4(array4);

                // int[] array5 = new int[] { 5, 8 }
                int[] array5 = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                SelectorSort1(array5);

                int[] array6 = new int[] { 5, 6, 8, 3, 9, 2, 1, 7 };
                InsertSort(array6);

                int[] array7 = new int[] { 5, 8, 6, 3, 9, 2, 1, 7 };
                MergeSort(array7, 0, array.Length - 1);

                int[] array8 = new int[] { 4, 4, 6, 5, 3, 2, 8, 1 };
                QuickSort1(array8, 0, array8.Length - 1);

                int[] array9 = new int[] { 4, 3, 7, 5, 6, 2, 8, 1 };
                QuickSort2(array9, 0, array9.Length - 1);

                int[] array10 = new int[] { 9, 3, 5, 4, 9, 1, 2, 7, 8, 1, 3, 6, 5, 3, 4, 0, 10, 9, 7, 9 };
                CountSort1(array10);

                int[] array11 = new int[] { 9, 3, 5, 4, 9, 1, 2, 7, 8, 1, 3, 6, 5, 3, 4, 0, 10, 9, 7, 9 };
                CountSort2(array11);

                double[] array12 = new double[] { 4.12, 6.421, 0.0023, 3.0, 2.123, 8.122, 4.12, 10.09 };
                bucketSort(array12);

                int[] array13 = new int[] { 1, 3, 2, 6, 5, 7, 8, 9, 10, 0 };
                HeapSort(array13);

                string[] array14 = { "qd", "abc", "qwe", "hhh", "a", "cws", "ope" };
                WriteLine(string.Join(",", radixSort(array14, 3))); 
#endif
            }
            {
                int[] nums = { 8, 19, 4, 2, 15, 3 };
                Sep.FindFinalValue(nums, 2);
            }
            {
                ListNode head = new ListNode(0);
                ListNode node1 = new ListNode(1);
                ListNode node2 = new ListNode(2);
                ListNode node3 = new ListNode(3);
                ListNode node4 = new ListNode(4);
                head.next = node1;
                node1.next = node2;
                node2.next = node3;
                node3.next = node4;
                Sep.ReverseList(head);
            }
            {
                ListNode head = new ListNode(0);
                ListNode node1 = new ListNode(1);
                ListNode node2 = new ListNode(2);
                ListNode node3 = new ListNode(3);
                ListNode node4 = new ListNode(4);
                head.next = node1;
                node1.next = node2;
                node2.next = node3;
                node3.next = node4;

                ListNode newHead = null;
                Sep.DoReverse(head, ref newHead);

            }
            {
                string s = "HOW ARE YOU";
                Sep.PrintVertically(s);
            }
            {
                Sep.Multiply("123", "456");
            }
            {
                int[][] board = new int[4][];
                board[0] = new int[3] { 0, 1, 0 };
                board[1] = new int[3] { 0, 0, 1 };
                board[2] = new int[3] { 1, 1, 1 };
                board[3] = new int[3] { 0, 0, 0 };
                Sep.GameOfLife(board);
            }
            {
                int[] nums = { };
                Sep.Massage(nums);
            }
            {
                Sep.Maximum69Number(9669);
            }
            {
                Sep.PartitionString("aaa");
            }
            {
                int[] nums = { 1, 1, 1, 6, 1, 1, 1 };
                Sep.MaxDistance(nums);
            }
            {

                Sep.LargestOddNumber("52");
            }
            {
                int[][] grid = new int[3][];
                grid[0] = new int[] { 59, 88, 44 };
                grid[1] = new int[] { 3, 18, 38 };
                grid[2] = new int[] { 21, 26, 51 };
                Sep.MaxIncreaseKeepingSkyline(grid);
            }
            {
                Sep.BalancedStringSplit("RLRRLLRLRL");
            }
            {
                int[] nums = new int[] { 1, 5, 2, 4, 1 };
                Sep.MinOperations(nums);
            }
            {
                string s = "bza";
                Sep.MinTimeToType(s);
            }
            {
                int[] nums = new int[] { 3, 2, 4, 1 };
                Sep.PancakeSort(nums);
            }
            {
                int[] nums = new int[] { 1, 4, 3, 2 };
                Sep.ArrayPairSum(nums);

            }
            {
                Sep.MinimumMoves("OXOX");
            }
            {
                int[] nums = { 1, 0, 0, 0, 1, 0, 0 };
                Sep.CanPlaceFlowers(nums, 2);
            }
            {
                int[] nums = new int[] { 1, -1, 1, -1 };
                Sep.CanThreePartsEqualSum(nums);
            }
            {
                Sep.MinAddToMakeValid(@"()))((");
            }
            {

                Sep.MinFlips("10111");
            }
            {
                int[] nums = new int[] { 3, 5, 4, 2, 4, 6 };
                Sep.MinPairSum(nums);
            }
            {
                int[] nums = new int[] { 1, 2, 1, 10 };
                Sep.LargestPerimeter(nums);
            }
            {
                int[] g = new int[] { 1, 2 };
                int[] s = new int[] { 1, 2, 3 };
                Sep.FindContentChildren(g, s);
            }
            {

                Sep.ValidPalindrome("aguokepatgbnvfqmgmlcupuufxoohdfpgjdmysgvhmvffcnqxjjxqncffvmhvgsymdjgpfdhooxfuupuculmgmqfvnbgtapekouga");
            }
            {
                int[] nums = new int[] { 3, 2, 2, 1 };
                Sep.NumRescueBoats(nums, 3);
            }
            {
                //nums
                //Sep.MinIncrementForUnique(nums);
            }
            {
                char[] s = new char[] { 'h', 'e', 'l', 'l', 'o' };
                Sep.ReverseString(s);
            }

            {
                string s = "abc", t = "ahbgdc";
                Sep.IsSubsequence(s, t);
            }
            {
                int[] nums = new int[] { 2, 7, 11, 15 };

                Sep.TwoSum(nums, 9);
            }
            {
                Sep.LengthOfLongestSubstring("pwwkew");
            }
            {
                //Sep.CountFairPairs(nums, -1000000000, 1000000000);
            }
            {
                string[] words = new string[] { "abc", "car", "ada", "racecar", "cool" };
                Sep.FirstPalindrome(words);
            }
            {
                int[] nums = new int[] { 5, 6, 5 };
                Sep.PairSums(nums, 11);
            }
            {
                Sep.ReversePrefix("abcd", 'z');
            }
            {
                Sep.CountSegments("Hello, my name is John");
            }
            {
                int[] nums = new int[] { 0, 1, 0, 3, 12 };
                Sep.MoveZeroes(nums);
            }
            {
                ListNode node1 = new ListNode(3);
                ListNode node2 = new ListNode(2);
                ListNode node3 = new ListNode(0);
                ListNode node4 = new ListNode(4);
                node1.next = node2;
                node2.next = node3;
                node3.next = node4;
                node4.next = node2;
                Sep.HasCycle(node1);
            }
            {
                ListNode node1 = new ListNode(3);
                ListNode node2 = new ListNode(2);
                ListNode node3 = new ListNode(0);
                ListNode node4 = new ListNode(4);
                node1.next = node2;
                node2.next = node3;
                node3.next = node4;
                node4.next = node2;
                Sep.DetectCycle(node1);
            }
            {
                int[] nums = new int[] { 3, 1, 3, 4, 3 };
                Sep.MaxOperations(nums, 6);
            }
            {
                int[] nums = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
                Sep.MaxArea(nums);
            }
            {
                string[] dic = { "abe", "abc" };
                Sep.FindLongestWord("abce", dic);
            }
            {
                int[] nums = { -1, 0 };
                Sep.TwoSum2(nums, -1);
            }
            {
                int[] nums = { 0, 1, 1, 1, 0, 1, 1, 0, 1 };
                Sep.LongestSubarray(nums);
            }
            {
                int[] nums = { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0 };
                Sep.LongestOnes(nums, 2);
            }
            {

                Sep.MaxVowels("abciiidef", 3);
            }
            {
                int[] nums = { 1, 1, 1, 1, 1, 1, 1, 1 };
                Sep.MinSubArrayLen(11, nums);
            }
            {
                Sep.CheckInclusion("ab", "eidboaoo");
            }
            {
                Sep.FindAnagrams("cbaebabacd", "abc");
            }
            {
                int[] nums = { 10, 5, 2, 6 };
                Sep.NumSubarrayProductLessThanK(nums, 100);
            }
            {
                int[] nums = { 1, 2, 3, 4 };
                Sep.RunningSum(nums);
            }
            {
                int[] nums = { 2, 3, -1, 8, 4 };
                Sep.FindMiddleIndex(nums);
            }
            {
                int[] nums = { 1, 3, 4, 8 };
                int[][] queries = new int[4][];
                queries[0] = new int[] { 0, 1 };
                queries[1] = new int[] { 1, 2 };
                queries[2] = new int[] { 0, 3 };
                queries[3] = new int[] { 3, 3 };
                Sep.XorQueries(nums, queries);
            }
            {
                int[] nums = { -2 };

                Sep.SubarraysDivByK(nums, 6);
            }
            {
                int[] nums = { 1, 2, 3, 4 };
                Sep.printCycleNTimes(nums, 3);
            }
            {
                int[] nums = { 0, 1, 1, 1, 0, 0, 1, 1, 0 };
                Sep.MinSwaps(nums);
            }
            {
                int[] nums = { 1, 2, 2, 3, 1, 4, 2 };
                Sep.FindShortestSubArray(nums);
            }
            {
                int[] nums = { 1, 2, 2, 3, 1, 4, 2 };

            }
            {
                int[] nums = { 1, 2, 0, 4, 5 };
                Sep.ConstructArr(nums);
            }
            {
                int[] nums = { -5, 1, 5, 0, -7 };
                Sep.LargestAltitude(nums);
            }
            {
                int[] nums = { -3, 6, 2, 5, 8, 6 };
                Sep.MinStartValue(nums);
            }
            {
                int[] nums = { 1, 4, 2, 5, 3 };
                Sep.SumOddLengthSubarrays(nums);
            }
            {
                int[] nums = { 1, -1, 0 };
                Sep.SubarraySum(nums, 0);
            }
            {
                int[] nums = { 23, 2, 4, 6, 7 };
                Sep.CheckSubarraySum(nums, 6);
            }
            {
                int[] nums = { 1, 2, 3, 4 };
                Sep.RangeSum(nums, 4, 1, 5);
            }
            {
                int[] nums = { 2, 2, 2 };
                Sep.MaxScore(nums, 2);
            }
            {
                int[] nums = { 1, 4, 6, 8, 10 };
                Sep.GetSumAbsoluteDifferences(nums);
            }
            {

                Sep.MaximumSubsequenceCount("abdcdbc", "ac");
            }
            {
                int[] nums = { 0, 0, 0, 0, 0 };
                Sep.NumSubarraysWithSum(nums, 0);
            }
            {
                int[] nums = { 10, 5, 10, 10 };
                Sep.GetDistances(nums);
            }
            {
                int[] nums = { 1, 1, 1 };
                Sep.SubarraySum1(nums, 2);
            }
            {
                int[] nums = { 2, 2, 2, 1, 2, 2, 1, 2, 2, 2 };
                Sep.NumberOfSubarrays(nums, 2);
            }
            {
                int[] citations = { 1, 2, 100 };
                Sep.HIndex(citations);
            }
            {
                int[] nums = { 7, 8 };
                BSearchRangeL.SearchRange(nums, 7);
            }
            {
                int[] nums = { 1 };
                BSearchInsertL.SearchInsert(nums, 0);
            }
            {
                int[][] mat = new int[2][];
                mat[0] = new int[2] { 0, 1 };
                mat[1] = new int[2] { 1, 0 };
                Sep.RowAndMaximumOnes(mat);
            }
            {
                int[][] mat = new int[3][];
                mat[0] = new int[3] { 1, 1, 0 };
                mat[1] = new int[3] { 1, 0, 1 };
                mat[2] = new int[3] { 0, 0, 0 };
                Sep.FlipAndInvertImage(mat);
            }
            {
                int[][] ints = new int[19][];
                ints[0] = new int[19] { 15, 7, 18, 11, 19, 10, 14, 16, 8, 2, 3, 6, 5, 1, 17, 12, 9, 4, 13 };
                ints[1] = new int[19] { 17, 15, 9, 8, 11, 13, 7, 6, 5, 1, 3, 16, 12, 19, 10, 2, 4, 14, 18 };
                ints[2] = new int[19] { 19, 14, 12, 10, 8, 9, 17, 16, 4, 3, 13, 18, 1, 5, 7, 11, 2, 15, 6 };
                ints[3] = new int[19] { 4, 2, 10, 15, 19, 16, 8, 9, 5, 3, 1, 11, 13, 14, 6, 18, 12, 17, 7 };
                ints[4] = new int[19] { 13, 19, 9, 16, 5, 8, 6, 12, 14, 11, 18, 10, 7, 2, 3, 4, 15, 17, 1 };
                ints[5] = new int[19] { 4, 7, 18, 11, 17, 16, 5, 12, 10, 1, 15, 13, 14, 6, 19, 2, 3, 9, 8 };
                ints[6] = new int[19] { 14, 5, 15, 1, 18, 6, 12, 7, 8, 9, 3, 13, 2, 10, 19, 4, 11, 16, 17 };
                ints[7] = new int[19] { 10, 3, 1, 8, 14, 19, 11, 18, 15, 13, 9, 12, 16, 17, 7, 4, 5, 2, 6 };
                ints[8] = new int[19] { 14, 13, 19, 18, 7, 2, 4, 8, 10, 17, 12, 5, 15, 1, 6, 9, 11, 3, 16 };
                ints[9] = new int[19] { 19, 8, 10, 18, 16, 12, 11, 17, 4, 9, 7, 2, 5, 13, 15, 3, 6, 1, 14 };
                ints[10] = new int[19] { 1, 10, 6, 14, 7, 18, 3, 9, 4, 16, 5, 11, 13, 17, 15, 8, 19, 2, 12 };
                ints[11] = new int[19] { 13, 10, 5, 16, 1, 19, 17, 3, 9, 11, 7, 8, 12, 6, 4, 2, 14, 15, 18 };
                ints[18] = new int[19] { 17, 2, 1, 6, 9, 19, 18, 14, 4, 11, 12, 13, 16, 5, 8, 7, 3, 10, 15 };
                ints[12] = new int[19] { 1, 4, 10, 5, 13, 6, 18, 11, 3, 2, 15, 14, 16, 12, 17, 19, 8, 9, 7 };
                ints[13] = new int[19] { 2, 14, 3, 12, 16, 17, 11, 9, 1, 6, 5, 19, 10, 13, 4, 18, 7, 15, 8 };
                ints[14] = new int[19] { 15, 9, 8, 18, 14, 13, 4, 12, 5, 17, 6, 1, 11, 16, 19, 3, 7, 2, 10 };
                ints[15] = new int[19] { 15, 8, 12, 16, 13, 2, 6, 19, 18, 14, 10, 5, 11, 9, 7, 1, 3, 17, 4 };
                ints[16] = new int[19] { 15, 6, 17, 7, 5, 3, 1, 9, 19, 12, 10, 11, 16, 14, 18, 8, 2, 13, 4 };
                ints[17] = new int[19] { 6, 11, 10, 14, 2, 13, 16, 1, 9, 15, 8, 19, 17, 3, 5, 18, 7, 4, 12 };
                Sep.CheckValid1(ints);
            }
            {
                Sep.GenerateMatrix(3);
            }
            {

                TreeNode treeNode1 = new TreeNode();
                treeNode1.val = 1;
                treeNode1.myIndex = 1;
                TreeNode treeNode2 = new TreeNode();
                treeNode2.val = 1;
                treeNode2.myIndex = 2;
                TreeNode treeNode3 = new TreeNode();
                treeNode3.val = 1;
                treeNode3.myIndex = 3;
                treeNode1.left = treeNode2;
                treeNode1.right = treeNode3;

                TreeNode treeNode11 = new TreeNode();
                treeNode11.val = 1;
                treeNode11.myIndex = 4;
                TreeNode treeNode21 = new TreeNode();
                treeNode21.val = 1;
                treeNode21.myIndex = 5;
                TreeNode treeNode31 = new TreeNode();
                treeNode31.val = 1;
                treeNode31.myIndex = 6;
                treeNode11.left = treeNode21;
                treeNode11.right = treeNode31;

                TreeNode root = new TreeNode();
                root.val = 1;
                root.myIndex = 0;
                root.left = treeNode1;
                root.right = treeNode11;

                TreeQuestion tq = new TreeQuestion();
                tq.IsUnivalTree(root);
            }
            {
                TreeNode treeNode1 = new TreeNode();
                treeNode1.val = 1;
                treeNode1.myIndex = 1;
                TreeNode treeNode2 = new TreeNode();
                treeNode2.val = 2;
                treeNode2.myIndex = 2;
                TreeNode treeNode3 = new TreeNode();
                treeNode3.val = 3;
                treeNode3.myIndex = 3;
                treeNode1.left = treeNode2;
                treeNode1.right = treeNode3;

                TreeNode treeNode11 = new TreeNode();
                treeNode11.val = 5;
                treeNode11.myIndex = 4;


                TreeNode treeNode16 = new TreeNode();
                treeNode16.val = 6;

                TreeNode treeNode17 = new TreeNode();
                treeNode17.val = 7;

                treeNode2.right = treeNode11;
                treeNode3.left = treeNode16;
                treeNode3.right = treeNode17;
                TreeQuestion tq = new TreeQuestion();
                tq.BinaryTreePaths(treeNode1);
            }
            {
                TreeNode treeNode1 = new TreeNode();
                treeNode1.val = 1;
                TreeNode treeNode2 = new TreeNode();
                treeNode2.val = 2;
                TreeNode treeNode3 = new TreeNode();
                treeNode3.val = 3;
                treeNode1.left = treeNode2;
                treeNode1.right = treeNode3;

                TreeNode treeNode11 = new TreeNode();
                treeNode11.val = 1;
                TreeNode treeNode21 = new TreeNode();
                treeNode21.val = 2;
                TreeNode treeNode31 = new TreeNode();
                treeNode31.val = 3;
                treeNode1.left = treeNode21;
                treeNode1.right = treeNode31;

                TreeQuestion tq = new TreeQuestion();
                tq.IsSameTree(treeNode1, treeNode11);
            }
            {
                TreeNode treeNode1 = new TreeNode();
                treeNode1.val = 2;
                treeNode1.myIndex = 1;
                TreeNode treeNode2 = new TreeNode();
                treeNode2.val = 3;
                treeNode2.myIndex = 2;
                TreeNode treeNode3 = new TreeNode();
                treeNode3.val = 4;
                treeNode3.myIndex = 3;
                treeNode1.left = treeNode2;
                treeNode1.right = treeNode3;

                TreeNode treeNode11 = new TreeNode();
                treeNode11.val = 5;
                treeNode11.myIndex = 4;
                //TreeNode treeNode21 = new TreeNode();
                //treeNode21.val = 1;
                //treeNode21.myIndex = 5;
                TreeNode treeNode31 = new TreeNode();
                treeNode31.val = 6;
                treeNode31.myIndex = 6;
                //treeNode11.left = treeNode21;
                treeNode11.right = treeNode31;

                TreeNode root = new TreeNode();
                root.val = 1;
                root.myIndex = 0;
                root.left = treeNode1;
                root.right = treeNode11;
                TreeQuestion tq = new TreeQuestion();
                tq.Flatten(root);

            }
            {


                TreeNode treeNode1 = new TreeNode();
                treeNode1.val = 2;
                treeNode1.myIndex = 1;
                TreeNode treeNode2 = new TreeNode();
                treeNode2.val = 4;
                treeNode2.myIndex = 2;
                TreeNode treeNode3 = new TreeNode();
                treeNode3.val = 5;
                treeNode3.myIndex = 3;
                treeNode1.left = treeNode2;
                treeNode1.right = treeNode3;

                TreeNode treeNode11 = new TreeNode();
                treeNode11.val = 3;
                treeNode11.myIndex = 4;
                //TreeNode treeNode21 = new TreeNode();
                //treeNode21.val = 6;
                //treeNode21.myIndex = 5;
                TreeNode treeNode31 = new TreeNode();
                treeNode31.val = 7;
                treeNode31.myIndex = 6;
                //treeNode11.left = treeNode21;
                treeNode11.right = treeNode31;


                TreeNode treeNode41 = new TreeNode();
                treeNode41.val = 8;
                treeNode41.myIndex = 7;
                treeNode2.left = treeNode41;

                TreeNode root = new TreeNode();
                root.val = 1;
                root.myIndex = 0;
                root.left = treeNode1;
                root.right = treeNode11;

                TreeQuestion tq = new TreeQuestion();
                tq.ListOfDepth(root);

            }
            {
                BackTrackQuestion qt = new BackTrackQuestion();
                int[] nums = new int[3] { 1, 2, 3 };
                qt.Subsets(nums);
            }
            {
                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                int[] nums = { 1, 2, 3 };
                backTrackQuestion.Subsets1(nums);
            }
            {

                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                int[] nums = { 2, 3, 6, 7 };
                backTrackQuestion.CombinationSum(nums, 7);
            }
            {

                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                int[] nums = { 10, 1, 2, 7, 6, 1, 5 };
                backTrackQuestion.CombinationSum2(nums, 8);
            }
            {
                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                backTrackQuestion.CombinationSum3(9, 45);
            }
            //{
            //    BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
            //    int[] nums = { 1, 2, 3 };
            //    backTrackQuestion.CombinationSum4(nums, 4);
            //}
            {
                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                int[] nums = { 4, 2, 1 };
                backTrackQuestion.CombinationSum4(nums, 32);
            }
            {
                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                backTrackQuestion.GenerateParenthesis(3);

            }
            {
                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                int[] nums = { 1, 2, 3 };
                backTrackQuestion.Permute1(nums);
            }
            {
                BackTrackQuestion backTrackQuestion = new BackTrackQuestion();
                int[] nums = { 1, 2, 3 };
                backTrackQuestion.PermuteUnique(nums);
            }
            {
                Sep.StrToInt("-91283472332");
            }
            {
                Sep.numDecodings("100");
            }
            {
                int[] nums = { 2, 0, 0 };
                Sep.CanJump(nums);
            }
            {
                int[] nums = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
                Sep.MaxSubArray(nums);
            }
            {
                int[] nums = { 1, -2, 0, 3 };
                Sep.MaximumSum(nums);
            }
            {
                int[] nums = { 1, 2, 3, 5, -6, 4, 0, 10 };
                Sep.GetMaxLen(nums);
            }
            {
                int[] nums = { 10, 9, 2, 5, 3, 7, 101, 18 };
                Sep.LengthOfLIS(nums);
            }
            {
                int[] nums = { 1, 3, 5, 4, 7 };
                Sep.FindNumberOfLIS(nums);
            }
            {
                int[] nums = { 1, 2, 5 };
                Sep.CanPartition(nums);
            }
            {
                string[] strs = { "10", "0001", "111001", "1", "0" };
                Sep.FindMaxForm(strs, 5, 3);
            }
            {
                int[] nums = { 2, 7, 4, 1, 8, 1 };
                Sep.LastStoneWeightII(nums);
            }
            {
                Sep.WaysToChange(5);
            }
            {
                int[] nums = { 1, 2, 5 };

                Sep.CoinChange1(nums, 11);
            }
            {

                int[][] mat = new int[4][];
                mat[0] = new int[] { 10, 3, 7, 7, 9, 6, 9, 8, 9, 5 };
                mat[1] = new int[] { 1, 1, 6, 8, 6, 7, 7, 9, 3, 9 };
                mat[2] = new int[] { 3, 4, 4, 1, 3, 6, 3, 3, 9, 9 };
                mat[3] = new int[] { 6, 9, 9, 3, 8, 7, 9, 6, 10, 6 };
                Sep.MinimizeTheDifference(mat, 5);
            }
            {
                int[][] mat = new int[2][];
                mat[0] = new int[] { -2147483646, -2147483645 };
                mat[1] = new int[] { 2147483646, 2147483647 };
                Sep.FindMinArrowShots(mat);
            }
            {
                int[] nums = new int[3];
                nums.Sum();
                nums.Average();
                foreach (var item in nums)
                {

                }
                nums.Max();
                nums.Min();
                int size = nums.Length;
                nums.Count();
                //int[] nums1 = new int[3] {3,...};
            }



            #endregion

            Oct oct = new Oct();
            {
                int[] nums = { 4, 2, 1 };
                oct.CheckPossibility(nums);
            }
            {
                int[] nums = { 1, 2, 3, 4, 5 };
                oct.MaximizeSum(nums, 3);
            }
            {
                oct.ReplaceSpaces("Mr John Smith    ", 13);
            }
            {
                char[][] mat = new char[9][];
                mat[0] = new char[] { '.', '8', '7', '6', '5', '4', '3', '2', '1' };
                mat[1] = new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
                mat[2] = new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
                mat[3] = new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
                mat[4] = new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
                mat[5] = new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
                mat[6] = new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
                mat[7] = new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
                mat[8] = new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };
                oct.IsValidSudoku(mat);
            }
            {
                char[][] board = new char[9][];
                board[0] = new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
                board[1] = new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
                board[2] = new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
                board[3] = new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
                board[4] = new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
                board[5] = new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
                board[6] = new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
                board[7] = new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
                board[8] = new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };
                oct.SolveSudoku(board);
                WriteLine();
                foreach (var row in board)
                {
                    WriteLine(string.Join(",", row));
                }

            }
            {
                MonotonicStack monotonicStack = new MonotonicStack();
                int[] nums = { 2, 1, 2, 4, 3 };
                monotonicStack.MonotonicStackTemplate(nums);
            }
            {
                MonotonicQueue monotonicQ = new MonotonicQueue();
                int[] nums = { 3, 1, 2, 5, 4 };
                monotonicQ.MaxSlidingWindow(nums, 3);
            }
            {
                // -4,-1,-1,0,1,2,4
                int[] nums = { -1, 0, 0, 1, 9, 2, -1, -4, 4, -4, 4 };
                oct.TwoSumWithoutDuplicateAns(nums, 0);
            }
            {
                oct.GetDuplicateNums(null);
            }
            {
                oct.RemoveDuplicateNums(null); ;
            }
            {
                int[] nums = new int[] { 4, 5, 2, 3, 1, 6 };
                //QuickSort(nums, 0, nums.Length-1);
                WriteLine(string.Join(",", nums));
            }
            {

#if Release
                LinkedList<int> linkedList = new LinkedList<int>();
                var x = linkedList.First();
                if (3 == linkedList.First())
                {
                    WriteLine("==");
                }
#endif
            }
            {
                int[] nums = new int[] { 1, 2 };
                var head = CommonHelper.BuildLinkNode(nums);
                oct.RemoveNthFromEnd(head, 4);
            }
            {
                oct.dfs_DoAdd(5);
                oct.IterationAdd(5);

                oct.PrintReverse("hello world!".ToArray());
            }
            {

                oct.ReverseWords("the sky is blue");
            }
            {
                Program p = new Program();
                var list = p.SolveNQueens(4);
                foreach (var li in list)
                {
                    foreach (var item in li)
                    {
                        WriteLine(item);
                    }
                    WriteLine("------------");
                }
            }
            {

                for (int i = 0; i < 9; i++)
                {
                    WriteLine(i / 3 + "," + i % 3);
                    if (i % 3 == 2)
                    {
                        WriteLine("********");
                    }
                }
            }
            {
                int[] nums = { 3, 6, 4, 1 };
                CommonHelper.BuildLinkNode(nums);
                WriteLine(string.Join(",", oct.ReverseBookList(CommonHelper.BuildLinkNode(nums))));
            }
            {
                oct.MaxDepth("(1+(2*3)+((8)/4))+1");
            }
            {
                char[][] board = new char[9][];
                board[0] = new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
                board[1] = new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
                board[2] = new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
                board[3] = new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
                board[4] = new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
                board[5] = new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
                board[6] = new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
                board[7] = new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
                board[8] = new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };
                SolveSudoku(board);
                foreach (var row in board)
                {
                    WriteLine(string.Join(",", row));
                }

            }
            {
                // 环形
                int[] nums = { 1, 2, 3 };
                int n = nums.Length;
                for (int i = 0; i < 2 * n; i++)
                {
                    Console.Write(nums[i % n]);
                    if (i == n - 1)
                        Console.WriteLine();
                }
            }
            {

                int[] nums = new int[] { 4, 5, 7, 2, 3, 1, 6 };
                QuickSort(nums, 0, nums.Length - 1);
            }
            {
                int[] nums = new int[] { 4, 5, 7, 2, 3, 1, 6 };
                MergeSort1(nums, 0, nums.Length - 1);

                WriteLine(string.Join(",", nums));

            }
            {
                string path = @"/../";
                var ans = oct.SimplifyPath(path);

                WriteLine(ans);
            }
            {

                oct.IsValidB("()[]{}");
            }
            {
                string s = "-456";
                int p = int.Parse(s);
                int res = 0;
                foreach (var c in s)
                {
                    res = res * 10 + (c - '0');
                }
            }
            {
                oct.Calculate("(1+(4+5+2)-3)+(6+8)");
            }
            {

                oct.CanConstruct("aa", "aab");
            }
            {
                oct.IsIsomorphic("paper", "title");
            }
            {
                string[] arr = { "eat", "tea", "tan", "ate", "nat", "bat" };
                oct.GroupAnagrams(arr);
            }
            {
                int[] nums = new int[] { 1, 2, 3, 1 };
                oct.ContainsNearbyDuplicate(nums, 3);
            }
            {
                int[] nums = { 100, 4, 200, 1, 3, 2 };
                oct.LongestConsecutive(nums);
            }
            {
                int[] nums1 = { 1, 2, 3, 0, 0, 0 };
                int[] nums2 = { 2, 5, 6 };
                oct.Merge(nums1, 3, nums2, 3);
            }
            {
                int[] nums = { -1 };
                oct.Rotate(nums, 2);
            }
            {
                // 0,0,1,1,1,1,2,3,3
                int[] nums = { 0, 0, 1, 1, 1, 1, 2, 3, 3 };
                oct.RemoveDuplicates1(nums);
            }
            {
                int[] nums1 = { 1, 1, 2 };
                int[] nums2 = { 1, 2, 3 };
                oct.KSmallestPairs(nums1, nums2, 2);
            }
            {

                oct.IsPalindrome(10);
            }
            {

                oct.IsPalindrome("A man, a plan, a canal: Panama");
            }
            {
                var l1 = CommonHelper.BuildLinkNode(new int[] { 9, 9, 9, 9, 9, 9, 9 });
                var l2 = CommonHelper.BuildLinkNode(new int[] { 9, 9, 9, 9 });
                oct.AddTwoNumbers(l1, l2);
            }
            {
                var l1 = CommonHelper.BuildLinkNode(new int[] { 1, 2, 4 });
                var l2 = CommonHelper.BuildLinkNode(new int[] { 1, 3, 4 });
                oct.MergeTwoLists(l1, l2);
            }
            {

                var l1 = CommonHelper.BuildLinkNode(new int[] { 1, 2, 3, 4, 5 });
                oct.ReverseList1(l1);
            }
            {
                var l1 = CommonHelper.BuildLinkNode(new int[] { 1, 4, 3, 2, 5, 2 });
                oct.Partition(l1, 3);
            }

            {

                oct.LetterCombinations("234");
            }
            {
                WriteLine("左边界");
                int[] nums = { 1, 1, 2, 2, 3, 4 };
                Console.WriteLine(oct.bSearch_left_bound(nums, 3)); // 正常
                Console.WriteLine(oct.bSearch_left_bound(nums, 0)); // 未找到 超左部分
                Console.WriteLine(oct.bSearch_left_bound(nums, 6)); // 未找到 超右部分
                Console.WriteLine(oct.bSearch_left_bound(nums, 2)); // 左边界
                Console.WriteLine(oct.bSearch_left_bound(nums, 1)); // 左边界
            }
            {
                WriteLine("右边界");
                int[] nums = { 1, 1, 2, 2, 3, 4 };
                Console.WriteLine(oct.bSearch_right_bound(nums, 3)); // 正常  4
                Console.WriteLine(oct.bSearch_right_bound(nums, 0)); // 未找到 超左部分 -1
                Console.WriteLine(oct.bSearch_right_bound(nums, 6)); // 未找到 超右部分 -1
                Console.WriteLine(oct.bSearch_right_bound(nums, 2)); // 右边界 3
                Console.WriteLine(oct.bSearch_right_bound(nums, 1)); // 右边界 1


            }
            {

                int[] nums = { 1, 1 };
                oct.FindPeakElement(nums);
            }
            {
                int[] nums = new int[] { 1 };
                Console.WriteLine(oct.bSearch_right_bound(nums, 1)); // 右边界 1
            }

            {
                int[] nums = { -10, -3, 0, 5, 9 };

                oct.SortedArrayToBST(nums);
            }
            {
                int[] nums = { 1, 3, -1, -3, 5, 3, 6, 7 };
                oct.MaxSlidingWindow(nums, 3);
            }
            {
                //TreeNode node1 = new TreeNode(1);
                //TreeNode node2 = new TreeNode(2);
                //TreeNode node3 = new TreeNode(3);
                //TreeNode node4 = new TreeNode(4);
                //TreeNode node5 = new TreeNode(5);
                //node3.left = node1;
                //node3.right = node5;
                //node5.left = node4;
                //node4.left = node2;

                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                node1.left = node3;
                node3.right = node2;

                oct.RecoverTree(node1);
            }
            {
                int[] nums = { 2, 1, 5, 6, 2, 3 };
                oct.LargestRectangleArea(nums);
            }
            {
                int[] nums = { 1, 1, 1, 2, 2, 3 };

                oct.TopKFrequent(nums, 2);
            }
            {

                oct.IsNumber("0e");
            }
            Warming warming = new Warming(); ;
            {
                warming.ClearContext();
                warming.Partition("aab");
            }
            {
                int[] nums = { 3, 2, 1, 5, 6, 4 };
                warming.FindKthLargest(nums, 2);
            }
            {
                int[] nums = { 0, 2, 3, 6 };
                warming.InventoryManagement(nums, 2);
            }
            {
                //warning.KWeakestRows(null,2);
            }
            {
                warming.IsValid("){");
            }
            {
                string[] tokens = new string[] { "4", "13", "5", "/", "+" };
                warming.EvalRPN(tokens);
            }
            {
                warming.LongestValidParentheses("()(())");
            }
            {
                warming.SimplifyPath("/home//foo/");
            }
            {
                warming.Calculate("3+2*2");
            }
            {
                char[][] matrix = new char[1][];
                matrix[0] = new char[] { '1', '0' };
                warming.MaximalRectangle(matrix);
            }
            {
                int[] nums = { 1, 10, 3, 3, 3 };
                warming.MaxKelements(nums, 3);
            }
            {
                //int[] prices = { 3, 2, 6, 5, 0, 3 };
                int[] prices = { 1 };
                warming.MaxProfitIV(2, prices);
            }
            {

                warming.Convert("AB", 1);
            }
            {
                warming.FourSum(new int[] { 1000000000, 1000000000, 1000000000, 1000000000 }, -294967296);
            }
            {

                warming.IsInterleave("aabcc", "dbbca", "aadbbcbcac");
            }
            {
                int[] nums = new int[] { 2, 3, 4, 6 };
                warming.TupleSameProduct(nums);
            }
            {
                warming.Divide(10, 3);
            }
            {
                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                node1.right = node2;
                node2.left = node3;
                warming.PreorderTraversal(node1);
            }
            {
                TreeNode node3 = new TreeNode(3);
                TreeNode node9 = new TreeNode(9);
                TreeNode node20 = new TreeNode(20);
                TreeNode node15 = new TreeNode(15);
                TreeNode node7 = new TreeNode(7);
                node3.left = node9;
                node3.right = node20;
                node20.left = node15;
                node20.right = node7;
                warming.LevelOrder(node3);
            }
            {
                //TreeNode node3 = new TreeNode(3);
                //TreeNode node9 = new TreeNode(9);
                //TreeNode node20 = new TreeNode(20);
                //TreeNode node15 = new TreeNode(15);
                //TreeNode node7 = new TreeNode(7);
                //node3.left = node9;
                //node3.right = node20;
                //node20.left = node15;
                //node20.right = node7;

                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                TreeNode node4 = new TreeNode(4);
                TreeNode node5 = new TreeNode(5);
                node1.left = node2;
                node2.left = node4;
                node1.right = node3;
                node3.right = node5;
                warming.ZigzagLevelOrder(node1);
            }
            {
                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                TreeNode node4 = new TreeNode(4);
                TreeNode node5 = new TreeNode(5);
                node1.left = node2;
                node2.right = node5;
                node1.right = node3;
                node3.right = node4;
                warming.RightSideView(node1);
            }
            {
                TreeNode node1 = new TreeNode(1);
                TreeNode node6 = new TreeNode(6);
                TreeNode node7 = new TreeNode(7);
                TreeNode node4 = new TreeNode(4);
                TreeNode node5 = new TreeNode(5);
                node5.left = node1;
                node5.right = node6;
                node6.left = node4;
                node6.right = node7;
                warming.IsValidBST(node5);
            }
            {
                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                TreeNode node4 = new TreeNode(4);
                TreeNode node5 = new TreeNode(5);
                TreeNode node6 = new TreeNode(6);
                TreeNode node7 = new TreeNode(7);
                TreeNode node8 = new TreeNode(8);
                TreeNode node9 = new TreeNode(9);
                TreeNode node10 = new TreeNode(10);
                node1.left = node5;
                node5.right = node4;
                node4.left = node9;
                node4.right = node2;
                node1.right = node3;
                node3.left = node10;
                node3.right = node6;
                warming.AmountOfTime(node1, 3);
            }
            {
                int[][] edges = new int[5][];
                edges[0] = new int[] { 0, 2 };
                edges[1] = new int[] { 0, 5 };
                edges[2] = new int[] { 2, 4 };
                edges[3] = new int[] { 1, 6 };
                edges[4] = new int[] { 5, 4 };

                warming.CountPairs(7, edges);
            }
            Cool _cool = new Cool();
            {
                int[][] mat = new int[3][];
                mat[0] = new int[] { 1, 2, 3 };
                mat[1] = new int[] { 4, 5, 6 };
                mat[2] = new int[] { 7, 8, 9 };
                _cool.DiagonalSum(mat);
            }
            {
                int[][] mat = new int[4][];
                mat[0] = new int[] { 2, 0, 0, 1 };
                mat[1] = new int[] { 0, 3, 1, 0 };
                mat[2] = new int[] { 0, 5, 2, 0 };
                mat[3] = new int[] { 4, 0, 0, 2 };
                _cool.CheckXMatrix(mat);
            }
            {
                //int[][] grid = new int[4][];
                //grid[0] = new int[] { 1, 2, 3, 4 };
                //grid[1] = new int[] { 12, 13, 14, 5 };
                //grid[2] = new int[] { 11, 16, 15, 6 };
                //grid[3] = new int[] { 10, 9, 8, 7 };
                int[][] grid = new int[1][];
                grid[0] = new int[] { 2, 3 };
                _cool.SpiralArray(grid);
            }
            {
                string[] detail = new string[] { "7868190130M7522", "5303914400F9211", "9273338290F4010" };
                _cool.CountSeniors(detail);
            }
            {
                int[][] provinces = new int[3][];
                provinces[0] = new int[] { 1, 1, 0 };
                provinces[1] = new int[] { 1, 1, 0 };
                provinces[2] = new int[] { 0, 0, 1 };
                _cool.FindCircleNum(provinces);
            }
            {
                // 1,null,2,null,3,null,4,null,null
                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                TreeNode node4 = new TreeNode(4);
                node1.right = node2;
                node2.right = node3;
                node3.right = node4;
                _cool.BalanceBST(node1);
            }
            {
                AVL avl = new AVL();
                TreeNode root = null;
                //int[] nums = new int[] { 88, 70, 61, 96, 120 };      // 70
                int[] nums = new int[] { 88, 70, 61, 96, 120, 90, 65 }; // 88
                for (int i = 0; i < nums.Length; i++)
                {
                    root = avl.Insert(root, nums[i]);
                }

                WriteLine(root.val);
                IList<IList<int>> list = CommonHelper.LevelOrder(root);
                foreach (var l in list)
                    WriteLine(string.Join(",", l));
            }
            {

                char[][] board = new char[9][];
                board[0] = new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
                board[1] = new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
                board[2] = new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
                board[3] = new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
                board[4] = new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
                board[5] = new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
                board[6] = new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
                board[7] = new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
                board[8] = new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };
                _cool.SolveSudoku(board);
            }
            {
                _cool.NumRollsToTarget(30, 30, 500);
            }
            {
                depthSearch ds = new depthSearch();
                int[] nums = new int[] { 2, 3, 5, 4, 1 };
                //int[] nums = new int[] { 5,4,3,2,1,0};
                //int[] nums = { 1, 3, 2 };
                ds.NextPermutation(nums);
            }
            {
                T1 tt = new T1();

                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                node1.left = node3;
                node3.right = node2;
                tt.RecoverTree(node1);
            }
            {
                Codec code = new Codec();
                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2);
                TreeNode node3 = new TreeNode(3);
                TreeNode node4 = new TreeNode(4);
                TreeNode node5 = new TreeNode(5);
                node1.left = node2;
                node1.right = node3;
                node3.left = node4;
                node3.right = node5;
                string data = code.serialize(node1);
                code.deserialize(data);
            }
            {
                T1 t1 = new T1();

                t1.ConvertToTitle(25);
            }
        }

        #region 归并
        public static void MergeSort1(int[] nums, int start, int end)
        {
            if (start >= end)
                return;

            int mid = (end - start) / 2 + start;
            MergeSort1(nums, start, mid);
            MergeSort1(nums, mid + 1, end);

            Merge1(nums, start, mid, end);
        }

        public static void Merge1(int[] nums, int start, int mid, int end)
        {

            int[] tmp = new int[end - start + 1];
            // 3p
            int p = 0;
            int i = start;
            int j = mid + 1;
            while (i <= mid && j <= end)
            {
                if (nums[i] <= nums[j])
                {
                    tmp[p] = nums[i];
                    p++;
                    i++;
                }
                if (nums[i] > nums[j])
                {
                    tmp[p] = nums[j];
                    p++;
                    j++;
                }
            }
            // left
            while (i <= mid)
            {
                tmp[p] = nums[i];
                p++; i++;
            }
            while (j <= end)
            {
                tmp[p] = nums[j];
                p++; j++;
            }

            for (int k = start; k <= end; k++)
            {
                nums[k] = tmp[k - start];
            }
        }
        #endregion


        #region 快排


        public static void QuickSort(int[] nums, int start, int end)
        {
            if (start >= end)
                return;
            int pivot = QuickSortByPivot(nums, start, end);
            QuickSort(nums, start, pivot - 1);
            QuickSort(nums, pivot + 1, end);
        }

        // [0,1,2],3,[5,4]
        //           
        //         p 
        public static int QuickSortByPivot(int[] nums, int start, int end)
        {
            // 2 pointer
            int pivot = start;
            int j = start + 1;
            for (int i = start + 1; i <= end; i++)
            {
                if (nums[i] <= nums[pivot])
                {
                    int x = nums[j];
                    nums[j] = nums[i];
                    nums[i] = x;

                    j++;
                }
            }
            int y = nums[pivot];
            nums[pivot] = nums[j - 1];
            nums[j - 1] = y;

            pivot = j - 1;
            return pivot;

        }
        #endregion

        #region 数独
        public static void SolveSudoku(char[][] board)
        {
            backtrack(board, 0, 0);
        }

        public static bool backtrack(char[][] board, int row, int col)
        {
            int m = 9, n = 9;
            // col超额，换行
            if (col == n)
            {
                return backtrack(board, row + 1, 0);
            }
            // row 超额 出现了一个答案了
            if (row == m)
            {
                return true;
            }
            // 非空则跳过这一列
            if (board[row][col] != '.')
            {
                return backtrack(board, row, col + 1);
            }
            // 空则尝试[1...9]
            for (char ch = '1'; ch <= '9'; ch++)
            {
                if (!IsValid(board, row, col, ch))
                    continue;

                board[row][col] = ch;

                if (backtrack(board, row, col + 1))
                    return true;

                board[row][col] = '.';
            }

            return false;
        }
        // 是否有效数独
        public static bool IsValid(char[][] board, int r, int c, char n)
        {
            for (int i = 0; i < 0; i++)
            {
                // 行
                if (board[r][i] == n)
                    return false;
                //列
                if (board[i][c] == n)
                    return false;
                // cube中
                if (board[(r / 3) * 3 + i / 3][(c / 3) * 3 + i % 3] == n)
                    return false;
            }

            return true;

        }

        #endregion


        #region 皇后
        IList<IList<string>> res = new List<IList<string>>();
        public IList<IList<string>> SolveNQueens(int n)
        {
            IList<string> board = new List<string>();
            for (int i = 0; i < n; i++)
            {
                board.Add(new string('.', n));
            }
            backtrack(board, 0);
            return res;
        }

        void backtrack(IList<string> board, int row)
        {
            if (row == board.Count)
            {
                res.Add(new List<string>(board));
                return;
            }
            for (int col = 0; col < board[row].Length; col++)
            {
                if (!isValid(board, row, col))
                    continue;
                board[row] = board[row].Substring(0, col) + 'Q' + board[row].Substring(col + 1);
                backtrack(board, row + 1);
                board[row] = board[row].Substring(0, col) + '.' + board[row].Substring(col + 1);
            }
        }

        bool isValid(IList<string> board, int row, int col)
        {
            int n = board.Count;
            for (int i = 0; i <= row; i++)
            {
                if (board[i][col] == 'Q')
                    return false;
            }
            for (int i = row - 1, j = col + 1;
               i >= 0 && j < n; i--, j++)
            {
                if (board[i][j] == 'Q')
                    return false;
            }
            for (int i = row - 1, j = col - 1;
               i >= 0 && j >= 0; i--, j--)
            {
                if (board[i][j] == 'Q')
                    return false;
            }

            return true;
        }
        #endregion


    }

    public class ResultMssageModel
    {
        public string message { get; set; }
        public string[] param { get; set; }
    }
}
