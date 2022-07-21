namespace July.algorithms.@base
{
    public class TrieNode
    {
        // 有多少单词共用这个节点
        public int path { get; set; }
        // 有多少单子以这个节点结尾
        public int end { get; set; }
        // 哈希结构，key代表该节点的一条字符路径； value代表字符路径指向的节点
        public TrieNode[] map { get; set; }

        public TrieNode()
        {
            path = 0;
            end = 0;
            map = new TrieNode[26];
        }
    }

    public class Trie
    {
        private TrieNode root { get; set; }

        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(string word)
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
                node = node.map[index]; // next node
                node.path++;
            }
            node.end++;
        }

        public void Delete(string word)
        {
            if (Search(word))
            {
                char[] chs = word.ToCharArray();
                TrieNode node = root;
                node.path++;
                int index = 0;
                for (int i = 0; i < chs.Length; i++)
                {
                    index = chs[i] - 'a';
                    if (node.map[index].path-- == 1)
                    {
                        node.map[index] = null;
                        return;
                    }
                    node = node.map[index];
                }
                node.end--;
            }
        }

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
                {
                    return false;
                }
                node = node.map[index];
            }
            return node.end != 0;
        }

        // 前缀的数量
        public int PrefixNumber(string pre)
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
                {
                    return 0;
                }
                node = node.map[index];
            }
            return node.path;
        }
    }
}
