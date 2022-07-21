using CDS;
using July.algorithms.day2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.daily
{
    internal class day2
    {
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
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
                    {
                        level.AddLast(cur.val);
                    }
                    else
                    {
                        level.AddFirst(cur.val);
                    }
                    if (cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if (cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
                flag = !flag;
                res.Add(level.ToList());
            }
            return res;
        }


        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null)
            {
                return res;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int sz = q.Count;

                IList<int> level = new List<int>();
                for (int i = 0; i < sz; i++)
                {
                    TreeNode cur = q.Dequeue();
                    level.Add(cur.val);
                    if (cur.left != null)
                        q.Enqueue(cur.left);
                    if (cur.right != null)
                        q.Enqueue(cur.right);
                }
                res.Add(level);
            }
            return res.Reverse().ToList();
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dummy = new ListNode();
            dummy.next = head;
            ListNode x = findFromEnd(dummy, n + 1);
            x.next = x.next.next;
            return dummy.next;
        }

        ListNode findFromEnd(ListNode head, int k)
        {
            ListNode p1 = head, p2 = head;
            for (int i = 0; i < k; i++)
            {
                p1 = p1.next;
            }

            while (p1 != null)
            {
                p1 = p1.next;
                p2 = p2.next;
            }
            return p2;
        }
        public ListNode RotateRight(ListNode head, int k)
        {
            for (int i = 0; i < k; i++)
            {
                head = RotateOne(head);
            }
            return head;
        }
        public ListNode RotateOne(ListNode head)
        {
            ListNode x = findFromEnd_manual(head, 2);
            ListNode newHead = x.next;
            newHead.next = head;
            x.next = null;
            return newHead;
        }
        public ListNode findFromEnd_manual(ListNode head, int k)
        {
            ListNode dummy = new ListNode(-1);
            ListNode p1 = dummy, p2 = dummy;
            dummy.next = head;
            for (int i = 0; i < k; i++)
                p1 = p1.next;

            while (p1 != null)
            {
                p1 = p1.next;
                p2 = p2.next;
            }
            return p2;
        }


        public ListNode Partition(ListNode head, int x)
        {
            // 占位符
            ListNode dummy1 = new ListNode();
            ListNode dummy2 = new ListNode();
            ListNode p1 = dummy1, p2 = dummy2;
            ListNode p = head;
            while (p != null)
            {
                if (p.val < x)
                {
                    p1.next = p;
                    p1 = p1.next;
                }
                else
                {
                    p2.next = p;
                    p2 = p2.next;
                }
                ListNode temp = p.next;
                p.next = null;
                p = temp;
                //p = p.next;
            }
            p1.next = dummy2.next;
            return dummy1.next;
        }

    }


}
