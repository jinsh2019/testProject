namespace August.algorithms.day26
{
    // 并查集
    internal class UnionFind
    {
        // 联通数量
        public int count { get; set; }
        // 父节点
        public int[] parent { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="n">自己连通自己</param>
        public UnionFind(int n)
        {
            count = n;
            parent = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        // 找到父节点
        public int find(int x)
        {
            if (parent[x] != x)
                parent[x] = find(parent[x]);
            return parent[x];
        }

        // 进行连通
        public void Union(int p, int q)
        {
            int rootP = find(p), rootQ = find(q);
            if (rootP == rootQ)
                return;
            parent[rootP] = rootQ;
            count--;
        }
        // 连通起来
        public bool Connected(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            return rootP == rootQ;
        }
    }
}
