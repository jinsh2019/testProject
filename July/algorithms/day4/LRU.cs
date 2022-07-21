using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.day4
{
    class Node
    {
        public int key { get; set; }
        public int val { get; set; }
        public Node next { get; set; }
        public Node pre { get; set; }
        public Node(int k, int v)
        {
            key = k;
            val = v;
        }
    }
    // 双端链表
    class DoubleList
    {
        private Node head, tail;
        public int size { get; set; }
        public DoubleList()
        {
            head = new Node(0, 0);
            tail = new Node(0, 0);
            size = 0;
            head.next = tail;
            tail.pre = head;
        }
        // O(1)
        public void addLast(Node x)
        {
            x.pre = tail.pre;
            x.next = tail;
            tail.pre.next = x;
            tail.pre = x;
            size++;
        }
        // O(1)
        public void remove(Node x)
        {
            x.pre.next = x.next;
            x.next.pre = x.pre;
            size--;
        }
        // O(1)
        public Node removeFirst()
        {
            if (head.next == tail)
                return null;
            Node first = head.next;
            remove(first);
            return first;
        }
    }
    internal class LRUCache
    {
        private Dictionary<int, Node> map;
        private DoubleList cache;
        private int cap;

        public LRUCache(int capacity)
        {
            cap = capacity;
            map = new Dictionary<int, Node>();
            cache = new DoubleList();
        }
        // O(1)
        public int Get(int key)
        {
            if (!map.ContainsKey(key))
                return -1;

            makeRecently(key);
            return map[key].val;
        }
        // O(1) 删除更新指定元素
        void makeRecently(int key)
        {
            Node x = map[key];
            cache.remove(x);
            cache.addLast(x);
        }
        // O(1)
        public void Put(int key, int val)
        {
            // 删除新增
            if (map.ContainsKey(key))
            {
                deleteKey(key);
                addRecently(key, val);
                return;
            }
            else // 新增更新
            {
                if (cap == cache.size)
                    removeLeastRecently(); // 删除
                addRecently(key, val);
            }
        }
        // O(1) 从链表中删除key
        void deleteKey(int key)
        {
            Node x = map[key];
            cache.remove(x);
            map.Remove(key);
        }
        // O(1)新增并最新
        void addRecently(int key, int val)
        {
            Node x = new Node(key, val);
            cache.addLast(x);
            if (map.ContainsKey(key))
                map[key] = x;
            else
                map.Add(key, x);
        }
        // O(1)
        // 1. 从链表中删除第一个node
        // 2. 从map中删除node.key
        void removeLeastRecently()
        {
            Node deletedNode = cache.removeFirst();
            int deletedKey = deletedNode.key;
            map.Remove(deletedKey);
        }
    }
}
