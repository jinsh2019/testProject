using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.day5
{
    internal class MySet
    {
        IList<IList<int>> res = new List<IList<int>>();
        LinkedList<int> track = new LinkedList<int>();

        #region 子集 Subsets
        public IList<IList<int>> Subsets(int[] nums)
        {
            backtrack(nums, 0);
            return res;
        }

        private void backtrack(int[] nums, int start)
        {
            res.Add(new List<int>(track));
            // 遍历N叉树
            for (int i = start; i < nums.Length; i++)
            {
                track.AddLast(nums[i]);
                backtrack(nums, i + 1);
                track.RemoveLast();
            }
        }
        //[[],[1],[1,2],[1,2,3],[1,3],[2],[2,3],[3]]
        #endregion

        #region 组合 Combine Cn k
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
        
        #region 全排列 Permute Pn
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
        //[[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]
        #endregion



        #region 797. 所有可能的路径 图的遍历
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            traverse(graph, 0);
            return res;
        }

        private void traverse(int[][] graph, int s)
        {
            // 添加节点 s 到路径
            track.AddLast(s);

            int n = graph.Length;
            if (s == n - 1)
            {
                // 到达终点
                res.Add(new List<int>(track));
                // 可以在这直接 return，但要 removeLast 正确维护 path
                // path.removeLast();
                // return;
                // 不 return 也可以，因为图中不包含环，不会出现无限递归
            }

            // 递归每个相邻节点
            foreach (int v in graph[s]) // bfs
                traverse(graph, v);

            // 从路径移出节点 s
            track.RemoveLast();
        }
        #endregion

        // 图的遍历框架 有环

        bool[] onPath;
        bool[] visited;
        bool hasCycle = false;
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = buildGraph(numCourses, prerequisites);
            visited = new bool[numCourses];
            onPath = new bool[numCourses];
            for (int i = 0; i < numCourses; i++)
                traverse(graph, i);

            return !hasCycle;
        }

        private void traverse(List<int>[] graph, int s)
        {
            if (onPath[s])
                hasCycle = true; // 出现环

            if (visited[s] || hasCycle)
                return; // 如果已经找到环，也不用再遍历了

            // 前序遍历代码位置
            visited[s] = true;
            onPath[s] = true;
            foreach (int v in graph[s])
                traverse(graph, v);
            // 后序遍历代码位置
            onPath[s] = false;
        }

        // 构造graph
        private List<int>[] buildGraph(int numCourses, int[][] prerequisites)
        {
            // 图中共有 numCourses 个节点
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
                graph[i] = new List<int>(); // 邻接表结构(带入、出度的)

            foreach (int[] edge in prerequisites)
            {
                int from = edge[1];
                int to = edge[0];
                graph[from].Add(to);
            }
            return graph;
        }
    }
}
