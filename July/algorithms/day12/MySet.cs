namespace July.algorithms.day12
{
    internal class MySet
    {
        IList<IList<int>> res = new List<IList<int>>();
        LinkedList<int> track = new LinkedList<int>();

        #region 子集
        public IList<IList<int>> Subsets(int[] nums)
        {
            backtrack(nums, 0);
            return res;
        }

        private void backtrack(int[] nums, int start)
        {
            res.Add(new List<int>(track));
            for (int i = start; i < nums.Length; i++)
            {
                track.AddLast(nums[i]);
                backtrack(nums, i + 1);
                track.RemoveLast();
            }
        }

        #endregion

        #region 组合
        public IList<IList<int>> Combine(int n, int k)
        {
            backtrack(1, n, k);
            return res;
        }

        private void backtrack(int start, int n, int k)
        {
            if (k == track.Count)
            {
                res.Add(new List<int>(track));
            }

            for (int i = start; i <= n; i++)
            {
                track.AddLast(i);
                backtrack(i + 1, n, k);
                track.RemoveLast();
            }
        }

        #endregion

        #region 排列
        public IList<IList<int>> Permute(int[] nums)
        {
            backtrack(nums);
            return res;
        }

        private void backtrack(int[] nums)
        {
            if (track.Count == nums.Length)
            {
                res.Add(new List<int>(track));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (track.Contains(nums[i]))
                    continue;
                track.AddLast(nums[i]);
                backtrack(nums);
                track.RemoveLast();
            }
        }
        #endregion

        // 797. 所有可能的路径
        public IList<IList<int>> AllPathSourceTarget(int[][] graph)
        {
            // 邻接表
            traverse(graph, 0);
            return res;
        }
        // 图的遍历框架
        private void traverse(int[][] graph, int s)
        {
            track.AddLast(s);

            int n = graph.Length;
            if (s == n - 1)
            {
                res.Add(new List<int>(track));
                track.RemoveLast();
                return;
            }

            foreach (int v in graph[s])
                traverse(graph, v);
            track.RemoveLast();
        }

        bool[] onPath, visited;
        bool hasCycle = false;
        public bool Canfinish(int numsCourses, int[][] prerequisites)
        {
            // 转成容易使用的邻接表数据类型
            List<int>[] graph = buildGraph(numsCourses, prerequisites);
            visited = new bool[numsCourses];
            onPath = new bool[numsCourses];
            for (int i = 0; i < numsCourses; i++) {
                traverse(graph, i);
            }
            return !hasCycle;
        }

        private void traverse(List<int>[] graph, int s)
        {
            if (onPath[s])
                hasCycle = true;

            if (visited[s] || hasCycle)
                return;
            visited[s] = true;

            onPath[s] = true;
            foreach (var v in graph[s])
            {
                traverse(graph, v);
            }
            onPath[s] = false;
        }

        private List<int>[] buildGraph(int numsCourses, int[][] prerequisites)
        {
            List<int>[] graph = new List<int>[numsCourses];
            for (int i = 0; i < numsCourses; i++) {
                graph[i] = new List<int>();
            }
            foreach (int[] edge in prerequisites)
            {
                int from = edge[1], to = edge[0];
                graph[from].Add(to);
            }
            return graph;
        }
    }
}
