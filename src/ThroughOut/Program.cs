using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
namespace ThroughOut
{
    class Program
    {
        #region day1
        //704. 二分查找
        public static int Search(int[] nums, int target)
        {
            if (nums.Length == 0)
                return -1;

            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                var mid = (right - left) / 2 + left; // 防止溢出的情况
                if (nums[mid] == target)
                    return mid;
                else
                {
                    if (nums[mid] > target)
                        right = mid - 1;
                    else
                        left = mid + 1;
                }
            }

            return -1;
        }
        // 手敲
        public static int Search1(int[] nums, int target)
        {

            if (nums == null || nums.Length == 0)
                return -1;

            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {

                int mid = left + ((right - left) >> 1);
                if (nums[mid] == target)
                    return mid;
                else
                {
                    if (nums[mid] > target)
                        right = mid - 1;
                    else
                        left = mid + 1;
                }
            }
            return -1;
        }
        // 278. 第一个错误的版本 暴力版本
        public static int FirstBadVersion(int n)
        {
            if (n < 1 || n > int.MaxValue)
            {
                return -1;
            }
            while (n != 0)
            {
                if (!isBadVersion(n))
                    break;
                n--;
            }
            return n;
        }
        // 二分查找版本
        public static int FirstBadVersion1(int n)
        {
            int left = 1;
            int right = n;
            int mid = 0;
            while (left < right)
            {
                mid = left + ((right - left) / 2);
                if (isBadVersion(mid))
                    right = mid;
                else
                    left = mid + 1;
            }
            return left;
        }
        private static bool isBadVersion(int version)
        {
            if (version <= 4)
            {
                return false;
            }
            return true;
        }
        // 35. 搜索插入位置 (二分查找法)
        public static int searchInsert(int[] nums, int target)
        {
            // base case

            // main logic
            int left = 0;
            int right = nums.Length - 1;
            while (left <= right)
            {
                int mid = (right - left) / 2 + left;
                if (nums[mid] == target)
                {
                    return mid;
                }
                else
                {
                    if (nums[mid] > target)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
            }
            return left;
        }
        #endregion

        #region day2
        //977. 有序数组的平方 暴力解法
        public static int[] SortedSquares(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = nums[i] * nums[i];
            }
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < nums.Length - i - 1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
            }
            return nums;
        }
        // 手撸 时间复杂度O(n),空间复杂度O(n)
        public static int[] SortedSquares1(int[] nums)
        {
            List<int> listNeg = new List<int>();
            List<int> list = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < 0)
                {
                    listNeg.Add(nums[i] * nums[i]);
                }
                else
                {
                    list.Add(nums[i] * nums[i]);
                }
            }

            List<int> rs = new List<int>();
            int left = listNeg.Count - 1;
            int right = 0;
            while (left >= 0 && right < list.Count)
            {
                if (listNeg[left] > list[right])
                {
                    rs.Add(list[right]);
                    right++;
                }
                else
                {
                    rs.Add(listNeg[left]);
                    left--;
                }
            }

            if (left < 0) // 左边用完，把右数组剩余的加进去
            {
                for (int i = right; i < list.Count; i++)
                {
                    rs.Add(list[i]);
                }
            }

            if (right == list.Count) // 右边用完，把左数组剩余的加进去
            {
                for (int i = left; i >= 0; i--)
                {
                    rs.Add(listNeg[i]);
                }
            }
            return rs.ToArray();
        }
        // 对空间的优化
        public static int[] SortedSquares2(int[] nums)
        {
            int k = nums.Length - 1;
            int[] newnums = new int[nums.Length];
            for (int left = 0, right = nums.Length - 1; left <= right;) // 只走一遍
            {
                if (nums[left] * nums[left] > nums[right] * nums[right]) // left 从0个开始(最大), right 从length-1开始 （最大）
                {
                    newnums[k--] = nums[left] * nums[left]; // 用大值抢占位置
                    left++;
                }
                else
                {
                    newnums[k--] = nums[right] * nums[right];
                    right--;
                }
            }
            return newnums;
        }
        // 手撸 mock
        public static int[] SortedSquares3(int[] nums)
        {
            int k = nums.Length - 1;
            int[] newNums = new int[nums.Length];
            for (int left = 0, right = nums.Length - 1; left <= right;)
            {
                if (nums[left] * nums[left] > nums[right] * nums[right])
                {
                    newNums[k--] = nums[left] * nums[left];
                    left++;
                }
                else
                {
                    newNums[k--] = nums[right] * nums[right];
                    right--;
                }
            }
            return newNums;
        }


        // 189. 轮转数组
        // 手撸 1 符合题意
        public static int[] Rotate(int[] nums, int k)
        {
            int[] newNums = new int[nums.Length];
            for (int i = 0; i < k; i++)
            {
                newNums[nums.Length - k + i] = nums[i];
            }
            for (int i = k; i < nums.Length; i++)
            {
                newNums[i - k] = nums[i];
            }
            for (int i = 0; i < newNums.Length; i++)
            {
                nums[i] = newNums[i];
            }
            return newNums;
        }


        // 手撸 2 符合测试用例
        public static int[] Rotate1(int[] nums, int k)
        {
            int[] newNums = new int[nums.Length];

            for (int i = 0; i <= k; i++)
            {
                newNums[nums.Length - k + i - 1] = nums[i];
            }
            for (int i = k + 1; i < nums.Length; i++)
            {
                newNums[i - k - 1] = nums[i];
            }
            for (int i = 0; i < newNums.Length; i++)
            {
                nums[i] = newNums[i];
            }
            return newNums;
        }
        // mock k代表从第几个位置进行旋转
        public static int[] Rotate2(int[] nums, int k) // 精炼 
        {
            int n = nums.Length;
            int[] newArr = new int[n];
            for (int i = 0; i < n; ++i)
            {
                newArr[(i + k) % n] = nums[i]; // 3,4,5,6,0,1,2
            }
            Array.Copy(newArr, 0, nums, 0, n);
            return nums;
        }
        #endregion

        #region day3
        //  283. 移动零 
        // 手撸 穷举情况
        public static int[] MoveZeroes(int[] nums)
        {
            for (int i = 0, j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] == 0 && nums[j] == 0) // 都为0， j往左移动
                {
                    continue;
                }
                else if (nums[i] == 0 && nums[j] != 0) // 交换i，j; i++
                {
                    int tmp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = tmp;
                    i++;
                }
                else if (nums[i] != 0 && nums[j] == 0)
                {
                    i++;
                }
                else // nums[i] != 0 && nums[j] != 0 
                {
                    i++;
                }
            }

            return nums;
        }

        // mock
        public static int[] MoveZeroes2(int[] nums)
        {
            int left = 0; // left pointer
            for (int right = 0; right < nums.Length; right++) // right pointer
            {
                if (nums[right] != 0) // swap values; left pointer++
                {
                    var tmp = nums[right];
                    nums[right] = nums[left];
                    nums[left++] = tmp;
                }
            }

            return nums;
        }

        // 167. 两数之和 II - 输入有序数组 （有重复，不能使用map）
        // 手撸 两次dic
        public static int[] TwoSum(int[] numbers, int target)
        {

            Dictionary<int, int> map = new Dictionary<int, int>();

            int l = 0; int r = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                map.Add(numbers[i], i);
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                int offset = target - numbers[i];
                if (map.ContainsKey(offset))
                {
                    l = i + 1;
                    r = map[offset] + 1;
                    break;
                }
            }

            return new int[] { l, r };
        }

        // 常量 手撸代码 使用双指针
        public static int[] TwoSum1(int[] numbers, int target)
        {
            int left = 0;
            int right = numbers.Length - 1;
            while (left < right)
            {
                if (target - numbers[left] == numbers[right])
                {
                    break;
                }
                else if (target - numbers[left] < numbers[right])
                {
                    right--;
                }
                else
                    left++;
            }
            if (left == right)
            {
                throw new ArgumentException("no answer!");
            }
            return new int[] { left, right };
        }
        #endregion

        #region day4
        //344. 反转字符串
        public static char[] ReverseString(char[] s)
        {
            int left = 0;
            int right = s.Length - 1;
            char temp;
            while (left < right)
            {
                temp = s[left];
                s[left++] = s[right];
                s[right--] = temp;
            }

            return s;
        }
        // 557. 反转字符串中的单词 III
        // 手撸代码 时间复杂度O(n) 空间复杂度O(n)
        public static string ReverseWords(string s)
        {
            string[] sStr = s.Split(' ');
            int left = 0;
            int right = int.MaxValue;
            char temp;
            for (int i = 0; i < sStr.Length; i++)
            {
                char[] s1 = sStr[i].ToArray();
                left = 0;
                right = s1.Length - 1;
                while (left < right)
                {
                    temp = s1[left];
                    s1[left++] = s1[right];
                    s1[right--] = temp;
                }
                sStr[i] = string.Concat(s1);
            }

            return string.Join(" ", sStr); ;
        }
        #endregion

        #region day5
        // 876. 链表的中间结点
        // 手撸
        public static ListNode MiddleNode(ListNode head)
        {
            int cur = 0;
            ListNode left = head;
            while (left.next != null)
            {
                cur++;
                left = left.next;
            }
            int mid = (cur + 1) / 2;
            while (head.next != null)
            {
                if (mid == 0)
                    break;
                head = head.next;
                mid--;
            }
            return head;
        }

        //mock
        public static ListNode MiddleNode1(ListNode head)
        {
            ListNode fast = head;
            ListNode slow = head;
            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }
            return slow;
        }
        // 手撸 19. 删除链表的倒数第 N 个结点
        //mock
        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode cur = head;
            while (cur != null) // 开始计数
            {
                n--;
                cur = cur.next;
            }
            if (n == 0) // 头结点是倒数第n个
            {
                head = head.next;
            }
            if (n < 0) // 求n=0的位置，需要删除
            {
                cur = head;
                while (++n != 0) // 不为0的前一个节点
                {
                    cur = cur.next;
                }
                cur.next = cur.next.next;
            }
            return head;
        }
        public static ListNode RemoveNthFromEnd1(ListNode head, int n)
        {
            ListNode dummy = new ListNode(0, head);
            int length = getLength(head);
            ListNode cur = dummy;
            for (int i = 0; i < length - n + 1; i++) //  length - n + 1 将要被删除
            {
                cur = cur.next;
            }
            cur.next = cur.next.next;
            ListNode ans = dummy.next;
            return ans;
        }
        private static int getLength(ListNode head)
        {
            int length = 0;
            while (head != null)
            {
                ++length;
                head = head.next;
            }
            return length;
        }
        // 手撸 前后指针
        public static ListNode RemoveNthFromEn2(ListNode head, int n)
        {
            ListNode dummy = new ListNode(0, head);
            ListNode second = dummy;
            ListNode first = head;

            for (int i = 0; i < n; ++i)// f指向第n个节点
            {
                first = first.next;
            }

            while (first != null) // f 指向最后一个节点， S指向第L-n个节点 4-2
            {
                first = first.next;
                second = second.next;
            }
            second.next = second.next.next; // 删除第 L-n+1个节点
            ListNode ans = dummy.next; // 第一个节点被删除的情况
            return ans;
        }
        #endregion

        #region day6
        // 3. 无重复字符的最长子串
        // 手撸不行，mock
        public static int LengthOfLongestSubstring(string s)
        {
            if (s.Length < 2) // 排除小于2 的情况
                return s.Length;

            int left = 0;
            int right = 0;
            int ans = 0;
            HashSet<char> charSet = new HashSet<char>(); // 用来存储不重复的字符
            while (right < s.Length)
            {
                if (charSet.Contains(s[right]) == false) // 右指针不断往右移动
                {
                    charSet.Add(s[right++]);
                    ans = Math.Max(ans, right - left);
                }
                else // 遇到包含重复字符，左指针往右移动一位
                {
                    charSet.Remove(s[left++]);
                }
            }
            return ans; // 问题在于不能缩进多次，只能一次一次的缩
        }
        // mock  pwwkew
        public static int LengthOfLongestSubstring1(string s)
        {
            HashSet<char> hashset = new HashSet<char>();
            int n = s.Length;
            int right = -1;
            int ans = 0;

            for (int i = 0; i < n; i++)
            {
                if (i != 0) // 遇到相等的情况，需要从hashset中移除
                {
                    hashset.Remove(s[i - 1]);
                }
                while (right + 1 < n && !hashset.Contains(s[right + 1])) // 不断地移动右指针
                {
                    hashset.Add(s[right + 1]);
                    right++;
                }
                ans = Math.Max(ans, right - i + 1); //  
            }
            return ans; // 优选的原因： 支持多字符缩进，直接缩到i 
        }

        //  567. 字符串的排列
        // 手撸
        public static bool CheckInclusion(string s1, string s2)
        {
            int n = s1.Length, m = s2.Length;
            if (n > m)
                return false;
            int[] cnt1 = new int[26];
            int[] cnt2 = new int[26];
            for (int i = 0; i < n; i++)
            {
                ++cnt1[s1[i] - 'a'];  // 统计s1中字符的数量 0-25
                ++cnt2[s2[i] - 'a'];  // 统计s2中字符的数量 0-25
            }
            if (isSameValue(cnt1, cnt2)) // 判断数组相等
            {
                return true;
            }
            for (int i = n; i < m; i++) // 从n开始，继续往前走
            {
                ++cnt2[s2[i] - 'a'];     // 多统计的一个
                --cnt2[s2[i - n] - 'a']; // 少统计的一个
                if (isSameValue(cnt1, cnt2))
                    return true;
            }
            return false;
        }

        private static bool isSameValue(int[] cnt1, int[] cnt2)
        {
            for (int i = 0; i < 26; i++)
            {
                if (cnt1[i] != cnt2[i])
                    return false;
            }
            return true;
        }
        #endregion

        #region day7 数组的dfs，bfs
        static int[] dx = { 1, 0, 0, -1 };
        static int[] dy = { 0, 1, -1, 0 };

        // 733. 图像渲染
        // dfs
        public static int[,] floodFill_dfs(int[,] image, int sr, int sc, int newColor)
        {
            int currColor = image[sr, sc];
            if (currColor != newColor)
            {
                dfs_floodFill(image, sr, sc, currColor, newColor);
            }
            return image;
        }
        // 深度优先搜索 
        private static void dfs_floodFill(int[,] image, int x, int y, int currColor, int newColor) // 不是板子
        {
            if (image[x, y] == currColor)
            {
                image[x, y] = newColor;
                for (int i = 0; i < 4; i++)
                {
                    int mx = x + dx[i];// [1, 0, 0, -1]
                    int my = y + dy[i];// [0, 1, -1, 0]
                    if (mx >= 0 && mx < image.GetLength(0) && my >= 0 && my < image.GetLength(1))
                    {
                        dfs_floodFill(image, mx, my, currColor, newColor);
                    }
                }
            }
        }
        // bfs 广度优先遍历
        public static int[,] floodFillbybfs(int[,] image, int sr, int sc, int newColor)
        {
            int currColor = image[sr, sc];
            if (currColor == newColor)
                return image;

            int n = image.GetLength(0); // rows
            int m = image.GetLength(1); // cols

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { sr, sc }); // 将初始点进入队列 (1,1)
            image[sr, sc] = newColor;
            while (queue.Count != 0)
            {
                int[] cell = queue.Dequeue();
                int x = cell[0];
                int y = cell[1];
                for (int i = 0; i < 4; i++)
                {
                    int mx = x + dx[i]; // [1, 0, 0, -1]
                    int my = y + dy[i]; // [0, 1, -1, 0]
                    if (mx >= 0 && mx < n && my >= 0 && my < m && image[mx, my] == currColor)
                    {
                        queue.Enqueue(new int[] { mx, my });
                        image[mx, my] = newColor;
                    }
                }
            }
            return image;
        }

        // 695 岛屿的最大面积 mock
        public static int maxAreaOfIsland_dfs(int[,] grid)
        {
            int m = grid.GetLength(0), n = grid.GetLength(1);
            int ans = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        ans = Math.Max(ans, dfs(grid, i, j));
                    }
                }
            }
            return ans;
        }
        public static int dfs(int[,] grid, int r, int c)
        {
            if (r < 0 || c < 0 || r >= grid.GetLength(0) || c >= grid.GetLength(1)) // 考虑边界
                return 0
                    ;
            if (grid[r, c] != 1) // 0 or visited
                return 0;

            //count++; // 业务逻辑
            grid[r, c] = 2; // mark as visited

            var up = dfs(grid, r - 1, c); // 上
            var down = dfs(grid, r + 1, c); // 下
            var left = dfs(grid, r, c - 1); // 左
            var right = dfs(grid, r, c + 1); // 右

            return 1 + up + down + left + right;
        }

        public static int maxAreaOfIsland_bfs(int[,] grid)
        {
            int ans = 0;
            for (int r = 0; r != grid.GetLength(0); ++r)
            {
                for (int c = 0; c != grid.GetLength(1); ++c)
                {
                    int area = 0;
                    Queue<int> queuer = new Queue<int>(); // 行坐标
                    Queue<int> queuec = new Queue<int>(); // 列坐标
                    queuer.Enqueue(r);
                    queuec.Enqueue(c);

                    while (queuer.Count != 0)
                    {
                        int _r = queuer.Dequeue();
                        int _c = queuec.Dequeue();
                        if (_r < 0 || _c < 0 || _r == grid.GetLength(0) || _c == grid.GetLength(1)) // 检查越界
                            continue;

                        if (grid[_r, _c] != 1) // 不是陆地
                            continue;

                        ++area;

                        grid[_r, _c] = 2; // mark as visited

                        // 上 
                        queuer.Enqueue(_r - 1);
                        queuec.Enqueue(_c);
                        // 下
                        queuer.Enqueue(_r + 1);
                        queuec.Enqueue(_c);
                        // 左
                        queuer.Enqueue(_r);
                        queuec.Enqueue(_c - 1);
                        // 右
                        queuer.Enqueue(_r);
                        queuec.Enqueue(_c + 1);

                    }
                    ans = Math.Max(ans, area);
                }
            }
            return ans;
        }
        #endregion

        #region day8 tree的dfs， bfs
        // 617. 合并二叉树
        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            if (root1 == null || root2 == null)
            {
                return root1 == null ? root2 : root1;
            }

            return dfsTree(root1, root2);
        }
        // 递归实现
        private TreeNode dfsTree(TreeNode r1, TreeNode r2)
        {
            // 递归中止条件
            if (r1 == null || r2 == null)
                return r1 == null ? r2 : r1;

            r1.val += r2.val; // 具体逻辑
            r1.left = dfsTree(r1.left, r2.left);
            r1.right = dfsTree(r1.right, r2.right);

            return r1;
        }

        // mock  bfs
        public TreeNode bfs_mergeTrees(TreeNode root1, TreeNode root2)
        {
            // base case
            if (root1 == null || root2 == null)
                return root1 == null ? root2 : root1;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root1);
            queue.Enqueue(root2);

            while (queue.Count > 0) // 升级为大于
            {
                TreeNode r1 = queue.Dequeue();
                TreeNode r2 = queue.Dequeue();
                r1.val += r2.val; // 具体逻辑

                if (r1.left != null && r2.left != null) // bfs
                {
                    queue.Enqueue(r1.left);
                    queue.Enqueue(r2.left);
                }
                else if (r1.left == null) // 此处省略很多话
                {
                    r1.left = r2.left;
                }
                // 右节点同理 
                if (r1.right != null && r2.right != null)
                {
                    queue.Enqueue(r1.right);
                    queue.Enqueue(r2.right);
                }
                else if (r1.right == null)
                {
                    r1.right = r2.right;
                }
            }
            return root1;
        }


        // 116. 填充每个节点的下一个右侧节点指针
        public static Node Connect_bfs(Node root)
        {
            if (root == null)
                return null;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            root.next = null;
            Node preNode = null;
            while (queue.Count > 0)
            {
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    Node node = queue.Dequeue();

                    if (i < size - 1)// 舍去最后一个节点
                    {
                        node.next = queue.Peek();
                    }

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
            }
            return root;
        }

        // next 指针真绝了  morris 遍历也是使用next指针
        public static Node connectNextPointer(Node root)
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

        #region day9
        // 542. 01 矩阵
        public static int[,] UpdateMatrix_bfs(int[,] mat)
        {
            int n = mat.GetLength(0);
            int m = mat.GetLength(1);

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { 0, 0 });
            mat[0, 0] = 2;
            while (queue.Count > 0)
            {
                int[] cell = queue.Dequeue();
                int x = cell[0];
                int y = cell[1];

                WriteLine($"{x},{y}");
                for (int i = 0; i < 4; i++)
                {
                    int mx = x + dx[i];//  [1, 0, 0, -1] 逆时针
                    int my = y + dy[i];//  [0, 1, -1, 0]

                    if (mx >= 0 && mx < n && my >= 0 && my < m && mat[mx, my] != 2)
                    {
                        queue.Enqueue(new int[] { mx, my });
                        mat[mx, my] = 2;
                    }
                }
            }
            return mat;
        }

        public static int[,] UpdateMatrix_dfs(int[,] mat)
        {
            mat[0, 0] = 2;
            mat_dfs(mat, 0, 0);
            return mat;
        }

        private static void mat_dfs(int[,] mat, int sr, int sc)
        {
            for (int i = 0; i < 4; i++)
            {
                int mx = sr + dx[i];// [1, 0, 0, -1]
                int my = sc + dy[i];// [0, 1, -1, 0]
                if (mx >= 0 && mx < mat.GetLength(0) && my >= 0 && my < mat.GetLength(1) && mat[mx, my] != 2)
                {
                    mat[mx, my] = 2;
                    WriteLine($"{mx},{my}");
                    mat_dfs(mat, mx, my);
                }
            }
        }

        // 542. 01 矩阵
        private static int[,] updateMatrix(int[,] matrix)
        {
            // 首先将所有的 0 都入队，并且将 1 的位置设置成 -1，表示该位置是 未被访问过的 1
            Queue<int[]> queue = new Queue<int[]>();
            int m = matrix.GetLength(0), n = matrix.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        queue.Enqueue(new int[] { i, j });
                    }
                    else
                    {
                        matrix[i, j] = -1;
                    }
                }
            }

            int[] dx = new int[] { 1, 0, -1, 0 };
            int[] dy = new int[] { 0, 1, 0, -1 };
            while (queue.Count > 0)
            {
                int[] cell = queue.Dequeue();
                int x = cell[0], y = cell[1];
                for (int i = 0; i < 4; i++)
                {
                    int newX = x + dx[i];
                    int newY = y + dy[i];
                    // 如果四邻域的点是 -1，表示这个点是未被访问过的 1
                    // 所以这个点到 0 的距离就可以更新成 matrix[x][y] + 1。
                    if (newX >= 0 && newX < m && newY >= 0 && newY < n
                            && matrix[newX, newY] == -1)
                    {
                        matrix[newX, newY] = matrix[x, y] + 1;
                        queue.Enqueue(new int[] { newX, newY });
                    }
                }
            }

            return matrix;
        }
        // 994. 腐烂的橘子
        //  多源最短路径： 又一个盲区！
        public static int orangesRotting(int[,] grid)
        {
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            Queue<int[]> queue = new Queue<int[]>();

            int count = 0; // count 表示新鲜橘子的数量
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (grid[r, c] == 1)
                    {
                        count++;
                    }
                    else if (grid[r, c] == 2)
                    {
                        queue.Enqueue(new int[] { r, c });
                    }
                }
            }

            int round = 0; // round 表示腐烂的轮数，或者分钟数
            while (count > 0 && queue.Count > 0)
            {
                round++;
                int qcount = queue.Count();
                for (int i = 0; i < qcount; i++) // 存储量了多个源: 1. 对多个源头进行遍历; 2.进行计数
                {
                    int[] cell = queue.Dequeue();
                    int x = cell[0];
                    int y = cell[1];

                    if (x - 1 >= 0 && grid[x - 1, y] == 1)
                    {
                        grid[x - 1, y] = 2;
                        count--;
                        queue.Enqueue(new int[] { x - 1, y });
                    }
                    if (x + 1 < m && grid[x + 1, y] == 1)
                    {
                        grid[x + 1, y] = 2;
                        count--;
                        queue.Enqueue(new int[] { x + 1, y });
                    }
                    if (y - 1 >= 0 && grid[x, y - 1] == 1)
                    {
                        grid[x, y - 1] = 2;
                        count--;
                        queue.Enqueue(new int[] { x, y - 1 });
                    }
                    if (y + 1 < qcount && grid[x, y + 1] == 1)
                    {
                        grid[x, y + 1] = 2;
                        count--;
                        queue.Enqueue(new int[] { x, y + 1 });
                    }
                }
            }

            if (count > 0)
            {
                return -1;
            }
            else
            {
                return round;
            }

        }
        #endregion

        #region day10
        // 21. 合并两个有序链表  1. nextp;
        public ListNode mergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null || list2 == null)
                return list1 != null ? list1 : list2;

            ListNode head = list1.val < list2.val ? list1 : list2;
            ListNode cur1 = head == list1 ? list1 : list2;
            ListNode cur2 = head == list1 ? list2 : list1;
            ListNode pre = null;
            ListNode next = null;
            while (cur1 != null && cur2 != null)
            {
                if (cur1.val <= cur2.val)
                {
                    pre = cur1;
                    cur1 = cur1.next;
                }
                else
                {
                    next = cur2.next;
                    pre.next = cur2;
                    cur2.next = cur1;
                    pre = cur2;
                    cur2 = next;
                }
            }
            pre.next = cur1 == null ? cur2 : cur1;
            return head;
        }

        public ListNode mergeTwoListsRec(ListNode l1, ListNode l2)
        {
            if (l1 == null || l2 == null)
                return l1 != null ? l1 : l2;

            if (l1.val < l2.val)
            {
                l1.next = mergeTwoListsRec(l1.next, l2);
                return l1;
            }
            else
            {
                l2.next = mergeTwoListsRec(l1, l2.next);
                return l2;
            }
        }
        //  206. 反转链表 1. next指针的方式
        public ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                ListNode next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            return prev;
        }
        // 2. 递归方式
        public static ListNode ReverseList1(ListNode head)
        {
            if (head == null || head.next == null) //  最后一个节点进行反转
            {
                return head;
            }
            ListNode t = ReverseList1(head.next); //head.next 是倒数第二个节点
            head.next.next = head; // 1->2->3->4<-5
            head.next = null; //  1->2->3->4<-5
            return t;         //           |
                              //           v
                              //          null
        }
        // 3. 双指针
        public static ListNode ReverseList2(ListNode head)
        {
            if (head == null)
                return null;
            ListNode cur = head;
            while (head.next != null)
            {
                ListNode t = head.next.next;
                head.next.next = cur;
                cur = head.next;
                head.next = t;
            }
            return cur;
        }

        #endregion

        #region day11
        // 77. 组合
        public static IList<IList<int>> Combine(int n, int k)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (k <= 0 || n < k)
                return res;

            IList<int> path = new List<int>();
            dfs_Combile(n, k, 1, path, res);
            return res;
        }

        private static void dfs_Combile(int n, int k, int begin, IList<int> path, IList<IList<int>> res)
        {
            if (path.Count == k)
            {
                res.Add(new List<int>(path));
                return;
            }

            for (int i = begin; i <= n - (k - path.Count) + 1; i++)
            {
                path.Add(i);
                dfs_Combile(n, k, i + 1, path, res);
                path.RemoveAt(path.Count - 1);
            }
        }

        // 46. 全排列
        public static IList<IList<int>> Permute(int[] nums)
        {
            int len = nums.Length;
            IList<IList<int>> res = new List<IList<int>>();

            if (len == 0)
                return res;

            bool[] used = new bool[len];
            List<int> path = new List<int>();
            dfs_permute(nums, len, 0, path, used, res);
            return res;

        }

        private static void dfs_permute(int[] nums, int len, int depth, List<int> path, bool[] used, IList<IList<int>> res)
        {
            if (depth == len)
            {
                res.Add(new List<int>(path));
                return;
            }

            for (int i = 0; i < len; i++)
            {
                if (!used[i])
                {
                    path.Add(nums[i]);
                    used[i] = true;

                    dfs_permute(nums, len, depth + 1, path, used, res);
                    used[i] = false;
                    path.RemoveAt(path.Count - 1);
                }


            }
        }

        // 784. 字母大小写全排列
        public IList<string> LetterCasePermutation(string s)
        {
            StringBuilder path = new StringBuilder(s);
            IList<string> ans = new List<string>();
            dfs_permutation(s, 0, ans, path);

            return ans;
        }

        private void dfs_permutation(string s, int pos, IList<string> ans, StringBuilder path)
        {
            if (pos == s.Length)
            {
                ans.Add(path.ToString());
                return;
            }


            if (char.IsDigit(s[pos]))
            {
                path.Insert(pos, s[pos]);
                dfs_permutation(s, pos + 1, ans, path);
            }
            else if (char.IsLower(s[pos]))
            {
                path.Insert(pos, s[pos]);
                dfs_permutation(s, pos + 1, ans, path);
                path.Insert(pos, char.IsUpper(s[pos]));
                dfs_permutation(s, pos + 1, ans, path);
                dfs_permutation(s, pos + 1, ans, path);
            }
            else
            {
                path.Insert(pos, s[pos]);
                dfs_permutation(s, pos + 1, ans, path);
                path.Insert(pos, char.IsLower(s[pos]));
                dfs_permutation(s, pos + 1, ans, path);
            }
        }
        #endregion

        #region day12
        //  70. 爬楼梯
        //  使用递归的方式解决问题：超时
        public static int ClimbStairs(int n)
        {
            if (n < 2)
                return 1;
            var c1 = ClimbStairs(n - 1);
            var c2 = ClimbStairs(n - 2);
            return c1 + c2;
        }
        //  使用滚动数组的方式解决问题
        public static int ClimbStairs1(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
            return dp[n];
        }

        // 198. 打家劫舍
        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            int length = nums.Length;
            if (length == 1)
                return nums[0];

            int[] dp = new int[length];
            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < length; i++)
            {
                dp[i] = Math.Max(dp[i - 2] + nums[i], dp[i - 1]);
            }
            return dp[length - 1];
        }

        public int Rob1(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int length = nums.Length;
            if (length == 1)
                return nums[0];

            int first = nums[0];
            int second = Math.Max(nums[0], nums[1]);

            for (int i = 2; i < length; i++)
            {
                int temp = second;
                second = Math.Max(first + nums[i], second);
                first = temp;
            }

            return second;
        }

        // 120. 三角形最小路径和

        public int MinimumTotal(IList<IList<int>> triangle)
        {
            throw new NotImplementedException();
        }
        #endregion

        static void Main(string[] args)
        {
            #region day1
            int[] t1 = { -1, 0, 3, 5, 9, 12 };
            WriteLine(Search(t1, 9));
            WriteLine(Search1(t1, 9));
            FirstBadVersion(5);
            FirstBadVersion1(5);
            int[] t2 = { 1, 3, 5, 6 };
            int[] t3 = { 1, 3, 5, 6 };
            int[] t4 = { 1, 3, 5, 6 };
            int[] t5 = { 1, 3, 5, 6 };
            int[] t6 = { 1 };
            WriteLine(searchInsert(t2, 5));
            WriteLine(searchInsert(t3, 2));
            WriteLine(searchInsert(t4, 7));
            WriteLine(searchInsert(t5, 0));
            WriteLine(searchInsert(t6, 0));
            #endregion

            #region day2
            //day2
            int[] t7 = { -4, -1, 0, 3, 10 };
            WriteLine(string.Join(',', SortedSquares(t7)));
            int[] t8 = { 0, 2 };//{ -5, -3, -2, -1 };//{ 1 };//{ -4, -1, 0, 3, 10 };
            WriteLine(string.Join(',', SortedSquares1(t8)));
            int[] t9 = { -4, -1, 0, 3, 10 };//{ 0, 2 };//{ -5, -3, -2, -1 };//{ 1 };//{ -4, -1, 0, 3, 10 };
            WriteLine(string.Join(',', SortedSquares2(t9)));
            int[] t10 = { -4, -1, 0, 3, 10 };//{ 0, 2 };//{ -5, -3, -2, -1 };//{ 1 };//{ -4, -1, 0, 3, 10 };
            WriteLine(string.Join(',', SortedSquares3(t10)));

            int[] t11 = { 1, 2, 3, 4, 5, 6, 7 };// { -1, -100, 3, 99 }; //{ 1, 2, 3, 4, 5, 6, 7 };
            WriteLine(string.Join(',', Rotate(t11, 3)));
            int[] t12 = { 1, 2, 3, 4, 5, 6, 7 };
            WriteLine(string.Join(',', Rotate1(t12, 3)));

            int[] t13 = { 1, 2, 3, 4, 5, 6, 7 };
            WriteLine(string.Join(',', Rotate2(t13, 3)));
            #endregion

            #region day3
            int[] t14 = { 0, 1, 0, 3, 12 };
            WriteLine(string.Join(',', MoveZeroes(t14)));

            int[] t15 = { 2, 7, 11, 15 };
            int[] t16 = { 2, 3, 4 };
            WriteLine(string.Join(',', TwoSum1(t16, 7)));
            #endregion

            #region day4
            char[] t17 = new char[] { 'h', 'e', 'l', 'l', 'o' };
            WriteLine(string.Join(',', ReverseString(t17)));

            string t18 = "Let's take LeetCode contest";
            WriteLine(string.Join(',', ReverseWords(t18)));
            WriteLine("Hello World!");
            #endregion

            #region day5
            var note4 = CommonHelper.BuildLinkNode(new int[] { 1, 2, 3, 4 });
            MiddleNode1(note4);
            var note3 = CommonHelper.BuildLinkNode(new int[] { 1, 2, 3 });
            MiddleNode1(note3);
            var note4v = CommonHelper.BuildLinkNode(new int[] { 1, 2, 3, 4 });
            RemoveNthFromEn2(note4v, 2);
            #endregion

            #region day6
            string t19 = "abcabcbb";
            LengthOfLongestSubstring(t19);
            string t20 = "pwwkew";
            LengthOfLongestSubstring1(t20);

            var s2 = "eidbaooo";
            var s1 = "ab";
            CheckInclusion(s1, s2);

            var s3 = "ab";
            var s4 = "eidboaoo";
            CheckInclusion(s3, s4);
            //  CheckInclusion1(s3, s4);
            #endregion

            #region day7
            // 锯齿型数组也被称为数组中的数组。定义锯齿型数组的语法形式如下。
            // 数据类型[][]  数组名 = new 数据类型[数组长度][];
            // 数组名[0] = new 数据类型[数组长度];
            // http://c.biancheng.net/view/2849.html
            int[][] image1 = new int[3][]; // { { 1, 1, 1 }, { 1, 1, 0 }, { 1, 0, 1 } };
            image1[0] = new int[] { 1, 1, 1 };
            image1[1] = new int[] { 1, 1, 0 };
            image1[2] = new int[] { 1, 0, 1 };

            int[,] image2 = new int[3, 3]{
                { 1, 1, 1 },
                { 1, 1, 0 },
                { 1, 0, 1 } };
            floodFillbybfs(image2, 1, 1, 2);

            int[,] image3 = new int[3, 3] {
                { 1, 1, 1 },
                { 1, 1, 0 },
                { 1, 0, 1 } };
            floodFill_dfs(image3, 1, 1, 2);

            int[,] islands ={
                {0,0,1,0,0,0,0,1,0,0,0,0,0},
                {0,0,0,0,0,0,0,1,1,1,0,0,0},
                {0,1,1,0,1,0,0,0,0,0,0,0,0},
                {0,1,0,0,1,1,0,0,1,0,1,0,0},
                {0,1,0,0,1,1,0,0,1,1,1,0,0},
                {0,0,0,0,0,0,0,0,0,0,1,0,0},
                {0,0,0,0,0,0,0,1,1,1,0,0,0},
                {0,0,0,0,0,0,0,1,1,0,0,0,0}
                };

            maxAreaOfIsland_dfs(islands);

            int[,] t27 = new int[,]{
                { 1,1,0,0,0},
                { 1,1,0,0,0},
                { 0,0,1,0,0},
                { 0,0,0,1,1}
                };
            maxAreaOfIsland_dfs(t27);

            int[,] t28 = new int[,]{
                { 1,1,0,0,0},
                { 1,1,0,0,0},
                { 0,0,1,0,0},
                { 0,0,0,1,1}
                };
            maxAreaOfIsland_bfs(islands);
            maxAreaOfIsland_bfs(t28);
            #endregion

            #region day8
            int[] t29 = { 1, 2, 3, 4, 5, 6, 7 };
            var node1 = CommonHelper.BuildNode(t29);
            Connect_bfs(node1); //  bsf

            int[] t30 = { 1, 2, 3, 4, 5, 6, 7 };
            var node2 = CommonHelper.BuildNode(t30);
            connectNextPointer(node2); // next指针
            #endregion

            #region day9
            int[,] mat1 = {
                { 0, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 0 }
                };
            UpdateMatrix_bfs(mat1); // 干一杯 bfs


            int[,] mat2 = {
                { 0, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 0 }
                };
            UpdateMatrix_dfs(mat2); // 在干一杯 dfs 旋转顺序与 dx的设置有关系

            int[,] grid = { { 2, 1, 1 }, { 1, 1, 0 }, { 0, 1, 1 } };
            orangesRotting(grid);
            #endregion

            #region day10
            ListNode d10node5 = new ListNode(5);
            ListNode d10node4 = new ListNode(4, d10node5);
            ListNode d10node3 = new ListNode(3, d10node4);
            ListNode d10node2 = new ListNode(2, d10node3);
            ListNode d10node1 = new ListNode(1, d10node2);
            ReverseList1(d10node1);
            #endregion

            #region day11
            Combine(4, 2);

            int[] nums = { 1, 2, 3 };
            Permute(nums);
            #endregion

            WriteLine(ClimbStairs(25));

        }
    }
}

