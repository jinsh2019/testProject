using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.day5
{
    internal class Patterns
    {
        // 使用数组实现 O(n*n) 约瑟夫环
        public void findTheWinner(int n, int m)
        {
            int[] a = new int[n]; // 标志位
            int cnt = 0, i = 0, k = 0;
            while (cnt != n)
            {
                if (a[i] == 0)
                {
                    k++;
                    if (k == m)
                    {
                        a[i] = 1;
                        cnt++;
                        // i 出具顺序
                        Console.Write(i + 1 + " ");
                        k = 0;
                    }
                }
                i++;
                if (i == n) i = 0;
            }
        }

        public int[] NextGreaterElement(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            Stack<int> s = new Stack<int>();
            for (int i = n - 1; i >= 0; i--)
            {
                while (s.Count != 0 && s.Peek() <= nums[i])
                {
                    s.Pop();
                }
                res[i] = s.Count == 0 ? -1 : s.Peek();
                s.Push(nums[i]);
            }
            return res;
        }

        public int[] NextGreaterElementCircleNums(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];

            Stack<int> s = new Stack<int>();
            for (int i = 2 * n - 1; i >= 0; i--)
            {
                while (s.Count != 0 && s.Peek() <= nums[i % n])
                    s.Pop();

                res[i % n] = s.Count == 0 ? -1 : s.Peek();
                s.Push(nums[i % n]);
            }
            return res;
        }

    }
}
