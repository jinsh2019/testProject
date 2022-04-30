using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class day11
    {
        // 206. 反转链表
        public ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            ListNode last = ReverseList(head.next);
            head.next.next = head; // 1,2,3,4,5
            head.next = null;
            return last;
        }

        // 翻转以 head 为起点的n个节点， 返回新的头结点
        ListNode successor = null;
        public ListNode ReverseN(ListNode head, int n)
        {
            if (n == 1)
            {
                successor = head.next;
                return head;
            }

            ListNode last = ReverseN(head.next, n - 1);

            head.next.next = head; // 1,2,3,4,5,6,7
            head.next = successor; // rs = 6,7,1,2,3,4,5

            return last;
        }
        // 92. 反转链表 II
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            // base case
            if (m == 1)
                return ReverseN(head, n);
            // 前进到反转的起点触发 base case
            head.next = ReverseBetween(head.next, m - 1, n - 1);
            return head;
        }

        // 25. K 个一组翻转链表
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null) return null;
            // 区间[a,b) 包含k个待翻转元素
            ListNode a, b;
            a = b = head;
            for (int i = 0; i < k; i++)
            {
                // 不足k个， 不需要反转， base case
                if (b == null) return head;
                b = b.next;
            }
            // 反转前k个元素
            ListNode newHead = ReverseAB(a, b);
            // 递归反转后序链表并连接起来
            a.next = ReverseKGroup(b, k);

            return newHead;
        }
        // 反转以a 为头节点的链表
        private ListNode Reverse(ListNode a)
        {
            ListNode pre, cur, nxt;
            pre = null; cur = a; nxt = a;
            while (cur != null)
            {
                nxt = cur.next;
                // 逐个节点反转
                cur.next = pre;
                // 更新指针位置
                pre = cur; // 更1
                cur = nxt; // 更2
            }
            // 返回反转后的头结点
            return pre;
        }

        // 反转区间[a,b)的元素， 注意是左闭右开
        ListNode ReverseAB(ListNode a, ListNode b)
        {
            ListNode pre, cur, nxt;
            pre = null; cur = a; nxt = a;
            // while 终止的条件改一下就行了
            while (cur != b)
            {
                nxt = cur.next;
                cur.next = pre;
                pre = cur;
                cur = nxt;
            }

            // 返回反转后的头结点
            return pre;
        }


        // 在s中寻找以s[l]和s[r]为中心的最长回文串
        public string palindrome(string s, int l, int r)
        {
            // 防止索引越界
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                // 向两边展开
                l--; r++;
            }
            // 返回以s[l] 和 s[r] 为中心的最长回文串
            return s.Substring(l + 1, r - l - 1);
        }

        public bool isPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;
            while (left < right)
            {
                if (s[left] != s[right])
                    return false;
                left++; right--;
            }

            return true;
        }
        // 234. 回文链表
        ListNode left;
        bool isPalindrome(ListNode head)
        {
            left = head;
            return traverse(head);
        }

        private bool traverse(ListNode right)
        {
            if (right == null) return true;
            bool last = traverse(right.next);
            // 后序遍历代码
            last = last && (right.val == left.val);
            left = left.next;
            return last;
        }

        // 303. 区域和检索 - 数组不可变
        // class

    }

    // 303. 区域和检索 - 数组不可变
    public class NumArray
    {
        // 前缀和数组
        private int[] preSum;

        // 输入一个数组，构造前缀和
        public NumArray(int[] nums)
        {
            // preSum[0]=0, 便于计算累加和
            preSum = new int[nums.Length + 1];
            for (int i = 1; i <= nums.Length; i++)
            {
                // 计算 nums的累加和
                preSum[i] = preSum[i - 1] + nums[i - 1];
            }
        }

        // 查询闭区间[left,right] 的累加和
        public int SumRange(int left, int right)
        {
            return preSum[right + 1] - preSum[left];
        }
    }

    public class NumMatrix
    {
        // 定义: preSum[i,j] 记录matrix 中子矩阵[0,0,i,j]的元素和
        public int[,] preSum;

        public NumMatrix(int[,] matrix)
        {
            int m = matrix.GetLength(0), n = matrix.GetLength(1);
            if (m == 0 || n == 0) return;

            // 构造前缀和矩阵
            preSum = new int[m + 1, n + 1];
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    // 计算每个矩阵[0,0,i,j]的元素和
                    preSum[i, j] = preSum[i - 1, j] + preSum[i, j - 1] + matrix[i - 1, j - 1] - preSum[i - 1, j - 1];
                }
            }
        }
        // 计算子矩阵[x1,y1,x2,y2]的元素和
        public int sumRegion(int x1, int y1, int x2, int y2)
        {
            // 目标矩阵之和由四个相邻矩阵运算获得
            return preSum[x2 + 1, y2 + 1] - preSum[x1, y2 + 1] - preSum[x2 + 1, y1] + preSum[x1, y1];
        }
    }
}
