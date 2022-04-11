using System;
using System.Collections.Generic;
using static System.Console;

namespace testProject
{
    /// <summary>
    /// 单链表
    /// </summary>
    public class Node
    {
        public int val;
        public Node next;
        public Node(int x)
        {
            val = x;
        }
    }

    /// <summary>
    /// 双向链表
    /// </summary>
    public class DoubleNode
    {
        public int value;
        public DoubleNode last;
        public DoubleNode next;
        public DoubleNode(int data)
        {
            this.value = data;
        }
    }
    /// <summary>
    /// 简述
    ///     使用双指针移动的方式对链表进行翻转
    ///    node pre,next; cur
    ///    while(cur!=null) 
    ///    next = cur.next
    ///    cur.next = pre (major logic)
    ///    pre = cur
    ///    cur = next
    /// </summary>
    class Program
    {
        /// <summary>
        /// 翻转单向链表
        /// </summary>
        /// <param name="cur"></param>
        /// <returns></returns>
        public static Node ReverseList(Node cur)
        {
            Node next;
            Node pre = null;
            while (cur != null)
            {
                next = cur.next; // 初始化next指针

                cur.next = pre; // 把当前的next指向pre

                pre = cur; // 把pre  指向当前
                cur = next; // 把pre  指向当前
            }
            return pre;
        }
        /// <summary>
        /// 翻转双向链表
        /// </summary>
        /// <param name="cur"></param>
        /// <returns></returns>
        public static DoubleNode ReverseList(DoubleNode cur)
        {
            DoubleNode next = null;
            DoubleNode pre = null;
            while (cur != null)
            {
                next = cur.next;

                cur.next = pre;
                cur.last = next;

                pre = cur;
                cur = next;
            }
            return pre;
        }
        /// <summary>
        /// 判断是否有环
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static Boolean hasCycle(Node head)
        {
            if (head == null)
                return false;
            Node fast = head;
            Node slow = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if (fast == slow)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 合并两个链表 且满足单调不减原则
        /// </summary>
        /// <param name="pHead1"></param>
        /// <param name="pHead2"></param>
        /// <returns></returns>
        public static Node Merge(Node pHead1, Node pHead2)
        {
            // base case
            if (pHead1 == null)
                return pHead2;
            if (pHead2 == null)
                return pHead1;

            Node rsNode = null;
            Node cur = null;
            while (pHead1 != null || pHead2 != null)
            {
                if (rsNode != null && pHead1 == null)
                { cur.next = pHead2; break; }
                if (rsNode != null && pHead2 == null)
                { cur.next = pHead1; break; }

                if (pHead1.val <= pHead2.val)
                {
                    if (rsNode == null)
                        cur = rsNode = pHead1;
                    else
                    {
                        cur.next = pHead1;
                        cur = cur.next;
                    }
                    pHead1 = pHead1.next;
                }
                else
                {
                    if (rsNode == null)
                        rsNode = pHead2;
                    else
                    {
                        cur.next = pHead2;
                        cur = cur.next;
                    }
                    pHead2 = pHead2.next;
                }

            }
            return rsNode;
        }

        /// <summary>
        /// 链表排序1 时间复杂度低
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static Node sortInList(Node head)
        {
            // write code here
            if (head == null || head.next == null)
                return head;

            // 分支过程
            Node fast = head.next;
            Node slow = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            Node tmp = slow.next;
            slow.next = null;

            // 递归过程
            Node left = sortInList(head);
            Node right = sortInList(tmp);

            // new node
            Node h = new Node(0);
            Node rs = h;

            while (left != null && right != null)
            {
                if (left.val < right.val)
                {
                    h.next = left;
                    left = left.next;
                }
                else
                {
                    h.next = right;
                    right = right.next;
                }
                h = h.next;
            }

            h.next = left != null ? left : right;
            return rs.next;
        }
        /// <summary>
        /// 链表排序1 空间复杂度低
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static Node selectionSort(Node head)
        {
            Node tail = null; // 排序部分尾部
            Node cur = head;  // 未排序部分头部
            Node smallPre = null; // 最小节点的前一个节点
            Node small = null; // 最小节点
            while (cur != null)
            {
                small = cur;
                smallPre = getSmallestPreNode(cur);
                if (smallPre != null)
                {
                    small = smallPre.next;
                    smallPre.next = small.next;
                }

                cur = cur == small ? cur.next : cur;
                if (tail == null)
                {
                    head = small;
                }
                else
                {
                    tail.next = small;
                }
                tail = small;
            }

            return head;
        }

        /// <summary>
        /// 是否是回文链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool isPail(Node head)
        {
            // write code here
            Node fast = head;
            Node slow = head;
            Boolean odd = false;

            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }
            if (fast != null)
            {
                slow = slow.next;
            }

            Node rNode = ReverseList(slow);

            while (rNode != null)
            {
                if (rNode.val != head.val)
                    return false;
                rNode = rNode.next;
                head = head.next;
            }

            return true;
        }

        private static Node getSmallestPreNode(Node head)
        {
            Node smallPre = null;
            Node small = head;
            Node pre = head;
            Node cur = head.next;
            while (cur != null)
            {
                if (cur.val < small.val)
                {
                    smallPre = pre;
                    small = cur;
                }
                pre = cur;
                cur = cur.next;
            }
            return smallPre;
        }

        /// <summary>
        /// 删除重复的元素
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static Node deleteDuplicates(Node head)
        {
            Node ta = head;
            Node tb = head;
            Node cur = head;
            // write code here
            while (tb != null)
            {
                if (tb.next != null && ta.val == tb.next.val)
                {
                    tb = tb.next;
                }
                else
                {
                    cur.next = tb.next;
                    cur = ta = tb = tb.next;
                }
            }

            return head;
        }
        /// <summary>
        /// 找到第一个公共位置
        /// </summary>
        /// <param name="pHead1"></param>
        /// <param name="pHead2"></param>
        /// <returns></returns>
        public static Node FindFirstCommonNode(Node pHead1, Node pHead2)
        {
            //  ta 遍历 phead1+phead2， tb遍历 phead2+phead1
            Node ta = pHead1;
            Node tb = pHead2;
            while (ta != tb)
            {
                ta = ta != null ? ta.next : pHead2;
                tb = tb != null ? tb.next : pHead1;
            }
            return ta;
        }

        #region NC3 链表中环的入口结点
        /// <summary>
        /// 时间 O(n)
        /// 空间 O(n)
        /// </summary>
        /// <param name="pHead"></param>
        /// <returns></returns>
        public static Node EntryNodeOfLoop(Node pHead)
        {
            Dictionary<Node, int> dic = new Dictionary<Node, int>();
            while (pHead != null)
            {
                if (!dic.ContainsKey(pHead))
                {
                    dic.TryAdd(pHead, pHead.val);
                    pHead = pHead.next;
                }
                else
                {
                    return pHead;
                }
            }
            return null;
        }
        /// <summary>
        /// 空间O(1)
        /// </summary>
        /// <param name="pHead"></param>
        /// <returns></returns>
        public static Node EntryNodeOfLoopv1(Node pHead)
        {
            Node slow = pHead;
            Node fast = pHead;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if (slow == fast)
                { break; }
            }
            if (fast == null || fast.next == null) return null;
            fast = pHead;
            while (fast != slow)
            {
                slow = slow.next;
                fast = fast.next;
            }
            return slow;

        }
        #endregion
        static void Main(string[] args)
        {
            #region 单链表翻转
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            node1.next = node2;
            node2.next = node3;
            ReverseList(node1);
            #endregion

            #region 双向链表翻转
            DoubleNode dNode1 = new DoubleNode(1);
            DoubleNode dNode2 = new DoubleNode(2);
            DoubleNode dNode3 = new DoubleNode(3);
            dNode1.next = dNode2;
            dNode2.next = dNode3;
            dNode2.last = dNode1;
            dNode3.last = dNode2;
            ReverseList(dNode1);
            #endregion

            #region 是否有环
            Node hasCycleNode1 = new Node(1);
            Node hasCycleNode2 = new Node(2);
            Node hasCycleNode3 = new Node(3);
            Node hasCycleNode4 = new Node(4);
            Node hasCycleNode5 = new Node(5);
            hasCycleNode1.next = hasCycleNode2;
            hasCycleNode2.next = hasCycleNode3;
            hasCycleNode3.next = hasCycleNode4;
            hasCycleNode4.next = hasCycleNode5;
            hasCycleNode5.next = hasCycleNode2;
            hasCycle(hasCycleNode1);
            #endregion

            #region 合并两个链表
            Node mergeNode1 = new Node(1);
            Node mergeNode3 = new Node(3);
            Node mergeNode5 = new Node(5);
            mergeNode1.next = mergeNode3;
            mergeNode3.next = mergeNode5;

            Node mergeNode2 = new Node(1);
            Node mergeNode4 = new Node(3);
            Node mergeNode6 = new Node(5);
            //mergeNode2.next = mergeNode4;
            //mergeNode4.next = mergeNode6;

            Merge(mergeNode1, null);
            #endregion

            #region 链表排序  {1,2,5,4,3}
            Node sortNode1 = new Node(1);
            Node sortNode2 = new Node(2);
            Node sortNode3 = new Node(3);
            Node sortNode4 = new Node(4);
            Node sortNode5 = new Node(5);
            sortNode1.next = sortNode2;
            sortNode2.next = sortNode5;
            sortNode5.next = sortNode4;
            sortNode4.next = sortNode3;

            // sortInList(sortNode1);

            selectionSort(sortNode1);
            #endregion

            #region 回文结构
            Node pailNode1 = new Node(1);
            Node pailNode2 = new Node(2);
            Node pailNode3 = new Node(3);

            Node pailNode4 = new Node(2);
            Node pailNode5 = new Node(1);
            Node pailNode6 = new Node(3);
            // odd
            pailNode1.next = pailNode2;
            pailNode2.next = pailNode3;
            pailNode3.next = pailNode4;
            pailNode4.next = pailNode5;
            //// even
            //pailNode1.next = pailNode2;
            //pailNode2.next = pailNode3;
            //pailNode3.next = pailNode6;
            //pailNode6.next = pailNode4;
            //pailNode4.next = pailNode5;
            isPail(pailNode1);
            #endregion

            Node Dupli1 = new Node(1);
            Node Dupli2 = new Node(1);
            Node Dupli3 = new Node(3);
            Node Dupli4 = new Node(4);
            Node Dupli5 = new Node(4);
            Node Dupli6 = new Node(6);
            Dupli1.next = Dupli2;
            Dupli2.next = Dupli3;
            Dupli3.next = Dupli4;
            Dupli4.next = Dupli5;
            Dupli5.next = Dupli6;
            deleteDuplicates(Dupli1);


            Node _entryNodeofLoop1 = new Node(1);
            Node _entryNodeofLoop2 = new Node(2);
            Node _entryNodeofLoop3 = new Node(3);
            Node _entryNodeofLoop4 = new Node(4);
            Node _entryNodeofLoop5 = new Node(5);
            _entryNodeofLoop1.next = _entryNodeofLoop2;
            _entryNodeofLoop2.next = _entryNodeofLoop3;
            _entryNodeofLoop3.next = _entryNodeofLoop4;
            //_entryNodeofLoop4.next = _entryNodeofLoop5;
            //_entryNodeofLoop5.next = _entryNodeofLoop3;

            WriteLine(EntryNodeOfLoop(_entryNodeofLoop1)?.val);
            WriteLine(EntryNodeOfLoopv1(_entryNodeofLoop1).val);
        }
    }
}
