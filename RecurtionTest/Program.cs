using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml;
using static System.Console;

namespace RecurtionTest
{
    // 1. 问题的定义是按递归定义的(Fibonacci函数，阶乘...)
    // 2. 问题的解法是递归的(有些问题只能使用递归方法来解决，例如：汉诺塔问题)
    // 3. 数据结构是递归的(链表、树等的操作，包括树的遍历)
    class Program
    {
        // 问题的定义是按递归定义的
        #region 阶乘
        // 阶乘
        public static long f(int n)
        {
            if (n == 1) // 递归中止条件
                return 1; //简单场景
            return n * f(n - 1); //相同重复逻辑，缩小问题的规模
        }
        // 递归转非递归实现
        // 1. 自己建立"堆栈"来保存这些内容以便代替系统栈，比如树的三种非遍历方式
        // 2. 把对递归的调用转变为对循环的处理
        public static long f_loop(int n)
        {
            long result = n;
            while (n > 1)
            {
                n--;
                result = result * n;
            }
            return result;
        }
        #endregion

        #region 斐波那契数列
        // 斐波那契数列
        public static int Fibonacci(int n)
        {
            if (n == 1 || n == 2) // 递归终止条件
                return 1; // 简单情景

            return Fibonacci(n - 1) + Fibonacci(n - 2); // 相同重复逻辑，缩小问题的规模
        }

        //  优化斐波那契数列
        public static int optimizeFibonacci(int first, int second, int n)// n 目标项
        {
            if (n > 0)
            {
                if (n == 1) // 递归终止条件
                {
                    return first; // 简单情景
                }
                else if (n == 2) // 递归终止条件
                {
                    return second; // 简单情景
                }
                else if (n == 3) // 递归终止条件
                {
                    return first + second; // 简单情景
                }

                return optimizeFibonacci(second, first + second, n - 1);
            }
            return -1;
        }

        public static int fibonacci_loop(int n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }

            int result = 1;
            int first = 1;  // 自己维护的栈，一遍状态回溯
            int second = 1; // 自己维护的栈，一遍状态回溯

            for (int i = 3; i <= n; i++)
            {
                result = first + second;
                first = second;
                second = result;
            }
            return result;
        }

        // 使用数据存储菲波那切数列
        public static int fabonacci_array(int n)
        {
            if (n > 0)
            {
                // 使用数组代替栈，用以存储每项的值
                int[] arr = new int[n];
                arr[0] = arr[1] = 1;

                for (int i = 2; i < n; i++)
                {
                    arr[i] = arr[i - 1] + arr[i - 2];
                }
                return arr[n - 1];
            }
            return -1;
        }
        #endregion

        #region 跳台阶
        // 跳台阶
        public static int Ladder(int n)
        {
            if (n <= 2)
                return n;

            return Ladder(n - 1) + Ladder(n - 2);
        }
        #endregion

        #region 获取杨辉三角指定行、列的值
        // 获取杨辉三角指定行、列的值
        /**
            1
           1 1
          1 2 1
         1 3 3 1
         */
        public static int getValue(int x, int y)
        {
            if (y <= x && y >= 0)
            {
                if (y == 0 || x == y)
                {
                    return 1;
                }
                else
                {
                    return getValue(x - 1, y - 1) + getValue(x - 1, y);
                }
            }
            return -1;
        }
        #endregion

        #region 链表翻转
        // 链表翻转
        public static Node ReverseList(Node head)
        {
            // 1. 递归结束条件
            if (head == null || head.next == null)
            {
                return head;
            }

            Node lastNode = ReverseList(head.next); // head: 1,2,3
            Node preNode = head.next; //4
            preNode.next = head;//4.next->3
            head.next = null;//3.next->null 准备将3.next->2, 通过2.next->3 没有变化这个条件

            return lastNode;
        }
        #endregion

        #region 判断回文
        // 使用递归实现
        public static bool isPaliindromeString_recursive(string s)
        {
            int start = 0;
            int end = s.Length - 1;
            if (end > start)
            {
                if (s[start] != s[end])
                {
                    return false;
                }
                else
                {
                    return isPaliindromeString_recursive(s.Substring(start + 1).Substring(0, end - 1));
                }
            }
            return true;
        }
        // 递归改循环
        public static bool isPalindromeString_loop(string s)
        {
            char[] str = s.ToCharArray();
            int start = 0;
            int end = str.Length - 1;
            while (end > start)
            {
                if (str[end] != str[start])
                    return false;
                else
                {
                    end--;
                    start++;
                }
            }
            return true;
        }


        #endregion

        #region 字符串全排列 使用dsf方式
        public static List<List<int>> permute(int[] nums)
        {
            int len = nums.Length;
            List<List<int>> res = new List<List<int>>();
            if (len == 0)
                return res;

            Queue<int> path = new Queue<int>();
            bool[] used = new bool[len];
            dfs(nums, len, 0, path, used, res);

            return res;
        }

        private static void dfs(int[] nums, int len, int depth, Queue<int> path, bool[] used, List<List<int>> res)
        {
            if (depth == len)
            {
                res.Add(new List<int>(path));
                return;
            }
            for (int i = 0; i < len; i++)
            {
                if (used[i])
                    continue;

                path.Enqueue(nums[i]);
                used[i] = true;
                dfs(nums, len, depth + 1, path, used, res);
                path.Dequeue();
                used[i] = false;
            }
        }
        /// <summary>
        /// 生产排列组合的递归写法
        /// </summary>
        /// <param name="t">数组</param>
        /// <param name="k">其实排列值</param>
        /// <param name="n">数组长度</param>
        public static void perm1(int[] t, int k, int n)
        {
            if (k == n - 1)
            {
                for (int i = 0; i < n; i++)
                {
                    Write(t[i] + " ");
                }
                WriteLine();
            }
            else
            {
                for (int i = k; i < n; i++)
                {
                    int tmp = t[i]; t[i] = t[k]; t[k] = tmp; //一次挑选n个字母中的一个,和前位置替换
                    perm1(t, k + 1, n); //再对其余的n-1个字母一次挑选
                    tmp = t[i]; t[i] = t[k]; t[k] = tmp; //再换回来
                }
            }
        }
        #endregion

        #region 二分查找问题
        public static int binarySearch(int[] array, int low, int high, int target)
        {
            // 递归中止条件
            if (low < high)
            {
                int mid = (low + high) >> 1;
                if (array[mid] == target)
                    return mid + 1;
                else if (array[mid] > target)
                {
                    binarySearch(array, low, mid - 1, target);
                }
                else
                {

                    binarySearch(array, mid + 1, high, target);
                }
            }
            return -1;
        }

        public static int binarySearchLoop(int[] array, int low, int high, int target)
        {
            while (low < high)
            {
                int mid = (low + high) >> 1;
                if (array[mid] == target)
                {
                    return mid + 1;
                }
                else if (array[mid] > target)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }

            }
            return -1;
        }
        #endregion

        // 问题解法按递归算法实现
        #region hano
        public static void moveDish(int level, char from, char inter, char to)
        {
            if (level == 1) // 递归终止条件
            {
                WriteLine("from " + from + " level " + level + " to " + to);
            }
            else
            {
                // 递归调用: 将level-1个盘子从from移到inter(不是一次性移动，每次只能移动一个盘子，其中to用于周转)
                moveDish(level - 1, from, to, inter); // 递归调用缩小问题规模
                // 将第level个盘子从A移到C
                WriteLine("from " + from + " level " + level + " to " + to);
                // 递归调用: 将level-1个盘子从inter移到to，from用于周转
                moveDish(level - 1, inter, from, to);// 递归调用缩小问题规模
            }
        }
        #endregion

        // 数据的结构是按递归定义的
        #region 返回二叉树的深度
        public static int getTreeDepth(Tree t)
        {
            if (t == null)
                return 0;

            int left = getTreeDepth(t.left);
            int right = getTreeDepth(t.right);

            return left > right ? left + 1 : right + 1;
        }

        public static string preOrder(Node root)
        {
            StringBuilder sb = new StringBuilder();
            if (root == null)
                return "";
            else
            {
                sb.Append(root.data + " ");
                sb.Append(preOrder(root.left));
                sb.Append(preOrder(root.right));
                return sb.ToString();
            }
        }

        #endregion

        static void Main(string[] args)
        {
            WriteLine(Fibonacci(10));
            WriteLine(Ladder(10));

            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            node1.next = node2;
            node2.next = node3;
            node3.next = node4;
            ReverseList(node1);

            // Permutation
            char[] chars = { '1', '2', '3' };

            int[] nums = new int[] { 1, 2, 3 };
            permute(nums);
            binarySearch(nums, 0, nums.Length - 1, 1);

            int ndisk = 30;
            moveDish(ndisk, 'A', 'B', 'C');
            WriteLine("Hello World!");
        }


        private static void swap(char[] s, int from, int to)
        {
            char temp = s[from];
            s[from] = s[to];
            s[to] = temp;
        }
    }


    public class Tree
    {
        public int data { get; set; }
        public Tree left { get; set; }
        public Tree right { get; set; }
    }
}
