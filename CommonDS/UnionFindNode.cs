﻿using System;

// 高级数据结构：
// 优先级队列      PriorityQueue
// 字典树（前缀树） TrieNode
// 红黑树          X
// 分段树          X
// 树状数组         X
// 跳表            LinkedNode + NextPoint
// 并查集          Union Find

// common data structure
namespace CDS
{
    /// <summary>
    /// A UnionFindNode represents a set of nodes that it is a member of.
    /// 
    /// You can get the unique representative node of the set a given node is in by using the Find method.
    /// Two nodes are in the same set when their Find methods return the same representative.
    /// The IsUnionedWith method will check if two nodes' sets are the same (i.e. the nodes have the same representative).
    ///
    /// You can merge the sets two nodes are in by using the Union operation.
    /// There is no way to split sets after they have been merged.
    /// </summary>
    public class UnionFindNode
    {
        private UnionFindNode _parent;
        private uint _rank;

        /// <summary>
        /// Creates a new disjoint node, representative of a set containing only the new node.
        /// </summary>
        public UnionFindNode()
        {
            _parent = this;
        }

        /// <summary>
        /// Returns the current representative of the set this node is in.
        /// Note that the representative is only accurate untl the next Union operation.
        /// </summary>
        public UnionFindNode Find()
        {
            if (!ReferenceEquals(_parent, this)) _parent = _parent.Find();
            return _parent;
        }

        /// <summary>
        /// Determines whether or not this node and the other node are in the same set.
        /// </summary>
        public bool IsUnionedWith(UnionFindNode other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return ReferenceEquals(Find(), other.Find());
        }

        /// <summary>
        /// Merges the sets represented by this node and the other node into a single set.
        /// Returns whether or not the nodes were disjoint before the union operation (i.e. if the operation had an effect).
        /// </summary>
        /// <returns>True when the union had an effect, false when the nodes were already in the same set.</returns>
        public bool Union(UnionFindNode other)
        {
            if (other == null) throw new ArgumentNullException("other");
            var root1 = this.Find();
            var root2 = other.Find();
            if (ReferenceEquals(root1, root2)) return false;

            if (root1._rank < root2._rank)
            {
                root1._parent = root2;
            }
            else if (root1._rank > root2._rank)
            {
                root2._parent = root1;
            }
            else
            {
                root2._parent = root1;
                root1._rank++;
            }
            return true;
        }
    }
    public class UnionFind
    {
        int[] roots;
        int size; // 集合数量

        public UnionFind(int n)
        {
            roots = new int[n];
            for (int i = 0; i < n; i++)
            {
                roots[i] = i;
            }
            size = n;
        }

        public int find(int i)
        {
            if (i == roots[i])
            {
                return i;
            }
            return roots[i] = find(roots[i]);
        }

        public void union(int p, int q)
        {
            int pRoot = find(p);
            int qRoot = find(q);
            if (pRoot != qRoot)
            {
                roots[pRoot] = qRoot;
                size--;
            }
        }
    }

    public class UnionFindL
    {
        // 返回图中的连通分量个数
        public int count { get; set; }
        // 存储每个节点的父节点
        private int[] parent;

        // n 为图中节点的个数
        public UnionFindL(int n)
        {
            this.count = n;
            this.parent = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        // 将节点 p 和节点 q 连通
        public void union(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            if (rootP == rootQ)
                return;
            parent[rootQ] = rootP;
            // 两个连通分量合并成一个连通分量
            count--;
        }

        // 判断节点 p 和节点 q 是否连通
        public bool connected(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            return rootP == rootQ;
        }

        private int find(int x)
        {
             if(parent[x] != x) //  父非自己
                parent[x] = find(parent[x]); // 找到根，顺手赋值给递归中所有人节点根
            return parent[x];
        }
    }
}
