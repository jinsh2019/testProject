namespace July.algorithms.day22;

// 岛屿的数量： 使用Union Find来实现岛屿的数量
public class UnionFind
{
    public int count { get; set; }
    public int[] parent { get; set; }

    public UnionFind(int n)
    {
        count = n;
        parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }
    }

    int find(int x)
    {
        if (parent[x] != x)
            parent[x] = find(parent[x]);
        return parent[x];
    }

    public void Union(int p, int q)
    {
        int rootP = find(p), rootQ = find(q);
        if(rootQ == rootQ)
            return;
        parent[rootP] = rootQ;
        count--;
    }

    public bool Connected(int p, int q)
    {
        int rootP = find(p), rootQ = find(q);
        return rootP == rootQ;
    }
}