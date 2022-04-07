using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using static System.Console;

namespace arrProject
{
    class Program
    {
        /// <summary>
        /// 合并数组且仍然有序
        /// </summary>
        /// <param name="A"></param>
        /// <param name="m"></param>
        /// <param name="B"></param>
        /// <param name="n"></param>
        public static void merge(int[] A, int m, int[] B, int n)
        {
            int end = m + n - 1;
            int i = m - 1;
            int j = n - 1;
            while (end != 0 && i >= 0 && j >= 0)
            {
                if (A[i] > B[j])
                    A[end--] = A[i--];
                else
                    A[end--] = B[j--];
            }
            while (j >= 0)
            {
                A[end--] = B[j--];
            }
        }
        /// <summary>
        /// 两数之和
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] twoSum(int[] numbers, int target)
        {
            // write code here
            Dictionary<int, int> map = new Dictionary<int, int>();
            //遍历数组
            for (int i = 0; i < numbers.Length; i++)
            {
                //将不包含target - numbers[i]，装入map中，包含的话直接返回下标
                if (map.ContainsKey(target - numbers[i]))
                    return new int[] { map[target - numbers[i]], i };
                else
                    map.Add(numbers[i], i);
            }
            throw new ArgumentException();
        }

        #region 买入卖出的股票最大利润
        /// <summary>
        /// 买入卖出的股票最大利润
        /// 求最大利润最大 贪心
        ///    后面的值-前面的值
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int maxProfit(int[] prices)
        {
            var maxValue = 0;
            var minValue = prices[0];
            // write code here
            for (int i = 0; i < prices.Length; i++)
            {
                minValue = Math.Min(minValue, prices[i]);
                maxValue = Math.Max(maxValue, prices[i] - minValue);
            }
            return maxValue;
        }
        /// <summary>
        /// 贪心
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int maxProfitDP(int[] prices)
        {
            // write code here
            if (prices == null || prices.Length == 0) return 0;
            int len = prices.Length;
            // 动态规划数组
            int[] dp = new int[len];
            int min = prices[0];
            for (int i = 0; i < prices.Length; i++)
            {
                // 更新在i前的最小值
                if (prices[i] < min)
                {
                    min = prices[i];
                }
                if (i == 0)
                {
                    dp[i] = 0;
                }
                else
                {
                    // 状态转移方程，dp[i]的值在dp[i-1]和（当前值-当前最小值）中的最大值
                    dp[i] = Math.Max(dp[i - 1], prices[i] - min);
                }
            }
            // 返回最后的结果
            return dp[len - 1];
        }

        public static int maxProfitRec(int[] prices)
        {
            // write code here
            // 默认收益为0
            int max = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                for (int j = i + 1; j < prices.Length; j++)
                {
                    int profit = prices[j] - prices[i];
                    // 当收益大于记录的值时，更新最大收益
                    if (profit > max)
                    {
                        max = profit;
                    }
                }
            }
            return max;
        }

        /**
         * 
         * @param prices int整型一维数组 
         * @return int整型
         */
        public static int maxProfitDP_g(int[] prices)
        {
            // write code here
            if (prices.Length == 0) return 0;//如果给定数组的长度为0，可以直接返回结果0
            int min_input = prices[0];//初始化买入价格为第一个元素
            int max_output = 0;//定义一个最大的利润
            for (int i = 1; i < prices.Length; i++)
            {
                min_input = Math.Min(min_input, prices[i]);//保存最小的买入价格
                max_output = Math.Max(max_output, prices[i] - min_input);//每遍历一个位置，就需要求出当前位置卖出的价钱
            }
            return max_output;

        }
        #endregion

        /// <summary>
        /// 获取指定数字的数量
        /// </summary>
        /// <param name="array"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int GetNumberOfK(int[] array, int k)
        {
            if (array.Length == 0)
                return 0;

            int mid = array.Length / 2;
            if (array[mid] > k)
            {
                mid = array.Length / 2 - mid;
            }
            else if (array[mid] < k)
            {
                mid = mid / 2;
            }
            else
            {
                int left = mid;
                int right = mid;
                while (k == array[left])
                {
                    if (--left == -1)
                        break;
                }
                while (k == array[right])
                {
                    if (++right == array.Length)
                        break;
                }
                return right - left - 1;
            }
            return 0;
        }

        /// <summary>
        /// 求数组中超过一半的数字 求众数
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int MoreThanHalfNum_Solution(int[] array)
        {
            // base case
            if (array.Length == 0)
            {
                return 0;
            }

            int count = 0;
            int cur = array[0];
            for (int i = 0; i < array.Length; i++)
            {

                if (cur == array[i])
                    count++;
                else if (cur != array[i] && count == 0)
                {
                    cur = array[i];
                    count++;
                }
                else // cur != array[i] && count != 0
                    count--;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == cur) { count++; }
            }
            if (count >= array.Length / 2) { return cur; }
            return 0;
        }

        /**
        * 最大乘积 
        * 求3数乘积最大
        * @param A int整型一维数组 
        * @return long长整型
        */
        public static long solve(int[] A)
        {
            // write code here
            // 最大的第二大的和第三大的
            int max1 = int.MinValue, max2 = int.MinValue, max3 = int.MinValue;
            //最小的和第二小的（负数的时候要用）
            int min1 = int.MaxValue, min2 = int.MaxValue;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] < min1)
                {//更新最小值
                    min2 = min1;
                    min1 = A[i];
                }
                else if (A[i] < min2)
                {//更新第二小
                    min2 = A[i];
                }
                if (A[i] > max1)
                {//更新最大值
                    max3 = max2;
                    max2 = max1;
                    max1 = A[i];
                }
                else if (A[i] > max2)
                {//更新第二大
                    max3 = max2;
                    max2 = A[i];
                }
                else if (A[i] > max3)
                {//更新第三大
                    max3 = A[i];
                }
            }
            return Math.Max((long)min1 * min2 * max1, (long)max1 * max2 * max3);
        }

        #region NC77 调整数组顺序使奇数位于偶数前面(一)
        /// <summary>
        /// 考察纯编码能力以及理解能力
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] reOrderArray(int[] array)
        {
            // 指向偶数点的前一个数
            int p = 0;
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] % 2 == 1)
                {
                    int tmp = array[i];
                    // 依次进行移动【i-1，p】到【i-1+1,p+1】
                    for (int k = i - 1; k >= p; --k)
                    {
                        array[k + 1] = array[k];
                    }
                    // 将奇数赋值给p，然后p++
                    array[p] = tmp;
                    p++;
                }
            }
            return array;
        }

        #endregion

        #region 机器人到达指定位置的方法数

        public static int walk(int N, int cur, int rest, int P)
        {
            if (rest == 0)
            {
                return cur == P ? 1 : 0;
            }

            if (cur == 1)
            {
                return walk(N, 2, rest - 1, P);
            }

            if (cur == N)
            {
                return walk(N, N - 1, rest - 1, P);
            }

            return walk(N, cur + 1, rest - 1, P) + walk(N, cur - 1, rest - 1, P);
        }

        public static int ways1(int N, int M, int K, int P)
        {
            if (N < 2 || K < 1 || M < 1 || M > N || P < 1 || P > N)
            {
                return 0;
            }
            return walk(N, M, K, P);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="N">范围</param>
        /// <param name="cur">当前位置</param>
        /// <param name="rest">剩余部署</param>
        /// <param name="P">停留点</param>
        /// <returns></returns>
        public static int ways2(int N, int cur, int rest, int P)
        {
            if (N < 2 || rest < 1 || cur < 1 || cur > N || P < 1 || P > N)
            {
                return 0;
            }

            int[,] dp = new int[rest + 1, N + 1];
            dp[0, P] = 1;
            for (int i = 0; i <= rest; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    if (j == 1)
                    {
                        dp[i, j] = dp[i - 1, 2];
                    }
                    else if (j == N)
                    {
                        dp[i, j] = dp[i - 1, N - 1];
                    }
                    else
                    {
                        dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j + 1];
                    }
                }
            }
            return dp[rest, cur];
        }
        #endregion

        #region 数组的partition调整
        /// <summary>
        /// 整体有序，要求左部分递增且不重复
        /// </summary>
        /// <param name="arr"></param>
        public static void leftUnique(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            int u = 0;
            int i = 1;
            while (i != arr.Length)
            {
                if (arr[i++] != arr[u])
                {
                    swap(arr, ++u, i - 1); // u 扩容，并交换 (1,0) 
                }
            }
        }

        private static void swap(int[] arr, int v1, int v2)
        {
            int tmp = arr[v1];
            arr[v1] = arr[v2];
            arr[v2] = tmp;
        }

        /// <summary>
        /// 要求具有三部分：左是0， 中是1，有是2
        /// </summary>
        /// <param name="arr"></param>
        public static void sort012(int[] arr)
        {
            if (arr == null || arr.Length < 2)
                return;

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
                    swap(arr, index, --right); // new
                }
                else
                {
                    index++;
                }
            }
        }
        #endregion

        #region NC19 连续子数组的最大和
        public static int FindGreatestSumOfSubArray(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return 0;
            }
            int max = int.MinValue;
            int cur = 0;
            for (int i = 0; i < array.Length; i++)
            {
                cur += array[i];
                max = Math.Max(max, cur);
                cur = cur < 0 ? 0 : cur;
            }
            return max;
        }
        #endregion

        #region 自然数排序
        /// <summary>
        /// 总是把不等的数，换到一个合适的位置，然后next
        /// </summary>
        /// <param name="arr"></param>
        public static void sort1(int[] arr)
        {
            int tmp = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                while (arr[i] != i + 1)
                {
                    tmp = arr[arr[i] - 1]; // i=2; arr[5-1]===> i=3; 
                    arr[arr[i] - 1] = arr[i]; // arr[5-1 ] =  arr[2]
                    arr[i] = tmp;
                }
            }
        }
        /// <summary>
        /// 把不对的换到正确的位置，倒着找数
        /// </summary>
        /// <param name="arr"></param>
        public static void sort2(int[] arr)
        {
            int tmp = 0;  // 当前数
            int next = 0; // 前一个数
            for (int i = 0; i < arr.Length; i++)
            {
                tmp = arr[i];
                while (arr[i] != i + 1)
                {
                    next = arr[tmp - 1];
                    arr[tmp - 1] = tmp;
                    tmp = next;
                }
            }
        }
        #endregion

        #region NC101 缺失数字
        /// <summary>
        /// 二分查找法 ，一定是连续的自然数才成立
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int solve1(int[] arr)
        {
            int l = 0;
            int r = arr.Length;
            while (l <= r)
            {
                int mid = (l + r) / 2;
                if (arr[mid] == mid)
                {
                    l = mid + 1; //此时答案位于mid右侧 
                }
                else
                {
                    r = mid - 1; // 不相等继续往右
                }
            }
            return l;
        }
        #endregion
        static void Main(string[] args)
        {
            #region 合并数组且仍然有序
            int[] A = { 1, 2, 3, 0, 0, 0 };
            int[] B = { 2, 5, 6 };
            merge(A, 3, B, 3);
            #endregion

            #region 在数组中是否可以找到 两数之和等于k的
            // int[] numbers = { 9, 5, 7, 4, 1 }; // 9
            int[] numbers = { 3, 2, 4 }; // 6
            var rs = twoSum(numbers, 6);
            #endregion

            #region  买入卖出的股票最大利润
            //int[] prices = { 2, 4, 1 };
            int[] prices = { 2, 5, 3, 7, 1 };
            maxProfit(prices);
            maxProfitDP(prices);
            maxProfitRec(prices);
            Console.WriteLine("Hello World!");
            #endregion

            #region 获取指定数字的数量
            int[] array = { 2, 2, 2 };
            GetNumberOfK(array, 2);
            #endregion

            #region  求数组中超过一半的数字 求众数
            int[] array1 = { 1, 2, 3, 2, 2, 2, 5, 4, 2 };
            MoreThanHalfNum_Solution(array1);
            #endregion

            int[] _reOrderArray = { 1, 2, 4, 5 };
            WriteLine(string.Join(',', reOrderArray(_reOrderArray)));

            int[] _leftUnique = { 1, 2, 2, 2, 2, 3, 3, 4, 5, 6, 6, 7, 7, 8, 8, 8, 9 };
            leftUnique(_leftUnique);

            int[] _sort012 = { 0, 1, 2, 0, 1, 2, 0, 0, 1, 1, 2, 2, 2, 2, 2, 0 };
            sort012(_sort012);

            int[] _sort1 = { 1, 2, 5, 3, 4 };
            sort1(_sort1);
            int[] _sort2 = { 1, 2, 5, 3, 4 };
            sort2(_sort2);

            int[] _solve1 = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };//{ 0,1, 3, 4, 5, 6 };
          //  WriteLine(solve1(_solve1));



            string[] fruits = { "apple", "passionfruit", "banana", "mango",
                      "orange", "blueberry", "grape", "strawberry" };

            var list = fruits.Where(fruit => fruit.Length == 100).ToList();

            //foreach (int length in lengths)
            //{
            //    Console.WriteLine(length);
            //}
            ReadKey();
        }
    }
}
