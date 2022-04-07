using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace SQProjectW
{
    /// <summary>
    /// 按层级打印
    /// zigzag 打印
    /// </summary>
    public class Node
    {
        public int value;
        public Node left;
        public Node right;
        public Node(int data)
        {
            value = data;
        }

        public Node(int data, Node left, Node right)
        {
            value = data;
            this.left = left;
            this.right = right;
        }


        public void printByLevel(Node head)
        {
            if (head == null)
                return;

            Queue<Node> queue = new Queue<Node>();
            int level = 1;
            Node last = head;
            Node nlast = null;
            queue.Enqueue(head);

            WriteLine("Level " + (level++) + " : ");
            while (queue.Count() != 0)
            {
                head = queue.Dequeue();
                WriteLine(head.value + " ");
                if (head.left != null)
                {
                    queue.Enqueue(head.left);
                    nlast = head.left;
                }
                if (head.right != null)
                {
                    queue.Enqueue(head.right);
                    nlast = head.right;
                }
                if (head == last && queue.Count() != 0)
                {
                    WriteLine("\nLevel " + (level++) + ":");
                    last = nlast;
                }
            }
            //queue.
            // LinkedListNode // 双端队列
            // LinkedList  // 双端队列 加强版

        }

        public void printByZigZag(Node head)
        {
            if (head == null)
                return;

            LinkedList<Node> _dq = new LinkedList<Node>();
            int level = 1;
            bool lr = true;
            Node last = head;
            Node nlast = null;
            _dq.AddFirst(head);
            printLevelAndOrientation(level++, lr);
            while (_dq.Count != 0)
            {
                if (lr)
                {
                    head = _dq.First(); _dq.RemoveFirst();
                    if (head.left != null)
                    {
                        nlast = nlast == null ? head.left : nlast;
                        _dq.AddLast(head.left);
                    }
                    if (head.right != null)
                    {
                        nlast = nlast == null ? head.right : nlast;
                        _dq.AddLast(head.right);
                    }

                }
                else
                {
                    head = _dq.Last(); _dq.RemoveLast();
                    if (head.right != null)
                    {
                        nlast = nlast == null ? head.right : nlast;
                        _dq.AddFirst(head.right);
                    }
                    else
                    {
                        nlast = nlast == null ? head.left : nlast;
                        _dq.AddFirst(head.left);
                    }
                }
                WriteLine(head.value + " ");
                if (head == last && _dq.Any())
                {
                    lr = !lr;
                    last = nlast;
                    nlast = null;
                    WriteLine();
                    printLevelAndOrientation(level++, lr);

                }
            }
            WriteLine();
        }

        private void printLevelAndOrientation(int level, bool lr)
        {
            WriteLine("Level " + level + " from ");
            WriteLine(lr ? "left to right: " : "right to left: ");
        }
    }
    class Program
    {
        #region 用两个栈实现队列
        static Stack<int> stack1 = new Stack<int>();
        static Stack<int> stack2 = new Stack<int>();

        public static void push(int node)
        {
            stack1.Push(node);
        }

        public static int pop()
        {
            if (stack2.Count != 0)
                return stack2.Pop();
            else
            {
                while (stack1.Count != 0)
                    stack2.Push(stack1.Pop());
                if (stack2.Count != 0)
                    return stack2.Pop();
                else
                    throw new ApplicationException("No data in queue!");
            }
            throw new ApplicationException("No data in queue!");
        }
        #endregion

        #region 括号序列
        public static bool isValid(String s)
        {
            // write code here

            Stack<char> stack = new Stack<char>();
            var arr = s.ToCharArray();
            foreach (var item in arr)
            {
                if (item == '(')
                {
                    stack.Push(')');
                }
                else if (item == '[')
                {
                    stack.Push(']');
                }
                else if (item == '{')
                {
                    stack.Push('}');
                }
                else if (!stack.Any() || stack.Pop() != item)
                {
                    return false;
                }
            }
            return !stack.Any();
        }
        #endregion

        static void Main(string[] args)
        {
            #region 用两个栈实现队列
            Program.push(1);
            Program.push(2);
            Program.pop();
            Program.pop();
            #endregion

            #region 括号序列
            string str = "{}[]()";
            isValid(str);
            #endregion

            //  Node node2 = new Node(2);
            Node node4 = new Node(4);
            Node node6 = new Node(6);
            Node node7 = new Node(7);
            Node node8 = new Node(8);

            Node node5 = new Node(5, node7, node8);
            Node node3 = new Node(3, node5, node6);
            Node node2 = new Node(2, node4, null);
            Node node1 = new Node(1, node2, node3);
           // node1.printByLevel(node1);
            node1.printByZigZag(node1);
            WriteLine();
        }
    }


    /// <summary>
    /// 实现min 函数的栈
    /// </summary>
    internal class MyStack
    {
        private Stack<int> _stack = new Stack<int>();
        private Stack<int> _minstack = new Stack<int>();
        public void push(int node)
        {
            if (_minstack.Count() == 0)
            {
                _stack.Push(node);
                _minstack.Push(node);
            }
            else if (_minstack.Count() != 0 && node <= _minstack.Peek())
            {
                _stack.Push(node);
                _minstack.Push(node);
            }
            else
            {
                _minstack.Push(_minstack.Peek());
            }
        }

        public void pop()
        {
            _stack.Pop();
            _minstack.Pop();
        }

        public int top()
        {
            return _stack.Peek();
        }

        public int min()
        {
            return _minstack.Peek();
        }
    }
}
