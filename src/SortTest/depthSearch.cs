using CDS;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace SortTest
{
    internal class depthSearch
    {
        bool[][] rows = new bool[9][];
        bool[][] cols = new bool[9][];
        bool[][][] cells = new bool[3][][];

        public void SolveSudoku(char[][] board)
        {
            for (int i = 0; i < 9; i++)
            {
                rows[i] = new bool[9];
                cols[i] = new bool[9];
            }
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new bool[3][];
                for (int j = 0; j < 3; j++)
                {
                    cells[i][j] = new bool[9];
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '.') continue;
                    int t = board[i][j] - '1';
                    rows[i][t] = cols[j][t] = cells[i / 3][j / 3][t] = true;
                }
            }

            dfs_SolveSudoku(board, 0, 0);
        }
        public bool dfs_SolveSudoku(char[][] board, int x, int y)
        {
            if (y == 9)
            {
                x++;
                y = 0;
            }

            if (x == 9)
                return true;

            if (board[x][y] != '.')
                return dfs_SolveSudoku(board, x, y + 1);

            for (int i = 0; i < 9; i++)
            {
                if (rows[x][i] || cols[y][i] || cells[x / 3][y / 3][i])
                    continue;

                board[x][y] = (char)(i + '1');
                rows[x][i] = cols[y][i] = cells[x / 3][y / 3][i] = true;

                if (dfs_SolveSudoku(board, x, y + 1)) return true;

                board[x][y] = '.';
                rows[x][i] = cols[y][i] = cells[x / 3][y / 3][i] = false;
            }

            return false;
        }
        /// <summary>
        /// 22. 括号生成 
        /// 1. 左右括号都为n时，即为n对括号
        /// 2. 左括号小于n,递归添加左括号
        /// 3. 右括号小于n，同时右括号小于左括号 递归添加右括号
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// 
        List<string> res_GenerateParenthesis = new List<string>();
        public IList<string> GenerateParenthesis(int n)
        {
            dfs_GenerateParenthesis(0, 0, n, "");
            return res_GenerateParenthesis;
        }

        void dfs_GenerateParenthesis(int lc, int rc, int n, string seq)
        {
            if (lc == n && rc == n)                 // 左右括号都为n时，即为n对括号
            {
                res_GenerateParenthesis.Add(seq);
                return;
            }

            if (lc < n)                             // 左括号小于n,递归添加左括号
            {
                dfs_GenerateParenthesis(lc + 1, rc, n, seq + "(");
            }
            if (rc < n && rc < lc)                  // 右括号小于n，同时右括号小于左括号 递归添加右括号
            {
                dfs_GenerateParenthesis(lc, rc + 1, n, seq + ")");
            }
        }

        /// <summary>
        /// 17. 电话号码的字母组合
        /// 1. 组合长度满足条件，返回
        /// 2. 遍历idx数字映射的字符串
        /// 3. 回溯
        /// 4. 下一个数字
        /// 5. 回溯
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        /// 
        string[] map_LetterCombinations = new string[]{
                "","","abc","def","ghi",
                "jkl","mno","pqrs","tuv","wxyz"
            };
        List<string> res_LetterCombinations = new List<string>();
        public IList<string> LetterCombinations(string digits)
        {
            if (digits == null || digits.Length == 0)
                return res_LetterCombinations;
            dfs_LetterCombinations(digits, 0, new StringBuilder());
            return res_LetterCombinations;
        }
        void dfs_LetterCombinations(string digits, int idx, StringBuilder combine)
        {
            if (combine.Length == digits.Length)            // 组合长度满足条件，返回
            {
                res_LetterCombinations.Add(combine.ToString());
                return;
            }

            string s = map_LetterCombinations[digits[idx] - '0'];                // 遍历idx数字映射的字符串
            for (int i = 0; i < s.Length; i++)
            {
                combine.Append(s[i]);                       // 回溯

                dfs_LetterCombinations(digits, idx + 1, combine);              // 下一个数字

                combine.Remove(combine.Capacity - 1, 1);    // 回溯
            }
        }


        /// <summary>
        /// 39. 组合总和
        /// 1. 大于
        /// 2. 等于 |满足条件
        /// 3. 小于 |当前索引唯一，组合向后取，不产生重复组合
        /// 4. 允许重复组合i不变
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// 
        IList<IList<int>> res_CombinationSum = new List<IList<int>>();
        List<int> path_CombinationSum = new List<int>();
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            dfs_CombinationSum(candidates, target, 0, 0);
            return res_CombinationSum;
        }
        void dfs_CombinationSum(int[] candidates, int target, int start, int sum)
        {
            if (sum > target)                               // 大于
                return;

            if (target == sum)                              // 等于 |满足条件
            {
                res_CombinationSum.Add(new List<int>(path_CombinationSum));
                return;
            }

            for (int i = start; i < candidates.Length; i++) // 小于 |当前索引唯一，组合向后取，不产生重复组合
            {
                sum += candidates[i];
                path_CombinationSum.Add(candidates[i]);

                dfs_CombinationSum(candidates, target, i, sum);            // 允许重复组合i不变

                sum -= candidates[i];
                path_CombinationSum.RemoveAt(path_CombinationSum.Count - 1);
            }
        }
        /// <summary>
        /// 40. 组合总和 II
        /// candidates 中的每个数字在每个组合中只能使用 一次 
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// 
        IList<IList<int>> res_CombinationSum2 = new List<IList<int>>();
        IList<int> path_CombinationSum2 = new List<int>();
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            if (candidates == null || candidates.Length == 0)
                return res_CombinationSum2;

            Array.Sort(candidates);

            dfs_CombinationSum2(candidates, target, 0, 0);

            return res_CombinationSum2;
        }
        public void dfs_CombinationSum2(int[] candidates, int target, int start, int sum)
        {
            if (sum > target)                                       // 大于
                return;

            if (target == sum)                                      // 等于
            {
                res_CombinationSum2.Add(new List<int>(path_CombinationSum2));
                return;
            }


            for (int i = start; i < candidates.Length; i++)         // 小于 使用start索引，组合顺序不重复
            {
                if (i > start && candidates[i] == candidates[i - 1])// 当前上下文，使用一次相同的数字
                    continue;
                sum += candidates[i];
                path_CombinationSum2.Add(candidates[i]);

                dfs_CombinationSum2(candidates, target, i + 1, sum);                // 从下一个idx开始使用 

                sum -= candidates[i];
                path_CombinationSum2.RemoveAt(path_CombinationSum2.Count - 1);
            }
        }


        /// <summary>
        /// 46. 全排列
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        IList<IList<int>> res_Permute = new List<IList<int>>();
        public IList<IList<int>> Permute(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return res_Permute;

            dfs_Permute(nums, new List<int>());

            return res_Permute;
        }
        void dfs_Permute(int[] nums, List<int> path)
        {
            if (path.Count == nums.Length)
            {
                res_Permute.Add(new List<int>(path));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (path.Contains(nums[i]))
                    continue;
                path.Add(nums[i]);
                dfs_Permute(nums, path);
                path.RemoveAt(path.Count - 1);
            }
        }

        /// <summary>
        /// 47. 全排列 II
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        /// 
        IList<IList<int>> res_PermuteUnique = new List<IList<int>>();

        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return res_PermuteUnique;
            }
            Array.Sort(nums);
            bool[] visited = new bool[nums.Length];
            dfs_PermuteUnique(nums, new List<int>(), visited);

            return res_PermuteUnique;
        }


        public void dfs_PermuteUnique(int[] nums, List<int> path, bool[] visited)
        {
            if (path.Count == nums.Length)
            {
                res_PermuteUnique.Add(new List<int>(path));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (visited[i])
                    continue;
                if (i > 0 && nums[i] == nums[i - 1] && !visited[i - 1])
                    continue;

                path.Add(nums[i]);
                visited[i] = true;

                dfs_PermuteUnique(nums, path, visited);

                visited[i] = false;
                path.RemoveAt(path.Count - 1);
            }
        }

        /// <summary>
        /// 77. 组合
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// 
        IList<IList<int>> res_Combine = new List<IList<int>>();
        public IList<IList<int>> Combine(int n, int k)
        {
            if (n == 0)
                return res_Combine;

            dfs_Combine(n, k, 1, new List<int>());

            return res_Combine;
        }
        public void dfs_Combine(int n, int k, int start, List<int> path)
        {
            if (path.Count == k)
            {
                res_Combine.Add(new List<int>(path));
                return;
            }

            for (int i = start; i <= n; i++)                    // start： 相同上下文向后取值，避免重复
            {
                path.Add(i);
                dfs_Combine(n, k, i + 1, path);                 // i+1: 避免使用相同索引的值
                path.RemoveAt(path.Count - 1);
            }
        }


        IList<IList<int>> res_Subsets = new List<IList<int>>();
        /// <summary>
        /// 78. 子集
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<IList<int>> Subsets(int[] nums)
        {
            IList<int> path = new List<int>();
            dfs_Subsets(nums, 0, path);
            return res_Subsets;
        }

        private void dfs_Subsets(int[] nums, int start, IList<int> path)
        {
            res_Subsets.Add(new List<int>(path));

            for (int i = start; i < nums.Length; ++i)               // start: 相同上下文向后取值，避免重复
            {
                path.Add(nums[i]);
                dfs_Subsets(nums, i + 1, path);                     // i+1: 避免使用相同索引的值
                path.RemoveAt(path.Count - 1);
            }
        }


        IList<IList<int>> res_SubsetsWithDup = new List<IList<int>>();


        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            Array.Sort(nums);
            dfs_SubsetsWithDup(nums, 0, new List<int>());
            return res_SubsetsWithDup;
        }
        void dfs_SubsetsWithDup(int[] nums, int start, List<int> path)
        {
            res_SubsetsWithDup.Add(new List<int>(path));

            for (int i = start; i < nums.Length; i++)                   // start: 同一上下文向后取值，避免重复
            {
                if (i > start && nums[i] == nums[i - 1])
                    continue;

                path.Add(nums[i]);
                dfs_SubsetsWithDup(nums, i + 1, path);                 // i+1: 避免使用相同索引的值
                path.RemoveAt(path.Count - 1);
            }

        }

        /// <summary>
        /// 52. N 皇后 II
        /// 1. 越界，具有一组解
        /// 2. 枚举列的所有点
        /// 3. 是一个有效点，则进行递归
        /// 4. 转到下一行
        /// 5. 正上
        /// 6. 右上
        /// 7. 左上
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// 
        IList<IList<StringBuilder>> res_TotalNQueens = new List<IList<StringBuilder>>();
        public int TotalNQueens(int n)
        {
            List<StringBuilder> board = new List<StringBuilder>();
            for (int i = 0; i < n; i++)
                board.Add(new StringBuilder(new string('.', n)));

            dfs_TotalNQueens(board, 0);
            return res_TotalNQueens.Count;
        }

        public void dfs_TotalNQueens(List<StringBuilder> board, int row)
        {
            if (row == board.Count)
            {                            // 越界，具有一组解
                res_TotalNQueens.Add(board);
                return;
            }

            for (int col = 0; col < board.Count; col++)
            {        // 枚举列的所有点
                if (!isValid(board, row, col)) continue;        // 是一个有效点，则进行递归

                board[row][col] = 'Q';
                dfs_TotalNQueens(board, row + 1);                            // 转到下一行
                board[row][col] = '.';
            }
        }

        bool isValid(IList<StringBuilder> board, int row, int col)
        {
            for (int i = 0; i <= row; i++)
            {                     // 正上
                if (board[i][col] == 'Q')
                    return false;
            }
            for (int i = row - 1, j = col + 1;
                     i >= 0 && j < board.Count;
                     i--, j++)
            {                                 // 右上
                if (board[i][j] == 'Q')
                    return false;
            }
            for (int i = row - 1, j = col - 1;
                     i >= 0 && j >= 0; i--, j--)
            {               // 左上
                if (board[i][j] == 'Q')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 79. 单词搜索
        /// </summary>
        /// <param name="board"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        bool res_Exist = false;
        public bool Exist(char[][] board, string word)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    if (dfs_Exist(board, word, i, j, 0))
                        return true;
                }
            }
            return false;
        }
        public bool dfs_Exist(char[][] board, string word, int i, int j, int idx)
        {
            if (idx == word.Length)
                return true;

            if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length)
                return false;

            if (board[i][j] == word[idx])
            {
                idx++;
                char c = board[i][j];
                board[i][j] = '.';

                bool res = dfs_Exist(board, word, i + 1, j, idx)
                    || dfs_Exist(board, word, i - 1, j, idx)
                    || dfs_Exist(board, word, i, j + 1, idx)
                    || dfs_Exist(board, word, i, j - 1, idx);

                board[i][j] = c;

                return res;
            }

            return false;
        }



        /// <summary>
        /// 113. 路径总和 II
        /// </summary>
        IList<IList<int>> res_PathSum = new List<IList<int>>();
        public IList<IList<int>> PathSum(TreeNode root, int targetSum)
        {
            if (root == null) return res_PathSum;
            dfs_PathSum(root, targetSum, new List<int>());

            return res_PathSum;
        }

        public void dfs_PathSum(TreeNode root, int targetSum, List<int> path)
        {
            path.Add(root.val);
            targetSum -= root.val;

            if (root.left == null && root.right == null && targetSum == 0)
                res_PathSum.Add(new List<int>(path));   // 不需要返回，回溯找其他节点

            if (root.left != null)
                dfs_PathSum(root.left, targetSum, path);
            if (root.right != null)
                dfs_PathSum(root.right, targetSum, path);

            targetSum += root.val;
            path.RemoveAt(path.Count - 1);
        }


        /// <summary>
        /// 131. 分割回文串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// 
        IList<IList<string>> res_Partition = new List<IList<string>>();
        public IList<IList<string>> Partition(string s)
        {
            if (s == null || s.Length == 0)
                return res_Partition;

            dfs_Partition(s, 0, new List<string>());

            return res_Partition;
        }

        public void dfs_Partition(string s, int start, List<string> path)
        {
            if (start == s.Length)
                res_Partition.Add(new List<string>(path));        // 不需要返回，path回溯后寻找其他节点

            for (int i = start; i < s.Length; i++)
            {
                if (!isPalindrome(s, start, i))
                    continue;

                path.Add(s.Substring(start, i - start + 1));

                dfs_Partition(s, i + 1, path);

                path.RemoveAt(path.Count - 1);
            }
        }
        public bool isPalindrome(string s, int lo, int hi)
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
        /// 140. 单词拆分 II
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDict"></param>
        /// <returns></returns>
        /// 
        HashSet<string> set_WordBreak;
        List<string> res_WordBreak = new List<string>();
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            if (s == null || s.Length == 0)
                return res_WordBreak;
            set_WordBreak = new HashSet<string>(wordDict);
            dfs_WordBreak(s, 0, "");
            return res_WordBreak;
        }
        public void dfs_WordBreak(string s, int start, string path)
        {
            if (start == s.Length)
            {
                res_WordBreak.Add(path);
                return;
            }

            for (int i = start; i < s.Length; i++)
            {
                string word = s.Substring(start, i - start + 1);    // 从第一个字符作为单词首字母
                if (set_WordBreak.Contains(word))                             // 在字典中存在
                {
                    dfs_WordBreak(s, i + 1, path.Length != 0 ? path + " " + word : word);
                }
            }
        }

        /// <summary>
        /// 547. 省份数量
        /// </summary>
        /// <param name="isConnected"></param>
        /// <returns></returns>
        public int FindCircleNum(int[][] isConnected)
        {
            int n = isConnected.Length;
            bool[] visited = new bool[n];

            int provinces = 0;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    dfs(isConnected, i, visited);
                    provinces++;
                }
            }

            return provinces;
        }
        void dfs(int[][] isConnection, int i, bool[] visited)
        {
            for (int j = 0; j < isConnection.Length; j++)
            {
                if (isConnection[i][j] == 1 && !visited[j])
                {
                    visited[j] = true;
                    dfs(isConnection, j, visited);
                }
            }
        }

        /// <summary>
        /// LCP 07. 传递信息
        /// 1. 化为邻接表
        /// 2. 统计能到达n-1的次数
        /// 3. Base Case k次用完
        /// 4. 如果传递到n-1, ways++
        /// 5. 枚举邻接表
        /// 6. 递归newStart，k-1次
        /// </summary>
        /// <param name="n"></param>
        /// <param name="relation"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// 
        List<int>[] graph_NumWays;
        int ways = 0;
        public int NumWays(int n, int[][] relation, int k)
        {
            graph_NumWays = new List<int>[n];               // 化为邻接表
            for (int i = 0; i < n; i++)
                graph_NumWays[i] = new List<int>();
            for (int i = 0; i < relation.Length; i++)
            {
                int from = relation[i][0];
                int to = relation[i][1];
                graph_NumWays[from].Add(to);
            }
            dfs_NumWays(n, k, 0);                           // 统计能到达n-1的次数，从0位置开始

            return ways;
        }
        void dfs_NumWays(int n, int k, int start)
        {
            if (k == 0)                         // Base Case k次用完
            {
                if (start == n - 1)             // 如果传递到n-1, ways++
                    ways++;
                return;
            }

            var edge = graph_NumWays[start];            // 枚举邻接表
            foreach (var newStart in edge)
            {
                dfs_NumWays(n, k - 1, newStart);        // 递归newStart，k-1次
            }
        }

        /// <summary>
        /// 797. 所有可能的路径
        /// LCR 110. 所有可能的路径
        /// [0..n-1] 所有可能的路径
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        IList<IList<int>> res_AllPathsSourceTarget = new List<IList<int>>();
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            IList<int> path = new List<int>();
            dfs_AllPathsSourceTarget(graph, 0, path);

            return res_AllPathsSourceTarget;
        }

        private void dfs_AllPathsSourceTarget(int[][] graph, int start, IList<int> path)
        {
            path.Add(start);

            if (start == graph.Length - 1)              // 满足访问0..n-1个节点
                res_AllPathsSourceTarget.Add(new List<int>(path));

            foreach (var newStart in graph[start])      // 枚举图中每一个顶点
                dfs_AllPathsSourceTarget(graph, newStart, path);             // 递归每一个顶点

            path.RemoveAt(path.Count - 1);              // 从路径中删除节点
        }


        /// <summary>
        /// 133. 克隆图
        /// </summary>
        HashSet<Node> visited = new HashSet<Node>();
        Dictionary<Node, Node> mapO2C = new Dictionary<Node, Node>();
        public Node CloneGraph(Node node)
        {
            if (node == null)
                return node;
            dfs_CloneGraph(node);
            return mapO2C[node];
        }

        private void dfs_CloneGraph(Node node)
        {
            if (node == null)
                return;

            if (visited.Contains(node))
                return;

            visited.Add(node);

            if (!mapO2C.ContainsKey(node))
                mapO2C[node] = new Node(node.val);    // 在此处clone出来节点

            Node cloneNode = mapO2C[node];            // 拿出节点，准备初始化neighbors
            foreach (var neighbor in node.neighbors)
            {
                dfs_CloneGraph(neighbor);

                Node cloneNeighbor = mapO2C[neighbor];  // 把clone出来的neighbor添加到cloneNode中
                cloneNode.neighbors.Add(cloneNeighbor);
            }
        }


        /// <summary>
        /// 31. 下一个排列
        /// 分析：
        /// 切分nums，
        /// 1. 找到第一个正序索引(k-1)  while
        /// 2. 交换第一个比k-1小的数，没有则选最后一个数 2，3，1 while
        /// 3. [k,n-1]之后的数需要从小达到排序
        /// 
        /// 1, 3, 2
        /// k-1 k
        ///  2, 3, 1
        ///  2, 1, 3 排序
        ///    .            .             .
        ///      .  =>   .       => .
        /// .                  .       .
        /// 
        /// </summary>
        /// <param name="nums"></param>
        public void NextPermutation(int[] nums)
        {
            int k = nums.Length - 1;
            while (k > 0 && nums[k - 1] >= nums[k])     // 找到第一个正序位置(k-1) 2，[3],5,4,1
                k--;

            if (k == 0)                                 // 全逆序：5,4,3,2,1
                Array.Reverse(nums);
            else
            {
                int t = nums.Length - 1;
                while (nums[t] <= nums[k - 1])          // 不大于nums[k-1]的索引的前一个数, 2，[3],5,{4},1 
                    t--;
                swap(nums, t, k - 1);                   // 交换位置 2,{4},5,[3],1 ::基于正序3，交换后3在k之后最小

                Array.Reverse(nums, k, nums.Length - k); // 进行翻转[k..n-1]，2,4,1,3,5
            }
        }
        public void swap(int[] nums, int i, int j)
        {
            int tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }
        /// <summary>
        /// 2698. 求一个整数的惩罚数
        /// 1. 返回平方和
        /// 2. 分割字符串s "1296" 1,12,129,[1296], 2,29,[296],9,[96]
        /// 3. #129 >37 时 break
        /// 4. #tot 分割后相加
        /// 5. 划分子问题
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int PunishmentNumber(int n)
        {
            int res = 0;
            for (int i = 1; i <= n; i++)
            {
                string s = (i * i).ToString();
                if (dfs(s, 0, 0, i))
                {
                    res += i * i;                           // 1
                }
            }

            return res;
        }
        // tot 各个位数相加
        public bool dfs(string s, int start, int tot, int target)
        {
            if (start == s.Length)
                return tot == target;

            int sum = 0;
            for (int i = start; i < s.Length; i++)          // 2
            {
                sum = sum * 10 + s[i] - '0';                // 3
                if (sum + tot > target)                     // 4
                    break;
                if (dfs(s, i + 1, sum + tot, target))      // 5
                    return true;
            }
            return false;
        }
    }

}