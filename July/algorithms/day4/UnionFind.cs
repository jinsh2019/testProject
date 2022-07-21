namespace July.algorithms.day4
{
    internal class UnionFind
    {
        public int count { get; set; }
        public int[] parent { get; set; }
        public UnionFind(int n) {
            this.count = n;
            parent = new int[n];
            for (int i = 0; i < n; i++) { 
                parent[i] = i;  
            }
        }

        int Find(int x) {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }

        public void Union(int p,int q)
        {
            int rootP = Find(p), rootQ = Find(q);
            if (rootP == rootQ)
                return;
            parent[rootQ] = rootP;
            count--;
        }

        public bool Connected(int p, int q)
        {
            int rootP = Find(p), rootQ = Find(q);
            return rootP == rootQ;
        }
    }
}
