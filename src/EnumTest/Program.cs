using System;
using static System.Console;

namespace EnumTest
{
    class Program
    {

        // 100-999  暴力枚举
        public static bool findx(int s)
        {
            for (int i = 100; i <= 999; i++)
            {
                if (s == i)
                    return true;
            }
            return false;
        }

        // 100元 5元一只公鸡，3元一只母鸡， 3只小鸡1元， 买一百只鸡

        public static void BuyChichken()
        {
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 0; j <= 33; j++)
                {
                    if ((100 - i * 5 - j * 3) * 3 == 100 - i - j)
                    {
                        Write(i + " " + j + " " + (100 - i * 5 - j * 3) * 3);
                        WriteLine();
                    }
                }
            }
        }
        // 自然数组的排序

        #region 自然数组的排序
        // 根据条件将value-1 看成索引
        public static void sort1(int[] arr)
        {
            int tmp = 0;
            int next = 0;
            for (int i = 0; i != arr.Length; i++)
            {
                tmp = arr[i]; // 获取i中的值 5
                while (arr[i] != i + 1) // 碰到不相等的数 进行以下逻辑 2位置的值是5
                {
                    next = arr[tmp - 1]; // 通过tmp的不断变化找到arr中正确的值
                    arr[tmp - 1] = tmp;  // 将tmp放入tmp-1位置
                    tmp = next;
                }
            }
        }
        // 思路与上面一致
        public static void sort2(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                while (arr[i] != i + 1) // 只要一直换不到，就停不下来
                {
                    int tmp = arr[arr[i] - 1];
                    arr[arr[i] - 1] = arr[i]; // 一定能放正确
                    arr[i] = tmp;
                }
            }
        }
        #endregion

        // 要么所有的偶数下标都是偶数，要么所有的奇数下标都是奇数
        public static void modify(int[] arr)
        {
            if (arr == null || arr.Length < 2)
                return;
            int even = 0;
            int odd = 1;
            int end = arr.Length - 1;
            while (even <= end && odd <= end) // 思路： 找最后一个数值的奇偶性，换奇偶的坐标
            {
                if ((arr[end] & 1) == 0) // 如果end的值是奇数
                {
                    swap(arr, end, even); // 交换末尾数
                    even += 2;// 奇数1->3
                }
                else
                {
                    swap(arr, end, odd); //
                    odd += 2; // 偶数0->2
                }

            }

        }
        // 子数组的最大累加和
        // 根据自然数的性质：
        //  如果是负数，重新计算累加cur， 之前的和是最大。
        public static int maxSum(int[] arr)
        {
            if (arr == null || arr.Length == 0) return 0;


            int max = int.MinValue;
            int cur = 0;
            for (int i = 0; i < arr.Length; i++){
                cur += arr[i];
                max = Math.Max(max, cur); // 如果没有正数，取最大值即可； 如果正负均存在，找到最大值即可
                cur = cur < 0 ? 0 : cur;
            }

            return max;
        }

        // 在数组中找到一个局部最小的位置
        public static int getLessIndex(int[] arr)
        {
            if (arr == null || arr.Length == 0) return -1;//不存在

            if (arr.Length == 1 || arr[0] < arr[1]) return 0;

            if (arr[arr.Length - 1] < arr[arr.Length - 2]) return arr.Length - 1;

            int left = 1;
            int right = arr.Length - 2;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (arr[mid] > arr[mid - 1]) right = mid - 1;
                else if (arr[mid] > arr[mid + 1]) left = mid + 1;
                else
                    return mid;
            }
            return left;
        }

        // 不包含本位置值的累乘数组
        public static int[] product1(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return null;
            }
            int count = 0;
            int all = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0) // 求出来所有不为0的乘积
                {
                    all *= arr[i];
                }
                else
                {
                    count++;
                }
            }

            int[] res = new int[arr.Length];
            if (count == 0) // 如有没有0
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = all / arr[i];
                }
            }
            if (count == 1) // 如果有1个0；如果有多个0，全部为0
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == 0)
                    {
                        res[i] = all;
                    }
                }
            }

            return res;
        }

        // 不包含本位置值的累乘数组
        public static int[] product2(int[] arr)
        {
            if (arr == null || arr.Length < 2) return null;
            int[] res = new int[arr.Length];
            res[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                res[i] = res[i - 1] * arr[i]; //0:arr[0]; 1: arr[0]*arr[1]; 2: arr[0]*arr[1]*res[2]
            }
            int tmp = 1;
            for (int i = arr.Length - 1; i > 0; i--)
            {
                res[i] = res[i - 1] * tmp; // 不包含自己的累乘
                tmp *= arr[i]; // 从end进行累乘
            }
            res[0] = tmp; // 第一个是所有的乘积

            return res;
        }

        // partition 调整
        public void leftUnique(int[] arr) // 有序数组实现左边 partition
        {
            if (arr == null || arr.Length < 2)
                return;

            int u = 0;
            int i = 1;
            while (i != arr.Length)
            {
                if (arr[i++] != arr[u]) // 如果不相等，进行交换
                {
                    swap(arr, ++u, i - 1);
                }
            }
        }
        // 1,2,3 partition
        public void partition2(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }
            int left = -1;
            int index = 0;
            int right = arr.Length;
            while (index < right)
            {
                if (arr[index] == 0)
                {
                    swap(arr, ++left, index++);
                }
                else if (arr[index] == 2)
                {
                    swap(arr, index, --right);
                }
                else
                {
                    index++;
                }
            }
        }
        static void Main(string[] args)
        {

            findx(783);
            BuyChichken();
            sort1(new int[] { 1, 2, 5, 3, 4 });

            modify(new int[] { 6, 1, 3, 2, 4, 8 });

            maxSum(new int[] { 1, -2, 3, 5, -2, 6, -1 });
            maxSum(new int[] { -2, -1 });

            product2(new int[] { 2, 3, 1, 4 });
            WriteLine("Hello World!");
        }
        private static void swap(int[] arr, int index1, int index2)
        {
            int tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }
    }
}
