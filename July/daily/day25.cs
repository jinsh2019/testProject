using CDS;
using System.Text;

namespace July.daily;

public class day25
{
    int level = 0;
    LinkedList<int> track = new LinkedList<int>();
    IList<IList<int>> res = new List<IList<int>>();
    bool[] used;
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null)
            return null;

        ListNode a = head;
        ListNode b = head;
        for (int i = 0; i < k; i++)
        {
            if (b == null)
                return head;
            b = b.next;
        }

        ListNode newHead = reverse(a, b);
        a.next = ReverseKGroup(b, k);
        return newHead;
    }
    /// <summary>
    /// [a,b)
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    ListNode reverse(ListNode a, ListNode b)
    {
        ListNode pre, cur, nxt;
        pre = null;
        cur = a;
        nxt = a;
        while (cur != b)
        {
            nxt = cur.next;
            cur.next = pre;

            pre = cur;
            cur = nxt;
        }
        return pre;
    }



    #region 排列
    public IList<IList<int>> Permutation(int[] nums)
    {
        #region log params
        Console.Write("Permutation:nums:");
        nums.ToList().ForEach(x => Console.Write(x + ","));
        Console.WriteLine();
        #endregion
        backtrack(nums);
        #region log res
        res.ToList().ForEach(x =>
            {
                x.ToList().ForEach(x => Console.Write(x + ","));
                Console.WriteLine();
            });
        #endregion
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
            if (track.Contains(nums[i]))  // 通过Contains避免重复选择
                continue;

            track.AddLast(nums[i]);     // track
            backtrack(nums);            // 通过recursion对下一个数字进行选择
            track.RemoveLast();     // path
        }

        level--;
    }
    #endregion

    #region 子集，组合
    // 子集
    public IList<IList<int>> Subsets(int[] nums)
    {
        #region logs params
        Console.Write("Subsets:nums:");
        nums.ToList().ForEach(x => Console.Write(x + ","));
        #endregion
        backtrack(nums, 0);
        #region logs result
        res.ToList().ForEach(x =>
            {
                x.ToList().ForEach(x => Console.Write(x + ","));
                Console.WriteLine();
            });
        #endregion
        return res;
    }

    private void backtrack(int[] nums, int s)
    {
        res.Add(new List<int>(track)); // 添加所有track

        // 主循环i控制主干数据的选择(1,2,3)
        // s 控制主干的子数据的选择（2,3）
        for (int i = s; i < nums.Length; i++) //通过s控制选择； (1),(1,2),(1,2,3),(1,3),(2),(2,3)
        {
            track.AddLast(nums[i]); // 
            backtrack(nums, i + 1);
            track.RemoveLast();
        }
    }

    // 组合

    public IList<IList<int>> Combine(int n, int k)
    {
        #region logs params
        Console.WriteLine("Combine:n:" + n + ":k:" + k);
        #endregion
        backtrack(1, n, k);

        #region logs result
        res.ToList().ForEach(x =>
        {
            x.ToList().ForEach(x => Console.Write(x + ","));
            Console.WriteLine();
        });
        #endregion
        return res;
    }

    private void backtrack(int s, int n, int k)
    {
        if (k == track.Count)
        {
            res.Add(new List<int>(track));
            return;
        }
        // 主循环i控制主干数据的选择(1,2,3)
        // s 控制主干的子数据的选择（2,3）
        for (int i = s; i <= n; i++)
        {
            track.AddLast(i);
            backtrack(i + 1, n, k);
            track.RemoveLast();
        }
    }
    #endregion

    // 组合2
    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        #region Log: Params
        Console.Write("组合2:candidates:");
        candidates.ToList().ForEach(x => Console.Write(x + ","));
        Console.WriteLine("target:" + target);
        #endregion
        Array.Sort(candidates);
        backtrack(0, candidates, target);
        #region logs result
        res.ToList().ForEach(x =>
        {
            x.ToList().ForEach(x => Console.Write(x + ","));
            Console.WriteLine();
        });
        #endregion
        return res;
    }
    void backtrack(int s, int[] nums, int target)
    {
        // base Case
        if (target < 0)
            return;

        if (target == 0)
        {
            res.Add(new List<int>(track));
            return;
        }

        for (int i = s; i < nums.Length; i++)
        {
            if (i > s && nums[i] == nums[i - 1]) continue;
            track.AddLast(nums[i]);
            target -= nums[i];
            backtrack(i + 1, nums, target);
            target += nums[i];
            track.RemoveLast();
        }
    }

    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        Array.Sort(nums);
        used = new bool[nums.Length];
        backtrackUnique(nums);
        return res;
    }
    void backtrackUnique(int[] nums)
    {
        if (track.Count == nums.Length)
        {
            res.Add(new List<int>(track));
            return;
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (used[i])
                continue;
            if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1])
                continue;

            track.AddLast(nums[i]);
            used[i] = true;
            backtrack(nums);
            used[i] = false;
            track.RemoveLast();
        }
    }
    // 下一个排列
    public void NextPermutation(int[] nums)
    {
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            for (int j = nums.Length - 1; j >= 0; j--)
            {
                if (nums[i] < nums[j])
                {
                    swap(nums, i, j);
                    Array.Sort(nums, i + 1, nums.Length - i - 1);
                    return;
                }
            }
        }
        Array.Sort(nums);
    }

    private void swap(int[] nums, int i, int j)
    {
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }

    // 3. 无重复字符的最长子串
    // input:  s = "abcabcbb"
    // output: 3
    Dictionary<char, int> window = new Dictionary<char, int>();
    public int LengthOfLongestSubstring(string s)
    {
        int l = 0, r = 0;
        int res = 0;
        while (r < s.Length)
        {
            char c = s[r];

            if (window.ContainsKey(c))
                window[c]++;
            else
                window.Add(c, 1);

            r++;
            while (window[c] > 1)
            {
                char d = s[l];
                l++;
                window[d]--;
            }
            res = Math.Max(res, r - l);
        }
        return res;
    }

    // 159. 至多包含两个不同字符的最长子串
    public int LengthOfLongestSubstringTwoDistinct(string s)
    {
        int l = 0, r = 0;
        int res = 0;
        while (r < s.Length)
        {
            char c = s[r];
            if (window.ContainsKey(s[r]))
                window[c]++;
            else
                window.Add(c, 1);

            r++;
            while (window.Keys.Count > 2)
            {
                char d = s[l];
                l++;
                window[d]--;
                if (window[d] == 0)
                    window.Remove(d);
            }
            res = Math.Max(res, r - l);
        }

        return res;
    }


    public int LengthOfLongestSubstringKDistinct(string s, int k)
    {
        int l = 0, r = 0;
        int res = 0;
        while (r < s.Length)
        {
            char c = s[r];
            if (window.ContainsKey(s[r]))
                window[c]++;
            else
                window.Add(c, 1);

            r++;
            while (window.Keys.Count > k)
            {
                char d = s[l];
                l++;
                window[d]--;
                if (window[d] == 0)
                    window.Remove(d);
            }
            res = Math.Max(res, r - l);
        }

        return res;
    }

    // 53. 最大子数组和
    // 输入：nums = [-2,1,-3,4,-1,2,1,-5,4]
    // 输出：6
    public int MaxSubArray(int[] nums)
    {
        int n = nums.Length;
        // dp[i] 包括i位置，最大子数组和
        int[] dp = new int[n];
        // 初始化第一个值
        dp[0] = nums[0];
        // 状态转移方程 dp[i] =  Max(dp[i-1]+ nums[i],nums[i]);
        for (int i = 1; i < n; i++)
        {
            dp[i] = Math.Max(dp[i - 1] + nums[i], nums[i]);
        }
        int ans = int.MinValue;
        foreach (int rs in dp)
        {
            ans = Math.Max(ans, rs);
        }
        return ans;
    }

    // 1,2,3
    // 4,5,6
    // 7,8,9
    public void Rotate(int[][] matrix)
    {
        if (matrix == null)
            return;

        int n = matrix.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int temp = matrix[i][j];
                matrix[i][j] = matrix[j][i];
                matrix[j][i] = temp;
            }
        }
        foreach (int[] row in matrix)
            reverse(row);
    }

    private void reverse(int[] row)
    {
        int i = 0, j = row.Length - 1;
        while (i < j)
        {
            int temp = row[i];
            row[i] = row[j];
            row[j] = temp;
            i++;
            j--;
        }
    }

    // 103. 二叉树的锯齿形层序遍历
    // 输入：root = [3,9,20,null,null,15,7]
    // 输出：[[3],[20,9],[15,7]]
    public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        if (root == null)
            return res;

        Queue<TreeNode> q = new Queue<TreeNode>();
        q.Enqueue(root);

        bool flag = true;
        while (q.Count != 0)
        {
            int sz = q.Count;

            LinkedList<int> level = new LinkedList<int>();
            for (int i = 0; i < sz; i++)
            {
                TreeNode cur = q.Dequeue();

                if (flag)
                    level.AddLast(cur.val);
                else
                    level.AddFirst(cur.val);

                if (cur.left != null)
                    q.Enqueue(cur.left);
                if (cur.right != null)
                    q.Enqueue(cur.right);
            }
            flag = !flag;
            res.Add(new List<int>(level));
        }

        return res;
    }

    // 5. 最长回文子串
    //输入：s = "babad"
    //输出："bab"
    public string LongestPalindrome(string s)
    {
        string res = "";
        for (int i = 0; i < s.Length; i++)
        {
            string s1 = Palindrome(s, i, i);
            string s2 = Palindrome(s, i, i + 1);
            res = res.Length > s1.Length ? res : s1;
            res = res.Length > s2.Length ? res : s2;
        }
        return res;
    }
    // (l,r) 开区间
    private string Palindrome(string s, int l, int r)
    {
        while (l >= 0 && r < s.Length && s[r] == s[l])
        { r++; l--; }
        return s.Substring(l + 1, r - l - 1);
    }

    // "the sky is blue"
    public string ReverseWords(string s)
    {
        string[] arr = s.Split(" ");

        Stack<string> stack = new Stack<string>();
        for (int i = 0; i < arr.Length; i++)
        {
            stack.Push(arr[i]);
        }

        StringBuilder sb = new StringBuilder();
        while (stack.Count != 0)
        {
            string word = stack.Pop();
            if (word == "")
                continue;
            sb.Append(word).Append(" ");
        }

        return sb.ToString().Trim();
    }
    // 33. 搜索旋转排序数组
    //输入：nums = [4,5,6,7,0,1,2], target = 6
    //输出：4

    public int Search(int[] nums, int target)
    {
        int n = nums.Length;
        if (n == 0)
            return -1;
        if (n == 1)
            return nums[0] == target ? 0 : -1;

        int left = 0, right = nums.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target) return mid;

            if (nums[0] <= nums[mid]) // 左边部有序
            {
                if (nums[0] <= target && target < nums[mid])     // [0..t..mid)
                    right = mid - 1;
                else
                    left = mid + 1;
            }
            else // 右半部有序
            {
                if (nums[mid] < target && target <= nums[nums.Length - 1]) // (mid..t..end]
                    left = mid + 1;
                else
                    right = mid - 1;
            }
        }
        return -1;
    }


    public int[] SearchRange(int[] nums, int target)
    {
        // 返回左右边界
        return new int[] { left_bound(nums, target), right_bound(nums, target) };
    }
    // 左边界
    int left_bound(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else if (nums[mid] > target)
            {
                right = mid - 1;
            }
            else if (nums[mid] == target)
            {
                right = mid - 1; // 右指针向左收敛，用以找到left的闭区间边界
            }
        }
        if (left >= nums.Length || nums[left] != target)
        {
            return -1;
        }
        return left;
    }
    // 右边界
    int right_bound(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else if (nums[mid] > target)
            {
                right = mid - 1;
            }
            else if (nums[mid] == target)
            {
                left = mid + 1; // 左指针向右收敛，用以找到right的闭区间边界
            }
        }
        if (right < 0 || nums[right] != target)
        {
            return -1;
        }
        return right;
    }


    /*
     [
        "ABCE",
        "SFCS",
        "ADEE"
      ]
    word = "ABCCED", -> returns true,
    word = "SEE", -> returns true,
    word = "ABCB", -> returns false.
     */
    string targetWord;
    public bool searchWord(string[] arr, string word)
    {
        // init
        char[][] matrix = new char[arr.Length][];
        List<char> listchar = new List<char>();
        for (int k = 0; k < arr.Length; k++)
        {
            for (int p = 0; p < arr[k].Length; p++)
            {
                listchar.Add(arr[k][p]);
            }
            matrix[k] = listchar.ToArray();
            listchar.Clear();
        }

        // main logic
        int m = matrix.Length; // row
        int n = matrix[0].Length; // col
        targetWord = word;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (dfs(matrix, i, j, 0))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool dfs(char[][] matrix, int row, int col, int s)
    {
        // base Case
        if (row < 0 || row > matrix.Length || col < 0 || col > matrix[0].Length)
            return false;

        if (s == targetWord.Length - 1){
            return true;
        }

        if (matrix[row][col] == targetWord[s]){
            return  dfs(matrix, row + 1, col, s + 1) ||
                    dfs(matrix, row - 1, col, s + 1) ||
                    dfs(matrix, row, col + 1, s + 1) ||
                    dfs(matrix, row, col - 1, s + 1);
        }
        return false;
    }
}
