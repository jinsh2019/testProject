using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Console;

namespace TreeProject
{
    class Program
    {
        #region 0. 初始化数据
        /// <summary>
        /// 构建一颗树
        /// </summary>
        /// <returns></returns>
        private static Node BuildTree()
        {
            Node head1 = new Node(1);
            Node head2 = new Node(2);
            Node head3 = new Node(3);
            head1.left = head2;
            head1.right = head3;
            Node head4 = new Node(4);
            Node head5 = new Node(5);
            head2.left = head4;
            head2.right = head5;
            Node head6 = new Node(6);
            Node head7 = new Node(7);
            head3.left = head6;
            head3.right = head7;
            return head1;
        }

        private static Node BuildTreeSymmetric()
        {
            Node head1 = new Node(1);
            Node head2 = new Node(2);
            Node head3 = new Node(2);
            head1.left = head2;
            head1.right = head3;
            Node head4 = new Node(4);
            Node head5 = new Node(5);
            head2.left = head4;
            head2.right = head5;
            Node head6 = new Node(5);
            Node head7 = new Node(4);
            head3.left = head6;
            head3.right = head7;
            return head1;
        }
        #endregion

        #region 0.1 递归实现遍历树
        /// <summary>
        /// 先序遍历
        /// </summary>
        /// <param name="head"></param>
        public static void preOrderRecur(Node head)
        {
            if (head == null)
                return;
            Write(head.Value + " ");
            preOrderRecur(head.left);
            preOrderRecur(head.right);
        }
        /// <summary>
        /// 中序遍历
        /// </summary>
        /// <param name="head"></param>
        public static void inOrderRecur(Node head)
        {
            if (head == null)
                return;
            inOrderRecur(head.left);
            WriteLine(head.Value + " ");
            inOrderRecur(head.right);
        }
        /// <summary>
        /// 后续遍历
        /// </summary>
        /// <param name="head"></param>
        public static void postOrderRecur(Node head)
        {
            if (head == null)
                return;
            postOrderRecur(head.left);
            postOrderRecur(head.right);
            Write(head.Value + " ");
        }
        #endregion

        #region 0.2 非递归实现遍历树
        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <param name="head"></param>
        public static void preOrderUnRecur(Node head)
        {
            WriteLine("pre-order: ");
            if (head != null)
            {
                Stack<Node> stack = new Stack<Node>();
                stack.Push(head);
                while (stack.Count != 0)
                {
                    head = stack.Pop();
                    Write(head.Value + " ");
                    if (head.right != null)
                    {
                        stack.Push(head.right);
                    }
                    if (head.left != null)
                    {
                        stack.Push(head.left);
                    }
                }
            }
        }
        /// <summary>
        /// 中序遍历
        /// </summary>
        /// <param name="head"></param>
        public static void inOrderUnRecur(Node head)
        {
            WriteLine("in-order: ");
            if (head != null)
            {
                Stack<Node> stack = new Stack<Node>();
                while (stack.Count != 0 || head != null)
                {
                    if (head != null)
                    {
                        stack.Push(head);
                        head = head.left;
                    }
                    else
                    {
                        head = stack.Pop();
                        Write(head.Value + " ");
                        head = head.right;
                    }
                }
            }
        }

        /// <summary>
        /// 使用两个栈实现的非递归遍历
        /// </summary>
        /// <param name="head"></param>
        public static void postOrderUnRecur1(Node head)
        {
            WriteLine("pos-order 1:  ");
            if (head != null)
            {
                Stack<Node> s1 = new Stack<Node>();
                Stack<Node> s2 = new Stack<Node>();
                s1.Push(head);
                while (s1.Count != 0)
                {
                    head = s1.Pop();
                    s2.Push(head);
                    if (head.left != null)
                    {
                        s1.Push(head.left);
                    }
                    if (head.right != null)
                    {
                        s1.Push(head.right);
                    }
                }
                while (s2.Count != 0)
                {
                    Write(s2.Pop().Value + " ");
                }
            }
        }
        /// <summary>
        /// 使用一个栈实现的非递归遍历
        /// </summary>
        /// <param name="head"></param>
        public static void postOrderUnRecur2(Node head)
        {
            WriteLine("post-order 2: ");
            if (head != null)
            {
                Stack<Node> stack = new Stack<Node>();
                stack.Push(head);
                Node c = null;
                while (stack.Count != 0)
                {
                    c = stack.Peek();
                    if (c.left != null && head != c.left && head != c.right)
                    {
                        stack.Push(c.left);
                    }
                    else if (c.right != null && head != c.right)
                    {
                        stack.Push(c.right);
                    }
                    else
                    {
                        Write(stack.Pop().Value + " ");
                        head = c;
                    }
                }
            }
        }
        #endregion

        #region 2 二叉树的最小深度
        /// <summary>
        ///  二叉树的最小深度
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int inDepth1(Node head)
        {
            if (head == null)
            {
                return 0;
            }

            return process(head, 1);
        }
        /// <summary>
        /// 当前节点来到的节点是cur，cur所在深度是level
        /// 确保遍历到cur子树中的所有叶节点，并将最矮的叶节点高度返回
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int process(Node cur, int level)
        {
            if (cur.left == null && cur.right == null)
                return level;

            int ans = int.MaxValue;
            if (cur.left != null)
            {
                ans = Math.Min(process(cur.left, level + 1), ans);
            }
            if (cur.right != null)
            {
                ans = Math.Min(process(cur.right, level + 1), ans);
            }

            return ans;
        }
        #endregion

        #region 3 打印二叉树
        public static void printTree(Node head)
        {
            WriteLine("Binary Tree: ");
            printInOrder(head, 0, "H", 17);
            WriteLine();
        }

        private static void printInOrder(Node head, int height, string around, int len)
        {
            if (head == null)
                return;

            printInOrder(head.right, height + 1, "v", len);
            string val = around + head.Value + around;
            int lenM = val.Length;
            int lenL = (len - lenM) / 2;
            int lenR = len - lenM - lenL;
            val = getSpace(lenL) + val + getSpace(lenR);
            WriteLine(getSpace(height * len) + val);
            printInOrder(head.left, height + 1, "^", len);
        }

        private static string getSpace(int num)
        {
            string space = " ";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(space);
            }
            return sb.ToString();
        }
        #endregion

        #region 4 序列化 and 反序列化一棵树

        public static string serialByPre(Node head)
        {
            if (head == null)
                return "#!";
            string res = head.Value + "!";
            res += serialByPre(head.left);
            res += serialByPre(head.right);
            return res;
        }

        public static Node deSerialByPreStr(string preStr)
        {
            string[] values = preStr.Split("!");
            Queue<string> queue = new Queue<string>();
            for (int i = 0; i < values.Length; i++)
            {
                queue.Enqueue(values[i]);
            }
            return deSerialByQueue(queue);
        }

        private static Node deSerialByQueue(Queue<string> queue)
        {
            string value = queue.Dequeue();
            if (value.Equals("#"))
            {
                return null;
            }
            Node head = new Node(int.Parse(value));
            head.left = deSerialByQueue(queue);
            head.right = deSerialByQueue(queue);
            return head;
        }
        #endregion

        #region 5 判断t1树是否包含t2树全部的拓扑结构
        /// <summary>
        /// 1. check: 同步检查当前节点及其t1,t2结构是否相同
        /// 2. contains: 检查t1的左/右孩子是否包含t2，若包含则开始 check t1，t2，不包含继续往下找
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static Boolean contains(Node t1, Node t2)
        {
            if (t2 == null)
                return true;
            if (t1 == null)
                return false;
            return check(t1, t2) || contains(t1.left, t2) || contains(t1.right, t2);
        }
        /// <summary>
        /// check 同步检查当前节点及其t1,t2结构是否相同
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static Boolean check(Node t1, Node t2)
        {
            if (t2 == null)
                return true;
            if (t1 == null || t1.Value != t2.Value)
                return false;

            return check(t1.left, t2.left) || check(t1.right, t2.right);
        }
        #endregion

        #region 6 判断t1树中是否有与t2树拓扑结构完全相同的子树
        public static Boolean isSubTree(Node t1, Node t2)
        {
            string t1Str = serialByPre(t1);
            string t2Str = serialByPre(t2);
            return getIndexOf(t1Str, t2Str) != -1;
        }
        // KMP
        private static int getIndexOf(string s, string m)
        {
            if (s == null || m == null || m.Length < 1 || s.Length < m.Length)
                return -1;

            char[] ss = s.ToCharArray();
            char[] ms = m.ToCharArray();
            int si = 0;
            int mi = 0;
            int[] next = getNextArray(ms);
            while (si < ss.Length && mi < ms.Length)
            {
                if (ss[si] == ms[mi])
                {
                    si++;
                    mi++;
                }
                else if (next[mi] == -1)
                {
                    si++;
                }
                else
                {
                    mi = next[mi];
                }
            }
            return mi == ms.Length ? si - mi : -1;
        }

        public static int[] getNextArray(char[] ms)
        {
            if (ms.Length == 1)
            {
                return new int[] { -1 };
            }

            int[] next = new int[ms.Length];
            next[0] = -1;
            next[1] = 0;
            int pos = 2;
            int cn = 0;
            while (pos < next.Length)
            {
                if (ms[pos - 1] == ms[cn])
                {
                    // next[pos++] == ++cn;
                }
                else if (cn > 0)
                {
                    cn = next[cn];

                }
                else
                {
                    next[pos++] = 0;
                }
            }
            return next;
        }
        #endregion

        #region NC13 二叉树的最大深度
        public static int maxDepth(Node root)
        {
            // write code here
            if (root == null)
                return 0;
            int leftHeight = maxDepth(root.left);
            int rightHeight = maxDepth(root.right);
            int height = Math.Max(leftHeight, rightHeight) + 1;
            return height;
        }

        public static int maxDepthByQueue(Node head)
        {
            if (head == null)
                return 0;

            LinkedList<Node> linkedList = new LinkedList<Node>();
            linkedList.AddFirst(head);
            int count = 0;
            while (linkedList.Count != 0)
            {
                int size = linkedList.Count;
                while (size-- > 0)
                {
                    Node cur = linkedList.First.Value;
                    linkedList.RemoveFirst();
                    if (cur.left != null)
                        linkedList.AddLast(cur.left);
                    if (cur.right != null)
                        linkedList.AddLast(cur.right);
                }
                count++;
            }
            return count;
        }
        public static int maxDepthByStack(Node head)
        {
            if (head == null)
                return 0;

            Stack<Node> stack = new Stack<Node>();
            Stack<int> level = new Stack<int>();
            stack.Push(head);
            level.Push(1);
            int max = 0;
            while (stack.Count != 0)
            {
                Node node = stack.Pop();
                int temp = level.Pop();
                max = Math.Max(temp, max);
                if (node.left != null)
                {
                    stack.Push(node.left);
                    level.Push(temp + 1);
                }
                if (node.right != null)
                {
                    stack.Push(node.right);
                    level.Push(temp + 1);
                }
            }
            return max;
        }
        #endregion

        #region 7 判断对称
        /// <summary>
        /// 1.递归判断对称
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool isSymmetric(Node head)
        {
            // write code here
            if (head == null)
                return true;
            return checkSymmetric(head.left, head.right);

        }
        public static bool checkSymmetric(Node left, Node right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;
            if (left.Value == right.Value && checkSymmetric(left.left, right.right) && checkSymmetric(left.right, right.left))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 2 非递归判断对称
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool isSymmetricByQueue(Node head)
        {
            // write code here
            if (head == null)
                return true;
            Queue<Node> _queue = new Queue<Node>();
            _queue.Enqueue(head.left);
            _queue.Enqueue(head.right);

            while (_queue.Count != 0)
            {
                Node left = _queue.Dequeue();
                Node right = _queue.Dequeue();
                if (left == null && right == null)
                    continue;
                if (left == null || right == null)
                    return false;
                if (left.Value != right.Value)
                    return false;

                _queue.Enqueue(left.left);
                _queue.Enqueue(right.right);
                _queue.Enqueue(left.right);
                _queue.Enqueue(right.left);
            }
            return true;

        }
        #endregion

        #region NC11 将升序数组转化为平衡二叉搜索树
        /// <summary>
        /// NC11 将升序数组转化为平衡二叉搜索树
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Node sortedArrayToBST(int[] num)
        {
            if (num == null)
                return null;

            return helper(num, 0, num.Length - 1);
        }
        private static Node helper(int[] num, int left, int right)
        {
            if (left > right)
                return null;

            int mid = left + (right - left + 1) / 2;
            Node lNode = helper(num, left, mid - 1);
            Node rNode = helper(num, mid + 1, right);
            Node node = new Node(num[mid]);
            if (lNode != null) node.left = lNode;
            if (rNode != null) node.right = rNode;
            return node;
        }
        #endregion

        #region NC81 二叉搜索树的第k个结点
        static IList<Node> list = new List<Node>();
        /// <summary>
        /// 二叉搜索树的第k个结点
        /// </summary>
        /// <param name="pRoot"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        static Node KthNode(Node pRoot, int k)
        {
            OrderTraversal(pRoot);
            if (k < 1 || list.Count < k)
                return null;
            return list[k - 1];
        }
        static void OrderTraversal(Node node)
        {
            if (node != null)
            {
                //if (list.Count >= k)
                //    return;
                OrderTraversal(node.left);
                list.Add(node);
                OrderTraversal(node.right);
            }
        }
        #endregion

        #region NC55 最长公共前缀
        /// <summary>
        /// 最长公共前缀
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string longestCommonPrefix(String[] strs)
        {
            //边界条件判断
            if (strs == null || strs.Length == 0)
                return "";
            //默认第一个字符串是他们的公共前缀
            String pre = strs[0];
            int i = 1;
            while (i < strs.Length)
            {
                //不断的截取
                while (strs[i].IndexOf(pre) != 0)
                    pre = pre.Substring(0, pre.Length - 1);
                i++;
            }
            return pre;
        }
        #endregion

        #region 合并二叉树
        /// <summary>
        /// 合并二叉树
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public Node mergeTrees(Node t1, Node t2)
        {
            // write code here
            if (t1 == null) return t2;
            if (t2 == null) return t1;
            t1.Value += t2.Value;
            t1.left = mergeTrees(t1.left, t2.left);
            t1.right = mergeTrees(t1.right, t2.right);
            return t1;
        }
        #endregion


        static void Main(string[] args)
        {
            Node head = BuildTree();
            // preOrderRecur(head);
            // inOrderRecur(head);
            // postOrderRecur(head);
            // preOrderUnRecur(head);
            inOrderUnRecur(head);
            WriteLine();
            postOrderUnRecur1(head);
            WriteLine();
            postOrderUnRecur2(head);
            WriteLine(inDepth1(head));
            WriteLine();
            printTree(head);

            WriteLine();
            string serialStr = serialByPre(head);

            WriteLine(serialStr);
            deSerialByPreStr(serialStr);

            WriteLine();
            Node node = new Node(8);
            contains(head, node);

            // inDepth1(head);

            WriteLine(maxDepth(head));
            WriteLine(maxDepthByQueue(head));

            WriteLine(maxDepthByStack(head));


            Node Symmetrichead = BuildTreeSymmetric();
            WriteLine(isSymmetric(head));

            WriteLine(isSymmetricByQueue(Symmetrichead));
            WriteLine(isSymmetricByQueue(head));

            int[] arr = { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            var rs = sortedArrayToBST(arr);
            printTree(rs);
            WriteLine(KthNode(rs, 3).Value);


            //{1,3,2,5},{2,1,3,#,4,#,7}
            // res {3,4,5,5,4,#,7}
            // mergeTrees();
            string[] arrStr = { "abcd", "abcde" };
            longestCommonPrefix(arrStr);
            ReadKey();
        }
    }
}
