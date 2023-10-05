using CDS;
using System.Collections.Generic;

namespace MyTest
{
    public class day18
    {
        // 单调栈 [2,1,2,4,3] -> [4,2,4,-1,-1]
        /// <summary>
        /// 503. 下一个更大元素 II
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int[] NextGreaterElement(int[] nums)
        {
            int[] res = new int[nums.Length];
            Stack<int> s = new Stack<int>();
            // 比最后一个数还小的数给 -1
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                // peek大于nums[i]停止
                while (s.Count > 0 && s.Peek() <= nums[i])
                {
                    s.Pop();
                }
                // peek的值就是右边最大的一个
                res[i] = s.Count == 0 ? -1 : s.Peek();
                // 压入栈，找下一个数
                s.Push(nums[i]);
            }
            return res;
        }

        /// <summary>
        /// 739. 每日温度
        /// </summary>
        /// <param name="temperatures"></param>
        /// <returns></returns>
        public int[] DailyTemperatures(int[] temperatures)
        {
            int[] res = new int[temperatures.Length];
            Stack<int> s = new Stack<int>();
            // 比最后一个数还小的数给 0
            for (int i = temperatures.Length - 1; i >= 0; i--)
            {
                // 从后开始取值： peek到大于temperatures[i]停止
                while (s.Count > 0 && temperatures[s.Peek()] <= temperatures[i])
                {
                    s.Pop();
                }
                // peek的值就是右边最大的一个，求间距
                res[i] = s.Count == 0 ? 0 : (s.Peek() - i);
                s.Push(i); // 把第几天压入栈中
            }

            return res;
        }

        // 最小生成树 kruskal 算法
        // 判断输入的若干条边是否能够构造出一棵树结构
        bool validTree(int n, int[][] edges)
        {
            // 初始化 0..n-1 共n 个节点
            UnionFindL uf = new UnionFindL(n);
            // 遍历所有边，将组成边的两个节点进行连接
            foreach (int[] edge in edges)
            {
                int u = edge[0];
                int v = edge[1];
                // 若两个节点已经再同一个连通分量中，会产生环
                if (uf.connected(u, v))
                {
                    return false;
                }
                // 这条边不会产生环，可以是树的一部分
                uf.union(u, v);
            }

            // 要保证最后只生成了一颗树， 只有一个连通分量
            return uf.count == 1;
        }
    }
}
