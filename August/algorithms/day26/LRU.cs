namespace August.algorithms.day26
{
    /// <summary>
    /// 带有key，val的双向节点
    /// </summary>
    public class Node
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
    /// <summary>
    /// 带有头和尾的双指针链表
    /// 对cache进行排序
    /// </summary>
    public class DoubleList
    {
        private Node head, tail;
        public int size { get; set; }

        public DoubleList()
        {
            head = new Node(0, 0);
            tail = new Node(0, 0);
            size = 0;
            head.next = tail;
            head.pre = null;
            tail.pre = head;
            tail.next = null;
        }
        /// <summary>
        /// 添加到队尾
        /// </summary>
        /// <param name="node"></param>
        public void addLast(Node node)
        {
            // 设置新增的node
            node.pre = tail.pre;
            node.next = tail;
            // 设置tail
            tail.pre.next = node;
            tail.pre = node;
            size++;
        }
        /// <summary>
        /// 删除对首元素
        /// </summary>
        /// <returns></returns>
        public Node removeFirst()
        {
            if (head.next == tail)
            {
                return null;
            }

            Node first = head.next;
            remove(first);
            return first;
        }
        /// <summary>
        /// 移出一个元素
        /// </summary>
        /// <param name="node"></param>
        public void remove(Node node)
        {
            // 修补前后节点关系
            node.pre.next = node.next;
            node.next.pre = node.pre;
            size--;
        }

    }
    /// <summary>
    /// 最近使用算法
    /// 1. 最近使用，则不删除；
    /// 2. 新增cache，调用addLast；
    /// 3. 更新cache，调用remove(),然后再addLast;
    /// 4. 超出 size，调用removeFirst；
    /// </summary>
    internal class LRUCache
    {
        /// <summary>
        /// get 的复杂度 O(1)
        /// </summary>
        private Dictionary<int, Node> map;
        private DoubleList cache;
        private int cap;

        public LRUCache(int capacity)
        {
            cap = capacity;
            map = new Dictionary<int, Node>();
            cache = new DoubleList();
        }
        public int Get(int key)
        {
            if (!map.ContainsKey(key))
                return -1;
            makeRecently(key);
            return map[key].val;
        }

        private void makeRecently(int key)
        {
            Node node = map[key];
            cache.remove(node);
            cache.addLast(node);
        }

        public void Put(int key, int val)
        {
            if (map.ContainsKey(key))
            {
                delete(key);
                addRecently(key, val);
            }
            else
            {
                if (cap == cache.size)
                    removeLeastRecently();
                // 1. 超配额，删除Node后再调用
                // 2. 未超配额，仅仅update
                addRecently(key, val); 
            }
        }

        private void removeLeastRecently()
        {
            Node deletedNode = cache.removeFirst();
            int deletedKey = deletedNode.key;
            map.Remove(deletedKey);
        }

        private void addRecently(int key, int val)
        {
            Node x = new Node(key, val);
            cache.addLast(x);
            if (map.ContainsKey(key))
                map[key] = x;
            else
                map.Add(key, x);
        }

        private void delete(int key)
        {
            Node node = map[key];
            cache.remove(node);
            map.Remove(key);
        }
    }
}
