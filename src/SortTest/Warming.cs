using CDS;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SortTest
{
    public class Warming
    {
        public IList<IList<string>> res = new List<IList<string>>();
        public List<string> path = new List<string>();
        public int[][] memo;

        public void ClearContext()
        {
            res = new List<IList<string>>();
            path = new List<string>();
            memo = null;
        }
        /// <summary>
        /// 131. 分割回文串
        /// 分析：使用回溯的方法，将字符串按顺序进行排列，
        /// 同时对新生成字符串回文性进行判断
        /// 1. 回溯
        /// 2. start是字符位置，如果s遍历完成，则把path添加到结果集中
        /// 3. 判断是否是回文
        /// 4. 截取字符串
        /// </summary>
        public IList<IList<string>> Partition(string s)
        {
            backtrack(s, 0);                                // 1. 

            return res;
        }

        public void backtrack(string s, int start)          // 2. 
        {
            if (start == s.Length)
                res.Add(new List<string>(path));

            for (int i = start; i < s.Length; i++)
            {
                if (!isPalindrome(s, start, i))             // 3. 
                    continue;

                path.Add(s.Substring(start, i + 1 - start));// 4. 
                Console.WriteLine("start:" + start + " substring:" + s.Substring(start, i + 1 - start));
                backtrack(s, i + 1);
                Console.WriteLine("Remove::start:" + start + " substring:" + path[path.Count - 1]);
                path.RemoveAt(path.Count - 1);              // 5

            }
        }

        private bool isPalindrome(string s, int lo, int hi)
        {
            while (lo < hi)
            {
                if (s[lo] != s[hi])
                {
                    return false;
                }
                lo++;
                hi--;
            }
            return true;
        }

        /// <summary>
        /// 132. 分割回文串 II
        /// 分析: 需要s中的回文串缓存到g[l][r]中，以备后面dp使用
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int MinCut(string s)
        {
            int n = s.Length;
            char[] chs = s.ToCharArray();

            // g[l][r] 代表 [l,r] 这一段是否为回文串
            bool[][] g = new bool[n + 1][];
            for (int i = 0; i < n + 1; i++)
            {
                g[i] = new bool[n + 1];
            }

            for (int r = 1; r < n + 1; r++)
            {
                for (int l = r; l >= 1; l--)
                {
                    if (l == r)
                    {
                        g[l][r] = true;
                    }
                    else
                    {
                        // 在 l 和 r 字符相同的前提下
                        if (chs[l - 1] == chs[r - 1])
                        {
                            // 如果 l 和 r 长度只有 2；或者 [l+1,r-1] 这一段满足回文，则[l,r]属于回文
                            if (r - l == 1 || g[l + 1][r - 1])
                            {
                                g[l][r] = true;
                            }
                        }
                    }
                }
            }

            // dp[r] 代表将 [1,r] 这一段分割成若干回文子串所需要的最小分割次数
            int[] dp = new int[n + 1];
            for (int r = 1; r <= n; r++)
            {
                // 如果 [1,r] 满足回文，不需要分割
                if (g[1][r])
                {
                    dp[r] = 0;
                }
                else
                {
                    // 先设定一个最大分割次数（r 个字符最多消耗 r - 1 次分割）
                    dp[r] = r - 1;
                    // 在所有符合 [l,r] 回文的方案中取最小值
                    for (int l = 1; l <= r; l++)
                    {
                        if (g[l][r])
                            dp[r] = Math.Min(dp[r], dp[l - 1] + 1);
                    }
                }
            }
            return dp[n];
        }
        /// <summary>
        /// 164. 最大间距
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaximumGap(int[] nums)
        {
            if (nums == null)
                return 0;
            if (nums.Length < 2)
                return 0;

            Array.Sort(nums);
            int maxAns = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                maxAns = Math.Max(Math.Abs(nums[i] - nums[i - 1]), maxAns);
            }
            return maxAns;
        }

        /// <summary>
        /// 95. 不同的二叉搜索树 II
        /// 分析：[1..n] 构造二叉搜索树 
        /// 举例： [1]为根 [2..n]右； [2]为根 [1]为左， [3..n]为右
        /// 每一个数都有作为根的可能性，迭代[1..n]
        /// 使用后序遍历得到leftTree和rightTree
        /// root[i].left
        /// root[i].right
        /// 1. 建立递归函数
        /// 2. 结果集，因为使用递归，不需要设置为全局变量
        /// 3. 当lo > hi 说明树构造完毕
        /// 4. 穷举 root 节点的所有可能
        /// 5. 递归构造出左右子树的所有合法
        /// 6. 穷举所有左右子树的组合
        /// 7. 以i作为根节点 root 的值
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IList<TreeNode> GenerateTrees(int n)
        {
            if (n == 0)
                return new List<TreeNode>();

            return BuildBST(1, n);                          // 1. 
        }
        /// <summary>
        /// 自定向下， 返回root
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private IList<TreeNode> BuildBST(int lo, int hi)
        {
            IList<TreeNode> res = new List<TreeNode>();     // 2. 
            // base case
            if (lo > hi)                                    // 3. 
            {
                res.Add(null);
                return res;
            }

            for (int mid = lo; mid <= hi; mid++)                   // 4.  
            {

                IList<TreeNode> leftTree = BuildBST(lo, mid - 1); // 5. 
                IList<TreeNode> rightTree = BuildBST(mid + 1, hi);

                foreach (var left in leftTree)               // 6. 
                {
                    foreach (var right in rightTree)
                    {

                        TreeNode root = new TreeNode(mid);     // 7. 
                        root.left = left;
                        root.right = right;
                        res.Add(root);
                    }
                }
            }

            return res;                                     // 8. 返回结果集
        }

        /// <summary>
        /// 96. 不同的二叉搜索树
        /// 分析：使用 95 不同的二叉搜索树 II来做，当数量达到19时，超时， 因此考虑使用
        /// 备忘录技巧进行剪枝，使用memo[n+1][n+1] 对数量 [lo..hi] 形成的树进行存储，lo,hi的取值范围[1..n]
        /// 1. 定义备忘录
        /// 2. 使用递归 返回二叉树的不同种树
        /// 3. 作为叶子节点
        /// 4. 备忘录存在
        /// 5. 循环里面带着递归
        /// 6. 组合
        /// 7. 给memo进行赋值
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int NumTrees(int n)
        {
            memo = new int[n + 1][];                // 1. 
            for (int i = 0; i < n + 1; i++)
                memo[i] = new int[n + 1];

            return Count(1, n);                     // 2. 
        }
        /// <summary>
        /// 自定向下
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private int Count(int lo, int hi)
        {
            if (lo > hi)                            // 3. 
                return 1;

            if (memo[lo][hi] != 0)                  // 4. 
                return memo[lo][hi];

            int res = 0;
            for (int mid = lo; mid <= hi; mid++)    // 5. 
            {
                int left = Count(lo, mid - 1);
                int right = Count(mid + 1, hi);
                res += left * right;                // 6. 
            }

            memo[lo][hi] = res;                     // 7. 

            return res;
        }

        /// <summary>
        /// 220. 存在重复元素 III
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            SortedSet<long> sl = new SortedSet<long>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (sl.GetViewBetween((long)nums[i] - (long)t, (long)nums[i] + (long)t).Count() > 0)
                    return true;
                sl.Add(nums[i]);
                if (sl.Count() > k)
                    sl.Remove(nums[i - k]);
            }
            return false;
        }

        /// <summary>
        /// 31. 下一个排列
        /// </summary>
        /// <param name="nums"></param>
        public void NextPermutation(int[] nums)
        {
            int k = nums.Length - 1;
            while (k > 0 && nums[k - 1] >= nums[k]) // 正数找到第一个正序位置k 3，2，4，1,5
            {
                k--;
            }

            if (k <= 0)                             // 全逆序：5,4,3,2,1
            {
                Array.Reverse(nums);
            }
            else
            {
                int t = nums.Length - 1;
                while (nums[t] <= nums[k - 1])      // 倒数找到比k-1的值小的数 3，2，4，[1],5
                    t--;

                swap(nums, t, k - 1);                // 交换位置     3，[1]，4，[2],5

                Array.Reverse(nums, k, nums.Length - 1 - k); // 进行翻转，3，[1]，5，[2],4
            }
        }
        public void swap(int[] nums, int i, int j)
        {
            int tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }

        /// <summary>
        /// LCR 076. 数组中的第 K 个最大元素
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int FindKthLargest(int[] nums, int k)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(nums[i], nums[i]);
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            int ans = pq.Peek();
            return ans;
        }
        /// <summary>
        /// LCR 159. 库存管理 III
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public int[] InventoryManagement(int[] stock, int cnt)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < stock.Length; i++)
            {
                pq.Enqueue(stock[i], -stock[i]);
                if (pq.Count > cnt)
                {
                    pq.Dequeue();
                }
            }
            IList<int> res = new List<int>();
            while (pq.Count != 0)
            {
                res.Add(pq.Dequeue());
            }

            return res.ToArray();
        }
        /// <summary>
        /// 面试题 17.14. 最小K个数
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int[] SmallestK(int[] arr, int k)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < arr.Length; i++)
            {
                pq.Enqueue(arr[i], -arr[i]);
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            IList<int> res = new List<int>();
            while (pq.Count != 0)
            {
                res.Add(pq.Dequeue());
            }

            return res.ToArray();

        }
        /// <summary>
        /// 1337. 矩阵中战斗力最弱的 K 行
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int[] KWeakestRows(int[][] mat, int k)
        {
            int m = mat.Length;
            int n = mat[0].Length;
            int[] sum = new int[n];
            for (int i = 0; i < m; i++)
            {
                int sumRow = 0;
                for (int j = 0; j < n; j++)
                {
                    sumRow += mat[i][j];
                }
                sum[i] = sumRow;
            }

            sum = new int[] { 1, 4, 1, 1 };
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < sum.Length; i++)
            {
                pq.Enqueue(i, -sum[i]);
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            IList<int> res = new List<int>();
            while (pq.Count != 0)
            {
                res.Add(pq.Dequeue());
            }

            return res.Reverse().ToArray(); ;
        }

        /// <summary>
        /// 20. 有效的括号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsValid(string s)
        {
            if (s.Length < 2)
                return false;
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '[' || s[i] == '{')
                {
                    stack.Push(s[i]);
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        if (s[i] == ')' && stack.Peek() != '(')
                        {
                            return false;
                        }
                        if (s[i] == ']' && stack.Peek() != '[')
                        {
                            return false;
                        }
                        if (s[i] == '}' && stack.Peek() != '{')
                        {
                            return false;
                        }
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return stack.Count != 0 ? false : true;
        }


        /// <summary>
        /// 150. 逆波兰表达式求值
        /// ["2","1","+","3","*"]
        /// s1: 
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "+")
                {
                    int a = stack.Pop();
                    int b = stack.Pop();
                    stack.Push(b + a);
                }
                else if (tokens[i] == "-")
                {
                    int a = stack.Pop();
                    int b = stack.Pop();
                    stack.Push(b - a);
                }
                else if (tokens[i] == "*")
                {
                    int a = stack.Pop();
                    int b = stack.Pop();
                    stack.Push(b * a);
                }
                else if (tokens[i] == "/")
                {
                    int a = stack.Pop();
                    int b = stack.Pop();
                    stack.Push(b / a);
                }
                else
                {
                    stack.Push(int.Parse(tokens[i]));
                }
            }

            return stack.Peek();
        }

        /// <summary>
        /// 32. 最长有效括号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LongestValidParentheses(string s)
        {
            Stack<int> stack = new Stack<int>();
            int[] dp = new int[s.Length + 1]; // 定义dp[i]以结尾的最长有效括号数,i是索引+1
            dp[0] = 0;                        // 声明都不选，最长有效括号 0
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Push(i);
                    dp[i + 1] = 0;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        // ()(())
                        // dp[0] ={0,0,2,0,0,2,6}
                        //             l       i
                        int leftIndex = stack.Pop();
                        int len = i - leftIndex + 1 + dp[leftIndex];
                        dp[i + 1] = len;
                    }
                    else
                    {
                        dp[i + 1] = 0;
                    }
                }
            }

            // 计算最长子串的长度
            int res = 0;
            for (int i = 0; i < dp.Length; i++)
            {
                res = Math.Max(res, dp[i]);
            }
            return res;
        }

        /// <summary>
        /// 42. 接雨水
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int Trap(int[] height)
        {
            int l = 0, r = height.Length - 1;
            int lmax = 0, rmax = 0, ans = 0;
            while (l < r)
            {
                lmax = Math.Max(lmax, height[l]); //截至到l左最大高度
                rmax = Math.Max(rmax, height[r]); //截至到r右最大高度
                if (lmax < rmax)                  // 高度最小的那个，开始装雨水
                {
                    ans += lmax - height[l];
                    l++;
                }
                else
                {
                    ans += rmax - height[r];
                    r--;
                }
            }

            return ans;
        }

        /// <summary>
        /// 71. 简化路径
        /// 
        /// 1. 初始化路径
        /// 2. 上级目录
        /// 3. 非当前目录  且 非空字符产,进栈
        /// 4. 无任何目录，以/ 开头
        /// 5. 栈底目录名作为父目录
        /// /home//foo/
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string SimplifyPath(string path)
        {
            string[] arr = Regex.Split(path, "/+");
            Stack<string> stack = new Stack<string>();
            foreach (var s in arr)                          // 1
            {
                if (s == "..")                              // 2
                {
                    if (stack.Count != 0)
                    {
                        stack.Pop();
                    }
                }
                else if (s != "." && s != "")               // 3
                {
                    stack.Push(s);
                }
            }

            if (stack.Count == 0)                           // 4
            {
                return "/";
            }

            string ans = "";
            while (stack.Count != 0)
            {
                ans = "/" + stack.Pop() + ans;             // 5
            }
            return ans;
        }


        /// <summary>
        /// 227. 基本计算器 II
        /// 分析： 使用栈作为存储结构实现正负数相加
        /// */号优先级较高，所以，遇到*/需要取之前的num
        /// 计算后，再入栈
        /// 1. 使用栈存储需要计算的数字
        /// 2. 运算符号
        /// 3. 如果时2位数字以上位数相乘
        /// 4. 如果非数字且非空，或 到尾部
        /// 5. 前一个数字
        /// 6. 遇到+/-入栈，等待使用
        /// 7. 遇到*// 栈顶元素作为pre，num作为后续
        /// 8. 计算后入栈等待使用
        /// 9. 连加
        /// 3+5 / 2 
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int Calculate(string s)
        {
            Stack<int> stack = new Stack<int>();                    // 1
            int num = 0;
            char sign = '+';                                        // 2

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (char.IsDigit(c))                                // 3
                {
                    num = num * 10 + (c - '0');
                }

                if ((!char.IsDigit(c) && c != ' ')                  // 4
                    || i == s.Length - 1)
                {
                    int pre = 0;                                    // 5
                    switch (sign)
                    {
                        case '+':                                   // 6
                            stack.Push(num);
                            break;
                        case '-':
                            stack.Push(-num);
                            break;
                        case '*':                                   // 7，
                            pre = stack.Peek();
                            stack.Pop();
                            stack.Push(pre * num);                  // 8
                            break;
                        case '/':
                            pre = stack.Peek();
                            stack.Pop();
                            stack.Push(pre / num);
                            break;
                    }

                    sign = c;
                    num = 0;
                }
            }
            int res = 0;
            while (stack.Count != 0)                                // 9
            {
                res += stack.Peek();
                stack.Pop();
            }

            return res;
        }

        /// <summary>
        /// 84. 柱状图中最大的矩形
        /// 1. 单调递增栈
        /// 2. 当前元素小于栈顶元素，可以确定面积
        /// 3. 获取栈顶元素
        /// 4. 获取左边界
        /// 5. 宽度 (i - left - 1)
        /// </summary>
        /// <param name="heights"></param>
        /// <returns></returns>
        public int LargestRectangleArea(int[] heights)
        {
            Stack<int> stack = new Stack<int>();            // 1
            int ans = 0;
            List<int> list = new List<int>(heights);
            list.Add(-1);
            for (int i = 0; i < list.Count; i++)
            {
                while (stack.Count != 0                     // 2
                    && list[i] < list[stack.Peek()])
                {
                    int idx = stack.Pop();                  // 3 
                    int left = stack.Count == 0 ? -1 : stack.Peek();    // 4
                    ans = Math.Max(ans, (i - left - 1) * list[idx]);    // 5

                }
                stack.Push(i);
            }

            return ans;
        }

        /// <summary>
        /// 85. 最大矩形
        /// 分析：
        /// 是84题的升级版本，求从第i行的最大矩形面积
        /// 1. 遍历举行，求第i行的的柱子高度
        /// 2.  根据高度，计算面积是多少
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public int MaximalRectangle(char[][] matrix)
        {
            int m = matrix.Length, n = matrix[0].Length;
            int[] heights = new int[n];
            int ans = 0;
            for (int i = 0; i < m; i++)                 // 遍历举行，求第i行的的柱子高度
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == '1')
                        heights[j]++;
                    else
                        heights[j] = 0;
                }
                ans = Math.Max(ans, maxArea(heights));  // 根据高度，计算面积是多少
            }

            return ans;
        }
        // 与84题一样
        private int maxArea(int[] heights)
        {
            Stack<int> stack = new Stack<int>();
            int ans = 0;
            IList<int> list = new List<int>(heights);
            list.Add(-1);
            for (int i = 0; i < list.Count; i++)
            {
                while (stack.Count != 0 && list[i] < list[stack.Peek()])
                {
                    int idx = stack.Pop();
                    int left = stack.Count == 0 ? -1 : stack.Peek();
                    ans = Math.Max(ans, (i - left - 1) * list[idx]);
                }
                stack.Push(i);
            }

            return ans;
        }


        /// <summary>
        /// 2530. 执行 K 次操作后的最大分数
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public long MaxKelements(int[] nums, int k)
        {
            PriorityQueue<long, long> pq = new PriorityQueue<long, long>();
            foreach (var num in nums)
            {
                pq.Enqueue(num, -num);
            }
            long ans = 0;
            while (k != 0)
            {
                long tmp = pq.Dequeue();
                ans += tmp;
                tmp = (long)Math.Ceiling(((double)tmp / 3));
                pq.Enqueue(tmp, -tmp);
                k--;
            }

            return (long)ans;
        }

        /// <summary>
        /// 121. 买卖股票的最佳时机
        /// 7,1,5,3,6,4
        ///   b     s
        ///   
        /// 7,6,4,3,1
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfit(int[] prices)
        {
            int minPrice = prices[0];
            int ans = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (minPrice > prices[i])
                {
                    minPrice = prices[i];
                }
                else
                {
                    ans = Math.Max(ans, prices[i] - minPrice);
                }
            }

            return ans;
        }

        /// <summary>
        /// 122. 买卖股票的最佳时机 II
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfitII(int[] prices)
        {
            int ans = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i - 1] < prices[i])
                {
                    ans += prices[i] - prices[i - 1];
                }
            }

            return ans;
        }

        /// <summary>
        /// 123. 买卖股票的最佳时机 III
        /// 7,1,5,3,4,6
        ///   b s b   s
        /// 前后缀分解的思想来解决 将prices 分成左右两组
        /// 左侧做一笔交易的最大利润+ 右侧做一笔交易的最大利润
        /// left[], right[]
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfitIII(int[] prices)
        {
            int n = prices.Length;
            int[] left = new int[n];            // 左侧数组left[i]  截至到i最大利润
            int[] right = new int[n];           // 右侧数组right[i] 截止到i的最大利润
            for (int i = 1, minPrice = prices[0]; i < n; i++)
            {
                left[i] = Math.Max(left[i - 1], prices[i] - minPrice);
                minPrice = Math.Min(minPrice, prices[i]);       // 取最小值
            }

            for (int i = n - 2, maxPrice = prices[n - 1]; i >= 0; i--)
            {
                right[i] = Math.Max(right[i + 1], maxPrice - prices[i]);
                maxPrice = Math.Max(maxPrice, prices[i]);       // 取最大值
            }

            int ans = 0;
            for (int i = 0; i < n; i++)
            {
                ans = Math.Max(ans, left[i] + right[i]);        // 求和最大
            }
            return ans;
        }
        /// <summary>
        /// 188. 买卖股票的最佳时机 IV
        /// 分析：
        ///  dp[i][j][0] 第i天，第j次交易，空仓
        ///  dp[i][j][1] 第i天，第j次交易，满仓
        /// 转移方程： 
        /// dp[i][j][0] = Math.Max(dp[i - 1][j][0], dp[i - 1][j][1] + prices[i - 1]) 
        /// 第i天，第j次交易 空仓 = 第i-1天j次交易的空仓 与  第i-1天j次交易后 卖出 取其大
        /// dp[i][j][1] = Math.Max(dp[i - 1][j][1], dp[i - 1][j - 1][0] - prices[i - 1])
        /// 第i天，第j次交易 满仓 = 第i-1天j次交易的满仓 与  第i-1天j次交易后 买入 取其大
        /// </summary>
        /// <param name="k"></param>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfitIV(int k, int[] prices)
        {
            int n = prices.Length;
            int[][][] dp = new int[n + 1][][];
            for (int i = 0; i < n + 1; i++)
            {
                dp[i] = new int[k + 1][];               // [i] 天完成k次交易
            }
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < k + 1; j++)
                {
                    dp[i][j] = new int[2];              // [i] 天第k次交易中的是空仓或满仓
                }
            }
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < k + 1; j++)
                {
                    for (int m = 0; m < 2; m++)
                    {
                        if (m == 0)
                            dp[i][j][m] = 0;
                        else
                            dp[i][j][m] = -0x3f;        // 0x3f接近无穷大的数: 无穷大+无穷大=无穷大
                    }
                }
            }

            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < k + 1; j++)
                {
                    dp[i][j][0] = Math.Max(dp[i - 1][j][0], dp[i - 1][j][1] + prices[i - 1]);
                    dp[i][j][1] = Math.Max(dp[i - 1][j][1], dp[i - 1][j - 1][0] - prices[i - 1]);
                }
            }
            int ans = 0;
            for (int i = 0; i < k + 1; i++)
            {
                ans = Math.Max(ans, dp[n][k][0]);
            }

            return ans;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="n"></param>
        ///// <param name="k"></param>
        ///// <returns></returns>
        //public string GetPermutation(int n, int k)
        //{

        //}


        /// <summary>
        /// 6. N 字形变换
        /// 行数 0..numRows
        /// 方向 down= false;
        /// 1. 定义row， 包含numRows个字符
        /// 2. 方向默认向上
        /// 3. 将字符放在第idx位置上
        /// 4. 遇到头尾，改变方向
        /// 5. 向上或向下
        /// 6. 串联字符串
        /// 7. 返回结果
        /// </summary>
        /// <param name="s"></param>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public string Convert(string s, int numRows)
        {
            if (numRows == 1)
                return s;
            string[] row = new string[numRows];             // 1
            bool down = false;                              // 2
            for (int i = 0, idx = 0; i < s.Length; i++)
            {
                row[idx] += s[i];                           // 3
                if (idx == 0 || idx == numRows - 1)         // 4
                    down = !down;
                idx += down ? 1 : -1;                       // 5
            }

            string ans = "";                                // 6
            for (int i = 0; i < numRows; i++)
            {
                ans += row[i];
            }

            return ans;                                     // 7
        }

        /// <summary>
        /// 18. 四数之和
        /// 1. 去重i
        /// 2. 去重j
        /// 3. 两数之和 有重复
        /// 4. 去重
        /// 5. 去重
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            IList<IList<int>> res = new List<IList<int>>();
            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;              // 去重i
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1]) continue;      // 去重j
                    int l = j + 1, r = nums.Length - 1;
                    long sum = (long)target - (long)nums[i] - (long)nums[j];                   // 两数之和 有重复
                    while (l < r)
                    {
                        if (nums[l] + nums[r] == sum)
                        {
                            res.Add(new int[] { nums[i], nums[j], nums[l], nums[r] });
                            while (l < r && nums[l] == nums[l + 1]) l++;    // 去重
                            while (l < r && nums[r] == nums[r - 1]) r--;    // 去重
                            l++; r--;
                        }
                        else if (nums[l] + nums[r] < sum)
                        {
                            l++;
                        }
                        else
                        {
                            r--;
                        }
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// 97. 交错字符串
        /// 1. 状态数组
        /// 2. 处理边界问题
        /// 3. 空串组合成空串
        /// 4. s1[i]与 s[i+j]相等
        /// 5. 前面情况相等时,赋值给dp(i,j)
        /// 6. s2[j]与 s[i+j]相等
        /// 7. ** 两个字符串第[i][j]可能相等，则前面两种情况只要一个相等即可
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <param name="s3"></param>
        /// <returns></returns>
        public bool IsInterleave(string s1, string s2, string s3)
        {
            int n = s1.Length, m = s2.Length;
            if (n + m != s3.Length) return false;

            bool[][] dp = new bool[n + 1][];                            // 1
            for (int i = 0; i < n + 1; i++)
                dp[i] = new bool[m + 1];

            s1 = " " + s1;                                              // 2
            s2 = " " + s2;
            s3 = " " + s3;
            dp[0][0] = true;                                            // 3

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    if (i > 0 && (s1[i] == s3[i + j]))                  // 4
                    {
                        dp[i][j] = dp[i - 1][j];                        // 5
                    }
                    if (j > 0 && (s2[j] == s3[i + j]))                  // 6
                    {
                        dp[i][j] = dp[i][j] || dp[i][j - 1];            // 7 
                    }
                }
            }

            return dp[n][m];
        }

        /// <summary>
        /// 140. 单词拆分 II 
        /// s = "catsanddog", wordDict = ["cat","cats","and","sand","dog"]
        /// 使用dfs对字符串进行拆分,且与wordDict 进行匹配
        /// c
        ///  ca
        ///     cat
        /// a
        ///   at
        ///     ats
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDict"></param>
        /// <returns></returns>
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            HashSet<string> set = new HashSet<string>(wordDict);
            List<string> res = new List<string>();
            dfs(s, 0, "", set, res);
            return res;
        }

        private void dfs(string s, int start, string cur, HashSet<string> set, List<string> res)
        {
            if (start == s.Length)                                                      // base case
            {
                res.Add(cur);
                return;
            }

            for (int i = start; i < s.Length; i++)
            {
                string word = s.Substring(start, i - start + 1);                        // 从 start 开始截取
                if (set.Contains(word))
                {
                    dfs(s, i + 1, cur.Length != 0 ? cur + " " + word : word, set, res); // 递归从下一个字符开始
                }
            }
        }

        /// <summary>
        /// 1726. 同积元组
        /// 2,3,4,6
        /// [3,4] [2,6] 
        /// [3,4] [6,2]
        /// [4,3] [2,6]
        /// [4,3] [6,2]
        /// 
        /// [2,6] [3,4]
        /// [2,6] [4,3]
        /// [6,2] [3,4]
        /// [6,2] [4,3]
        /// 分析：数学 + 哈希
        /// [a,b]2种组合，[c,d] 2种【组合】，
        /// 同积 [m1,m2] 2种组合 从内部看组合有：2*2*2=8种
        /// 基础结论: 任意选择两组数对，可以构成8个不同的同积元组
        /// 如果有2种选择，则需要从C(2,2)种不同组合 2*(2-1)/2 * 8 = 8种
        /// 如果有3种选择，则需要从C(3,2)种不同组合 3*(3-1)/2 * 8 = 24种
        /// 如果有4种选择，则需要从C(4,2)种不同组合 4*(4-1)/2 * 8 = 48种
        /// 如果有n种选择，则需要从C(n,2)种不同组合 n*(n-1)/2 * 8 =n*(n-1)*4种
        /// 需要统计nums种有多少同积，根据公式算出组合总数
        /// 1. Dic<乘积，计数>
        /// 2. 新增+修改
        /// 3. 利用公式进行计算
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int TupleSameProduct(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();      // 1
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    int mul = nums[i] * nums[j];
                    dic.TryAdd(mul, 0);                                 // 2                     
                    dic[mul]++;
                }
            }
            int ans = 0;
            foreach (KeyValuePair<int, int> pair in dic)
            {
                int val = pair.Value;
                ans += val * (val - 1) / 2 * 8;                           // 3
            }

            return ans;
        }


        /// <summary>
        /// 974. 和可被 K 整除的子数组
        /// （前缀模） 用于统计同余数目
        /// [4,5,0,-2,-3,1]
        /// [4,5,0,-2,-3,1]
        /// sum[l..r] = sum[r] - sum[l]
        /// 
        /// 1. 处理前缀同余第0个位置
        /// 2. 处理前缀同余第[1..n-1]个位置
        /// 3. count 数组
        /// 4. 首次ans+0，第二次+1,第三次+2
        /// 5. count数组做统计
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int SubarraysDivByK(int[] nums, int k)
        {
            int[] sum = new int[nums.Length];

            if (nums[0] % k < 0)                        // 1
            {
                sum[0] = (nums[0] % k) + k;
            }
            else
            {
                sum[0] = nums[0] % k;
            }

            for (int i = 1; i < nums.Length; i++)       // 2
            {
                sum[i] = (sum[i - 1] + nums[i]) % k;
                if (sum[i] < 0) sum[i] += k;
            }

            int[] count = new int[3 * 10000 + 1];       // 3
            count[0] = 1;
            int ans = 0;
            for (int i = 0; i < sum.Length; i++)
            {
                ans += count[sum[i]];                   // 4. 首次ans+0，第二次+1,第三次+2
                count[sum[i]]++;                        // 5. count数组做统计
            }

            return ans;
        }


        /// <summary>
        /// 29. 两数相除
        /// 知识点： 快速幂
        /// (10,3)
        /// 3的倍数     记录倍数 
        ///  -3，        -1 2^0
        ///  -6，        -2 2^1
        ///  -12，       -4 2^2
        ///  -24，       -8 2^3
        ///  
        /// 2^31是int 最大值，做31次运算即可
        /// 利用映射关系，每次对除数取差值，同时添加除数
        /// 1. 初始化快速幂 
        /// 2. 避免越界
        /// 3. -12 > -10 ， -3>-4
        /// 4. ans = -2 -3
        /// 5. x= -1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Divide(int x, int y)
        {
            bool sign = (x > 0 && y < 0) || (x < 0 && y > 0);
            if (x > 0) x = -x;
            if (y > 0) y = -y;

            List<KeyValuePair<int, int>> exp = new List<KeyValuePair<int, int>>();
            for (int i = y, j = -1; j >= x; i += i, j += j)         // 1. 初始化快速幂 
            {
                exp.Add(new KeyValuePair<int, int>(i, j));
                if (i < int.MinValue / 2) break;                    // 2. 避免越界
            }

            int ans = 0;
            for (int i = exp.Count - 1; i >= 0; i--)
            {
                if (exp[i].Key >= x)                                // 3. -12 > -10 ， -3>-4
                {
                    ans += exp[i].Value;                            // 4. ans = -2 -3
                    x -= exp[i].Key;                                // 5. x= -1
                }
            }

            if (sign) return ans;
            if (ans == int.MinValue) return int.MaxValue;

            return -ans;
        }

        /// <summary>
        /// 144. 二叉树的前序遍历
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<int> PreorderTraversal(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null) return res;
            dfs_PreorderTraversal(root, res);
            return res;
        }
        public void dfs_PreorderTraversal(TreeNode root, IList<int> res)
        {
            if (root == null) return;

            res.Add(root.val);
            dfs_PreorderTraversal(root.left, res);
            dfs_PreorderTraversal(root.right, res);
        }


        /// <summary>
        /// 94. 二叉树的中序遍历
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<int> InorderTraversal(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null) return res;
            dfs_InorderTraversal(root, res);
            return res;
        }

        private void dfs_InorderTraversal(TreeNode root, IList<int> res)
        {
            if (root == null) return;

            dfs_InorderTraversal(root.left, res);
            res.Add(root.val);
            dfs_InorderTraversal(root.right, res);
        }

        /// <summary>
        /// 102. 二叉树的层序遍历
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null) return res;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                int size = queue.Count;
                List<int> list = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    TreeNode cur = queue.Dequeue();
                    list.Add(cur.val);
                    if (cur.left != null)
                        queue.Enqueue(cur.left);
                    if (cur.right != null)
                        queue.Enqueue(cur.right);
                }
                res.Add(list);
            }

            return res;
        }

        /// <summary>
        /// 103. 二叉树的锯齿形层序遍历
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null) return res;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            bool bflag = true;
            while (queue.Count != 0)
            {
                int size = queue.Count;
                List<int> list = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    TreeNode node = queue.Dequeue();

                    if (node.left != null)
                        queue.Enqueue(node.left);
                    if (node.right != null)
                        queue.Enqueue(node.right);

                    list.Add(node.val);
                }

                if (!bflag) list.Reverse();
                res.Add(list);
                bflag = !bflag;
            }

            return res;
        }


        /// <summary>
        /// 199. 二叉树的右视图
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<int> RightSideView(TreeNode root)
        {
            IList<int> res = new List<int>();
            if (root == null) return res;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                int size = queue.Count;
                int last = queue.Peek().val;
                for (int i = 0; i < size; i++)
                {
                    TreeNode node = queue.Dequeue();
                    if (node.right != null)
                        queue.Enqueue(node.right);
                    if (node.left != null)
                        queue.Enqueue(node.left);
                }

                res.Add(last);
            }

            return res;
        }

        /// <summary>
        /// 98. 验证二叉搜索树
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public bool IsValidBST(TreeNode root)
        {
            return dfs_IsValidBST(root, null, null);

        }

        private bool dfs_IsValidBST(TreeNode root, TreeNode min, TreeNode max)
        {
            if (root == null) return true;

            if (min != null && root.val <= min.val)
                return false;
            if (max != null && root.val >= max.val)
                return false;

            return dfs_IsValidBST(root.right, root, max) && dfs_IsValidBST(root.left, min, root);
        }

        /// <summary>
        /// LCR 174. 寻找二叉搜索树中的目标节点 第k大的数
        /// 二叉搜索树中序遍历先左后右： 递增序列
        /// 二叉搜索树中序遍历先右后左： 递减序列
        /// </summary>
        /// <param name="root"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public int FindTargetNode(TreeNode root, int cnt)
        {
            if (root == null) return -1;

            int ans = 0, rank = 0;
            dfs_FindTargetNode(root, cnt, ref rank, ref ans);

            return ans;
        }
        void dfs_FindTargetNode(TreeNode root, int k, ref int rank, ref int ans)
        {
            if (root == null) return;

            dfs_FindTargetNode(root.right, k, ref rank, ref ans);
            rank++;
            if (k == rank)
            {
                ans = root.val;
                return;
            }
            dfs_FindTargetNode(root.left, k, ref rank, ref ans);
        }
        /// <summary>
        /// LCR 155. 将二叉搜索树转化为排序的双向链表
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>

        public Node TreeToDoublyList(Node root)
        {
            return null;
        }

        /// <summary>
        /// 104. 二叉树的最大深度
        ///    2
        ///  1   NULL
        /// n n  n  n
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;

            int lMax = MaxDepth(root.left);
            int rMax = MaxDepth(root.right);

            return Math.Max(lMax, rMax) + 1;
        }

        /// <summary>
        /// 110. 平衡二叉树
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public bool IsBalanced(TreeNode root)
        {
            if (root == null) return true;

            bool bflag = false;
            isBalance(root, ref bflag);
            return bflag;
        }
        int isBalance(TreeNode root, ref bool bflag)
        {
            if (root == null) return 0;

            int lMax = isBalance(root.left, ref bflag);
            int rMax = isBalance(root.right, ref bflag);

            if (Math.Abs(lMax - rMax) > 1)
                bflag = false;

            return Math.Max(lMax, rMax) + 1;
        }

        /// <summary>
        /// 543. 二叉树的直径
        /// </summary>
        int maxDiameter = 0;
        public int DiameterOfBinaryTree(TreeNode root)
        {
            if (root == null) return 0;
            maxDepth_DiameterOfBinaryTree(root);
            return maxDiameter;
        }

        private int maxDepth_DiameterOfBinaryTree(TreeNode root)
        {
            if (root == null) return 0;

            int lMax = maxDepth_DiameterOfBinaryTree(root.left);
            int rMax = maxDepth_DiameterOfBinaryTree(root.right);

            maxDiameter = Math.Max(lMax + rMax, maxDiameter);

            return Math.Max(lMax, rMax) + 1;
        }

        /// <summary>
        /// 101. 对称二叉树
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null) return true;
            return dfsCheck(root.left, root.right);
        }

        private bool dfsCheck(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;

            if (left.val != right.val)
                return false;

            return dfsCheck(left.left, right.right) && dfsCheck(left.right, right.left);
        }

        /// <summary>
        /// 226. 翻转二叉树
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return null;

            TreeNode temp = root.right;
            root.right = root.left;
            root.left = temp;

            InvertTree(root.left);
            InvertTree(root.right);

            return root;
        }

        /// <summary>
        /// 236. 二叉树的最近公共祖先
        /// </summary>
        /// <param name="root"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null) return root;

            TreeNode left = LowestCommonAncestor(root.left, p, q);
            TreeNode right = LowestCommonAncestor(root.right, p, q);

            if (root == p || root == q)
                return root;
            if (left != null && right != null)
                return root;

            return left == null ? right : left;
        }

        /// <summary>
        /// 112. 路径总和
        /// </summary>
        /// <param name="root"></param>
        /// <param name="targetSum"></param>
        /// <returns></returns>
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
                return false;

            if (root.left == null && root.right == null)
                return root.val == targetSum;

            return HasPathSum(root.left, targetSum - root.val)
                || HasPathSum(root.right, targetSum - root.val);

        }

        /// <summary>
        /// 124. 二叉树中的最大路径和
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int MaxPathSum(TreeNode root)
        {
            if (root == null) return 0;
            int ans = int.MinValue;
            OneSideMax(root, ref ans);
            return ans;
        }

        private int OneSideMax(TreeNode root, ref int ans)
        {
            if (root == null) return 0;

            int lMax = Math.Max(0, OneSideMax(root.left, ref ans));
            int rMax = Math.Max(0, OneSideMax(root.right, ref ans));

            ans = Math.Max(ans, lMax + rMax + root.val);

            return Math.Max(lMax, rMax) + root.val;
        }

        /// <summary>
        /// 105. 从前序与中序遍历序列构造二叉树
        /// </summary>
        Dictionary<int, int> valToIndex = new Dictionary<int, int>();
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            for (int i = 0; i < inorder.Length; i++)
            {
                valToIndex.Add(inorder[i], i);
            }
            return BuildTreeI(preorder, 0, preorder.Length - 1,
                                inorder, 0, inorder.Length - 1);
        }

        private TreeNode BuildTreeI(int[] preorder, int preStart, int preEnd,
                                    int[] inorder, int inStart, int inEnd)
        {
            if (preStart > preEnd)
            {
                return null;
            }

            int rootVal = preorder[preStart];
            int index = valToIndex[rootVal];
            int leftSize = index - inStart;

            TreeNode root = new TreeNode(rootVal);

            root.left = BuildTreeI(preorder, preStart + 1, preStart + leftSize,
                                    inorder, inStart, index - 1);
            root.right = BuildTreeI(preorder, preStart + leftSize + 1, preEnd,
                                    inorder, index + 1, inEnd);

            return root;
        }


        /// <summary>
        /// 2385. 感染二叉树需要的总时间
        /// 1. 前序遍历，设置感染深度
        /// 2. 遍历完左树，找不到感染点，inleft设置为false
        /// 3. 在当前节点感染
        /// 4. 在  左节点感染
        /// 5. 在  右节点感染
        /// 6. 返回深度
        /// </summary>
        int ans = 0;    // 最短用时
        int depth = -1; // 起始节点的高度

        public int AmountOfTime(TreeNode root, int start)
        {
            if (root == null) return 0;

            dfs(root, 0, start);
            return ans;
        }
        int dfs(TreeNode root, int level, int start)
        {
            if (root == null) return 0;

            if (root.val == start)                          // 1
                depth = level;

            int l = dfs(root.left, level + 1, start);

            bool inleft = depth != -1;                      // 2

            int r = dfs(root.right, level + 1, start);

            if (root.val == start)                          // 3
                ans = Math.Max(ans, Math.Max(l, r));
            if (inleft)                                     // 4
                ans = Math.Max(ans, depth - level + r);
            else                                            // 5
                ans = Math.Max(ans, depth - level + l);

            return Math.Max(l, r) + 1;                      // 6
        }

        /// <summary>
        /// 2316. 统计无向图中无法互相到达点对数
        /// 1. 转为邻接矩阵
        /// 2. 之前所有连通分量s
        /// 3. 节点i的连通分量的节点数t
        /// 4. s * t 不可达到点的对数 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        public long CountPairs(int n, int[][] edges)
        {
            List<int>[] graph = new List<int>[n];            // 1
            for (int i = 0; i < n; i++) graph[i] = new List<int>();

            foreach (int[] edge in edges)
            {
                int from = edge[1];
                int to = edge[0];
                graph[from].Add(to);
                graph[to].Add(from);
            }

            // 不可达到点数
            bool[] visited = new bool[n];
            long ans = 0;
            long s = 0;                                     // 2
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                int t = dfs_CountPairs(graph, i, visited);  // 3             
                ans += s * t;                               // 4  
                s += t;
            }

            return ans;
        }

        int dfs_CountPairs(List<int>[] graph, int i, bool[] visited)
        {
            if (visited[i]) return 0;

            visited[i] = true;
            int cnt = 1;
            for (int j = 0; j < graph[i].Count; j++)
                cnt += dfs_CountPairs(graph, graph[i][j], visited);

            return cnt;
        }

        /// <summary>
        /// 547. 省份数量
        /// </summary>
        /// <param name="isConnected"></param>
        /// <returns></returns>
        public int FindCircleNum(int[][] isConnected)
        {
            int n = isConnected.Length;             // 市的数量
            bool[] visited = new bool[n];           // 是否已经访问

            int provinces = 0;                      // 省份的数量
            for (int i = 0; i < n; i++)             // 深搜每一个市
            {
                if (!visited[i])                    // 仅访问未访问过的城市
                {
                    dfs(isConnected, visited, n, i);
                    provinces++;
                }
            }

            return provinces;
        }
        /// <summary>
        /// 设置连通i城市的所有城市为访问过
        /// </summary>
        /// <param name="isConnected"></param>
        /// <param name="visited"></param>
        /// <param name="n"></param>
        /// <param name="i"></param>
        private void dfs(int[][] isConnected, bool[] visited, int n, int i)
        {
            if (visited[i]) return;

            for (int j = 0; j < n; j++)                            // 遍历每一个城市
            {
                if (isConnected[i][j] == 1 && !visited[j])         // 连通且未访问过
                {
                    visited[j] = true;
                    dfs(isConnected, visited, n, j);               // 深搜
                }
            }
        }
        /// <summary>
        /// 733. 图像渲染
        /// </summary>
        int[] dx = { -1, 0, 0, 1 };
        int[] dy = { 0, 1, -1, 0 };

        public int[][] FloodFill(int[][] image, int sr, int sc, int color)
        {
            int curColor = image[sr][sc];
            if (curColor != color)
            {
                dfs_FloodFill(image, sr, sc, curColor, color);
            }
            return image;
        }

        private void dfs_FloodFill(int[][] image, int x, int y, int curColor, int color)
        {
            if (image[x][y] == curColor)
            {
                image[x][y] = color;
                for (int i = 0; i < 4; i++)
                {
                    int mx = x + dx[i], my = y + dy[i];
                    if (mx >= 0 && mx < image.Length && my >= 0 && my < image[0].Length)
                    {
                        dfs_FloodFill(image, mx, my, curColor, color);
                    }
                }
            }
        }
    } /// class end



    /// 284. 窥视迭代器
    class PeekingIterator
    {
        private IEnumerator<int> iterator;
        private bool flag = false;
        // iterators refers to the first element of the array.
        public PeekingIterator(IEnumerator<int> iterator)
        {
            // initialize any member here.
            this.iterator = iterator;
            flag = true;
        }

        // Returns the next element in the iteration without advancing the iterator.
        public int Peek()
        {
            return iterator.Current;
        }

        // Returns the next element in the iteration and advances the iterator.
        public int Next()
        {
            int ret = iterator.Current;
            flag = iterator.MoveNext();
            return ret;
        }

        // Returns false if the iterator is refering to the end of the array of true otherwise.
        public bool HasNext()
        {
            return flag;
        }
    }


    /// <summary>
    /// 155. 最小栈
    /// 
    /// [-2，0，-3]
    /// s1: -2,0,-3,1
    /// s2: -2,-3
    /// </summary>
    public class MinStack2
    {
        Stack<int> stack1;
        Stack<int> minStack;

        public MinStack2()
        {
            stack1 = new Stack<int>();
            minStack = new Stack<int>();
        }

        void push(int val)
        {
            stack1.Push(val);
            if (minStack.Count == 0 || minStack.Peek() >= val)
            {
                minStack.Push(val);
            }

        }

        void Pop()
        {
            if (stack1.Count != 0)
            {
                int val = stack1.Pop();
                if (val == minStack.Peek())
                {
                    minStack.Pop();
                }
            }
        }

        int Top()
        {
            if (stack1.Count != 0)
            {
                return stack1.Peek();
            }
            throw new Exception();
        }
        int GetMin()
        {
            if (minStack.Count != 0)
                return minStack.Peek();
            throw new Exception();
        }
    }



    /// <summary>
    /// 211. 添加与搜索单词 - 数据结构设计
    /// 分析：因为带有通配符.，遇到非字母时，需要检查其他26个字母
    /// 1. 是字母，检查下一个分支
    /// 2. 非字母，则26个分支都需要检查
    /// </summary>
    public class WordDictionary
    {
        private Trie trie;

        public WordDictionary()
        {
            trie = new Trie();
        }

        public void AddWord(string word)
        {
            trie.Insert(word);
        }

        public bool Search(string word)
        {
            return DFS(word, 0, trie.root);
        }

        private bool DFS(String word, int index, TrieNode node)
        {
            if (index == word.Length)           // base Case
                return node.end != 0;


            char ch = word[index];
            if (char.IsLetter(ch))                  // 1
            {
                int Idx = ch - 'a';
                TrieNode child = node.map[Idx];
                if (child != null && DFS(word, index + 1, child))
                {
                    return true;
                }
            }
            else
            {
                for (int i = 0; i < 26; i++)        // 2
                {
                    TrieNode child = node.map[i];
                    if (child != null && DFS(word, index + 1, child))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }


}/// namespace end
