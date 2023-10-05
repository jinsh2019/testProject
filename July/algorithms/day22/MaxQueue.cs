namespace July.algorithms.day22;

public class MaxQueue
{
    private LinkedList<int> linkedList;
    private LinkedList<int> maxLinkedList; //  最大q

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

    // 加入队列尾部
    public void Push_back(int val)
    {
        while (maxLinkedList.Count != 0 && maxLinkedList.Last != null && maxLinkedList.Last.Value < val)
            maxLinkedList.RemoveLast();
        maxLinkedList.AddLast(val);
        linkedList.AddLast(val);
    }
    // 弹出队列
    public int Pop_front(int val)
    {
        if (linkedList.Count == 0)
            return -1;
        // q 每必弹出
        int ans = linkedList.First.Value;
        linkedList.RemoveFirst();
        // d LinkedList有限弹出
        if (ans == maxLinkedList.First.Value)
            maxLinkedList.RemoveFirst();

        return ans;

    }
}