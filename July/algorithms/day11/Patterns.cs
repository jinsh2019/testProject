using System.Text;

namespace July.algorithms.day11
{
    internal class Patterns
    {
        public int LastRemaining(int n, int m)
        {
            if (n == 1)
                return 0;
            return (LastRemaining(n - 1, m) + m) % m; // 递归 分解问题
        }

        //503. 下一个更大元素 II
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

        public char FirstUniqChar(string s)
        {
            if (s == null || s.Length == 0)
            {
                return ' ';
            }
            if (s.Length == 1)
            {
                return s[0];
            }

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

        public int NetworkDelayTime(int[][] times, int n, int k)
        {
            List<int[]>[] graph = new List<int[]>[n + 1];
            for (int i = 1; i <= n; i++)
                graph[i] = new List<int[]>();

            for (int i = 0; i <= times.Length; i++)
            {
                int from = times[i][0], to = times[i][1], weight = times[i][2];
                graph[from].Add(new int[] { to, weight });
            }

            int[] distTo = dijkstra(k, graph);

            int res = 0;
            for (int i = 1; i < distTo.Length; i++)
            {
                if (distTo[i] == int.MaxValue)
                    return -1;
                res = Math.Max(res, distTo[i]);
            }

            return res;
        }

        class State
        {
            public int id { get; private set; } // nodeId
            public int distFromStart { get; private set; } // 与start的距离
            public State(int id, int distFromStart)
            {
                this.id = id;
                this.distFromStart = distFromStart;
            }
        }
        // 为啥使用优先级队列？
        private int[] dijkstra(int start, List<int[]>[] graph)
        {
            int[] distTo = new int[graph.Length]; // 距离数组
            Array.Fill(distTo, int.MaxValue);
            distTo[start] = 0;

            PriorityQueue<State, int> pq = new PriorityQueue<State, int>();
            pq.Enqueue(new State(start, 0), 0);

            while (pq.Count != 0)
            {
                State curState = pq.Dequeue();
                int curNodeId = curState.id;
                int curDistFromStart = curState.distFromStart;

                if (curDistFromStart > distTo[curNodeId])
                    continue;

                foreach (int[] neighbor in graph[curNodeId]) // 遍历邻接表
                {
                    int nextNodeId = neighbor[0]; // 下一个结点
                    int distToNextNode = distTo[curNodeId] + neighbor[1];

                    if (distTo[nextNodeId] > distToNextNode) // 目前到下一个节点的距离更大时，更新距离数组。
                    {
                        distTo[nextNodeId] = distToNextNode;
                        pq.Enqueue(new State(nextNodeId, distToNextNode), distToNextNode);
                    }
                }
            }
            return distTo;
        }
        // 647
        public int CountSubstrings(String s)
        {
            int n = s.Length;
            StringBuilder t = new StringBuilder("$#");
            for (int i = 0; i < n; ++i)
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
            // 双指针技巧
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
                // 记录扫描过程中的最大值
                res = Math.Max(res, count);
            }

            return res;
        }


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

        public int FirstMissingPositive(int[] nums)
        {
            // 从 1...n
            // 有 n+1个数字

            // 1 比较特殊，首先判断是否有1
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 1)
                {
                    return 1;
                }
            }
            // 把所有负数设置为1
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] <= 0)
                {
                    nums[i] = 1;
                }
            }
            // 根据索引把对应的nums中的值置为负数
            for (int i = 0; i < nums.Length; i++)
            {
                nums[nums[i]] = -Math.Abs(nums[nums[i]]);
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0)
                    return i + 1;
            }
            return -1;
        }
    }


}
