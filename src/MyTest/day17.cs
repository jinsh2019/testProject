using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    public class day17
    {
        // 797. 所有可能的路径
        IList<IList<int>> res = new List<IList<int>>();

        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            // 路径
            LinkedList<int> path = new LinkedList<int>();
            traverse(graph, 0, path);
            return res;
        }
        // 图的遍历框架
        private void traverse(int[][] graph, int s, LinkedList<int> path)
        {
            // 添加节点s 到路径
            path.AddLast(s);

            int n = graph.Length; // 邻接矩阵最后一个结点
            if (s == n - 1)
            {
                // s 到达终点 --> 邻接矩阵最后一个结点
                res.Add(new List<int>(path));
                // 可以在这直接return，但要removeLast 正确维护 path
                path.RemoveLast();
                return;
                //  不return 也可以，因为图中不包含环，不会出现无限递归
            }

            foreach (var v in graph[s])
            {
                traverse(graph, v, path);
            }
            path.RemoveLast();

        }

        //207. 课程表

        bool[] visited;
        bool[] onPath;

        bool hasCycle = false;
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = buildGraph(numCourses, prerequisites);
            visited = new bool[numCourses];
            onPath = new bool[numCourses];

            for (int i = 0; i < numCourses; i++)
            {
                traverseCanFinish(graph, i);
            }
            return !hasCycle;
        }

        List<int>[] buildGraph(int numCourses, int[][] prerequisites)
        {
            // 共有 numCourses 个节点 
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                graph[i] = new List<int>();
            }

            foreach (int[] edge in prerequisites)
            {
                int from = edge[1], to = edge[0];
                graph[from].Add(to);
            }
            return graph;
        }

        // 从节点s 开始 DFS 遍历， 将遍历过的节点记为true
        void traverseCanFinish(List<int>[] graph, int s)
        {
            if (onPath[s])
                hasCycle = true;

            if (visited[s] || hasCycle)
                return;

            visited[s] = true;
            onPath[s] = true;
            foreach (int t in graph[s])
            {
                traverseCanFinish(graph, t);
            }
            onPath[s] = false;
        }
        // 210. 课程表 II

        List<int> postOrder = new List<int>();
        //bool[] visited;
        //bool[] onPath;

        //bool hasCycle = false;
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = buildGraph(numCourses, prerequisites);
            visited = new bool[numCourses];
            onPath = new bool[numCourses];

            for (int i = 0; i < numCourses; i++)
            {
                traverseFindOrder(graph, i);
            }

            if (hasCycle)
                return new int[] { };

            // 逆序后遍历结果即为拓扑排序结果
            postOrder.Reverse();
            int[] res = new int[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                res[i] = postOrder[i];
            }
            return res;
        }

        private void traverseFindOrder(List<int>[] graph, int s)
        {
            if (onPath[s])
                hasCycle = true;

            if (visited[s] || hasCycle)
                return;

            // pre
            onPath[s] = true;
            visited[s] = true;
            foreach (int t in graph[s])
            {
                traverseFindOrder(graph, t);
            }
            // post
            postOrder.Add(s);
            onPath[s] = false;
        }


        //207. 课程表
        public bool CanFinishBFS(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = buildGraph(numCourses, prerequisites);
            // 构建入度数组
            int[] indegree = new int[numCourses];
            foreach (int[] edge in prerequisites)
            {
                int from = edge[1], to = edge[0];
                // 节点 to 的入度+1
                indegree[to]++;
            }

            // 根据入度初始化队列中的节点
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < numCourses; i++)
            {
                if (indegree[i] == 0)
                {
                    // 节点i 没有入度，即没有依赖的节点
                    // 可以作为拓扑排序的起点，加入队列
                    q.Enqueue(i);
                }
            }
            // 记录遍历的节点个数
            int count = 0;
            // 开始执行BFS 循环
            while (q.Count > 0)
            {
                // 弹出节点 cur, 并将它指向节点的入度减一
                int cur = q.Dequeue();
                count++;
                foreach (int next in graph[cur])
                {
                    indegree[next]--;
                    if (indegree[next] == 0)
                        // 如果入度变为0， 说明next依赖的节点都已被遍历
                        q.Enqueue(next);
                }
            }

            // 如果所有结点都被遍历过，说明不成环
            return count == numCourses;
        }

        //785. 判断二分图
        // 记录图是否符合二分图性质
        private bool ok = true;
        // 记录图中节点的颜色， false 和 true 代表两种不同的颜色
        private bool[] color;
        //private bool[] visited;
        public bool IsBipartite(int[][] graph)
        {
            int n = graph.Length;
            color = new bool[n];
            visited = new bool[n];

            // 因为图不一定是联通的，可能存在多个子图
            // 所以要把每个节点都作为起点进行一次遍历
            // 如果发现任何一个子图不是二分图，整幅图都不算二分图
            for (int v = 0; v < n; v++)
            {
                if (!visited[v])
                {
                    traverseDFS(graph, v);
                }
            }

            return ok;
        }

        // DFS 遍历框架
        private void traverseDFS(int[][] graph, int v)
        {
            // 如果已经确定不是二分图了，就不用浪费时间再递归遍历
            if (!ok) return;

            visited[v] = true;

            foreach (int w in graph[v])
            {

                if (!visited[w])
                {
                    // 相邻结点 w 没有被访问过
                    // 那么应该给结点w图上和结点v不同的颜色
                    color[w] = !color[v];
                    // 继续遍历 w
                    traverseDFS(graph, w);
                }
                else
                {
                    // 相邻结点 w  已经被访问过
                    // 根据 v 和 w 的颜色判断是否是二分图
                    if (color[w] == color[v])
                    {
                        // 若相同， 则此图不是二分图
                        ok = false;
                    }
                }
            }
        }

        // 输入一个二叉树的根节点，层序遍历这颗二叉树
        public void levelTraverse(TreeNode root)
        {
            if (root == null) return;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            int depth = 1;
            while (q.Count > 0)
            {
                int sz = q.Count;
                for (int i = 0; i < sz; i++)
                {
                    TreeNode cur = q.Dequeue();
                    Console.WriteLine($"节点{cur.val}在第{depth}层");
                    if (cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if (cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
                depth++;
            }
        }

        // 输入一个多叉树的根节点，层序遍历这颗多叉树
        public void levelTraverseNTree(NTreeNode root)
        {
            if (root == null) return;
            Queue<NTreeNode> q = new Queue<NTreeNode>();
            q.Enqueue(root);

            int depth = 1;
            //  从上到下遍历多叉树的每一层
            while (q.Count > 0)
            {
                int sz = q.Count;
                // 从左到右遍历每一层的每个节点
                for (int i = 0; i < sz; i++)
                {
                    NTreeNode cur = q.Dequeue();
                    Console.WriteLine($"节点{cur.val}在第{depth}层");

                    // 将下一层节点放入队列
                    foreach (NTreeNode child in cur.Children)
                    {
                        q.Enqueue(child);
                    }
                }
            }
        }

        // 输入起点，进行 BFS 搜索
        public int BSF(Node start)
        {
            //Queue<Node> q = new Queue<Node>();
            //HashSet<Node> visited = new HashSet<Node>();

            //q.Enqueue(start);
            //visited.Add(start);

            //int step = 0;
            //while (q.Count > 0)
            //{
            //    int sz = q.Count;
            //    for (int i = 0; i < sz; i++)
            //    {
            //        Node cur = q.Dequeue();
            //        Console.WriteLine($"从{start}到{cur.val}的最短距离是{step}");

            //        foreach (Node node in cur.adj)
            //        {
            //            if (!visited.Contains(node))
            //            {
            //                q.Enqueue(node);
            //                visited.Add(node);
            //            }
            //        }

            //    }
            //    step++;
            //}
            throw new NotImplementedException();

        }
    }

    public class LRUCache
    {
        // key -> Node(key, val)
        private Dictionary<int, Node> map;
        // Node(k1, v1) <-> Node(k2, v2)...
        private DoubleList cache;
        // 最大容量
        private int cap { get; set; }


        public LRUCache(int capacity)
        {
            this.cap = capacity;
            map = new Dictionary<int, Node>();
            cache = new DoubleList();
        }

        public int Get(int key)
        {
            if (!map.ContainsKey(key))
            {
                return -1;
            }
            // 将该数据提升为最近使用的
            makeRecently(key);
            return map[key].val;
        }

        public void Put(int key, int value)
        {
            if (map.ContainsKey(key))
            {
                // 删除旧的数据
                deleteKey(key);
                // 新插入的数据为最近使用的数据
                addRecently(key, value);
                return;
            }

            if (cap == cache.size)
            {
                // 删除最久未使用的元素
                removeLeastRecently();
            }
            // 添加为最近使用的元素
            addRecently(key, value);
        }


        /* 将某个 key 提升为最近使用的 */
        private void makeRecently(int key)
        {
            Node x = map[key];
            // 先从链表中删除这个节点
            cache.remove(x);
            // 重新插到队尾
            cache.addLast(x);
        }

        /* 添加最近使用的元素 */
        private void addRecently(int key, int val)
        {
            Node x = new Node(key, val);
            // 链表尾部就是最近使用的元素
            cache.addLast(x);
            // 别忘了在 map 中添加 key 的映射
            map.Add(key, x);
        }

        /* 删除某一个 key */
        private void deleteKey(int key)
        {
            Node x = map[key];
            // 从链表中删除
            cache.remove(x);
            // 从 map 中删除
            map.Remove(key);
        }

        /* 删除最久未使用的元素 */
        private void removeLeastRecently()
        {
            // 链表头部的第一个元素就是最久未使用的
            Node deletedNode = cache.removeFirst();
            // 同时别忘了从 map 中删除它的 key
            int deletedKey = deletedNode.key;
            map.Remove(deletedKey);
        }

    }
}
