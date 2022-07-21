namespace July.algorithms.day6
{
    internal class Patterns
    {
        // 约瑟夫环问题
        public void findtTheWinner(int n, int m)
        {
            int[] a = new int[n];
            int cnt = 0, i = 0, k = 0;
            while (cnt != 0)
            {
                if (a[i] == 0) // 表示没有出局
                {
                    k++;
                    if (k == m)
                    {
                        a[i] = 1;
                        cnt++;
                        Console.Write(i + 1 + " "); // 出局顺序
                        k = 0;
                    }
                }
                i++;
                if (i == n) i = 0;

            }
        }
        // 约瑟夫环
        public int LastRemaining(int n, int m)
        {
            if (n == 1)
                return 0;
            return (LastRemaining(n - 1, m) + m) % n;
        }

        public int[] NextGreaterElement(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            Stack<int> s = new Stack<int>(); // 维护一个降序栈
            for (int i = n - 1; i >= 0; i--)
            {
                while (s.Count != 0 && s.Peek() <= nums[i])
                    s.Pop();
                res[i] = s.Count == 0 ? -1 : s.Peek();
                s.Push(nums[i]);
            }
            return res;
        }

        public int nthUglyNumber(int n)
        {
            int[] factors = { 2, 3, 5 };
            HashSet<int> set = new HashSet<int>();
            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();
            set.Add(1);
            priorityQueue.Enqueue(1, 1);
            int ugly = 0;
            for (int i = 0; i < n; i++)
            {
                int cur = priorityQueue.Dequeue();
                ugly = cur;
                foreach (int facotr in factors)
                {
                    int next = cur = facotr;
                    if (set.Add(next))
                    {
                        priorityQueue.Enqueue(next, next);
                    }
                }
            }
            return ugly;
        }

        public int MinimumCost(int n, int[][] connections)
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


        public void NextPermutation(int[] nums)
        {
            int i = nums.Length - 2; // 123-> 132 -> 213
            while (i >= 0 && nums[i] >= nums[i + 1])   // find the first i nums[i] < nums[i+1]
                i--;
            if (i >= 0)
            {
                int j = nums.Length - 1;
                while (j >= 0 && nums[i] >= nums[j]) // find the first j num[i] < nums[j]
                    j--;
                swap(nums, i, j);
            }
            reverse(nums, i + 1);
        }

        public void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        public void reverse(int[] nums, int start)
        {
            int left = start, right = nums.Length - 1;
            while (left < right)
            {
                swap(nums, left, right);
                left++;
                right--;
            }
        }


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

        // 743. 网络延迟时间

        public int NetworkDelayTime(int[][] times, int n, int k)
        {   
            // 1. 使用邻接表的方式构建graph
            // 2. 数组索引代表节点，List<>代表邻接点, int[]代表{to，weight}
            List<int[]>[] graph = new List<int[]>[n + 1];
            for (int i = 1; i <= n; i++)
                graph[i] = new List<int[]>();
            
            // 构造图
            foreach (int[] edge in times)
            {
                int from = edge[0], to = edge[1], weight = edge[2];
                graph[from].Add(new int[] { to, weight });
            }
            
            // 启动 dijkstra 算法计算以节点 k 为起点到其他节点的最短路径
            int[] distTo = dijkstra(k, graph);

            // 找到最长的那一条最短路径
            int res = 0;
            for (int i = 1; i < distTo.Length; i++)
            {
                if (distTo[i] == int.MaxValue)
                {
                    // 有节点不可达，返回 -1
                    return -1;
                }
                res = Math.Max(res, distTo[i]);
            }
            return res; // 最后一个到达节点距离(本题是时间)
        }

        class State
        {
            // 节点id
            public int id { get; private set; }
            // 从Start到id 节点的距离
            public int distFromStart { get; private set; }

            public State(int id, int distFromStart)
            {
                this.id = id;
                this.distFromStart = distFromStart;
            }
        }
        // 输入一个起点 start，计算从 start 到其他节点的最短距离
        int[] dijkstra(int start, List<int[]>[] graph)
        {
            // 定义：distTo[i] 的值就是起点 start 到达节点 i 的最短路径权重
            int[] distTo = new int[graph.Length];
            Array.Fill(distTo, int.MaxValue);

            // base case，start 到 start 的最短距离就是 0
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
                
                foreach (int[] neighbor in graph[curNodeId])
                {
                    int nextNodeId = neighbor[0]; // to
                    int distToNextNode = distTo[curNodeId] + neighbor[1];  // weight

                    // 更新 dp table
                    if (distTo[nextNodeId] > distToNextNode)
                    {
                        distTo[nextNodeId] = distToNextNode;
                        pq.Enqueue(new State(nextNodeId, distToNextNode), distToNextNode);
                    }
                }
            }
            return distTo;
        }
    }
}
