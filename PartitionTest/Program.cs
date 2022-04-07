using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using static System.Console;
namespace PartitionTest
{
    class Program
    {

        #region NC71 旋转数组的最小数字
        static int minNumberInRotateArray(int[] rotateArray)
        {
            if (rotateArray.Length == 0)
                return 0;

            int low = 0;
            int high = rotateArray.Length - 1;
            int mid = 0;

            while (low < high)
            {
                // 子数组是非递减的数组，10111
                if (rotateArray[low] < rotateArray[high])
                    return rotateArray[low];
                mid = low + (high - low) / 2;
                if (rotateArray[mid] > rotateArray[low])
                    low = mid + 1;
                else if (rotateArray[mid] < rotateArray[high])
                    high = mid;
                else low++; // 110111 (5-0)/2+0 = 2
            }
            return rotateArray[low];
        }
        static int minNumberInRotateArrayv1(int[] array)
        {
            int low = 0;
            int high = array.Length - 1;
            int mid = 0;
            while (low < high)
            {
                if (low == high - 1)
                {
                    break;
                }
                if (array[low] < array[high])
                {
                    return array[low];
                }
                mid = (low + high) / 2;
                if (array[low] < array[high])
                {
                    high = mid;
                    continue;
                }
                else if (array[mid] > array[high])
                {
                    low = mid;
                    continue;
                }
                // 再次进行二分法
                int i = low;
                while (low < mid)
                {
                    if (array[i] == array[mid])
                    {
                        i++; // low..mid 一样的话 O(N)
                    }
                    else if (array[i] < array[mid])
                    {
                        return array[i]; // 找到了分割点
                    }
                    else
                    {
                        high = mid; // 进行二分法
                        break;
                    }
                }

            }
            return Math.Min(array[low], array[high]);

        }
        #endregion

        #region 在有序旋转数组中找到一个数
        /// <summary>
        /// 在有序旋转数组中找到一个数
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool isContainsInRotateArray(int[] nums, int target)
        {
            int low = 0;
            int high = nums.Length - 1;
            int mid = 0;
            while (low <= high)
            {
                mid = (low + high) / 2;
                if (nums[mid] == target)
                {
                    return true;
                }
                if (nums[low] == nums[mid] && nums[mid] == nums[high])
                {
                    while (low != mid && nums[low] == nums[mid])
                    {
                        low++;
                    }
                    if (low == mid)
                    {
                        low = mid + 1;
                        continue;
                    }
                }
                if (nums[low] != nums[mid])
                {
                    if (nums[mid] > nums[low])
                    {
                        if (target >= nums[low] && target < nums[mid])
                        {
                            high = mid - 1;
                        }
                        else
                        {
                            low = mid + 1;
                        }
                    }
                    else
                    {
                        if (target > nums[mid] && target <= nums[high])
                        {
                            low = mid + 1;
                        }
                        else
                        {
                            high = mid - 1;
                        }
                    }
                }
                else
                {
                    if (nums[mid] < nums[high])
                    {
                        if (target > nums[mid] && target <= nums[high])
                        {
                            low = mid + 1;
                        }
                        else
                        {
                            high = mid - 1;
                        }
                    }
                    else
                    {
                        if (target >= nums[mid] && target <= nums[high])
                        {
                            high = mid - 1;
                        }
                        else
                        {
                            low = mid + 1;
                        }
                    }
                }
            }

            return false;
        }
        #endregion

        #region 是否为回文数
        /// <summary>
        /// 121 22 -121 -22
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool isPalindrome(int n)
        {
            if (n == int.MinValue)
            {
                return false;
            }
            n = Math.Abs(n);
            int help = 1;
            while (n / help >= 10) // 防止help 溢出
            {
                help *= 10;
            }

            while (n != 0) // n=121, help =10  
            {
                if (n / help != n % 10)
                {
                    return false;
                }
                n = (n % help) / 10;
                help /= 100;
            }
            return true;
        }

        #endregion

        #region 翻转数字
        public static int reverse(int x)
        {
            int res = 0;
            while (x != 0)
            {
                int t = x % 10;  // 取个位的余数
                int newRes = res * 10 + t;
                //如果数字溢出，直接返回0
                if ((newRes - t) / 10 != res)
                    return 0;
                res = newRes;
                x = x / 10;
            }
            return res;
        }
        #endregion

        #region 判断字符串为回文
        public static bool judge(string str)
        {
            // write code here
            if (str == null || str == "")
                return true;
            char[] target = str.ToCharArray();
            int len = target.Length - 1;
            int mid = len / 2;
            for (int i = 0; i <= mid; i++)
            {
                if (target[i] != target[len - i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region NC65 斐波那契数列
        /// <summary>
        /// O(2^n) O(1) 从0 开始的
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Fibonacci(int n)
        {
            if (n <= 1)
            {
                return 1;
            }
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        /// <summary>
        /// 空间换时间 
        /// O(n) O(n)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int FibonacciSpace(int n)
        {
            int[] ans = new int[40];
            ans[0] = 0;
            ans[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                ans[i] = ans[i - 1] + ans[i - 2];
            }
            return ans[n];
        }
        /// <summary>
        /// 存储优化 O(n)，O(1)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int FibonacciSpacePri(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }

            int sum = 0;
            int two = 0;
            int one = 1;
            for (int i = 2; i <= n; i++)
            {
                sum = two + one;
                two = one;
                one = sum;
            }
            return sum;
        }
        #endregion

        #region NC103 反转字符串

        /// <summary>
        /// 不动原字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReveseStrSolve(string str)
        {
            // write code here
            if (str.Length == 0)
                return "";
            char[] arrChar = str.ToCharArray();
            int len = arrChar.Length - 1;
            int mid = len / 2;
            for (int i = 0; i <= mid; i++)
            {
                char tmp = arrChar[i];
                arrChar[i] = arrChar[len - i];
                arrChar[len - i] = tmp;

            }
            return string.Join("", arrChar);
        }

        /// <summary>
        /// 动原字符串 优化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReveseStrSolvePiro(string str)
        {
            // write code here
            int len = str.Length;
            if (len == 0) return "";
            char[] arrChar = new char[len];
            for (int i = 0; i < len; i++)
            {
                arrChar[i] = str[len - i - 1];
            }
            return string.Join("", arrChar);
        }
        #endregion

        #region NC151 最大公约数
        /// <summary>
        /// 辗转相除法: 如果q和r分别是m除以n的商及余数,即m=nq+r, 那么m和n的最大公约数等于n 和 r的最大公约数。
        /// </summary>
        /// <param name="m">被除数</param>
        /// <param name="n">余数</param>
        /// <returns></returns>
        public static int gcd(int m, int n)
        {
            // write code here
            return n == 0 ? m : gcd(n, m % n);
        }

        #endregion

        #region NC68 跳台阶
        /// <summary>
        /// 跳台阶
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int jumpFloor(int n)
        {
            // write code here
            if (n <= 1) return 1;
            return jumpFloor(n - 1) + jumpFloor(n - 2);
        }
        /// <summary>
        /// 一次跳1个台阶或2个台阶，跳n个台阶的方法有多少种
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int f1(int n)
        {
            if (n < 1)
            {
                return 0;
            }
            if (n == 1 || n == 2)
            {
                return 1;
            }

            return f1(n - 1) + f1(n - 2);
        }

        /// <summary>
        /// O(n)
        ///    res  = res   + pre 
        /// => f(n) = f(n-1)+ f(n+2)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int f2(int n)
        {
            if (n < 1)
                return 0;
            if (n == 1 || n == 2)
            { return 1; }

            int tmp = 0;
            int res = 2; // result
            int pre = 1;
            for (int i = 3; i <= n; i++)
            {
                tmp = res;
                res = res + pre;
                pre = tmp;
            }
            return res;
        }
        /// <summary>
        /// O(nlogn)
        /// 二阶递推数列
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int f3(int n)
        {
            if (n < 1)
            {
                return 0;
            }
            if (n == 1 || n == 2)
            {
                return 1;
            }
            int[,] baseData = { { 1, 1 }, { 1, 0 } };
            int[,] res = matrixPower(baseData, n - 2);
            return 2 * res[0, 0] + res[1, 0];
        }

        private static int[,] matrixPower(int[,] m, int p)
        {
            int[,] res = new int[m.GetLength(0), m.GetLength(1)];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                res[i, i] = 1;
            }
            int[,] tmp = m;
            for (; p != 0; p >>= 1)
            {
                if ((p & 1) != 0)
                {
                    res = muliMaxtri(res, tmp);
                }
                tmp = muliMaxtri(tmp, tmp);
            }
            return res;
        }

        private static int[,] muliMaxtri(int[,] m1, int[,] m2)
        {
            int[,] res = new int[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m2.GetLength(1); j++)
                {
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        res[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return res;
        }
        #endregion

        #region NC156 数组中只出现一次的数（其它数出现k次）


        /// <summary>
        /// NC156 数组中只出现一次的数（其它数出现k次）
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int foundOnceNumber(List<int> arr, int k)
        {
            // write code here
            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (var item in arr)
            {
                if (!map.ContainsKey(item))
                    map.Add(item, 1);
                else
                    map[item] += 1;
            }
            foreach (var item in map)
                if (item.Value == 1)
                    return item.Key;
            return -1;
        }
        #endregion

        #region NC31 第一个只出现一次的字符的位置
        /// <summary>
        /// 非hashtable，使用ASCII 进行计算 （只包含字符）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int FirstNotRepeatingChar(string str)
        {
            // 开辟一个数组
            int[] dp = new int[123];  // 存储字母出现的次数  例如: dp[122] = 2，表示:小写字母z，出现了两次
                                      // 统计出现的次数
            for (int i = 0; i < str.Length; i++)
            {
                dp[str[i]]++;
            }
            // 遍历字符串，若字符只出现一次，则返回下标
            for (int i = 0; i < str.Length; i++)
            {
                if (dp[str[i]] == 1)
                    return i;
            }
            return -1;
        }
        #endregion


        static void Main(string[] args)
        {
            int[] _numberInRotateArray = { 1, 1, 1, 0, 1, 1, 1, 1 }; //{ 1, 0, 1, 1, 1 } {3,4,5,1,2},{3,100,200,3}
            var rs = minNumberInRotateArray(_numberInRotateArray);
            var rs1 = minNumberInRotateArrayv1(_numberInRotateArray); // { 1, 1,1, 0, 1, 1, 1,1 } 更优
            WriteLine(rs1);
            int[] _numberInRotateArray1 = { 3, 4, 5, 1, 2 };
            var rs2 = isContainsInRotateArray(_numberInRotateArray1, 4);
            WriteLine(rs2);
            int paLindrome = 1221;  //121
            WriteLine(isPalindrome(paLindrome));
            WriteLine(reverse(87612));

            WriteLine(judge("abaa"));

            WriteLine(Fibonacci(9)); // 从0 开始的
            WriteLine(FibonacciSpace(10));

            WriteLine(FibonacciSpacePri(10));
            string rsStr = ReveseStrSolve("abcd");
            WriteLine(rsStr);

            WriteLine(ReveseStrSolvePiro("abcd"));
            WriteLine(gcd(8, 12)); // 3,6

            WriteLine(jumpFloor(7));

            WriteLine(f2(7));
            WriteLine(f3(7));

            int[] _foundOnceNumber = { 2, 2, 1 };// { 5, 4, 1, 1, 5, 1, 5 };
            WriteLine(foundOnceNumber(_foundOnceNumber.ToList(), 2));

            string _firstNotRepeatingChar = "221";
            WriteLine(FirstNotRepeatingChar(_firstNotRepeatingChar));
            ReadLine();
        }
    }
}
