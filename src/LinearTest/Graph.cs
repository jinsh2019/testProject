using System;
using System.Collections.Generic;
using System.Text;

namespace LinearOrNoneTest
{
    // 定点
    public class Vertex
    {
        public int data { get; set; }
        public Vertex(int data)
        {
            this.data = data;
        }
    }
    public class Graph
    {
        public int size { get; set; }
        public Vertex[] vertexes { get; set; }
        public List<int>[] adj { get; set; }

        // 邻接表表示法
        public Graph(int size)
        {
            this.size = size;
            vertexes = new Vertex[size];
            adj = new List<int>[size];
            for (int i = 0; i < size; i++)
            {
                vertexes[i] = new Vertex(i);
                adj[i] = new List<int>();
            }
        }


        // 深度遍历图 0，1，3，4，5，2
        public static void dfs(Graph graph, int start, bool[] visited)
        {
            Console.Write(graph.vertexes[start].data + ",");
            visited[start] = true;
            foreach (var index in graph.adj[start])
            {
                if (!visited[index])
                {
                    dfs(graph, index, visited);
                }
            }
        }
        // 广度遍历 0，1，2，3，4，5
        public static void bfs(Graph graph, int start, bool[] visited, Queue<int> queue)
        {
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                int front = queue.Dequeue();
                if (visited[front])
                {
                    continue;
                }
                Console.Write(graph.vertexes[front].data + ",");
                visited[front] = true;
                for (int i = 0; i < graph.adj[front].Count; i++)
                {
                    queue.Enqueue(graph.adj[front][i]);
                }
            }
        }
    }

    /*
     * 
     * 
   头节点     邻接表节点
    0   ---> 1，2，3
    1   ---> 0，3，4
    2   ---> 0
    3   ---> 0，1，4，5
    4   ---> 1，3，5
    5   ---> 3，4
     
     */
}
