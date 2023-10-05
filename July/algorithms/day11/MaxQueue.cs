namespace July.algorithms.day11
{
    internal class MaxQueue
    {
        LinkedList<int> linkedList;
        LinkedList<int> maxLinkedlist;
        public MaxQueue()
        {
            linkedList = new LinkedList<int>();
            maxLinkedlist = new LinkedList<int>();
        }
        /// <summary>
        /// GetMax
        /// </summary>
        /// <returns></returns>
        public int Max_Value()
        {
            if (maxLinkedlist.Count == 0)
                return -1;
            return maxLinkedlist.First.Value;
        }
        /// <summary>
        /// Enqueue
        /// </summary>
        /// <param name="val"></param>
        public void Push_back(int val)
        {
            while (maxLinkedlist.Count != 0 && maxLinkedlist.Last != null && maxLinkedlist.Last.Value < val)
            {
                maxLinkedlist.RemoveLast();
            }
            maxLinkedlist.AddLast(val);
            linkedList.AddLast(val);
        }
        /// <summary>
        /// Dequeue
        /// </summary>
        /// <returns></returns>
        public int Pop_front()
        {
            if (linkedList.Count == 0)
                return -1;
            int ans = linkedList.First.Value;
            linkedList.RemoveFirst();
            if (ans == maxLinkedlist.First.Value)
                maxLinkedlist.RemoveFirst();
            return ans;
        }
    }
}
