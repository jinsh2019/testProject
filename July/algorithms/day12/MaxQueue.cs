namespace July.algorithms.day12
{
    internal class MaxQueue
    {
        LinkedList<int> linkedList;
        LinkedList<int> maxLinkedList;
        public MaxQueue()
        {
            linkedList = new LinkedList<int>();
            maxLinkedList = new LinkedList<int>();
        }

        public int Max_value()
        {
            if (maxLinkedList.Count == 0)
                return -1;
            return maxLinkedList.First.Value;
        }

        public void Push_back(int val)
        {
            while (maxLinkedList.Count != 0 && maxLinkedList.Last != null && maxLinkedList.Last.Value < val)
            {
                maxLinkedList.RemoveLast();
            }
            maxLinkedList.AddLast(val);
            linkedList.AddLast(val);
        }
        public int Pop_front()
        {
            if (linkedList.Count == 0)
                return -1;
            int ans = linkedList.First.Value;
            if (ans == maxLinkedList.First.Value)
                maxLinkedList.RemoveFirst();

            return ans;
        }
    }
}
