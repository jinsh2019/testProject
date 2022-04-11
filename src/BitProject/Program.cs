using System;
using System.Security.Authentication;
using System.Text;
using static System.Console;
namespace BitProject
{
    class Program
    {
        #region NC112 进制转换
        public static string ConvertN2M(int M, int N)
        {
            if (M == 0) return "0";
            string s = "0123456789ABCDEF";
            StringBuilder sb = new StringBuilder();
            bool f = false;
            if (M < 0)
            {
                f = true;
                M = -M;
            }
            while (M != 0)
            {
                sb.Append(s[M % N]);
                M /= N;
            }
            if (f) sb.Append("-");
            Reverse(sb);
            return sb.ToString();
        }

        public static void Reverse(StringBuilder sb)
        {
            char t;
            int end = sb.Length - 1;
            int start = 0;

            while (end - start > 0)
            {
                t = sb[end];
                sb[end] = sb[start];
                sb[start] = t;
                start++;
                end--;
            }
        }
        #endregion

        #region 不用额外变量交换两个整数的值
        /// <summary>
        /// a = 4 100; b= 3 011
        /// a = 000
        /// b= 100
        /// a=011
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static void exchangeInt(int a, int b)
        {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
        }
        #endregion

        #region NC120 二进制中1的个数
        /// <summary>
        /// 直观的做法是取余，需要判断负数的情况,负数需要求补码，比较麻烦；需要选取以下操作：
        /// 对32位采取移位后“与”操作， 这个操作最牛
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int NumberOf1(int n)
        {
            int res = 0;
            for (int i = 0; i < 32; i++)
                res += (n >> i) & 1;
            return res;
        }

        #endregion
        static void Main(string[] args)
        {

            WriteLine(ConvertN2M(7, 2));
            WriteLine(ConvertN2M(10, 16));
            WriteLine(NumberOf1(-1)); // 2 10 -1 -2147483648
            WriteLine("Hello World!");
        }
    }
}
