using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class Algorithm
    {
        /*
        冒泡
        选择
        插入
        希尔
        归并
        快速
        堆排序
        计数排序
        桶排序
        基数排序
        */
        [TestMethod]
        public void SimpleExpression()
        {
        }

        /// <summary>
        /// 冒泡排序
        /// 初始值为：
        /// 9,8,3,2,7,6,5,4,
        /// 开始排序：
        /// 8,3,2,7,6,5,4,9,
        /// 3,2,7,6,5,4,8,9,
        /// 2,3,6,5,4,7,8,9,
        /// 2,3,5,4,6,7,8,9,
        /// 2,3,4,5,6,7,8,9,
        /// 2,3,4,5,6,7,8,9,
        /// 2,3,4,5,6,7,8,9,
        /// 2,3,4,5,6,7,8,9,
        /// </summary>
        [TestMethod]
        public void BubbleSort()
        {
            int[] arr = { 9, 8, 3, 2, 7, 6, 5, 4 };
            Console.WriteLine("原始数组：");
            printArr(arr);
            for (int i = 0; i < arr.Length - 1; i++) /* 外循环为排序趟数，len个数进行len-1趟 */
            {
                for (int j = 0; j < arr.Length - 1 - i; j++) /* 内循环为每趟比较的次数，第i趟比较len-i次 */
                {
                    if (arr[j] > arr[j + 1]) /* 相邻元素比较，若逆序则交换（升序为左大于右，降序反之） */
                    {
                        var temp = 0;
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;

                    }
                }

                printArr(arr);
            }
        }

        /// <summary>
        /// 选择排序
        /// 初始值为：
        /// 9 8 3 2 7 6 5 4 
        /// ********************
        /// 2 8 3 9 7 6 5 4 
        /// 2 3 8 9 7 6 5 4 
        /// 2 3 4 9 7 6 5 8 
        /// 2 3 4 5 7 6 9 8 
        /// 2 3 4 5 6 7 9 8 
        /// 2 3 4 5 6 7 9 8 
        /// 2 3 4 5 6 7 8 9 
        /// 2 3 4 5 6 7 8 9 
        /// </summary>
        [TestMethod]
        public void SelectionSort()
        {
            int[] arr = { 9, 8, 3, 2, 7, 6, 5, 4 };
            printArr(arr);
            Console.WriteLine("********************");
            for (int i = 0; i < arr.Length - 1; i++) /* 外循环为排序趟数，len个数进行len-1趟 */
            {
                int min = i; // 假设第一个数为最小值
                for (int j = i + 1; j < arr.Length; j++) //走訪未排序的元素
                    if (arr[j] < arr[min]) //找到目前最小值
                        min = j; //紀錄最小值
                var temp = arr[min];
                arr[min] = arr[i];
                arr[i] = temp; //做交換
                printArr(arr);
            }

            printArr(arr);
        }

        /// <summary>
        /// 插入排序
        /// 类似于查入扑克牌
        ///
        /// 9 8 3 2 7 6 5 4 
        /// ************
        /// 8 9 3 2 7 6 5 4 
        /// 3 8 9 2 7 6 5 4 
        /// 2 3 8 9 7 6 5 4 
        /// 2 3 7 8 9 6 5 4 
        /// 2 3 6 7 8 9 5 4 
        /// 2 3 5 6 7 8 9 4 
        /// 2 3 4 5 6 7 8 9 
        /// 2 3 4 5 6 7 8 9 
        /// </summary>
        [TestMethod]
        public void InsertSort()
        {
            int[] arr = { 9, 8, 3, 2, 7, 6, 5, 4 };
            for (int i = 1; i < arr.Length; i++) // 从第二个数开始取值
            {
                var temp = arr[i]; // 拿到手里的第一个值
                var j = i - 1;
                while ((j >= 0) && (arr[j] > temp)) // 与前一个进行比较，如果前一个数比较大，则
                {
                    arr[j + 1] = arr[j]; // 移动前一个数的位置到 i中，再次比较再往前的数
                    j--;
                }

                arr[j + 1] = temp;
            }

            printArr(arr);
        }


        /// <summary>
        /// 希尔排序
        /// 1.  第一次排序大致有序
        /// 2.  插入排序
        /// https://zhuanlan.zhihu.com/p/34914588
        /// </summary>
        [TestMethod]
        public void ShellSort()
        {
            int[] arr = { 9, 8, 3, 2, 7, 6, 5, 4 };
            printArr(arr);
            Console.WriteLine("******start******");
            shellSort(arr);
            Console.WriteLine("******end******");
        }

        public void shellSort(int[] arrays)
        {

            //增量每次都/2
            for (int step = arrays.Length / 2; step > 0; step /= 2)
            {

                //从增量那组开始进行插入排序，直至完毕
                for (int i = step; i < arrays.Length; i++)
                {

                    int j = i;
                    int temp = arrays[j];

                    // j - step 就是代表与它同组隔壁的元素
                    while (j - step >= 0 && arrays[j - step] > temp)
                    {
                        arrays[j] = arrays[j - step];
                        j = j - step;
                    }

                    arrays[j] = temp;
                }

                printArr(arrays);
            }


        }

        private void printArr(int[] arr)
        {
            arr.ToList().ForEach(i => Console.Write(i.ToString() + " "));
            Console.WriteLine();
        }


        // leetCode
        ///给定一个整数数组 nums 和一个整数目标值 target，请你在该数组中找出 和为目标值 的那 两个 整数，并返回它们的数组下标。
        /// <summary>
        /// == 查找表法 ==
        /// 输入：nums = [2,7,11,15], target = 9
        /// 输出：[0,1]
        /// 解释：因为 nums[0] + nums[1] == 9 ，返回 [0, 1] 。
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        [TestMethod]
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>(nums.Length - 1);
            dic.Add(nums[0], 0); // 2, 0
            for (int i = 1; i < nums.Length; i++)
            {
                if (dic.ContainsKey(target - nums[i])) // 9  - 7
                {
                    return new int[] { dic[target - nums[i]], i };
                }

                dic.Add(nums[i], i);
            }

            return new int[0];
        }

        [TestMethod]
        public int[] TwoSum1(int[] nums, int target)
        {
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            return new int[] { 0 };
        }
    }
}
