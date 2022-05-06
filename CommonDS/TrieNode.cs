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
        public TrieNode<T>[] children = new TrieNode<T>[256];
    }
}
