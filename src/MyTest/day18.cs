using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    public class day18
    {
        // 单调栈 [2,1,2,4,3] -> [4,2,4,-1,-1]
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

    }
    // 前缀树
    public class TrieNode
    {
        // 路径数
        public int path { get; set; }
        // 终点数
        public int end { get; set; }
        // 多叉树
        public TrieNode[] map;
        public TrieNode()
        {
            path = 0;
            end = 0;
            map = new TrieNode[26];
        }
    }

    public class Trie
    {
        private TrieNode root;
        public Trie()
        {
            root = new TrieNode();
        }
        // 插入
        public void insert(string word)
        {
            if (word == null)
                return;
            char[] chs = word.ToCharArray();
            TrieNode node = root;
            node.path++;
            int index = 0;

            for (int i = 0; i < chs.Length; i++)
            {
                index = chs[i] - 'a';
                if (node.map[index] == null)
                {
                    node.map[index] = new TrieNode();
                }
                node = node.map[index];
                node.path++;
            }
            node.end++;
        }
        // 删除
        public void delete(string word)
        {
            if (search(word)) // 是否有这个word
            {
                char[] chs = word.ToCharArray();
                TrieNode node = root;
                node.path++;
                int index = 0;
                for (int i = 0; i < chs.Length; i++)
                {
                    index = chs[i] - 'a';
                    if (node.map[index].path-- == 1) // 对路径一路删下去
                    {
                        node.map[index] = null;
                        return;
                    }
                    node = node.map[index];
                }
                node.end--; // 删除终点数
            }
        }

        // 搜索
        public bool search(string word)
        {
            if (word == null)
                return false;
            char[] chs = word.ToCharArray();
            TrieNode node = root;
            int index = 0;
            for (int i = 0; i < chs.Length; i++)
            {
                index = chs[i] - 'a';
                if (node.map[index] == null)
                {
                    return false;
                }
                node = node.map[index];// 取下一个node
            }
            return node.end != 0; // 非end， 则不存在
        }
        // 前缀数
        public int prefixNumber(string pre)
        {
            if (pre == null)
                return 0;
            char[] chs = pre.ToCharArray();
            TrieNode node = root;
            int index = 0;
            for (int i = 0; i < chs.Length; i++)
            {
                index = chs[i] - 'a';
                if (node.map[index] == null)
                    return 0;
                node = node.map[index];
            }
            return node.path;
        }

        public bool hasKeyWithPattern(string pattern)
        {
            return hasKeyWithPattern(root, pattern, 0);
        }

        private bool hasKeyWithPattern(TrieNode node, string pattern, int i)
        {
            if (node == null)
                return false;

            if (i == pattern.Length)
            {
                // 模式串走到了头， 看看匹配到的是否是一个键
                return node.map[i] != null;
            }
            char c = pattern[i];
            if (c != '.')
            {
                // 进入递归
                int index = c - 'a';
                return hasKeyWithPattern(node.map[index], pattern, i + 1);
            }
            for (int j = 0; j < 26; j++)
            {
                if (hasKeyWithPattern(node.map[j], pattern, i + 1))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
