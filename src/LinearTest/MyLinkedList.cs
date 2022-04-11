using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LinearTest
{

    public class Node
    {
        public int data { get; set; }
        public Node next { get; set; }
        public Node(int d)
        {
            data = d;
        }

    }
    public class MyLinkedList
    {
        // 头结点指针
        private Node head;
        // 尾结点指针
        private Node last;
        // 链表实际长度
        private int size;

        // 插入
        public void insert(int data, int index)
        {
            if (index < 0 || index > size)
            {
                throw new IndexOutOfRangeException();
            }

            Node insertedNode = new Node(data);
            if (size == 0)
            {
                head = insertedNode;
                last = insertedNode;
            }
            else if (index == 0)
            {
                insertedNode.next = head;
                head = insertedNode;
            }
            else if (size == index)
            {
                last.next = insertedNode;
                last = insertedNode;
            }
            else
            {

                Node preNode = get(index - 1);
                insertedNode.next = preNode.next.next;
                preNode.next = insertedNode;
            }
            size++;
        }

        public Node remove(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }

            Node removedNode = null;
            if (index == 0)
            {
                removedNode = head;
                head = head.next;
            }
            else if (index == size - 1)
            {
                Node preNode = get(index - 1);
                removedNode = preNode.next;
                preNode.next = null;
                last = preNode;
            }
            else
            {
                Node preNode = get(index - 1);
                Node nextNode = preNode.next.next;
                removedNode = preNode.next;
                preNode.next = nextNode;
            }
            size--;
            return removedNode;
        }

        // 查找
        public Node get(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }

            Node temp = head;
            for (int i = 0; i < index; i++)
            {
                temp = temp.next;
            }
            return temp;
        }

        public void output()
        {
            Node temp = head;
            while (temp != null)
            {
                Console.Write(temp.data);
                temp = temp.next;
            }

            Console.WriteLine();
        }
    }
}
