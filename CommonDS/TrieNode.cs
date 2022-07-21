using System;
using System.Collections.Generic;
using System.Text;

namespace CDS
{
    // 前缀树
    // Trie 树节点实现
    public class TrieNode<T>
    {
        public T val { get; set; }
        public TrieNode<T>[] children = new TrieNode<T>[26];
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
