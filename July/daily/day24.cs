using CDS;

namespace July.daily;

public class day24
{
    int sum = 0;
    public int RangeSumBST(TreeNode root, int low, int high)
    {
        if (root == null) return 0;
        traverse(root, low, high);
        return sum;
    }

    void traverse(TreeNode root, int low, int high)
    {
        if (root == null) return;

        if (root.val < low)
        {
            traverse(root.right, low, high);
        }
        else if (root.val > high)
        {
            traverse(root.left, low, high);
        }
        else
        {
            sum += root.val;
            traverse(root.left, low, high);
            traverse(root.right, low, high);
        }
    }


    private bool[] onPath;
    private bool[] visited;
    private bool hasCycle = false;

    public bool CanFinish(int numsCourses, int[][] prerequisites)
    {
        List<int>[] graph = buildGraph(numsCourses, prerequisites);

        visited = new bool[numsCourses];
        onPath = new bool[numsCourses];
        for (int i = 0; i < numsCourses; i++)
        {
            traverse(graph, i);
        }

        return !hasCycle;
    }
    // bsf
    private void traverse(List<int>[] graph, int s)
    {
        if (onPath[s])
            hasCycle=true;
        if (visited[s] || hasCycle)
            return;
        visited[s] = true;

        onPath[s] = true;
        foreach (var v in graph[s])
        {
            traverse(graph, v);
        }

        onPath[s] = false;
    }

    private List<int>[] buildGraph(int numsCourses, int[][] prerequisites)
    {

        List<int>[] graph = new List<int>[numsCourses];
        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (int[] edge in prerequisites)
        {
            int from = edge[1];
            int to = edge[0];
            graph[from].Add(to);
        }
        return graph;
    }

    public int LargestRectangleArea(int[] heights)
    {
        int n = heights.Length;
        int[] left = new int[n]; // 左边达到的最大距离
        int[] right = new int[n];// 右边达到的最大距离


        Stack<int> s = new Stack<int>();
        for (int i = 0; i < n; i++)
        {
            while (s.Count != 0 && heights[s.Peek()] >= heights[i])
                s.Pop();
            left[i] = (s.Count == 0 ? -1 : s.Peek());
            s.Push(i);
        }

        s.Clear();
        for (int i = n - 1; i >= 0; --i)
        {
            while (s.Count != 0 && heights[s.Peek()] >= heights[i])
                s.Pop();
            right[i] = (s.Count == 0 ? n : s.Peek());
            s.Push(i);
        }

        int ans = 0;
        for (int i = 0; i < n; i++)
            ans = Math.Max(ans, (right[i] - left[i] - 1) * heights[i]);
        return ans;
    }
}