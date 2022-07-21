namespace July.algorithms.day11
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

        public void addLast(Node x)
        {
            x.pre = tail.pre;
            x.next = tail;
            tail.pre.next = x;
            tail.pre = x;
            size++;
        }

        public void remove(Node x)
        {
            x.pre.next = x.next;
            x.next.pre = x.pre;
            size--;
        }

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
        private LRUCache(int capacity)
        {
            cap = capacity;
            map = new Dictionary<int, Node>();
            cache = new DoubleList();
        }

        public int Get(int key)
        {
            if (!map.ContainsKey(key))
            {
                return -1;
            }
            makeRecently(key);
            return map[key].val;
        }

        private void makeRecently(int key)
        {
            Node x = map[key];
            cache.remove(x);
            cache.addLast(x);
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
                {
                    removeLeastRecently();
                }
                addRecently(key, val);
            }
        }

        private void removeLeastRecently()
        {
            Node DeletedNode = cache.removeFirst();
            int deletedKey = DeletedNode.key;
            map.Remove(deletedKey);
        }

        private void addRecently(int key, int val)
        {
            Node x = new Node(key, val);
            cache.addLast(x);
            if (map.ContainsKey(key))
            {
                map[key] = x;
            }
            else
            {
                map.Add(key, x);
            }
        }

        private void delete(int key)
        {
            Node x = map[key];
            cache.remove(x);
            map.Remove(key);
        }
    }
}
