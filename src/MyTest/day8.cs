using CDS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyTest
{
    internal class day8
    {
        // 21. 合并两个有序链表
        ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            // 虚拟头结点
            ListNode dummy = new ListNode(-1);
            ListNode p = dummy;
            ListNode p1 = l1, p2 = l2;
            while (p1 != null && p2 != null)
            {
                // 比较p1 和 p2 两个指针
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
                //  p 指针不断前进
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
        // 23. 合并K个升序链表
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists.Length == 0) return null;
            // 虚拟头结点
            ListNode dummy = new ListNode(-1);
            ListNode p = dummy;
            // 优先级队列，堆最小
            PriorityQueue<ListNode, int> pq = new PriorityQueue<ListNode, int>();

            // 将k个链表的头结点加入最小堆
            foreach (ListNode head in lists)
            {
                if (head != null)
                    pq.Enqueue(head, head.val);
            }

            while (pq.Count > 0)
            {
                // 获取最小节点，接到结果链表中
                ListNode node = pq.Dequeue();
                p.next = node;
                if (node.next != null)
                    pq.Enqueue(node.next, node.next.val);

                // p 指针不断前进
                p = p.next;
            }
            return dummy.next;
        }

        // 19. 删除链表的倒数第 N 个结点
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            // 虚拟头结点
            ListNode dummy = new ListNode(-1);
            dummy.next = head;
            // 删除倒数第n个，要先找到倒数第n+1个节点
            ListNode x = findKFromEnd(head, n + 1);
            // 删除倒数第n个节点
            x.next = x.next.next;
            return dummy.next;
        }

        private ListNode findKFromEnd(ListNode head, int k)
        {
            ListNode p1 = head;
            // p1 先走k步
            for (int i = 0; i < k; i++)
            {
                p1 = p1.next;
            }
            ListNode p2 = head;
            // p1 和 p2 同时走 n-k 步
            while (p1 != null)
            {
                p2 = p2.next;
                p1 = p1.next;
            }
            // p2 现在指向第n-k个节点
            return p2;
        }
        // 876. 链表的中间结点
        public ListNode MiddleNode(ListNode head)
        {
            // 快慢指针初始化指向 head
            ListNode slow = head, fast = head;
            // 快指针走到末尾时停止
            while (fast != null && fast.next != null)
            {
                // 慢指针走一步，快指针走两步
                slow = slow.next;
                fast = fast.next.next;
            }
            // 慢指针指向中点
            return slow;
        }
        //  141. 环形链表
        public bool HasCycle(ListNode head)
        {
            ListNode slow = head, fast = head;
            // 快指针走到末尾时停止
            while (fast != null && fast.next != null)
            {
                // 慢指针走一步，快指针走两步
                slow = slow.next;
                fast = fast.next.next;
                // 快慢指针相遇， 说明含有环
                if (slow == fast)
                    return true;
            }
            // 不包含环
            return false;
        }

        // 142. 环形链表 II
        public ListNode DetectCycle(ListNode head)
        {
            ListNode fast, slow;
            fast = slow = head;
            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
                if (fast == slow) break;
            }
            if (fast == null || fast.next == null)
            {
                // fast 遇到空指针说明没有环
                return null;
            }

            // 重新指向头结点
            slow = head;
            // 快慢指针同步前进，相交点就是环起点
            while (slow != fast)
            {
                fast = fast.next;
                slow = slow.next;
            }
            return slow;
        }

        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            int lenA = 0, lenB = 0;
            // 计算两条链表的长度
            ListNode p1 = headA;
            ListNode p2 = headB;
            for (; p1 != null; p1 = p1.next)
            {
                lenA++;
            }
            for (; p2 != null; p2 = p2.next)
            {
                lenB++;
            }
            // 让 p1 和 p2 到达尾部的距离相同
            p1 = headA;
            p2 = headB;
            if (lenA > lenB)
            {
                for (int i = 0; i < lenA - lenB; i++)
                {
                    p1 = p1.next;
                }
            }
            else
            {
                for (int i = 0; i < lenB - lenA; i++)
                {
                    p2 = p2.next;
                }
            }
            // 看两个指针是否会相同，p1 == p2 时有两种情况：
            // 1、要么是两条链表不相交，他俩同时走到尾部空指针
            // 2、要么是两条链表相交，他俩走到两条链表的相交点
            while (p1 != p2)
            {
                p1 = p1.next;
                p2 = p2.next;
            }
            return p1;
        }
    }
}
