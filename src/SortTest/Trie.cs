namespace SortTest
{
    public class TrieNode
    {
        public int path;
        public int end;
        public TrieNode[] map { get; set; }
        public TrieNode()
        {
            path = end = 0;
            map = new TrieNode[26];
        }
    }
    /// <summary>
    /// 208. 实现 Trie (前缀树)
    /// </summary>
    public class Trie
    {
        private TrieNode root;
        public Trie()
        {
            root = new TrieNode();
        }

        /// <summary>
        /// 检索全文
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Search(string word)
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
                    return false;
                node = node.map[index];                     // 获取节点
            }

            return node.end != 0;                           // 字典中存在的条件时end != 0
        }
        /// <summary>
        /// 检索前缀
        /// </summary>
        /// <param name="pre"></param>
        /// <returns></returns>
        public bool StartsWith(string pre)
        {
            if (pre == null) return false;
            char[] chs = pre.ToCharArray();
            TrieNode node = root;
            int index = 0;
            for (int i = 0; i < chs.Length; i++)
            {
                index = chs[i] - 'a';
                if (node.map[index] == null)
                    return false;
                node = node.map[index];
            }

            return node.path != 0;
        }

        public void Insert(string word)
        {
            if (word == null) return;
            char[] chs = word.ToCharArray();
            TrieNode node = root;
            int index = 0;
            node.path++;                                // root.path 自增1
            for (int i = 0; i < chs.Length; i++)
            {
                index = chs[i] - 'a';  
                if (node.map[index] == null)
                    node.map[index] = new TrieNode();
                node = node.map[index];                 // 获取node节点
                node.path++;                        
            }
            node.end++;                                 //  word的最后一个node.end 自增1
        }

        public void Delete(string word)
        {
            if (Search(word))
            {
                char[] chs = word.ToCharArray();
                TrieNode node = root;
                int index = 0;
                node.path--;                            // root.path 自减1
                for (int i = 0; i < chs.Length; i++)
                {
                    index = chs[i] - 'a';
                    if (node.map[index].path == 1)      // 路径仅剩下1时
                    {
                        node.map[index] = null;
                        return;
                    }
                    node = node.map[index];             // 获取node节点
                }
                node.end--;
            }
        }
    }
}
