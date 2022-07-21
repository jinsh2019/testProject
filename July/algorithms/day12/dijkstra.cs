namespace July.algorithms.day12
{
    class State
    {
        public int id { get; set; }
        public int distFromStart { get; set; }
        public State(int id, int distFromStart)
        {
            this.id = id;
            this.distFromStart = distFromStart;
        }
    }
    internal static class dijkstra
    {
        public static int[] dijstra_1(int start, List<int[]>[] graph)
        {
            int[] distTo = new int[graph.Length];
            Array.Fill(distTo, int.MaxValue);
            distTo[start] = 0;

            PriorityQueue<State, int> pq = new PriorityQueue<State, int>();
            pq.Enqueue(new State(start, 0), 0);

            while (pq.Count != 0)
            {
                State curState = pq.Dequeue();
                int curNodeId = curState.id;
                int curDistFromStart = curState.distFromStart;

                if (curDistFromStart > distTo[curNodeId])
                    continue;

                foreach (int[] nextNode in graph[curNodeId])
                {
                    int nextNodeId = nextNode[0];
                    int distToNextNode = distTo[curNodeId] + nextNode[1];
                    if (distTo[nextNodeId] > distToNextNode)
                    {
                        distTo[nextNodeId] = distToNextNode;
                        pq.Enqueue(new State(nextNodeId, distToNextNode), distToNextNode);
                    }
                }
            }

            return distTo;
        }
    }
}
