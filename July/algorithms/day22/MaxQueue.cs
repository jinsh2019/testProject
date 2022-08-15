namespace July.algorithms.day22;

public class MaxQueue
{
    private LinkedList<int> q;
    private LinkedList<int> d; //  最大q

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

    // 加入队列尾部
    public void Push_back(int val)
    {
        while (d.Count != 0 && d.Last() < val)
            d.RemoveLast();
        d.AddLast(val);
        q.AddLast(val);
    }
    // 弹出队列
    public int Pop_front(int val)
    {
        if (q.Count == 0)
            return -1;
        // q 每必弹出
        int ans = q.First();
        q.RemoveFirst();
        // d LinkedList有限弹出
        if(ans == d.First())
            d.RemoveFirst();

        return ans;

    }
}