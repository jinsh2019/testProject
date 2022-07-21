using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.algorithms.day2
{
    // 并查集：使用数组实现了一个N叉树
    // 性质： 连通性，自反性，xx性
    internal class UnionFind
    {
        public int count { get; set; }
        public int[] parent { get; set; }

        public UnionFind(int n)
        {
            this.count = n;
            this.parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }
        }

        public void union(int p, int q) {
            int rootP = find(p);
            int rootQ = find(q);
            if (rootP == rootQ) {
                return;
            }
            parent[rootQ] = rootP;
            count--;
        }

        // 是否连通
        public bool connected(int p, int q) {
            int rootP = find(p);
            int rootQ = find(q);
            return rootQ == rootQ;
        }

        private int find(int x) {
            if (parent[x] != x)
                parent[x] = find(parent[x]);
            return parent[x]; 
        }
    }
}
