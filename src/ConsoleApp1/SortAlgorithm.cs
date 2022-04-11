using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// 冒泡排序
    /// </summary>
    static class Bubble
    {
        /// <summary>
        /// 缺陷：为什么需要交换，直接找到最大值，交换一次就行了
        /// </summary>
        /// <param name="arr"></param>
        public static void BubbleSort(this int[] arr)
        {
            int temp;
            for (int outter = arr.Length - 1; outter >= 1; outter--)
            {
                for (int inner = 0; inner <= outter - 1; inner++)
                {
                    if (arr[inner] > arr[inner + 1])
                    {
                        temp = arr[inner];
                        arr[inner] = arr[inner + 1];
                        arr[inner + 1] = temp;
                    }
                }
                //arr.sh
            }
        }
    }

    /// <summary>
    /// 选择排序
    /// </summary>
    static class Selection
    {
        /// <summary>
        /// 仅仅交换一次，并不是每两个都交换
        /// </summary>
        /// <param name="arr"></param>
        private static void SelectionSort(this int[] arr)
        {
            int min, temp;
            for (int outter = 0; outter < arr.Length; outter++)
            {
                min = outter;
                for (int inner = outter + 1; inner < arr.Length; inner++)
                {
                    if (arr[inner] < arr[min])
                    {
                        min = inner;
                    }
                }

                temp = arr[outter];
                arr[outter] = arr[min];
                arr[min] = temp;
            }
        }
    }
    /// <summary>
    /// 插入排序
    /// </summary>
    static class Insertion
    {
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="arr"></param>
        public static void InsertionSort(this int[] arr)
        {
            int inner, temp;
            for (int outter = 1; outter <= arr.Length; outter++)
            {
                temp = arr[outter];
                inner = outter;
                // 目标：获取插入的索引。 步骤：如果大于临时值，1.移动位置； 2. 找到下一个值进行比较；3. 获取插入索引后进行赋值
                while (inner > 0 && arr[inner - 1] >= temp)
                {
                    arr[inner] = arr[inner - 1];
                    inner -= 1;
                }

                arr[inner] = temp;
            }
        }
    }
    /// <summary>
    /// 希尔排序 
    /// </summary>
    static class Shell
    {
        public static void ShellSort(this int[] arr)
        {
            int inner = 0;
            int temp = 0;
            int increment = 0;
            while (increment <= arr.Length / 3) // 10--4    1 4
            {
                increment = increment * 3 + 1;
            }

            while (increment > 0)
            {
                for (int outter = increment; outter <= arr.Length - 1; outter++)
                {
                    temp = arr[outter]; // 4 1
                    inner = outter; // 
                    while ((inner > increment - 1) && arr[inner - increment] >= temp)
                    {
                        arr[inner] = arr[inner - increment];
                        inner -= increment;
                    }
                    arr[inner] = temp;
                    //arr.Show();
                } // increment=1 时就是插入排序一样的代码
                increment = (increment - 1) / 3;
                // arr.Show();
            }
        }
    }

    /// <summary>
    /// 归并排序
    /// </summary>
    static class Merge
    {
        private static void MergeSort(this int[] arr)
        {
            int[] temp = new int[arr.Length];
            PartSort(arr, 0, arr.Length - 1, temp);
        }

        private static void PartSort(int[] arr, int left, int right, int[] temp)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                PartSort(arr, left, middle, temp);
                PartSort(arr, middle + 1, right, temp);
                //Merge()

            }
        }

        //private static void Merge(int[] arr, int left, int mid, int right, int[] temp)
        //{
            //int i = left;
            //int j = mid + 1;
            //int t = 0;
            //while (i <= mid && j <= right)
            //{
            //    if (arr[i] <= arr[j])
            //    {
            //        temp[t] = arr[i];
            //        t++;
            //        i++;
            //    }
            //    else
            //    {
            //        temp[t] = arr[j];
            //        t++;
            //        j++;
            //    }
            //}

            //while (i < mid)
            //{
            //    temp[t++] = arr[i++];
            //}

            //while (j <= right)
            //{
            //    temp[t++] = arr[j++];
            //}

            //while (left <= right)
            //{
            //    arr[left++] = temp[t++];
            //}

            //// arr.Show();
        //}
    }

}
