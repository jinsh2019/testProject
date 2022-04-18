using CDS;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace ThroughOut1
{
    class Program
    {
        #region day1
        // 34. 在排序数组中查找元素的第一个和最后一个位置
        // 手撸 ： todo 在相等的地方还可以进行优化，二分查找
        public static int[] SearchRange(int[] nums, int target)
        {
            int[] rs = { -1, -1 };
            if (nums == null || nums.Length == 0)
                return rs;
            int left = 0;
            int right = nums.Length;
            int mid = 0;
            while (left <= right && mid < nums.Length) // 有重复的数字
            {
                if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else if (nums[mid] > target)
                {
                    right = mid - 1;
                }
                else
                {
                    rs[0] = rs[1] = mid;
                    for (int i = mid - 1; i >= 0; i--)
                    {
                        if (target == nums[i])
                            rs[0] = i;
                        else
                            break;
                    }
                    for (int i = mid + 1; i < nums.Length; i++)
                    {
                        if (target == nums[i])
                            rs[1] = i;
                        else
                            break;
                    }
                    break;
                }
                mid = left + (right - left) / 2;
            }

            return rs;
        }

        // 33. 搜索旋转排序数组
        // 有想到思路，但是没有贯彻下去思路
        public static int Search(int[] nums, int target) // 没有重复数字
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int left = 0;
            int right = nums.Length - 1;
            int mid = 0;
            while (left <= right)
            {
                mid = left + (right - left) / 2;
                if (nums[mid] == target)
                {
                    return mid;
                }

                if (nums[left] <= nums[mid]) // 左半部有序
                {
                    if (target >= nums[left] && target < nums[mid])
                    {   // 在左边找
                        right = mid - 1;
                    }
                    else
                    {   // 在右边找
                        left = mid + 1;
                    }
                }
                else // 右半部有序
                {
                    if (target <= nums[right] && target > nums[mid])
                    {   // 在右边找
                        left = mid + 1;
                    }
                    else
                    {   // 在左边找
                        right = mid - 1;
                    }
                }
            }
            return -1;
        }

        // 74. 搜索二维矩阵
        public static bool SearchMatrix(int[,] matrix, int target)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            int low = 0;
            int high = m * n - 1;
            while (low <= high)
            {
                int mid = (high - low) / 2 + low;
                // 5/3=1...0
                int r = mid / n; // mid 中的第几行
                int c = mid % n; // mid 中的模代表第几列
                int x = matrix[r, c];
                if (x < target)
                {
                    low = mid + 1;
                }
                else if (x > target)
                {
                    high = mid - 1;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region day2
        // 153. 寻找旋转排序数组中的最小值
        // 有序旋转找最小
        public static int findMin(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] < nums[right])
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return nums[left];
        }
        // 162. 寻找峰值
        public static int FindPeakElement(int[] nums)
        {
            // 无序找峰值
            int left = 0;
            int right = nums.Length - 1;

            while (left < right)
            {
                int mid = (right - left) / 2 + left;
                if (nums[mid] > nums[mid + 1]) // 如果中间值 大的话，向左找
                {
                    right = mid;
                }
                else // 中间值小，向右找
                {
                    left = mid + 1;
                }
            }
            return left;

        }
        #endregion

        #region day3
        // 82. 删除排序链表中的重复元素 II
        // 手撸
        public static ListNode DeleteDuplicates(ListNode head)
        {
            // base case
            if (head == null || head.next == null)
                return head;

            var left = head;
            var right = head.next;
            var dummy = new ListNode(-1);
            dummy.next = head;
            var pre = dummy;
            while (right != null && right.next != null)
            {
                if (left.val != right.val)
                {
                    if (left.next != right) // 不挨着就准备删
                    {
                        pre.next = right;
                        left = right;
                        right = right.next;
                    }
                    else // 一起移动
                    {
                        right = right.next;
                        left = left.next;
                        pre = pre.next;
                    }
                }
                else
                {
                    right = right.next;
                }
            }
            if (left.val == right.val)
                pre.next = null; //  pre 直接指向null
            else if (left.next != right)
                pre.next = right; // pre 直接指向right
            return dummy.next;
        }

        // 15. 三数之和 mock
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            IList<IList<int>> res = new List<IList<int>>();

            for (int k = 0; k < nums.Length - 2; k++)
            {
                if (nums[k] > 0)
                    break;
                if (k > 0 && nums[k] == nums[k - 1]) // 排除k的重复值
                    continue;
                int i = k + 1;
                int j = nums.Length - 1;

                while (i < j)
                {
                    int sum = nums[k] + nums[i] + nums[j];
                    if (sum < 0)
                        while (i < j && nums[i] == nums[++i]) ;// 排除i的重复值(使用最右边的一个i)
                    else if (sum > 0)
                        while (i < j && nums[j] == nums[--j]) ;// 排除j的重复值(使用最左边的一个j)
                    else
                    {
                        res.Add(new List<int>() { nums[k], nums[i], nums[j] });
                        while (i < j && nums[i] == nums[++i]) ;
                        while (i < j && nums[j] == nums[--j]) ;
                    }
                }
            }
            return res;
        }
        #endregion

        #region day4
        // 844. 比较含退格的字符串
        // 手撸
        public static bool BackspaceCompare(string s, string t)
        {
            var s1 = s.ToCharArray();
            var t1 = t.ToCharArray();
            Stack<char> sstack = new Stack<char>();
            Stack<char> tstack = new Stack<char>();
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != '#')
                {
                    sstack.Push(s1[i]);
                }
                else
                {
                    if (sstack.Count != 0)
                        sstack.Pop();
                }
            }

            for (int i = 0; i < t1.Length; i++)
            {
                if (t1[i] != '#')
                {
                    tstack.Push(t1[i]);
                }
                else
                {
                    if (tstack.Count != 0)
                        tstack.Pop();
                }
            }

            if (tstack.Count == sstack.Count)
            {
                while (tstack.Count != 0 && sstack.Count != 0)
                {
                    if (tstack.Pop() != sstack.Pop())
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return false;

        }

        // 11. 盛最多水的容器
        //  手撸代码
        public static int MaxArea(int[] height)
        {
            int left = 0;
            int right = height.Length - 1;
            var vol = (right - left) * Math.Min(height[left], height[right]);
            while (left < right)
            {
                if (height[left] < height[right])
                {
                    left++;
                }
                else
                {
                    right--;
                }
                vol = Math.Max((right - left) * Math.Min(height[left], height[right]), vol);
            }

            return vol;
        }

        // 986. 区间列表的交集
        public static int[,] IntervalIntersection(int[,] firstList, int[,] secondList)
        {
            List<int[,]> list = new List<int[,]>();
            int i = 0;
            int j = 0;
            while (i < firstList.GetLength(0) && j < secondList.GetLength(0))
            {
                int lo = Math.Max(firstList[i, 0], secondList[j, 0]); // 低位选最大值
                int hi = Math.Min(firstList[i, 1], secondList[j, 1]); // 高位选最小值
                if (lo <= hi)                                         //
                    list.Add(new int[,] { { lo, hi } });

                if (firstList[i, 1] < secondList[j, 1]) // 比较高位，谁小谁往下移一位
                    i++;
                else
                    j++;
            }
            // 返回结果
            int[,] ans = new int[list.Count, 2];// count行，2列
            for (int k = 0; k < list.Count; k++)
            {
                ans[k, 0] = list[k][0, 0]; // {{1,2},{5},}
                ans[k, 1] = list[k][0, 1];
            }
            return ans;
        }
        #endregion

        #region day5
        // 209. 长度最小的子数组
        // 这个手撸没有价值， 最坏情况O(n^2)
        public static int minSubArrayLen(int target, int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;
            if (nums[0] >= target)
                return 1;

            int left = 0;
            int right = 1;
            int dis = int.MaxValue;
            int result = nums[left];
            while (left <= right && right < nums.Length)
            {
                if ((result += nums[right]) < target)
                {
                    right++;
                }
                else if (result > target)
                {
                    if (nums[right] >= target)
                        return 1;
                    dis = Math.Min(right - left + 1, dis);
                    left++;
                    right = left + 1;
                    result = nums[left];
                }
                else
                {
                    dis = Math.Min(right - left + 1, dis);
                    left = right;
                    right = right + 1;
                    result = nums[left];
                }
            }

            return dis == int.MaxValue ? 0 : dis;
        }

        // mock 209
        // O(n)
        public static int MinSubArrayLen(int target, int[] nums)
        {
            int left = 0;
            int right = 1;

            int sum = nums[left];
            int result = int.MaxValue;

            if (sum >= target) return 1; // 第一个数字满足即返回

            while (right < nums.Length)
            {
                sum += nums[right];

                while (sum >= target)  // 不停的移动right 直到 sum >= target
                {
                    int len = right - left + 1;
                    result = result > len ? len : result;

                    sum -= nums[left++]; // 不停的减 left，知道不满足条件
                }

                right++;
            }

            if (result == int.MaxValue) return 0;
            else return result;
        }
        // 713. 乘积小于K的子数组
        // mock
        public static int numSubarrayProductLessThanK(int[] nums, int k)
        {
            if (k <= 1) return 0;
            int prod = 1; int ans = 0; int left = 0;
            for (int right = 0; right < nums.Length; right++)
            {
                prod *= nums[right];
                while (prod >= k)
                {
                    prod /= nums[left++];
                }
                ans += right - left + 1;
            }

            return ans;
        }
        #endregion

        #region day6
        // 200. 岛屿数量 
        // mock 手撸代码
        public static int NumIslands(int[,] grid)
        {
            // dfs 
            // 上下左右
            // 考虑边界问题
            int ans = 0;
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    ans += dfs_NumsOfIsland(grid, r, c) > 0 ? 1 : 0;
                }
            }

            return ans;
        }
        private static int dfs_NumsOfIsland(int[,] grid, int r, int c)
        {
            if (!inGrid(grid, r, c))
                return 0;

            if (grid[r, c] != 1)
                return 0;

            grid[r, c] = 2;

            return 1 + dfs_NumsOfIsland(grid, r - 1, c)
                    + dfs_NumsOfIsland(grid, r + 1, c)
                    + dfs_NumsOfIsland(grid, r, c - 1)
                    + dfs_NumsOfIsland(grid, r, c + 1);
        }
        private static bool inGrid(int[,] grid, int r, int c)
        {
            if (r >= 0 && r < grid.GetLength(0) && c >= 0 && c < grid.GetLength(1))
            {
                return true;
            }
            return false;
        }

        // 547. 省份数量
        public static int FindCircleNum(int[,] isConnected)
        {
            // int[][] isConnected 是无向图的邻接矩阵，n 为无向图的顶点数量
            int n = isConnected.GetLength(0);
            // 定义 bool 数组标识顶点是否被访问
            bool[] visited = new bool[n];
            // 定义 cnt 来累计遍历过的连通域的数量
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                // 若当前顶点 i 未被访问，说明又是一个新的连通域，则遍历新的连通域且cnt+=1.
                if (!visited[i])
                {
                    cnt++;
                    dfs(i, isConnected, visited);
                }
            }
            return cnt;
        }

        private static void dfs(int i, int[,] isConnected, bool[] visited)
        {
            // 对当前顶点 i 进行访问标记
            visited[i] = true;

            // 继续遍历与顶点 i 相邻的顶点（使用 visited 数组防止重复访问）
            for (int j = 0; j < isConnected.GetLength(0); j++)
            {
                if (isConnected[i, j] == 1 && !visited[j])
                {
                    dfs(j, isConnected, visited);
                }
            }
        }

        // 并查集的实现
        public int findCircleNum(int[][] isConnected)
        {
            int n = isConnected.GetLength(0);
            // 初始化并查集
            UnionFind uf = new UnionFind(n);
            // 遍历每个顶点，将当前顶点与其邻接点进行合并
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (isConnected[i][j] == 1)
                    {
                        uf.union(i, j);
                    }
                }
            }
            // 返回最终合并后的集合的数量
            return uf.find(n);
        }
        #endregion

        #region day7
        public static Node Connect(Node root)
        {
            if (root == null)
                return root;
            // 从根节点开始
            Node leftmost = root;

            while (leftmost.left != null)
            {
                // 遍历这一层节点组织成的链表， 为下一层的节点更新next指针
                Node head = leftmost;

                while (head != null)
                {
                    // CONNECTION 1
                    head.left.next = head.right; // 进行内部连接

                    // CONNECTION 2
                    if (head.next != null) // 紧邻父节点的链接
                        head.right.next = head.next.left;

                    // 指针向后移动
                    head = head.next;
                }

                // 去下一层的最左的节点
                leftmost = leftmost.left;
            }
            return root;
        }
        #endregion

        #region day8
        /*
 *   0 0 0
 *   0 0 0
 *   0 0 0
 * 
 * */
        static int[,] dir = {
            {0,1 }, // right
            {0,-1 },// left
            {1,0 }, // down
            {-1,0 },// up
            {1,-1 },// down left
            {-1,1 },// up right
            {-1,-1 },// up left
            {1,1 } // up right
            };

        public static int ShortestPathIbnaryMaxtrix(int[,] matrix)
        {
            int[] arr = { 1, 2, 3 };
            string.Join
                (" ", arr);
            System.Console.WriteLine();
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            if (matrix[0, 0] == 1 || matrix[m - 1, n - 1] == 1) // base case
                return -1;

            bool[,] visited = new bool[m, n];
            visited[0, 0] = true;
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[2] { 0, 0 }); //  存储 x,y

            int ans = 0;
            while (queue.Count != 0)
            {
                int size = queue.Count;
                for (int i = 0; i < size; i++) // 将queue弹出来
                {
                    int[] pop = queue.Dequeue();
                    if (pop[0] == m - 1 && pop[1] == n - 1) // 到达target
                        return ans + 1;
                    for (int k = 0; k < 8; k++)
                    {
                        int nextX = dir[k, 0] + pop[0];
                        int nextY = dir[k, 1] + pop[1];

                        if (nextX >= 0 && nextX < m && nextY >= 0 && nextY < n && !visited[nextX, nextY] && matrix[nextX, nextY] == 0)
                        {
                            queue.Enqueue(new int[] { nextX, nextY });
                            visited[nextX, nextY] = true;
                        }
                    }
                    ans++;
                }
            }
            return -1;
        }

        // 17. 电话号码的字母组合
        public static List<string> letterCombinations(string digits)
        {
            List<string> ans = new List<string>();
            if (digits.Length == 0)
                return ans;

            // array, tree, graph, map
            Dictionary<char, string> map = new Dictionary<char, string>();
            map.Add('2', "abc");
            map.Add('3', "def");
            map.Add('4', "ghi");
            map.Add('5', "jkl");
            map.Add('6', "mno");
            map.Add('7', "pqrs");
            map.Add('8', "tuv");
            map.Add('9', "wxyz");
            var cur = new StringBuilder();
            var nums = digits.ToCharArray();
            backtrack3(nums, 0, map, ans, cur);
            return ans;
        }

        private static void backtrack3(char[] nums, int k, Dictionary<char, string> map, List<string> ans, StringBuilder cur)
        {
            if (k == nums.Length)
            {
                ans.Add(cur.ToString());
            }
            else
            {
                char digit = nums[k]; // nums
                string letters = map[digit];
                int letterCount = letters.Length;
                for (int i = 0; i < letterCount; i++)
                {
                    cur.Append(letters[i]);
                    backtrack3(nums, k, map, ans, cur);
                    cur.Remove(k, 1);
                }
            }
        }

        #endregion

        #region day13 day14

        // 695 岛屿的最大面积
        public int MaxAreaOfIsland(int[,] grid)
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

        private int area(int[,] grid, int r, int c)
        {
            if (!inArea(grid, r, c))
                return 0;

            if (grid[r, c] != 1) // 岛屿的边界条件
                return 0;

            grid[r, c] = 2;
            return 1
                + area(grid, r - 1, c)
                + area(grid, r + 1, c)
                + area(grid, r, c - 1)
                + area(grid, r, c + 1);

        }

        // 22. 括号生成
        public IList<string> GenerateParenthesis(int n)
        {
            List<string> res = new List<string>();
            if (n == 0) return res;
            helper(res, "", n, n);

            return res;
        }

        private void helper(List<string> res, string s, int left, int right)
        {
            if (left > right)// 轮到右括号了  (=> () 
            {
                return;
            }
            if (left == 0 && right == 0)
            {
                res.Add(s);
                return;
            }
            if (left > 0)
            {
                helper(res, s + "(", left - 1, right);
            }
            if (right > 0)
            {
                helper(res, s + ")", left, right - 1);
            }
        }

        // 79. 单词搜索
        public bool Exist(char[][] board, string word)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    if (Exist(board, i, j, word, 0))
                        return true;
                }
            }
            return false;
        }

        private bool Exist(char[][] board, int i, int j, string word, int start)
        {
            if (start >= word.Length) return true;
            if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length) return false;
            if (board[i][j] == word[start++])
            {
                char c = board[i][j];
                board[i][j] = '#'; // marked
                bool res = Exist(board, i - 1, j, word, start) ||
                    Exist(board, i + 1, j, word, start) ||
                    Exist(board, i, j - 1, word, start) ||
                    Exist(board, i, j + 1, word, start);

                board[i][j] = c; // backing
                return res;
            }

            return false;
        }
        // house rober
        public int rob(int[] nums)
        {
            if (nums.Length == 0)
                return 0;
            // 子问题
            // f(k) = 偷[0..k)房间中的最大金额

            // f(0) = 0
            // f(1)= nums[0]
            // f(k) = max{rob(k-1), nums[k-1]+ rob(k-2)};
            int N = nums.Length;
            int[] dp = new int[N + 1];
            dp[0] = 0;
            dp[1] = nums[0];

            for (int k = 2; k <= N; k++)
            {
                // 套用子问题的递推关系
                dp[k] = Math.Max(dp[k - 1], nums[k - 1] + dp[k - 2]);
            }

            return dp[N];
        }

        // 213. 打家劫舍 II
        public int rob2(int[] nums)
        {
            int N = nums.Length;
            if (N == 1) return nums[0];

            // 动态规划数组
            int[] f = new int[N + 1];
            int[] g = new int[N + 1];
            // 定义起始位置
            f[1] = nums[0]; // 第一个房间作为起点
            g[2] = nums[1]; // 第二个房子作为起点

            for (int i = 2; i <= N - 1; i++)
            {
                f[i] = Math.Max(f[i - 1], f[i - 2] + nums[i - 1]);
            }

            for (int i = 3; i <= N; i++)
            {
                g[i] = Math.Max(g[i - 1], g[i - 2] + nums[i - 1]);
            }

            return Math.Max(f[N - 1], g[N]);
        }

        // 55. 跳跃游戏
        public bool CanJump(int[] nums)
        {
            // dp[i] 表示i位置可以到达
            bool[] dp = new bool[nums.Length];
            dp[0] = true;

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    // 1. 达到j位置；
                    // 2. nums[j] 的步长 + j位置 如果可以到达i位置，则dp[i]为true
                    if (dp[j] && nums[j] + j >= i)
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }
            return dp[nums.Length - 1];
        }


        public bool CanJump_greedy(int[] nums)
        {
            if (nums.Length == 1)
            {
                return true;
            }

            // 取最大覆盖跨度
            int cover = nums[0];
            for (int i = 1; i <= cover; i++)
            {
                cover = Math.Max(cover, i + nums[i]);
                if (cover >= nums.Length - 1)
                    return true;
            }

            return false;
        }

        // 201. 数字范围按位与
        public int RangeBitwiseAnd(int m, int n)
        {
            //m 要赋值给 i，所以提前判断一下
            if (m == int.MaxValue)
            {
                return m;
            }
            int res = m;
            for (int i = m + 1; i <= n; i++)
            {
                res &= i;
                if (res == 0 || i == int.MaxValue)
                {
                    break;
                }
            }
            return res;

        }

        // 202. 快乐数
        int bitSquareSum(int n)
        {
            int sum = 0;
            while (n > 0)
            {
                int bit = n % 10;
                sum += bit * bit;
                n = n / 10;
            }
            return sum;
        }
        // n = 19 or 2
        // 整体思路是： slow == fast 有 1 或 非1 两种情况
        bool isHappy(int n)
        {
            int slow = n, fast = n;
            do
            {
                slow = bitSquareSum(slow); // 82
                fast = bitSquareSum(fast); // 68
                fast = bitSquareSum(fast); // 100
            } while (slow != fast);

            return slow == 1;
        }

        // 62. 不同路径
        public int UniquePaths(int m, int n)
        {
            int[,] dp = new int[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 1;
                    }
                    else
                    {
                        dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                    }
                }
            }

            return dp[m - 1, n - 1];
        }

        // 322. 零钱兑换

        public int CoinChange(int[] nums, int n)
        {
            if (nums.Length == 0)
                return -1;

            int[] dp = new int[n + 1];
            dp[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                int min = int.MaxValue;
                for (int j = 0; j < nums.Length; j++)
                {
                    if (i - nums[j] >= 0 && dp[i - nums[j]] < min)
                    {
                        min = dp[i - nums[j]] + 1; // 状态转移方程
                    }
                }
                // memo[i] = (min == Integer.MAX_VALUE ? Integer.MAX_VALUE : min);
                dp[i] = min;
            }

            return dp[n] == int.MaxValue ? -1 : dp[n];
        }

        // 343. 整数拆分
        public int IntegerBreak(int n)
        {
            if (n <= 3) return n - 1;
            int a = n / 3, b = n % 3;
            if (b == 0) return (int)Math.Pow(3, a);
            if (b == 1) return (int)Math.Pow(3, a - 1) * 4;
            return (int)Math.Pow(3, a) * 2;
        }

        // 300. 最长递增子序列
        public int LengthOfLIS(int[] nums)
        {
            int[] dp = new int[nums.Length];
            int res = 0;

            foreach (var num in nums)
            {
                int i = 0, j = res;
                while (i < j)
                {
                    int m = (i + j) / 2;
                    if (dp[m] < num) i = m + 1;
                    else j = m;
                }
                dp[i] = num;
                if (res == j) res++;
            }
            return res;
        }

        // 91. 解码方法
        public int NumDecodings(String s)
        {
            int n = s.Length;
            int[] f = new int[n + 1];
            f[0] = 1;
            for (int i = 1; i <= n; ++i)
            {
                if (s[i - 1] != '0')
                {
                    f[i] += f[i - 1];
                }
                if (i > 1 && s[i - 2] != '0' && ((s[i - 2] - '0') * 10 + (s[i - 1] - '0') <= 26))
                {
                    f[i] += f[i - 2];
                }
            }
            return f[n];
        }


        public String LongestPalindrome(String s)
        {
            int len = s.Length;
            if (len < 2)
            {
                return s;
            }

            int maxLen = 1;
            int begin = 0;
            // dp[i,j] 表示 s[i..j] 是否是回文串
            bool[,] dp = new bool[len, len];
            // 初始化：所有长度为 1 的子串都是回文串
            for (int i = 0; i < len; i++)
            {
                dp[i, i] = true;
            }

            char[] charArray = s.ToCharArray();
            // 递推开始
            // 先枚举子串长度
            for (int L = 2; L <= len; L++)
            {
                // 枚举左边界，左边界的上限设置可以宽松一些
                for (int i = 0; i < len; i++)
                {
                    // 由 L 和 i 可以确定右边界，即 j - i + 1 = L 得
                    int j = L + i - 1;
                    // 如果右边界越界，就可以退出当前循环
                    if (j >= len)
                    {
                        break;
                    }

                    if (charArray[i] != charArray[j])
                    {
                        dp[i, j] = false;
                    }
                    else
                    {
                        if (j - i < 3)
                        {
                            dp[i, j] = true;
                        }
                        else
                        {
                            dp[i, j] = dp[i + 1, j - 1];
                        }
                    }

                    // 只要 dp[i,L] == true 成立，就表示子串 s[i..L] 是回文，此时记录回文长度和起始位置
                    if (dp[i, j] && j - i + 1 > maxLen)
                    {
                        maxLen = j - i + 1;
                        begin = i;
                    }
                }
            }
            return s.Substring(begin, begin + maxLen);
        }
        #endregion

        // 并查集
        static void Main(string[] args)
        {
            #region day1
            int[] t1 = { 5, 7, 7, 8, 8, 10 };
            SearchRange(t1, 8);

            int[] t2 = { 2, 2 };
            SearchRange(t2, 3);

            int[] t3 = { 4, 5, 6, 7, 0, 1, 2 };
            Search(t3, 0);


            int[,] t4 = {
                        { 1, 2, 3, 4 },
                        { 5, 6, 7, 8 },
                        { 9, 10, 11, 12 } };

            SearchMatrix(t4, 11);

            #endregion

            #region day2
            int[] t5 = { 3, 4, 5, 1, 2 };
            findMin(t5);

            int[] t6 = { 4, 5, 6, 7, 0, 1, 2 };
            findMin(t6);

            int[] t7 = { 11, 13, 15, 17 };
            findMin(t7);

            int[] t8 = { 2, 1 };
            findMin(t8);

            int[] t9 = { 1, 2, 3, 1 };
            FindPeakElement(t9);
            int[] t10 = { 1, 2, 1, 3, 5, 6, 4 };
            FindPeakElement(t10);
            #endregion

            #region day3
            int[] t11 = { 1, 2, 3, 3, 4, 4, 5 };
            ListNode lnode1 = CommonHelper.BuildLinkNode(t11);
            DeleteDuplicates(lnode1);
            int[] t12 = { 1, 1, 1, 2, 3 };
            ListNode lnode2 = CommonHelper.BuildLinkNode(t12);
            DeleteDuplicates(lnode2);

            int[] t13 = { 1, 1 };
            ListNode lnode3 = CommonHelper.BuildLinkNode(t13);
            DeleteDuplicates(lnode3);

            int[] t14 = { -1, 0, 1, 2, -1, -4 };
            ThreeSum(t14);
            int[] t15 = { };
            ThreeSum(t15);
            int[] t16 = { 0 };
            ThreeSum(t16);
            #endregion

            #region day4
            string s = "a#c";
            string t = "b";
            BackspaceCompare(s, t);

            int[] t17 = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
            MaxArea(t17);
            int[] t18 = { 2, 3, 4, 5, 18, 17, 6 };
            MaxArea(t18);
            int[] t19 = { 1, 1 };
            MaxArea(t19);

            int[,] t20 = new int[,] {{ 0, 2 },
                                     { 5, 10 },
                                     { 13, 23 },
                                     { 24, 25 }
                                    };
            int[,] t21 = new int[,]{{ 1,5 },
                                    {8,12 },
                                    {15,24 },
                                    {25,26 }
                                    };
            IntervalIntersection(t20, t21);
            #endregion

            #region day5

            int[] t22 = { 2, 3, 1, 2, 4, 3 };
            minSubArrayLen(7, t22);
            int[] t23 = { 1, 4, 4 };
            minSubArrayLen(4, t23);
            int[] t24 = { 1, 2, 3, 4, 5 };
            minSubArrayLen(11, t24);

            int[] t25 = { 10, 5, 2, 6 };
            numSubarrayProductLessThanK(t25, 100);
            #endregion

            #region day6
            int[,] t26 = new int[4, 5]{
                                    { 1,1,1,1,0},
                                    { 1,1,0,1,0},
                                    { 1,1,0,0,0},
                                    { 0,0,0,0,0}
                                    };
            NumIslands(t26);
            int[,] t27 = new int[,]{
                { 1,1,0,0,0},
                { 1,1,0,0,0},
                { 0,0,1,0,0},
                { 0,0,0,1,1}
                };

            NumIslands(t27);


            int[,] t28 = new[,]{
                {1,1,0},
                {1,1,0},
                {0,0,1}
                };
            FindCircleNum(t28);
            #endregion

            #region day7
            int[] t29 = { 1, 2, 3, 4, 5, 6, 7 };
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);
            Node node6 = new Node(6);
            Node node7 = new Node(7);
            node1.left = node2;
            node1.right = node3;
            node2.left = node4;
            node2.right = node5;
            node3.right = node7;

            //  Connect(node1);
            #endregion

            #region day8
            int[,] matrix1 = { { 0, 1 }, { 1, 0 } };
            ShortestPathIbnaryMaxtrix(matrix1);

            int[,] matrix2 = { { 0, 0, 0 }, { 1, 1, 0 }, { 1, 1, 0 } };
            ShortestPathIbnaryMaxtrix(matrix2);

            int[,] matrix3 = { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 1, 0 } };
            ShortestPathIbnaryMaxtrix(matrix3);

            int[][] matrix = { new int[] { 0, 0, 0 }, new int[] { 1, 1, 0 }, new int[] { 1, 1, 0 } };
            var test = CommonHelper.BuildTo2D(matrix);
            #endregion

            letterCombinations("23");
            WriteLine("Hello World!");
        }

        #region Private Method
        private void dfs(int[,] grid, int r, int c)
        {
            if (!inArea(grid, r, c))
                return;

            if (grid[r, c] != 1)
                return;

            grid[r, c] = 2;

            dfs(grid, r - 1, c); // 上下左右
            dfs(grid, r + 1, c);
            dfs(grid, r, c - 1);
            dfs(grid, r, c + 1);
        }

        private bool inArea(int[,] grid, int r, int c)
        {
            return 0 <= r && r < grid.Length && 0 <= c && c < grid.GetLength(1);
        }
        #endregion
    }
}
