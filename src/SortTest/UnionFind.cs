namespace SortTest
{
    /// <summary>
    /// 并查集
    /// </summary>
    public class UnionFind
    {
        // 连通分量个数
        public int count { get; set; }
        // 存储每个节点的父节点
        private int[] parent;

        // n 为图中节点的个数
        public UnionFind(int n)
        {
            count = n;
            parent = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        // 将节点 p 和节点 q 连通
        public void Union(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            if (rootP == rootQ) return; 

            parent[rootQ] = rootP;

            count--;
        }

        // 判断节点 p 和节点 q 是否连通
        public bool connected(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            return rootP == rootQ;
        }

        public int find(int x)
        {
            if (parent[x] != x) //  父非自己
                parent[x] = find(parent[x]); // 找到根，顺手赋值给递归中所有人节点根
            return parent[x];
        }
    }
}
