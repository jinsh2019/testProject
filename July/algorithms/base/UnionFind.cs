using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.@base
{
    internal class UnionFind
    {
        public int count { get; set; }
        private int[] parent { get; set; }

        public UnionFind(int n)
        {
            count = n;
            parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }
        }

        // 连通
        public void union(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            if (rootP == rootQ)
            {
                return;
            }
            parent[rootQ] = rootP;
            count--;
        }

        // 是否连通
        public bool connected(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            return rootP == rootQ;
        }

        private int find(int x)
        {
            if (parent[x] != x)
                parent[x] = find(parent[x]); // 找到根，顺手赋值给递归中所有人节点根
            return parent[x];
        }
    }
}
