using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
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
        public static void BubbleSort1(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
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
        public static void SelectorSort1(int[] array)
        {
            // i from 0 end with 6: 7 numbers
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                // j from 1 end with 7: 7 numbers
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }
                if (i != minIndex)
                {
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }

        }
        #endregion

        #region Insert Sort
        public static void InsertSort(int[] array)
        {
            // i from 0 end with 7: 8 numbers
            for (int i = 0; i < array.Length; i++)
            {
                int insertValue = array[i];
                int j = i - 1; // 插入点的前一个位置
                // 从右向左比较元素的同时，进行元素复制
                for (; (j >= 0) && (insertValue < array[j]); j--)
                {
                    array[j + 1] = array[j];
                }
                // insertValue的值插入适当位置
                array[j + 1] = insertValue;
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
        public static void MergeSort(int[] array, int start, int end)
        {
            if (start < end)
            {
                // 折半成两个小集合，分别进行递归
                int mid = (start + end) / 2;
                MergeSort(array, start, mid);
                MergeSort(array, mid + 1, end);
                // 把两个有序小集合，归并成一个大集合
                Merge(array, start, mid, end);
            }
        }

        private static void Merge(int[] array, int start, int mid, int end)
        {
            // 开辟额外大集合，设置指针
            int[] tempArray = new int[end - start + 1];
            int p1 = start;
            int p2 = mid + 1;
            int p = 0;
            // 比较两个小集合的元素，依次放入大集合
            while ((p1 <= mid) && (p2 <= end))
            {
                if (array[p1] <= array[p2])
                {
                    tempArray[p++] = array[p1++];
                }
                else
                {
                    tempArray[p++] = array[p2++];
                }
            }
            // 左侧小集合还有剩余，依次放入大集合尾部
            while (p1 <= mid)
            {
                tempArray[p++] = array[p1++];
            }
            // 右侧小集合还有剩余，依次放入大集合尾部
            while (p2 <= end)
            {
                tempArray[p++] = array[p2++];
            }
            // 把大集合的元素复制回原数组
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i + start] = tempArray[i];
            }
        }
        #endregion

        #region Quick Sort
        public static void QuickSort1(int[] arr, int startIndex, int endIndex)
        {
            // 递归结束条件： startIndex 大于或等于endIndex时
            if (startIndex >= endIndex)
            {
                return;
            }
            // 得到基准元素位置
            int pivotIndex = Partition1(arr, startIndex, endIndex);
            // 根据基准元素，分成两部分进行递归排序
            QuickSort1(arr, startIndex, pivotIndex - 1);
            QuickSort1(arr, pivotIndex + 1, endIndex);
        }
        // 分治 （双边循环）
        private static int Partition1(int[] arr, int startIndex, int endIndex)
        {
            // 取第1个位置的元素作为基准元素
            int pivot = arr[startIndex];
            int left = startIndex;
            int right = endIndex;
            while (left != right)
            {
                // 控制right指针比较并左移
                while (left < right && arr[right] > pivot)
                {
                    right--;
                }
                // 控制left指针比较并右移
                while (left < right && arr[left] <= pivot)
                {
                    left++;
                }
                // 交换left和right指针所指向的元素
                if (left < right)
                {
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
            }
            // pivot和指针重合点交换
            arr[startIndex] = arr[left];
            arr[left] = pivot;

            return left;
        }

        public static void QuickSort2(int[] arr, int startIndex, int endIndex)
        {
            // 递归结束条件: startIndex 大于或等于 endIndex时
            if (startIndex >= endIndex)
                return;
            // 得到基准元素位置
            int pivotIndex = Partition2(arr, startIndex, endIndex);
            // 根据基准元素，分成两部分进行递归排序
            QuickSort2(arr, startIndex, pivotIndex - 1);
            QuickSort2(arr, pivotIndex + 1, endIndex);
        }
        // 分治， 单边
        private static int Partition2(int[] arr, int startIndex, int endIndex)
        {
            // 取第1个位置的元素作为基准元素
            int pivot = arr[startIndex];
            int mark = startIndex;

            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (arr[i] < pivot)
                {
                    mark++;
                    int p = arr[mark];
                    arr[mark] = arr[i];
                    arr[i] = p;
                }
            }

            arr[startIndex] = arr[mark];
            arr[mark] = pivot;
            return mark;
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
            var errorMsg = "java.lang.Exception: end false {\"message\":\"m.pers.mail.message.mailOnlyOne\",\"param\":[\"1\",\"2\"]}";
            try
            {
                var result = "";
                var from = errorMsg.IndexOf('{');
                var to = errorMsg.LastIndexOf('}');
                var transRS = "abc{0},edf{1}";
                if (from != -1 && to != -1)
                {
                    var rmm = JsonConvert.DeserializeObject<ResultMssageModel>(errorMsg.Substring(from, to - from + 1));
                    if (rmm != null)
                    {
                        //transRS = await _localizationService.GetLocalizationByCategoryAsync(new LocalizationModel()
                        //{
                        //    Identity = new Guid(),
                        //    Category = rmm.message,
                        //    Culture = _cultureAccessor.CurrentCulture.Name
                        //});
                        for (int i = 0; i < rmm.param.Length; i++)
                        {
                            transRS = transRS.Replace(@"{" + i + "}", rmm.param[i]);
                        }
                    }
                    result += ":\n" + transRS;
                }
                else
                {
                    result += ":\n" + errorMsg;
                }
            }
            catch (Exception ex)
            {

            }
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
            ReadKey();
        }
    }

    public class ResultMssageModel
    {
        public string message { get; set; }
        public string[] param { get; set; }
    }
}
