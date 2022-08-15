﻿namespace July.algorithms.day5
{
    // 队列的最大值
    internal class MaxQueue
    {
        LinkedList<int> q;// fifo
        LinkedList<int> d;// 单调队列 desc
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

        public void Push_back(int value)
        {
            while (d.Count != 0 && d.Last() < value)
                d.RemoveLast();
            d.AddLast(value);
            q.AddLast(value);
        }

        public int Pop_front()
        {
            if (q.Count == 0)
                return -1;
            int ans = q.First();
            q.RemoveFirst();
            if (ans == d.First())
                d.RemoveFirst();
            return ans;
        }
    }
}