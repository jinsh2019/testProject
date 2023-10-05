namespace July.algorithms.day6
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

        public void Push_back(int value)
        {
            while (maxLinkedList.Count != 0 && maxLinkedList.Last != null && maxLinkedList.Last.Value < value)
                maxLinkedList.RemoveLast();
            maxLinkedList.AddLast(value);
            linkedList.AddLast(value);
        }

        public int Pop_front()
        {
            if (linkedList.Count == 0)
                return -1;
            int ans = linkedList.First.Value;
            linkedList.RemoveFirst();
            if (ans == maxLinkedList.First.Value)
                maxLinkedList.RemoveFirst();
            return ans;
        }
    }
}
