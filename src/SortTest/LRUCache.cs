using System.Collections.Generic;


/*
 LCR 031. LRU 缓存
 146. LRU 缓存
 面试题 16.25. LRU 缓存

 */
namespace SortTest
{
    /// <summary>
    /// 节点
    /// 带有key，val的双向Node
    /// </summary>
    public class Node
    {
        public int key;
        public int val;
        public Node prev;
        public Node next;
        public Node child;
        public Node left;
        public Node right;
        public List<Node> neighbors;
        /// <summary>
        /// 随机指针，指向的节点索引，不指向任何节点，则为空
        /// </summary>
        public Node random;
        public Node(int val) {
            this.val = val;
        }
        public Node(int key, int val)
        {
            this.key = key;
            this.val = val;
        }
    }

    /// <summary>
    /// 带有头和尾的双指针链表
    /// </summary>
    public class DoubleList
    {
        private Node head, tail;
        public int size;

        public DoubleList()
        {
            head = new Node(0, 0);
            tail = new Node(0, 0);
            size = 0;
            head.next = tail;
            head.prev = null;
            tail.prev = head;
            tail.next = null;
        }

        /// <summary>
        /// 添加节点到队尾 0(1)
        /// tail Node
        /// </summary>
        /// <param name="node"></param>
        public void addLast(Node node)
        {
            // node
            node.prev = tail.prev;
            node.next = tail;
            // tail
            tail.prev.next = node;
            tail.prev = node;
            size++;
        }

        /// <summary>
        /// 移除首个节点 0(1)
        /// </summary>
        /// <returns></returns>
        public Node removeFirst()
        {
            if (head.next == tail)
                return null;

            Node first = head.next;
            remove(first);
            return first;
        }

        /// <summary>
        /// 移除指定节点 0(1)
        /// </summary>
        /// <param name="node"></param>
        public void remove(Node node)
        {
            node.prev.next = node.next;
            node.next.prev = node.prev;
            size--;
        }
    }

    /// <summary>
    ///  链表尾部优先级最高
    ///  链表头部优先级最低应最先删除
    /// </summary>
    internal class LRUCache
    {
        /// <summary>
        /// get O(1)
        /// </summary>
        private Dictionary<int, Node> dic;
        /// <summary>
        /// 双端链表 使LRUCache具有有序性
        /// </summary>
        private DoubleList cacheList;
        /// <summary>
        /// 容量
        /// </summary>
        private int cap;

        public LRUCache(int capacity)
        {
            cap = capacity;
            dic = new Dictionary<int, Node>();
            cacheList = new DoubleList();

        }
        /// <summary>
        /// 获取指定节点 O(1)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Get(int key)
        {
            if (!dic.ContainsKey(key))
            {
                return -1;
            }
            makeRecently(key);

            return dic[key].val;
        }

        /// <summary>
        /// update or add O(1)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Put(int key, int val)
        {
            if (dic.ContainsKey(key))
            {
                delete(key);
                addRecently(key, val);
            }
            else
            {
                if (cap == cacheList.size)
                    removeLeastRecently();
                addRecently(key, val);
            }
        }

        /// <summary>
        /// 提升最近使用优先级
        /// </summary>
        /// <param name="key"></param>
        private void makeRecently(int key)
        {
            Node node = dic[key];
            cacheList.remove(node);
            cacheList.addLast(node);
        }
        
        private void delete(int key)
        {
            Node node = dic[key];
            cacheList.remove(node);
            dic.Remove(key);
        }

        private void addRecently(int key, int val)
        {
            Node newNode = new Node(key, val);
            cacheList.addLast(newNode);
            if (dic.ContainsKey(key))                   // update
            {
                dic[key] = newNode;
            }
            else                                        // add
            {
                dic.Add(key, newNode);
            }
        }

        private void removeLeastRecently()
        {
            Node deletedNode = cacheList.removeFirst();
            int deletedKey = deletedNode.key;
            dic.Remove(deletedKey);
        }
    }
}
