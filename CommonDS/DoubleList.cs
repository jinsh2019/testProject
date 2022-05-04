using System;
using System.Collections.Generic;
using System.Text;

namespace CDS
{
    public class DoubleList
    {
        // 头尾虚节点
        private Node head, tail;
        // 链表元素数
        public int size { get; set; }

        public DoubleList()
        {
            // 初始化双向链表的数据
            head = new Node(0, 0);
            tail = new Node(0, 0);
            head.right = tail;
            tail.left = head;
            size = 0;
        }

        // 在链表尾部添加节点 x，时间 O(1)
        public void addLast(Node x)
        {
            x.left = tail.left;
            x.right = tail;
            tail.left.right = x;
            tail.left = x;
            size++;
        }

        // 删除链表中的 x 节点（x 一定存在）
        // 由于是双链表且给的是目标 Node 节点，时间 O(1)
        public void remove(Node x)
        {
            x.left.right = x.right;
            x.right.left = x.left;
            size--;
        }

        // 删除链表中第一个节点，并返回该节点，时间 O(1)
        public Node removeFirst()
        {
            if (head.right == tail)
                return null;
            Node first = head.right;
            remove(first);
            return first;
        }

    }
}
