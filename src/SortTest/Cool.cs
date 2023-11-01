using CDS;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SortTest
{
    public class Cool
    {
        /// <summary>
        /// 1351. 统计有序矩阵中的负数
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int CountNegatives(int[][] grid)
        {
            int ans = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] < 0) ans++;
                }
            }
            return ans;
        }


        /// <summary>
        /// 1672. 最富有客户的资产总量
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public int MaximumWealth(int[][] accounts)
        {
            int ans = 0;
            for (int i = 0; i < accounts.Length; i++)
            {
                int total = 0;
                for (int j = 0; j < accounts[0].Length; j++)
                {
                    total += accounts[i][j];
                }
                ans = Math.Max(ans, total);
            }

            return ans;
        }

        /// <summary>
        /// 1572. 矩阵对角线元素的和
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int DiagonalSum(int[][] mat)
        {
            int ans = 0, n = mat.Length;
            for (int i = 0; i < n; i++)
                ans += mat[i][i];
            for (int i = 0; i < n; i++)
                ans += mat[i][n - 1 - i];
            if (n % 2 == 1)
                ans -= mat[n / 2][n / 2];
            return ans;
        }
        /// <summary>
        /// 2643. 一最多的行
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int[] RowAndMaximumOnes(int[][] mat)
        {
            int m = mat.Length, n = mat[0].Length;
            int[] ans = new int[2] { 0, 0 };
            for (int i = 0; i < m; i++)
            {
                int rCount = 0;
                for (int j = 0; j < n; j++) rCount += mat[i][j];
                if (rCount > ans[1])
                {
                    ans[0] = i;
                    ans[1] = rCount;
                }
            }
            return ans;
        }
        /// <summary>
        /// 2319. 判断矩阵是否是一个 X 矩阵
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public bool CheckXMatrix(int[][] grid)
        {
            int n = grid.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j || j == (n - 1 - i))
                    {
                        if (grid[i][j] == 0) return false;

                    }
                    else if (grid[i][j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 1582. 二进制矩阵中的特殊位置
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public int NumSpecial(int[][] mat)
        {
            int m = mat.Length, n = mat[0].Length, res = 0;
            int[] rowsSum = new int[m];
            int[] colsSum = new int[n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    rowsSum[i] += mat[i][j];
                    colsSum[j] += mat[i][j];
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i][j] == 1 && rowsSum[i] == 1 && colsSum[j] == 1)
                        res++;
                }
            }

            return res;
        }
        /// <summary>
        /// 1380. 矩阵中的幸运数
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public IList<int> LuckyNumbers(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            int[] minRow = new int[m];
            int[] maxCols = new int[n];
            Array.Fill(maxCols, int.MinValue);
            Array.Fill(minRow, int.MaxValue);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    minRow[i] = Math.Min(minRow[i], matrix[i][j]);
                    maxCols[j] = Math.Max(maxCols[j], matrix[i][j]);
                }
            }
            IList<int> res = new List<int>();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == minRow[i] && matrix[i][j] == maxCols[j])
                    {
                        res.Add(matrix[i][j]);
                    }
                }
            }

            return res;
        }
        /// <summary>
        /// LCR 146. 螺旋遍历二维数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public int[] SpiralArray(int[][] grid)
        {
            List<int> list = new List<int>();
            if (grid == null || grid.Length == 0) return list.ToArray();

            int m = grid.Length, n = grid[0].Length;
            int left = 0, right = n - 1;
            int up = 0, bottom = m - 1;

            while (true)
            {
                //from left to right
                for (int i = left; i <= right; i++) list.Add(grid[up][i]);
                if (++up > bottom) break;
                //from up to down
                for (int i = up; i <= bottom; i++) list.Add(grid[i][right]);
                if (--right < left) break;
                // from right to left
                for (int i = right; i >= left; i--) list.Add(grid[bottom][i]);
                if (--bottom < up) break;
                // from down to  up
                for (int i = bottom; i >= up; i--) list.Add(grid[i][left]);
                if (++left > right) break;
            }

            return list.ToArray();
        }

        ///// <summary>
        ///// 746. 使用最小花费爬楼梯
        ///// </summary>
        ///// <param name="cost"></param>
        ///// <returns></returns>
        //public int MinCostClimbingStairs(int[] cost)
        //{
        //    int n = cost.Length + 1;
        //    int[] dp = new int[n + 1];
        //    dp[0] = 0;
        //    d
        //}

        /// <summary>
        /// 2678. 老人的数目
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>

        public int CountSeniors(string[] details)
        {
            int ans = 0;
            for (int i = 0; i < details.Length; i++)
            {
                if (int.Parse(details[i].Substring(11, 2)) > 60)
                    ans++;
            }

            return ans;
        }
        /// <summary>
        /// LCR 116. 省份数量
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int FindCircleNum(int[][] grid)
        {
            int n = grid.Length;
            bool[] visited = new bool[n];
            int provinces = 0;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    dfs_FindCircleNum(grid, visited, i);
                    provinces++;
                }

            }

            return provinces;
        }
        /// <summary>
        /// 1,1,0
        /// 1,1,0
        /// 0,0,1
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="visited"></param>
        /// <param name="i"></param>
        private void dfs_FindCircleNum(int[][] grid, bool[] visited, int i)
        {
            for (int j = 0; j < grid.Length; j++)
            {
                Console.WriteLine("i:" + i + ",j:" + j + ", visited[j]:" + j + ":" + visited[j]);
                if (grid[i][j] == 1 && !visited[j])
                {
                    visited[j] = true;
                    dfs_FindCircleNum(grid, visited, j);
                }
            }
        }
        /// <summary>
        /// 1382. 将二叉搜索树变平衡
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public TreeNode BalanceBST(TreeNode root)
        {
            List<int> list = new List<int>();
            GetNums(root, list);
            var tree = BuildTree(0, list.Count - 1, list.ToArray());
            return tree;
        }

        private TreeNode BuildTree(int start, int end, int[] nums)
        {
            if (start > end) return null;

            int mid = start + (end - start) / 2;

            TreeNode root = new TreeNode(nums[mid]);
            root.left = BuildTree(start, mid - 1, nums);
            root.right = BuildTree(mid + 1, end, nums);

            return root;
        }

        private void GetNums(TreeNode root, List<int> datas)
        {
            if (root == null) return;

            GetNums(root.left, datas);
            datas.Add(root.val);
            GetNums(root.right, datas);
        }

        /// <summary>
        /// 200. 岛屿数量
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int NumIslands(char[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            int ans = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        ans++;
                        dfs_NumIslands(grid, i, j);
                    }
                }
            }

            return ans;
        }

        private void dfs_NumIslands(char[][] grid, int i, int j)
        {
            int m = grid.Length, n = grid[0].Length;
            if (i < 0 || i >= m || j < 0 || j >= n)
                return;

            if (grid[i][j] == '0')
                return;

            grid[i][j] = '0';
            dfs_NumIslands(grid, i + 1, j);
            dfs_NumIslands(grid, i - 1, j);
            dfs_NumIslands(grid, i, j + 1);
            dfs_NumIslands(grid, i, j - 1);
        }

        /// <summary>
        /// 463. 岛屿的周长
        /// 统计相邻的岛屿的数量
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int IslandPerimeter(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            int count = 0, cnt = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        cnt++;
                        if (i > 0 && grid[i - 1][j] == 1) count++;
                        if (i < m - 1 && grid[i + 1][j] == 1) count++;
                        if (j > 0 && grid[i][j - 1] == 1) count++;
                        if (j < n - 1 && grid[i][j + 1] == 1) count++;
                    }
                }
            }

            return 4 * cnt - count;
        }
        /// <summary>
        /// 695. 岛屿的最大面积
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int MaxAreaOfIsland(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            int maxArea = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        maxArea = Math.Max(maxArea, dfs_MaxAreaOfIsland(grid, i, j));
                    }
                }
            }

            return maxArea;
        }

        private int dfs_MaxAreaOfIsland(int[][] grid, int i, int j)
        {
            int m = grid.Length, n = grid[0].Length;
            if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] != 1)
                return 0;

            grid[i][j] = 2;
            return 1 +
                dfs_MaxAreaOfIsland(grid, i + 1, j) +
                dfs_MaxAreaOfIsland(grid, i - 1, j) +
                dfs_MaxAreaOfIsland(grid, i, j + 1) +
                dfs_MaxAreaOfIsland(grid, i, j - 1);
        }

        /// <summary>
        /// 130. 被围绕的区域
        /// </summary>
        /// <param name="board"></param>
        public void Solve(char[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            for (int i = 0; i < m; i++)
            {
                dfs_Solve(grid, i, 0);        // 第一列
                dfs_Solve(grid, i, n - 1);    // 最后一列
            }

            for (int i = 0; i < n; i++)
            {
                dfs_Solve(grid, 0, i);        // 第一行
                dfs_Solve(grid, m - 1, i);    // 最后一行
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 'O')
                    {
                        grid[i][j] = 'X';
                    }
                    if (grid[i][j] == '-')
                    {
                        grid[i][j] = 'O';
                    }
                }
            }

        }

        private void dfs_Solve(char[][] grid, int i, int j)
        {
            int m = grid.Length, n = grid[0].Length;
            if (i < 0 || i >= m || j < 0 || j >= n) return;

            if (grid[i][j] != 'O') return;

            grid[i][j] = '-';
            dfs_Solve(grid, i + 1, j);
            dfs_Solve(grid, i - 1, j);
            dfs_Solve(grid, i, j + 1);
            dfs_Solve(grid, i, j - 1);
        }

        /// <summary>
        /// 17. 电话号码的字母组合
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public IList<string> LetterCombinations(string digits)
        {
            if (digits == null || digits.Length == 0)
                return ans;

            dfs1(digits, 0, new StringBuilder());
            return ans;
        }
        string[] strs = new string[10]{
            "","","abc","def",
            "ghi","jkl","mno",
            "pqrs","tuv","wxyz"};
        List<string> ans = new List<string>();
        void dfs1(string digits, int idx, StringBuilder combine)
        {
            // 满足条件
            if (idx == digits.Length)
            {
                ans.Add(combine.ToString());
                return;
            }
            // 数字对应的字符串
            string s = strs[digits[idx] - '0'];
            for (int i = 0; i < s.Length; i++)
            {
                combine.Append(s[i]);
                dfs1(digits, idx + 1, combine);
                combine.Remove(combine.Length - 1, 1);
            }
        }

        List<string> res1 = new List<string>();
        /// <summary>
        /// 22. 括号生成 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IList<string> GenerateParenthesis(int n)
        {
            dfs2(0, 0, n, "");
            return res1;
        }
        public void dfs2(int lc, int rc, int n, string seq)
        {
            if (lc == n && rc == n)
            {
                res1.Add(seq);
            }

            if (lc < n) dfs2(lc + 1, rc, n, seq + "(");
            if (rc < n && lc > rc) dfs2(lc, rc + 1, n, seq + ")");
        }

        bool[][] rows = new bool[9][];
        bool[][] cols = new bool[9][];
        bool[][][] cells = new bool[3][][];
        /// <summary>
        /// 37. 解数独
        /// </summary>
        /// <param name="board"></param>
        public void SolveSudoku(char[][] board)
        {
            for (int i = 0; i < rows.Length; i++)
                rows[i] = new bool[9];
            for (int i = 0; i < cols.Length; i++)
                cols[i] = new bool[9];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new bool[3][];
                for (int j = 0; j < 3; j++)
                    cells[i][j] = new bool[9];
            }

            for (int i = 0; i < 9; i++)                 // 设置i行第t个数为true
            {
                for (int j = 0; j < 9; j++)             // 设置j列第t个数为true
                {
                    if (board[i][j] == '.')             // 设置(i/3)(j/3)宫格第t个数为true
                        continue;
                    int t = board[i][j] - '1';
                    rows[i][t] = cols[j][t] = cells[i / 3][j / 3][t] = true;
                }
            }

            dfs3(board, 0, 0);
        }
        public bool dfs3(char[][] board, int x, int y)
        {
            if (y == 9)                             // y越界，x下一行
            {
                x++;
                y = 0;
            }

            if (x == 9)                             // x越界找到有效数独
                return true;

            if (board[x][y] != '.')                 // y所搜下一个位置
                return dfs3(board, x, y + 1);

            for (int i = 0; i < 9; i++)             // 对于每一个'.'枚举所有可能的方案
            {                                       // 如果x行/y列第i个数据，继续循环
                if (rows[x][i] || cols[y][i] || cells[x / 3][y / 3][i])
                    continue;
                board[x][y] = (char)(i + '1');
                rows[x][i] = cols[y][i] = cells[x / 3][y / 3][i] = true;

                if (dfs3(board, x, y + 1))          // 存在一个数独的解
                    return true;

                rows[x][i] = cols[y][i] = cells[x / 3][y / 3][i] = false;
                board[x][y] = '.';
            }

            return false;
        }

        /// <summary>
        /// 1155. 掷骰子等于目标和的方法数
        /// dp[i][j] 使用i个筛子，和为j的方案数
        /// dp[0][0] 0个筛子，和为0的方案数
        /// dp[i][j] = dp[i-1][j]           // 不选择
        /// dp[i][j] = dp[i-1][j] + dp[i-1]
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int NumRollsToTarget(int n, int k, int target)
        {
            int[][] f = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {
                f[i] = new int[target + 1];
            }
            f[0][0] = 1;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    for (int x = 1; x <= k; ++x)
                    {
                        if (j - x >= 0)
                            f[i][j] = (f[i][j] + f[i - 1][j - x]) % 100000007;
                    }
                }
            }

            return f[n][target];
        }


        /// <summary>
        /// 1465. 切割后面积最大的蛋糕
        /// </summary>
        /// <param name="h"></param>
        /// <param name="w"></param>
        /// <param name="horizontalCuts"></param>
        /// <param name="verticalCuts"></param>
        /// <returns></returns>
        public int MaxArea(int h, int w, int[] horizontalCuts, int[] verticalCuts)
        {
            Array.Sort(horizontalCuts);
            Array.Sort(verticalCuts);
            int m = horizontalCuts.Length + 1;
            int n = verticalCuts.Length + 1;
            int[] hPreDiff = new int[m];
            int[] vPreDiff = new int[n];

            for (int i = 0; i < m - 1; i++)
            {
                if (i == 0) hPreDiff[0] = horizontalCuts[i];
                else hPreDiff[i] = horizontalCuts[i] - horizontalCuts[i - 1];
            }
            hPreDiff[m - 1] = h - horizontalCuts[horizontalCuts.Length - 1];

            for (int i = 0; i < n - 1; i++)
            {
                if (i == 0) vPreDiff[0] = verticalCuts[i];
                else vPreDiff[i] = verticalCuts[i] - verticalCuts[i - 1];
            }
            vPreDiff[n - 1] = w - verticalCuts[verticalCuts.Length - 1];

            long ans = 0;
            for (int i = 0; i < m; i++)
            {
                int hh = hPreDiff[i];
                for (int j = 0; j < n; j++)
                {
                    long area = ((long)hh * (long)vPreDiff[j]) % 1000000007;
                    ans = Math.Max(area, ans);
                }
            }
            return (int)ans;
        }



        /// <summary>
        /// 210. 课程表 II
        /// 1.建立邻接表
        /// 2.dfs graph
        /// 3.因为是后序遍历,需要进行反转
        /// 4.返回结果集
        /// 5.遍历邻接表
        /// 6.存在环路
        /// 7.已访问过或有环(逐一归回去)
        /// 8.以s为起始点进行dfs
        /// 9.始终无环，且遍历g结束即存在完成课程的路径
        /// 10.初始化图
        /// 11. from  to
        /// </summary>
        /// <param name="numCourses"></param>
        /// <param name="prerequisites"></param>
        /// <returns></returns>
        List<int> postorder = new List<int>();
        bool hasCycle = false;
        bool[] visited;
        bool[] onPath;
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = BuildGraph(numCourses, prerequisites);  // 建立邻接表
            visited = new bool[numCourses];
            onPath = new bool[numCourses];

            for (int i = 0; i < numCourses; i++)                        // bfs graph
                traverse(graph, i);

            if (hasCycle)
                return new int[] { };

            postorder.Reverse();                                        // 因为是后序遍历,需要进行反转
            int[] res = new int[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                res[i] = postorder[i];
            }
            return res;                                                 // 返回结果集
        }

        private void traverse(List<int>[] graph, int s)                 // 遍历邻接表
        {
            if (onPath[s])                                              // 存在环路            
                hasCycle = true;

            if (visited[s] || onPath[s])                                // 已访问过或有环(逐一归回去)
                return;

            onPath[s] = true;
            visited[s] = true;

            foreach (var t in graph[s])                                 // 以s为起始点进行dfs       
                traverse(graph, t);

            postorder.Add(s);                                           // 始终无环，且遍历g结束即存在完成课程的路径
            onPath[s] = false;

        }

        private List<int>[] BuildGraph(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = new List<int>[numCourses];              // 初始化图
            for (int i = 0; i < numCourses; i++)
                graph[i] = new List<int>();

            foreach (int[] v in prerequisites)                          // from  to
            {
                int from = v[1];
                int to = v[0];
                graph[from].Add(to);
            }
            return graph;
        }


        /// <summary>
        /// 2558. 从数量最多的堆取走礼物
        /// </summary>
        /// <param name="gifts"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public long PickGifts(int[] gifts, int k)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (int num in gifts)
            {
                pq.Enqueue(num, -num);
            }

            for (int i = 0; i < k; i++)
            {
                var temp = pq.Dequeue();
                if (temp != 0) temp = (int)Math.Sqrt(temp);
                pq.Enqueue(temp, -temp);
            }

            long ans = 0;
            while (pq.Count != 0)
                ans += pq.Dequeue();

            return ans;
        }

        /// <summary>
        /// 721. 账户合并
        /// 1. 初始化 map[email,idx]，如果具有相同email合并idx
        /// 2. 初始化map[email,idx]
        /// 3. 有相同key,合并idx
        /// 4. 通过获取idx根节点root，找到对应的emails，把idx Map的email添加到emails中
        /// 5. 找到对应的emails
        /// 6. 把idx Map的email添加到emails中
        /// 7. 更新map[idx,email]
        /// 8. 先排序emails，再将account+emails进行组合
        /// 9. 返回结果集
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            Dictionary<string, int> emailToIdx = new Dictionary<string, int>();
            int n = accounts.Count;
            UnionFind myUnion = new UnionFind(n);
            for (int i = 0; i < n; i++)                                 //  初始化 map[email,idx]，如果具有相同email合并idx
            {
                int cnt = accounts[i].Count;
                for (int j = 1; j < cnt; j++)
                {
                    string curEmail = accounts[i][j];
                    if (!emailToIdx.TryAdd(curEmail, i))                //  初始化map[email,idx]
                        myUnion.Union(i, emailToIdx[curEmail]);         //  有相同key,合并idx
                }
            }

            Dictionary<int, List<string>> idxToEmail = new Dictionary<int, List<string>>();
            foreach (var kv in emailToIdx)                              // 通过获取idx根节点root，找到对应的emails，把idx Map的email添加到emails中
            {
                int idx = myUnion.Find(kv.Value);                       // 找到对应的emails                       
                if (!idxToEmail.TryGetValue(idx, out List<string> emails))
                    emails = new List<string>();
                emails.Add(kv.Key);                                     // 把idx Map的email添加到emails中
                idxToEmail[idx] = emails;                               // 更新map[idx,email]
            }
            // combine idx + emails
            IList<IList<string>> res = new List<IList<string>>();       // 先排序emails，再将account+emails进行组合
            foreach (var kv in idxToEmail)
            {
                List<string> emails = kv.Value;
                emails.Sort(StringComparer.Ordinal);
                List<string> tmp = new List<string>() { accounts[kv.Key][0] };
                tmp.AddRange(emails);
                res.Add(tmp);
            }
            return res;                                                 // 返回结果集
        }

        /// <summary>
        /// 990. 等式方程的可满足性
        /// 1. 初始化26个字母的连通
        /// 2. 连通所有  == 
        /// 3. 检查所有  !=
        /// 4. 与连通不符，返回错误
        /// </summary>
        /// <param name="equations"></param>
        /// <returns></returns>
        public bool EquationsPossible(string[] equations)
        {
            UnionFind uf = new UnionFind(26);                           // 初始化26个字母的连通
            for (int i = 0; i < equations.Length; i++)
            {
                if (equations[i][1] == '=')                             // 连通所有  == 
                {
                    char x = equations[i][0];
                    char y = equations[i][3];
                    uf.Union(x - 'a', y - 'a');
                }
            }

            for (int i = 0; i < equations.Length; i++)
            {
                if (equations[i][1] == '!')                             // 检查所有  !=
                {
                    char x = equations[i][0];
                    char y = equations[i][3];
                    if (uf.IsConnected(x - 'a', y - 'a'))
                        return false;                                   // 与连通不符，返回错误
                }
            }
            return true;
        }

        /// <summary>
        /// 4. 寻找两个正序数组的中位数
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int n = nums1.Length + nums2.Length;
            if (n % 2 == 0)
            {
                int left = find(nums1, 0, nums2, 0, n / 2);      // => find([2],0, [], 0, 0) 
                int right = find(nums1, 0, nums2, 0, n / 2 + 1);  // => find([2],0, [], 0, 1)
                return (left + right) / 2.0;
            }
            else
            {
                return find(nums1, 0, nums2, 0, n / 2 + 1);
            }
        }

        int find(int[] nums1, int i, int[] nums2, int j, int k)
        {
            //if (nums1.Length - i > nums1.Length - j)                // 保证 nums1 的元素更少，方便处理
            //    return find(nums2, j, nums1, i, k);

            //if (nums1.Length == i)                                  // nums1 为空，直接取 nums2 的第 k 个元素
            //    return nums2[j + k - 1];

            //if (k == 1)                                             // k = 1 时，取两数组第一个元素的最小值
            //    return nums2.Length == 0 ? nums1[i] : Math.Min(nums1[i], nums2[j]);

            //int si = Math.Min((int)nums1.Length, i + k / 2);      // 分别对应两数组 k / 2 的下一个位置
            //int sj = j + k - k / 2;
            //if (nums1[si - 1] < nums2[sj - 1])
            //    return find(nums1, si, nums2, j, k - (si - i));
            //else
            //    return find(nums1, i, nums2, sj, k - (sj - j));
            return 0;
        }

        /// <summary>
        /// 二分查找
        /// int[] nums = new int[] { 1, 3, 4, 6, 6, 6, 7, 8, 9 };
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int BSearch(int[] nums, int target)
        {
            int l = -1, r = nums.Length;
            while (r - l > 1)
            {
                int mid = l + (r - l) / 2;
                if (IsGreen(nums[mid], target))
                    r = mid;
                else
                    l = mid;
            }
            return r;
        }
        public bool IsGreen(int val, int x)
        {
            return val < x;
        }
        // val >  x  1, 3, 4,  6,  6,  6, [7], 8, 9 右超目标值
        // val >= x  1, 3, 4, [6], 6, 6, 7, 8, 9    目标值的左边界
        // val < x   1, 3,[4], 6,  6, 6, 7, 8, 9    左超目标值    
        // val <= x  1, 3, 4,  6,  6, [6], 7, 8, 9  目标值的右边界
    }
}// class end   
