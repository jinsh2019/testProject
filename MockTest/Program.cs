using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using static System.Console;

namespace MockTest
{


    class Program
    {
        #region 之前
        #region Day2 1. 两数之和 15. 三数之和 2. 两数相加 (大数相加)
        // 1. 两数之和
        // 暴力解法
        public static int[] twoSum(int[] nums, int target)
        {

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (target == nums[i] + nums[j])
                    {
                        return new int[] { i, j };
                    }
                }
            }
            throw new ArgumentException("No two sum solution");
        }
        // 两遍hash表
        public static int[] twoSum1(int[] nums, int target)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++) // 第一遍
            {
                map.Add(nums[i], i);
            }
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                if (map.ContainsKey(complement) && map[complement] != i) // 第二遍
                {
                    return new int[] { i, map[complement] };
                }
            }
            throw new ArgumentException("No two sum solution");
        }

        // 一遍哈希表 社会信誉体系的建立
        public static int[] twoSum2(int[] nums, int target) // 更加精简
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i]; // 下一次补偿的时候，会拿到第一次补偿
                if (map.ContainsKey(complement))
                {
                    return new int[] { map[complement], i };
                }
                map.TryAdd(nums[i], i);
            }
            throw new ArgumentException("No two sum solution");
        }

        // 15. 三数之和
        // 灵活运用双指针的异动，除去重复数据
        public static IList<IList<int>> threeSum(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums); // O(nlogn) 先排个序
            IList<IList<int>> ans = new List<IList<int>>();

            for (int first = 0; first < nums.Length; first++)
            {
                if (first > 0 && nums[first] == nums[first - 1])
                {
                    continue;
                }

                int third = n - 1;
                int target = -nums[first];
                for (int second = first + 1; second < n; second++)
                {
                    if (second > first + 1 && nums[second] == nums[second - 1]) // 与第一个指针不等的时，继续往下走
                    {
                        continue;
                    }

                    while (second < third && nums[second] + nums[third] > target) // 不断降低数值。不考虑小于target的情况，小于target无论如何就可能到0了
                    {
                        --third;
                    }

                    if (second == third)// 不能使用重复的值，break掉，看下一个
                    {
                        break;
                    }

                    if (nums[second] + nums[third] == target)
                    {
                        IList<int> list = new List<int>();
                        list.Add(nums[first]);
                        list.Add(nums[second]);
                        list.Add(nums[third]);
                        ans.Add(list);
                    }
                }
            }
            return ans;
        }

        #endregion

        #region  2. 两数相加 (大数相加)
        // 手撸错误，没有进位
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            return null;
            //int p1 = 0;
            //int t1 = 1;
            //while (l1 != null)
            //{
            //    p1 += l1.val * t1;
            //    t1 *= 10;
            //    l1 = l1.next;
            //}

            //int p2 = 0;
            //int t2 = 1;
            //while (l2 != null)
            //{
            //    p2 += l2.val * t2;
            //    t2 *= 10;
            //    l2 = l2.next;
            //}

            //long rs = p1 + p2;
            //// rs%10
            //if (rs == 0)
            //    return new ListNode(0);

            //ListNode p = null;
            //ListNode root = null;
            //while (rs != 0)
            //{
            //    int _val = (int)rs % 10;
            //    if (p == null)
            //    {
            //        root = p = new ListNode(_val);

            //    }
            //    else
            //    {
            //        p.next = new ListNode(_val);
            //        p = p.next;
            //    }
            //    rs = rs / 10;
            //}
            //return root;
        }
        //  手撸代码 实现链表大数相加
        public static ListNode AddTwoNumbers1(ListNode l1, ListNode l2)
        {
            ListNode dummy = new ListNode(-1); // 哑指针
            var p1 = l1;
            var p2 = l2;
            var p3 = dummy;
            bool flag = false; // 是否进位
            while (p1 != null && p2 != null)
            {
                int ans = flag ? p1.val + p2.val + 1 : p1.val + p2.val;
                flag = setFlag(ans);
                p3.next = new ListNode(ans % 10);
                p1 = p1.next;
                p2 = p2.next;
                p3 = p3.next;
            }

            while (p1 != null)
            {
                var ans = flag ? p1.val + 1 : p1.val;
                flag = setFlag(ans);
                p3.next = new ListNode(ans % 10);
                p1 = p1.next;
                p3 = p3.next;
            }

            while (p2 != null)
            {
                var ans = flag ? p2.val + 1 : p2.val;
                flag = setFlag(ans);
                p3.next = new ListNode(ans % 10);
                p2 = p2.next;
                p3 = p3.next;
            }

            if (flag)
                p3.next = new ListNode(1);
            return dummy.next;
        }

        private static bool setFlag(int rs)
        {
            bool flag;
            if (rs > 9)
                flag = true;
            else
                flag = false;
            return flag;
        }
        #endregion
        #endregion

        #region 岛屿问题
        // base question 695. 岛屿的最大面积
        public static int MaxAreaOfIsland(int[,] grid)
        {
            int res = 0;
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] == 1)
                    {
                        int a = area(grid, r, c);
                        res = Math.Max(res, a);
                    }
                }
            }
            return res;
        }

        private static int area(int[,] grid, int r, int c)
        {
            if (!inArea(grid, r, c)) // 判断是否在区域内
                return 0;

            if (grid[r, c] != 1) // 遇到非陆地，返回0
                return 0;

            grid[r, c] = 2; // 设置已经访问过

            return 1 +
                area(grid, r - 1, c) +
                area(grid, r + 1, c) +
                area(grid, r, c - 1) +
                area(grid, r, c + 1);
        }

        // 463. 岛屿的周长
        // grid 进阶
        public static int IslandPerimeter(int[,] grid)
        {
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] == 1)
                    {
                        return dfs_IslandPerimeter(grid, r, c);
                    }
                }
            }
            return 0;
        }

        private static int dfs_IslandPerimeter(int[,] grid, int r, int c)
        {
            // 函数因为「坐标 (r, c) 超出网格范围」返回，对应一条黄色的边
            if (!inArea(grid, r, c))
                return 1;

            // 函数因为「当前格子是海洋格子」返回，对应一条蓝色的边
            if (grid[r, c] == 0)
                return 1;

            // 函数因为「当前格子是已遍历的陆地格子」返回，和周长没关系
            if (grid[r, c] != 1)
                return 0;

            grid[r, c] = 2;
            return dfs_IslandPerimeter(grid, r - 1, c) +
                dfs_IslandPerimeter(grid, r + 1, c) +
                dfs_IslandPerimeter(grid, r, c - 1) +
                dfs_IslandPerimeter(grid, r, c + 1);
        }
        #endregion

        #region 树问题
        // base question 102. 二叉树的层序遍历
        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();

            Queue<TreeNode> queue = new Queue<TreeNode>();

            if (root != null)
            {
                queue.Enqueue(root);
            }

            while (queue.Count != 0)
            {
                int n = queue.Count; // 每层的数量

                List<int> level = new List<int>();
                for (int i = 0; i < n; i++)
                {
                    TreeNode node = queue.Dequeue();

                    level.Add(node.val); // 业务逻辑, 加入第

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
                res.Add(level);
            }

            return res;
        }

        // 112. 路径总和
        public static bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null)
                return false;

            if (root.left == null && root.right == null)
            {
                return root.val == sum;
            }

            int target = sum - root.val;
            return HasPathSum(root.left, target) ||
                HasPathSum(root.right, target);
        }
        // LeetCode 113 - Path Sum II
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            IList<IList<int>> res = new List<IList<int>>(); // 有效路径集合
            List<int> path = new List<int>(); // 路径
            traverse(root, sum, path, res);
            return res;
        }

        private void traverse(TreeNode root, int sum, List<int> path, IList<IList<int>> res)
        {
            if (root == null)
                return;

            path.Add(root.val); // tracking

            if (root.left == null && root.right == null)
            {
                if (root.val == sum)  // 叶子节点成功
                    res.Add(new List<int>(path));
            }

            int target = sum - root.val;

            traverse(root.left, target, path, res);
            traverse(root.right, target, path, res);

            path.RemoveAt(path.Count - 1); // back
        }

        #endregion

        #region 链表问题
        // 206. 反转链表
        public static ListNode ReverseList(ListNode head)
        {
            ListNode cur = head;
            ListNode pre = null;
            while (cur != null)
            {
                ListNode cnext = cur.next; // 防止指针丢失

                if (pre == null)
                    cur.next = null;// 首节点置空
                else
                    cur.next = pre; // 一般情况翻转

                pre = cur; // pre 移动
                cur = cnext;// cur  移动
            }

            return pre; // 
        }

        #endregion

        #region 排列组合问题
        // 39. Combination Sum
        // 40. Combination Sum II
        public static IList<IList<int>> combinationSum(int[] candidates, int target)
        {
            Array.Sort(candidates);
            List<int> current = new List<int>();
            IList<IList<int>> res = new List<IList<int>>();
            backtrack_CSum2(candidates, 0, target, current, res);

            return res;

        }

        private static void backtrack_CSum(int[] candidates, int m, int target, List<int> current, IList<IList<int>> res)
        {
            if (target < 0)
                return;
            else if (target == 0)
            {
                res.Add(new List<int>(current));
                return;
            }

            // 和 Combinations 问题类似，需要升序
            for (int i = m; i < candidates.Length; i++)
            {
                // 选择数字 candidates[i]
                current.Add(candidates[i]);
                // 元素 candidates[m..i) 均失效
                // i+1=> i (可以重复)
                backtrack_CSum(candidates, i, target - candidates[i], current, res);
                // 撤销选择
                current.RemoveAt(current.Count - 1);

            }
        }

        private static void backtrack_CSum2(int[] candidates, int m, int target, List<int> current, IList<IList<int>> res)
        {
            if (target < 0)
                return;
            else if (target == 0)
            {
                res.Add(new List<int>(current));
                return;
            }

            // 和 Combinations 问题类似，需要升序
            for (int i = m; i < candidates.Length; i++)
            {
                // 代码调整处：候选集合遍历
                if (i > m && candidates[i] == candidates[i - 1])
                {
                    // 如果 candidates[i] 与前一个元素相等，说明不是相等元素中的第一个，跳过。
                    continue;
                }

                // 选择数字 candidates[i]
                current.Add(candidates[i]);
                // 元素 candidates[m..i) 均失效
                backtrack_CSum2(candidates, i + 1, target - candidates[i], current, res);
                // 撤销选择
                current.RemoveAt(current.Count - 1);

            }
        }

        // LeetCode 216 - Combination Sum III
        public IList<IList<int>> combinationSum3(int k, int n)
        {
            List<int> current = new List<int>();
            IList<IList<int>> res = new List<IList<int>>();
            backtrack_CSum3(k, 1, n, current, res);
            return res;

        }

        private void backtrack_CSum3(int k, int m, int target, List<int> current, IList<IList<int>> res)
        {
            if (target < 0)
                return;
            else if (target == 0)
            {
                if (current.Count == k)
                {
                    res.Add(new List<int>(current));
                }
            }
            if (current.Count > k)
                return;

            // 从候选集合中选择
            for (int i = m; i <= 9; i++)
            {
                // 选择数字 i
                current.Add(i);
                // 数字 [m..i) 均失效
                backtrack_CSum3(k, i + 1, target - i, current, res);
                // 撤销选择
                current.RemoveAt(current.Count - 1);
            }
        }
        // LeetCode 377 - Combination Sum IV
        // LeetCode 322 - Coin Change
        // 动态规划问题
        #endregion

        #region 前缀和
        private int[] preSum;

        public void NumArray(int[] nums)
        {
            int n = nums.Length;
            // 计算前缀和数组

            preSum = new int[n + 1];
            preSum[0] = 0;
            for (int i = 0; i < n; i++)
            {
                preSum[i + 1] = preSum[i] + nums[i];
            }
        }

        public int sumRange(int i, int j)
        {
            return preSum[j + 1] - preSum[i];
        }

        // 724. 寻找数组的中心(枢轴)下标 
        public int pivotIndex(int[] nums)
        {
            // 首先计算所有元素之和 S
            int S = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                S += nums[i];
            }

            int A = 0; // A 为前缀和
            // 迭代计算前缀和
            for (int i = 0; i < nums.Length; i++)
            {
                int x = nums[i];
                if (2 * A + x == S)
                {
                    return i;
                }
                A += x;
            }
            return -1;
        }
        // 560. 和为 K 的子数组
        public int SubarraySum(int[] nums, int k)
        {
            int N = nums.Length;

            // 计算前缀和数组
            // presum[k] 表示元素 nums[0..k) 之和
            int[] preSum = new int[N + 1];
            int sum = 0;
            for (int i = 0; i < N; i++)
            {
                preSum[i] = sum;
                sum += nums[i];
            }
            preSum[N] = sum;

            // sum of nums[i..j) = sum of nums[0..j) - sum of nums[0..i)
            int count = 0;
            for (int i = 0; i <= N; i++)
            {
                for (int j = i + 1; j <= N; j++)
                {
                    if (preSum[j] - preSum[i] == k)
                        count++;
                }
            }

            return count;
        }
        public int SubarraySum2(int[] nums, int k)
        {
            // 前缀和 -> 该前缀和（的值）出现的次数
            Dictionary<int, int> preSum = new Dictionary<int, int>();
            // base case，前缀和 0 出现 1 次
            preSum.Add(0, 1);

            int sum = 0;
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i]; // 计算前缀和
                // 查找有多少个 sum[i] 等于 sum[j] - k
                if (preSum.ContainsKey(sum - k))
                    res += preSum[sum - k];

                if (preSum.ContainsKey(sum))
                {
                    preSum[sum] = preSum[sum] + 1;
                }
                else
                {
                    preSum.Add(sum, 1);
                }
            }
            return res;
        }

        #endregion

        #region tree_dfs
        private static void tree_dfs(TreeNode root)
        {
            // base case
            if (root == null)
                return;

            // 访问两个相邻结点：左子结点、右子结点
            tree_dfs(root.left);
            tree_dfs(root.right);
        }
        #endregion

        #region tree_bfs
        private static void tree_bfs(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                TreeNode node = queue.Dequeue();

                if (node.left != null)
                    queue.Enqueue(node.left);

                if (node.right != null)
                    queue.Enqueue(node.right);
            }
        }
        #endregion

        #region grid_dfs
        public static void grid_dfs(int[,] grid, int r, int c)
        {
            // 判断 base case
            // 如果坐标 (r, c) 超出了网格范围，直接返回
            if (!inArea(grid, r, c))
                return;

            // 如果这个格子不是岛屿，直接返回
            if (grid[r, c] != 1)
                return;

            grid[r, c] = 2; // 将格子标记为「已遍历过」

            // 访问上、下、左、右四个相邻结点
            grid_dfs(grid, r - 1, c);
            grid_dfs(grid, r + 1, c);
            grid_dfs(grid, r, c - 1);
            grid_dfs(grid, r, c + 1);
        }
        // 判断坐标 (r, c) 是否在网格中
        public static bool inArea(int[,] grid, int r, int c)
        {
            return r >= 0 && r < grid.GetLength(0)
                && c >= 0 && c < grid.GetLength(1);
        }
        #endregion

        #region grid_bfs
        public static int grid_bfs(int[,] grid, int _r, int _c)
        {
            int N = grid.GetLength(0);
            Queue<int[]> queue = new Queue<int[]>();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; i++)
                {
                    if (grid[i, j] == 1)
                    {
                        queue.Enqueue(new int[] { i, j });
                    }
                }
            }

            // 如果地图上只有陆地或者海洋，返回 -1
            if (queue.Count == 0 || queue.Count == N * N)
                return -1;

            int distince = -1;
            while (queue.Count != 0)
            {
                distince++;
                int n = queue.Count;

                for (int i = 0; i < n; i++)
                {
                    int[] cell = queue.Dequeue();

                    int r = cell[0];
                    int c = cell[1];

                    if (r - 1 >= 0 && grid[r - 1, c] == 0)
                    {
                        grid[r - 1, c] = 2;  // 
                        queue.Enqueue(new int[] { r - 1, c });
                    }
                    if (r + 1 < N && grid[r + 1, c] == 0)
                    {
                        grid[r + 1, c] = 2;
                        queue.Enqueue(new int[] { r + 1, c });
                    }
                    if (c - 1 >= 0 && grid[r, c - 1] == 0)
                    {
                        grid[r, c - 1] = 2;
                        queue.Enqueue(new int[] { r, c - 1 });
                    }
                    if (c + 1 < N && grid[r, c + 1] == 0)
                    {
                        grid[r, c + 1] = 2;
                        queue.Enqueue(new int[] { r, c + 1 });
                    }

                }
            }

            return distince;
        }
        #endregion

        #region graph_dfs
        public static void graph_dfs(Graph graph, int start, bool[] visited)
        {
            // print
            visited[start] = true;
            for (int i = 0; i < graph.adj[start].Count; i++)
            {
                if (!visited[i])
                    graph_dfs(graph, i, visited);
            }
        }
        #endregion

        #region graph_bfs
        public static void graph_bfs(Graph graph, int start, bool[] visited, Queue<int> queue)
        {
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                int front = queue.Dequeue();
                if (visited[front])
                    continue;
                // print
                visited[front] = true;
                for (int i = 0; i < graph.adj[start].Count; i++)
                {
                    queue.Enqueue(i);
                }
            }
        }
        #endregion

        #region linkNode
        // linkNode
        public static void linkedListOps(ListNode head)
        {
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                prev = curr;
                curr = curr.next;
            }
        }
        #endregion

        #region backtracking
        // 78. 子集
        public static IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            List<int> cur = new List<int>();
            backtrack_subset(nums, 0, cur, res);

            return res;
        }
        private static void backtrack_subset(int[] nums, int k, List<int> cur, IList<IList<int>> res)
        {
            if (k == nums.Length)
            {
                res.Add(new List<int>(cur));
                return;
            }

            // 不选择第 k 个元素
            backtrack_subset(nums, k + 1, cur, res); // trace 猫

            // 选择第 k 个元素
            cur.Add(nums[k]);
            backtrack_subset(nums, k + 1, cur, res); // trace 狗

            cur.RemoveAt(cur.Count - 1); // backing
        }

        // 46. 全排列 
        public static IList<IList<int>> Permute(int[] nums)
        {
            List<int> current = new List<int>(nums);
            IList<IList<int>> res = new List<IList<int>>();
            permute_Pnn_backtrack(current, 0, res);
            return res;
        }
        // P(n,n)
        private static void permute_Pnn_backtrack(List<int> current, int k, IList<IList<int>> res)
        {
            if (k == current.Count)
            {
                res.Add(new List<int>(current));
                return;
            }
            // 从候选集合中选择
            for (int i = 0; i < current.Count; i++)
            {
                // 选择数字 current[i]
                current.Swap(k, i);
                // 将 k 加一
                permute_Pnn_backtrack(current, k + 1, res);
                // 撤销选择
                current.Swap(k, i);
            }
        }

        // current[0..m) 是已选集合， current[m..N) 是候选集合
        // P(n,k)
        private static void permute_Pnk_backtrack(int k, List<int> current, int m, IList<IList<int>> res)
        {
            // 当已选集合达到 k 个元素时，收集结果并停止选择
            if (m == k)
            {
                res.Add(new List<int>(current.GetRange(0, k)));
                return;
            }

            // 从候选集合中选择
            for (int i = m; i < current.Count; i++)
            {
                // 选择数字 current[i]
                current.Swap(m, i);
                permute_Pnk_backtrack(k, current, m + 1, res);
                // 撤销选择
                current.Swap(m, i);
            }
        }

        // C(n,k)
        public IList<IList<int>> Combine(List<int> nums, int k)
        {
            List<int> current = new List<int>();
            IList<IList<int>> res = new List<IList<int>>();
            backtrack_Cnk(k, nums, 0, current, res);
            return res;
        }

        // current 是已选集合， nums[m..N) 是候选集合
        private void backtrack_Cnk(int k, List<int> nums, int m, List<int> current, IList<IList<int>> res)
        {
            // 当已选集合达到 k 个元素时，收集结果并停止选择
            if (current.Count == k)
            {
                res.Add(new List<int>(current));
            }
            // 从候选集合中选择
            for (int i = m; i < nums.Count; i++)
            {
                // 选择数字 nums[i]
                current.Add(nums[i]);
                // 元素 nums[m..i) 均失效
                backtrack_Cnk(k, nums, i + 1, current, res);
                // 撤销选择
                current.RemoveAt(nums.Count - 1);
            }
        }

        public static IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            // 对元素排序，保证相等的元素相邻
            Array.Sort(nums);
            List<int> current = new List<int>(nums.Length);
            IList<IList<int>> res = new List<IList<int>>();
            backtrack_subsetWithDup(nums, 0, current, res);
            return res;
        }

        // 候选集合: nums[k..N)
        private static void backtrack_subsetWithDup(int[] nums, int k, List<int> current, IList<IList<int>> res)
        {
            if (k == nums.Length)
            {
                res.Add(new List<int>(current));
                return;
            }

            // 选择 nums[k]
            current.Add(nums[k]);
            backtrack_subsetWithDup(nums, k + 1, current, res);
            current.RemoveAt(current.Count - 1);

            int j = k;
            while (j < nums.Length && nums[j] == nums[k])
            {
                j++;
            }
            backtrack_subsetWithDup(nums, j, current, res);
        }

        public static IList<IList<int>> permuteUnique(int[] nums)
        {
            List<int> current = new List<int>(nums);
            IList<IList<int>> res = new List<IList<int>>();
            backtrack_permuteUnique(current, 0, res);
            return res;
        }

        // 已选集合 current[0..m)，候选集合 current[m..N)
        private static void backtrack_permuteUnique(List<int> current, int m, IList<IList<int>> res)
        {
            if (m == current.Count)
            {
                res.Add(new List<int>(current));
                return;
            }

            // 使用 set 辅助判断相等的候选元素是否已经出现过。
            HashSet<int> seen = new HashSet<int>();
            for (int i = m; i < current.Count; i++)
            {
                int e = current[i];
                if (seen.Contains(e))
                {
                    // 如果已经出现过相等的元素，则不选此元素
                    continue;
                }

                seen.Add(e);
                current.Swap(m, i);
                backtrack_permuteUnique(current, m + 1, res);
                current.Swap(m, i);
            }
        }

        public IList<IList<int>> combineUnique(int[] nums, int k)
        {
            Array.Sort(nums);
            List<int> current = new List<int>(nums.Length);
            IList<IList<int>> res = new List<IList<int>>();
            backtrack_combineUnique(k, nums, 0, current, res);
            return res;
        }

        // current 是已选集合， nums[m..N) 是候选集合
        private void backtrack_combineUnique(int k, int[] nums, int m, List<int> current, IList<IList<int>> res)
        {
            if (current.Count == k)
            {
                res.Add(new List<int>(current));
                return;
            }

            for (int i = m; i < nums.Length; i++)
            {
                if (i > m && nums[i] == nums[i - 1])
                {
                    // nums[i] 与前一个元素相等，说明不是相等元素中第一个出现的，跳过。
                    continue;
                }
                // 选择数字 nums[i]
                current.Add(nums[i]);
                // 元素 nums[m..i) 均失效
                backtrack_combineUnique(k, nums, i + 1, current, res);
                // 撤销选择
                current.RemoveAt(current.Count - 1);
            }
            throw new NotImplementedException();
        }

        #endregion

        static void Main(string[] args)
        {

            #region 之前
            {
                int[] t1 = { 2, 7, 11, 15 };
                WriteLine(string.Join(',', twoSum(t1, 9)));
                int[] t2 = { 2, 7, 11, 15 };
                WriteLine(string.Join(',', twoSum1(t2, 9)));

                int[] t3 = { -1, 0, 1, 2, -1, -4 };
                WriteLine(string.Join(',', threeSum(t3)));

                int[] t4 = { };

                // 2,4,3
                // 5,6,4
                ListNode l1 = new ListNode(2);
                ListNode l2 = new ListNode(4);
                ListNode l3 = new ListNode(3);
                l1.next = l2;
                l2.next = l3;
                ListNode l6 = new ListNode(4);
                ListNode l5 = new ListNode(6, l6);
                ListNode l4 = new ListNode(5, l5);
                AddTwoNumbers1(l1, l4);

                ListNode dummy1 = new ListNode(-1);
                ListNode p1 = dummy1;
                for (int i = 0; i < 7; i++)
                {
                    p1.next = new ListNode(9);
                    p1 = p1.next;
                }
                ListNode dummy2 = new ListNode(-1);
                ListNode p2 = dummy2;
                for (int i = 0; i < 4; i++)
                {
                    p2.next = new ListNode(9);
                    p2 = p2.next;
                }

                AddTwoNumbers1(dummy1.next, dummy2.next);

                AddTwoNumbers1(new ListNode(5), new ListNode(5));
            }
            #endregion


            MaxDistance(new int[][] { new int[] { 1, 0, 1 }, new int[] { 0, 0, 0 }, new int[] { 1, 0, 1 } });
            Graph graph = null;
            {
                graph = new Graph(6);
                graph.adj[0].Add(1);
                graph.adj[0].Add(2);
                graph.adj[0].Add(3);
                graph.adj[1].Add(0);
                graph.adj[1].Add(3);
                graph.adj[1].Add(4);
                graph.adj[2].Add(0);
                graph.adj[3].Add(0);
                graph.adj[3].Add(1);
                graph.adj[3].Add(4);
                graph.adj[3].Add(5);
                graph.adj[4].Add(1);
                graph.adj[4].Add(3);
                graph.adj[4].Add(5);
                graph.adj[5].Add(3);
                graph.adj[5].Add(4);
            }
            // 图的深度优先遍历
            graph_dfs(graph, 0, new bool[graph.size]);
            // 图的广度优先遍历
            graph_bfs(graph, 0, new bool[graph.size], new Queue<int>());




            // 78. 子集
            int[] t5 = { 1, 2, 3 };
            Subsets(t5);

            // LeetCode 90 - Subsets II
            int[] t6 = { 1, 2, 2 };
            SubsetsWithDup(t6);

            // LeetCode 47 - Permutations II:
            int[] t7 = { 1, 1, 2 };
            permuteUnique(t7);


            // 39. Combination Sum
            int[] t8 = { 2, 3, 6, 7 };
            combinationSum(t8, 7);


            // 40. 组合总和 II
            int[] t9 = { 10, 1, 2, 7, 6, 1, 5 };
            combinationSum(t9, 8);

            WriteLine("Hello World!");
        }

        public static int MaxDistance(int[][] multiArray)
        {
            int[,] grid = BuildTo2D(multiArray);
            int N = grid.GetLength(0);
            Queue<int[]> queue = new Queue<int[]>();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        queue.Enqueue(new int[] { i, j });
                    }
                }
            }

            // 如果地图上只有陆地或者海洋，返回 -1
            if (queue.Count == 0 || queue.Count == N * N)
                return -1;

            int distince = -1;
            while (queue.Count != 0)
            {
                distince++;
                int n = queue.Count;

                for (int i = 0; i < n; i++)
                {
                    int[] cell = queue.Dequeue();

                    int r = cell[0];
                    int c = cell[1];

                    if (r - 1 >= 0 && grid[r - 1, c] == 0)
                    {
                        grid[r - 1, c] = 2;  // 
                        queue.Enqueue(new int[] { r - 1, c });
                    }
                    if (r + 1 < N && grid[r + 1, c] == 0)
                    {
                        grid[r + 1, c] = 2;
                        queue.Enqueue(new int[] { r + 1, c });
                    }
                    if (c - 1 >= 0 && grid[r, c - 1] == 0)
                    {
                        grid[r, c - 1] = 2;
                        queue.Enqueue(new int[] { r, c - 1 });
                    }
                    if (c + 1 < N && grid[r, c + 1] == 0)
                    {
                        grid[r, c + 1] = 2;
                        queue.Enqueue(new int[] { r, c + 1 });
                    }

                }
            }

            return distince;
        }
        private static int[,] BuildTo2D(int[][] my2DArray)
        {
            if (my2DArray == null)
                return null;
            int m = my2DArray.GetLength(0);
            int n = my2DArray[0].GetLength(0);
            int[,] ans = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ans[i, j] = my2DArray[i][j];
                }
            }
            return ans;
        }

    }


    #region public class
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    //Definition for a binary tree node.
    public class TreeNode
    {
        public int val { get; set; }
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }
        TreeNode() { }
        public TreeNode(int val) { this.val = val; }
        public TreeNode(int val, TreeNode left, TreeNode right)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Vertex
    {
        int data;
        public Vertex(int data)
        {
            this.data = data;
        }
    }

    public class Graph
    {
        public int size { get; }
        public Vertex[] vertexes { get; }
        public List<int>[] adj { get; }

        public Graph(int size)
        {
            this.size = size;
            vertexes = new Vertex[size];
            adj = new List<int>[size];
            for (int i = 0; i < size; i++)
            {
                vertexes[i] = new Vertex(i);
                adj[i] = new List<int>();
            }

        }
    }

    /// <summary>
    /// A UnionFindNode represents a set of nodes that it is a member of.
    /// 
    /// You can get the unique representative node of the set a given node is in by using the Find method.
    /// Two nodes are in the same set when their Find methods return the same representative.
    /// The IsUnionedWith method will check if two nodes' sets are the same (i.e. the nodes have the same representative).
    ///
    /// You can merge the sets two nodes are in by using the Union operation.
    /// There is no way to split sets after they have been merged.
    /// </summary>
    public class UnionFindNode
    {
        private UnionFindNode _parent;
        private uint _rank;

        /// <summary>
        /// Creates a new disjoint node, representative of a set containing only the new node.
        /// </summary>
        public UnionFindNode()
        {
            _parent = this;
        }

        /// <summary>
        /// Returns the current representative of the set this node is in.
        /// Note that the representative is only accurate untl the next Union operation.
        /// </summary>
        public UnionFindNode Find()
        {
            if (!ReferenceEquals(_parent, this)) _parent = _parent.Find();
            return _parent;
        }

        /// <summary>
        /// Determines whether or not this node and the other node are in the same set.
        /// </summary>
        public bool IsUnionedWith(UnionFindNode other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return ReferenceEquals(Find(), other.Find());
        }

        /// <summary>
        /// Merges the sets represented by this node and the other node into a single set.
        /// Returns whether or not the nodes were disjoint before the union operation (i.e. if the operation had an effect).
        /// </summary>
        /// <returns>True when the union had an effect, false when the nodes were already in the same set.</returns>
        public bool Union(UnionFindNode other)
        {
            if (other == null) throw new ArgumentNullException("other");
            var root1 = this.Find();
            var root2 = other.Find();
            if (ReferenceEquals(root1, root2)) return false;

            if (root1._rank < root2._rank)
            {
                root1._parent = root2;
            }
            else if (root1._rank > root2._rank)
            {
                root2._parent = root1;
            }
            else
            {
                root2._parent = root1;
                root1._rank++;
            }
            return true;
        }
    }

    static class IListExtensions
    {
        public static void Swap<T>(
            this IList<T> list,
            int firstIndex,
            int secondIndex
        )
        {
            //Contract.Requires(list != null);
            //Contract.Requires(firstIndex >= 0 && firstIndex < list.Count);
            //Contract.Requires(secondIndex >= 0 && secondIndex < list.Count);
            if (firstIndex == secondIndex)
            {
                return;
            }
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
    #endregion




}
