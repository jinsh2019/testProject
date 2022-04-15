using System.Collections.Generic;

namespace CDS
{
    // 顶点
    public class Vertex
    {
        // 数字顶点
        private int Data { get; set; }
        // 字符串顶点
        private string sData { get; set; }
        public Vertex(int data)
        {
            Data = data;
        }
        public Vertex(string sData)
        {
            this.sData = sData;
        }
    }

    // 边
    public class Edge
    {
        // 索引
        public int Index { get; set; }
        //权重
        public int Weight { get; set; }
        public Edge(int index, int weight)
        {
            this.Index = index;
            this.Weight = weight;
        }
    }

    // 图
    public class Graph
    {
        public int size { get; }
        // 顶点
        public Vertex[] vertexes { get; }
        // 无权重邻接表
        public List<int>[] adjNoWeight { get; }
        // 有权重邻接表
        public List<Edge>[] adjWeight { get; }

        public Graph(int size)
        {
            this.size = size;
            vertexes = new Vertex[size];
            adjNoWeight = new List<int>[size];
            for (int i = 0; i < size; i++)
            {
                vertexes[i] = new Vertex(i);
                adjNoWeight[i] = new List<int>();
            }

            adjWeight = new List<Edge>[size];
            for (int i = 0; i < adjWeight.Length; i++)
            {
                adjWeight[i] = new List<Edge>();
            }
        }
    }
}
