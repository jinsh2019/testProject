namespace SortTest // https://paste.ubuntu.com/p/Wj9sYDzkxT/
{
    public class UnionFind
    {
        private int[] parent;
        public UnionFind(int n){
            parent = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        public void Union(int p, int q){
            parent[Find(q)] = Find(p);
        }

        public bool IsConnected(int p, int q){
            return Find(p) == Find(q);
        }

        public int Find(int idx){
            if (idx != parent[idx]) 
                parent[idx] = Find(parent[idx]);
            return parent[idx];
        }
    }
}
