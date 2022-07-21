namespace July.algorithms.day12
{
    internal class MaxQueue
    {
        LinkedList<int> q;
        LinkedList<int> d;
        public MaxQueue()
        {
            q = new LinkedList<int>();
            d = new LinkedList<int>();
        }

        public int Max_value()
        {
            if (d.Count == 0)
                return -1;
            return d.First();
        }

        public void Push_back(int val)
        {
            while (d.Count != 0 && d.Last() < val)
            {
                d.RemoveLast();
            }
            d.AddLast(val);
            q.AddLast(val);
        }
        public int Pop_front()
        {
            if (q.Count == 0)
                return -1;
            int ans = q.First();
            if (ans == d.First())
                d.RemoveFirst();

            return ans;
        }
    }
}
