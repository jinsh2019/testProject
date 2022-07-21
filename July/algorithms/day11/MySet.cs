namespace July.algorithms.day11
{
    internal class MySet
    {
        IList<IList<int>> res = new List<IList<int>>();
        LinkedList<int> track = new LinkedList<int>();

        #region subsets
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
        //[[],[1],[1,2],[1,2,3],[1,3],[2],[2,3],[3]] 
        #endregion

        #region combine C n k
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
                return;
            }

            for (int i = start; i <= n; i++)
            {
                track.AddLast(i);
                backtrack(i + 1, n, k);
                track.RemoveLast();
            }
        }

        #endregion

        #region Permute P n k
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

            for (int i = 0; i < nums.Length; i++) // 从0开始，一直到 nums.Length
            {
                if (track.Contains(nums[i])) // 排除已经在track中的数字
                    continue;
                track.AddLast(nums[i]);
                backtrack(nums);
                track.RemoveLast();
            }
        }
        #endregion

        #region traverse int[][] && List<int>[]
        public IList<IList<int>> AllPathSourceTarget(int[][] graph)
        {

            traverse(graph, 0);
            return res;
        }

        private void traverse(int[][] graph, int s)
        {
            track.AddLast(s);
            int n = graph.Length;
            if (s == n - 1)
            {
                res.Add(new List<int>(track));
                track.RemoveLast(); // can del these 2 lines
                return;
            }

            foreach (int v in graph[s])
                traverse(graph, v);
            track.RemoveLast();
        }

        bool[] onPath, visited;
        bool hasCycle = false;
        public bool Canfinish(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = buildGraph(numCourses, prerequisites);
            visited = new bool[numCourses];
            onPath = new bool[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
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

            // mark visited
            visited[s] = true;

            // below is backtrack
            onPath[s] = true;
            foreach (int v in graph[s])
                traverse(graph, v);
            onPath[s] = false;
        }

        private List<int>[] buildGraph(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
                graph[i] = new List<int>();

            foreach (int[] edge in prerequisites)
            {
                int from = edge[1], to = edge[0];
                graph[from].Add(to);
            }
            return graph;
        }
        #endregion
        // 遍历二维数组
        public int NumIslands(char[][] grid)
        {
            int res = 0;
            int m = grid.Length, n = grid[0].Length;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        res++;
                        dfs(grid, i, j);
                    }
                }
            }
            return res;
        }

        private void dfs(char[][] grid, int i, int j)
        {
            int m = grid.Length, n = grid[0].Length;
            if (i < 0 || j < 0 || i >= m || j >= n)
            {
                return;
            }
            if (grid[i][j] == '0')
            {
                return;
            }

            grid[i][j] = '0';
            dfs(grid, i + 1, j);
            dfs(grid, i, j + 1);
            dfs(grid, i - 1, j);
            dfs(grid, i, j - 1);

        }
    }
}
