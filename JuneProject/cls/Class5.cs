using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuneProject.cls
{
    internal class Class5
    {
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            if (m == 1)
                return reverseN(head, n); // 翻转以head为头直到n个元素为止

            // 递归到base Case 终止，做链表翻转
            head.next = ReverseBetween(head.next, m - 1, n - 1);
            return head;
        }
        // 后驱节点
        ListNode sucessor = null;
        // 反转以 head 为起点的 n 个节点，返回新的头节点
        ListNode reverseN(ListNode head, int n)
        {
            if (n == 1)
            {
                // 记录第 n + 1 个节点
                sucessor = head.next;
                return head;
            }
            // 递归到base Case为止， 返回需要翻转的最后一个节点作为头结点
            ListNode last = reverseN(head.next, n - 1);
            head.next.next = head; // 本来指向5，现在指向head
            head.next = sucessor; // head 指向5
            return last;// 作为头结点
        }

        // 3. 无重复字符的最长子串
        public int LengthOfLongestSubstring(string s)
        {
            Dictionary<char, int> window = new Dictionary<char, int>();

            int left = 0, right = 0;
            int res = 0; // 记录结果
            while (right < s.Length)
            {
                char c = s[right];

                // 增大窗口
                if (window.ContainsKey(c))
                    window[c]++;
                else
                    window.Add(c, 1);
                right++;

                // 缩小窗口
                while (window[c] > 1)
                {
                    char d = s[left];
                    left++;
                    window[d]--;
                }
                res = Math.Max(res, right - left);
            }
            return res;
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dummy = new ListNode(-1);
            dummy.next = head;
            // 欲删除倒数第n个节点，需要找到倒数第n+1个节点
            ListNode x = FindFromEnd(dummy, n + 1);
            x.next = x.next.next;
            return dummy.next;
        }
        ListNode FindFromEnd(ListNode head, int k)
        {
            ListNode p1 = head;
            for (int i = 0; i < k; i++)
            {
                p1 = p1.next;
            }

            ListNode p2 = head;
            while (p1 != null)
            {
                p2 = p2.next;
                p1 = p1.next;
            }
            return p2;
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            // 虚拟头结点
            ListNode dummy = new ListNode(-1), p = dummy;
            ListNode p1 = l1, p2 = l2;

            while (p1 != null && p2 != null)
            {
                // 比较 p1 和 p2 两个指针
                // 将值较小的节点接到p 指针
                if (p1.val > p2.val)
                {
                    p.next = p2;
                    p2 = p2.next;
                }
                else
                {
                    p.next = p1;
                    p1 = p1.next;
                }
                p = p.next;
            }

            if (p1 != null)
            {
                p.next = p1;
            }
            if (p2 != null)
            {
                p.next = p2;
            }

            return dummy.next;
        }

        public bool HasCycle(ListNode head)
        {
            ListNode slow = head, fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if (slow == fast)
                    return true;
            }
            return false;
        }

        // 234. 回文链表
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null) return null;

            ListNode a =head, b = head;
            for (int i = 0; i < k; i++)
            {
                if (b == null) return head;
                b = b.next;
            }

            ListNode newHead = reverse(a, b);
            a.next = ReverseKGroup(b, k); // 递归
            return newHead;
        }

        // 翻转[a,b)
        // 1,2,3
        ListNode reverse(ListNode a, ListNode b)
        {
            ListNode pre = null, cur = a, next = a;
            while (cur != b)
            {
                next = cur.next;
                cur.next = pre;
                pre = cur;
                cur = next;
            }
            return pre;
        }
        // 234. 回文链表
        public bool IsPalindrome(ListNode head)
        {
            ListNode slow, fast;
            slow = fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            // 奇数不为空
            if (fast != null)
            {
                slow = slow.next;
            }

            ListNode left = head;
            ListNode right = reverse(slow);
            while (right != null)
            {
                if (left.val != right.val)
                {
                    return false;
                }
                left = left.next;
                right = right.next;
            }
            return true;
        }

        ListNode reverse(ListNode head)
        {
            ListNode pre = null, cur = head;
            while (cur != null)
            {
                ListNode next = cur.next;
                cur.next = pre; // cur.next point pre eg: pre=null,cur=1 1->2->3->null  => 2->3..1->null
                pre = cur;      // pre point cur eg: pre=1
                cur = next;     // cur point next eg: cur=2
            }
            return pre;
        }
    }
}

