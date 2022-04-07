using System;
using System.Collections.Generic;
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

        #endregion

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

        public static int shortestPathIbnaryMaxtrix(int[,] matrix)
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
            ListNode lnode1 = BuildLinkNode(t11);
            DeleteDuplicates(lnode1);
            int[] t12 = { 1, 1, 1, 2, 3 };
            ListNode lnode2 = BuildLinkNode(t12);
            DeleteDuplicates(lnode2);

            int[] t13 = { 1, 1 };
            ListNode lnode3 = BuildLinkNode(t13);
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
            shortestPathIbnaryMaxtrix(matrix1);

            int[,] matrix2 = { { 0, 0, 0 }, { 1, 1, 0 }, { 1, 1, 0 } };
            shortestPathIbnaryMaxtrix(matrix2);

            int[,] matrix3 = { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 1, 0 } };
            shortestPathIbnaryMaxtrix(matrix3);
            #endregion
            int[][] matrix = { new int[] { 0, 0, 0 }, new int[] { 1, 1, 0 }, new int[] { 1, 1, 0 } };
            var test = BuildTo2D(matrix);
            WriteLine("Hello World!");
        }

        #region Private Method
        private static ListNode BuildLinkNode(int[] arr)
        {
            if (arr == null)
                return null;

            ListNode lnode = null;
            for (int i = arr.Length - 1; i >= 0; i--)
                lnode = lnode == null ? new ListNode(arr[i]) : new ListNode(arr[i], lnode);
            return lnode;
        }

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

        bool inArea(int[,] grid, int r, int c)
        {
            return 0 <= r && r < grid.Length && 0 <= c && c < grid.GetLength(1);
        }
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
        int area(int[,] grid, int r, int c)
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
        #endregion

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

    #region pre-definition class

    // definition for singly-linked list.
    public class ListNode
    {
        public int val { get; set; }
        public ListNode next { get; set; }
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
        TreeNode(int val) { this.val = val; }
        TreeNode(int val, TreeNode left, TreeNode right)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    // Definition for a Node.
    public class Node
    {
        public int val;
        public Node left;
        public Node right;
        public Node next;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }

    class UnionFind
    {
        int[] roots;
        int size; // 集合数量

        public UnionFind(int n)
        {
            roots = new int[n];
            for (int i = 0; i < n; i++)
            {
                roots[i] = i;
            }
            size = n;
        }

        public int find(int i)
        {
            if (i == roots[i])
            {
                return i;
            }
            return roots[i] = find(roots[i]);
        }

        public void union(int p, int q)
        {
            int pRoot = find(p);
            int qRoot = find(q);
            if (pRoot != qRoot)
            {
                roots[pRoot] = qRoot;
                size--;
            }
        }
    }

    #endregion
}
