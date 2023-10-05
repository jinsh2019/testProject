using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace August.algorithms.day26
{
    internal class Patterns
    {
        //503. 下一个更大元素 II
        public int[] NextGreaterElement(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            Stack<int> s = new Stack<int>();
            for (int i = n - 1; i > 0; i--)
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

        public bool IsUgly(int n)
        {
            if (n < 1)
                return false;
            int[] arr = { 2, 3, 5 };
            foreach (int item in arr)
            {
                while (n % item == 0)
                {
                    n /= item;
                }
            }
            return n == 1;
        }

        // 264. 丑数 II
        public int nthUglyNumber(int n)
        {
            int[] factors = { 2, 3, 5 };
            HashSet<int> set = new HashSet<int>();
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            set.Add(1);
            pq.Enqueue(1, 1);
            int ugly = 0;
            for (int i = 0; i < n; i++)
            {
                int cur = pq.Dequeue();
                ugly = cur;
                foreach (var factor in factors)
                {
                    int next = cur * factor;
                    if (set.Add(next))
                    {
                        pq.Enqueue(next, next);
                    }
                }
            }
            return ugly;
        }


        public int MininumCost(int n, int[][] connections)
        {
            UnionFind uf = new UnionFind(n + 1);

            Array.Sort(connections, (a, b) => a[2] - b[2]);

            int mst = 0;
            foreach (int[] edge in connections)
            {
                int u = edge[0];
                int v = edge[1];
                int weight = edge[2];
                if (uf.Connected(u, v))
                    continue;
                mst += weight;
                uf.Union(u, v);
            }

            return uf.count == 2 ? mst : -1;
        }

        // 在字符串s中找到唯一字符
        // 时间复杂度 O(n)
        // 空间复杂度 O(1)
        public char FirstUniqChar(string s)
        {
            if (s == null || s.Length == 0)
                return ' ';
            if (s.Length == 1)
                return s[0];

            int[] arr = new int[256];
            int index = 0;
            foreach (char c in s)
            {
                index = c;
                arr[index]++;
            }
            int res = -1;
            for (int i = 0; i < s.Length; i++)
            {
                index = s[i];
                if (arr[index] == 1)
                {
                    res = i;
                    break;
                }
            }
            return res == -1 ? ' ' : s[res];
        }

        // 647. 回文子串 Manacher
        public int CountSubstrings(string s)
        {
            int n = s.Length;
            StringBuilder t = new StringBuilder("$#");
            for (int i = 0; i < n; i++)
            {
                t.Append(s[i]);
                t.Append('#');
            }
            n = t.Length;
            t.Append('!');
            int[] f = new int[n];
            int iMax = 0, rMax = 0, ans = 0;
            for (int i = 1; i < n; ++i)
            {
                // 初始化 f[i]
                f[i] = i <= rMax ? Math.Min(rMax - i + 1, f[2 * iMax - i]) : 1;
                // 中心拓展
                while (t[i + f[i]] == t[i - f[i]])
                {
                    ++f[i];
                }
                // 动态维护 iMax 和 rMax
                if (i + f[i] - 1 > rMax)
                {
                    iMax = i;
                    rMax = i + f[i] - 1;
                }
                // 统计答案, 当前贡献为 (f[i] - 1) / 2 上取整
                ans += f[i] / 2;
            }

            return ans;
        }

        // 扫描线
        //253. 会议室 II
        public int MinMeetingRooms(int[][] meetings)
        {
            int n = meetings.Length;
            int[] begin = new int[n];
            int[] end = new int[n];
            for (int k = 0; k < n; k++)
            {
                begin[k] = meetings[k][0];
                end[k] = meetings[k][1];
            }
            Array.Sort(begin);
            Array.Sort(end);

            // 扫描过程中的计数器
            int count = 0;
            int res = 0, i = 0, j = 0;
            while (i < n && j < n)
            {
                if (begin[i] < end[j])
                {
                    // 扫描到一个红点
                    count++;
                    i++;
                }
                else
                {
                    // 扫描到一个绿点
                    count--;
                    j++;
                }

                res = Math.Max(res, count);
            }

            return res;
        }
        // 394. 字符串解码
        public string DecodeString(string s)
        {
            int multi = 0;
            StringBuilder res = new StringBuilder();
            LinkedList<int> stack_multi = new LinkedList<int>(); // 乘数栈(0-9) 瞬时
            LinkedList<string> stack_res = new LinkedList<string>(); // 原res栈(abc)
            foreach (char c in s.ToArray())
            {
                if (c == '[') // 遇到 [， 1. 压入multi； 2. 压入原res
                {
                    stack_multi.AddLast(multi);
                    stack_res.AddLast(res.ToString());
                    multi = 0;
                    res = new StringBuilder();
                }
                else if (c == ']') // 遇到 ], 1. 把乘数整体取出来， 并倍增新物料; 2. 取出原物料， 合并老新物料
                {
                    StringBuilder tmp = new StringBuilder();

                    int cur_multi = stack_multi.Last.Value;
                    stack_multi.RemoveLast();
                    for (int i = 0; i < cur_multi; i++)
                        tmp.Append(res);

                    string resLast = stack_res.Last.Value;
                    stack_res.RemoveLast();
                    res = new StringBuilder(resLast + tmp);
                }
                else if (c >= '0' && c <= '9')
                {
                    multi = multi * 10 + int.Parse(c + ""); // 维护multi，作为一个整体
                }
                else res.Append(c); // 维护res，作为一个整体
            }
            return res.ToString();
        }
    }
}

