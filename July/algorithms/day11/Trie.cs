namespace July.algorithms.day11
{
    public class TireNode
    {
        public int path { get; set; }
        public int end { get; set; }
        public TireNode[] map { get; set; }
        public TireNode()
        {
            path = end = 0;
            map = new TireNode[26];
        }
    }
    internal class Tire
    {
        private TireNode root { get; set; }
        public Tire()
        {
            root = new TireNode();
        }
        public bool search(string word)
        {
            if (word == null)
                return false;
            char[] chs = word.ToCharArray();
            TireNode node = root;
            int index = 0;
            for (int i = 0; i < chs.Length; i++)
            {
                index = chs[i] - 'a';
                if (node.map[index] == null)
                    return false;
                node = node.map[index];
            }
            return node.end != 0;
        }

        public bool startWith(string pre)
        {
            if (pre == null)
                return false;
            char[] chs = pre.ToArray();
            TireNode node = root;
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
            return node.path != 0;
        }

        public void insert(string word)
        {
            if (word == null)
                return;
            char[] chs = word.ToCharArray();
            TireNode node = root;
            int index = 0;
            node.path++;
            for (int i = 0; i < chs.Length; i++)
            {
                index = chs[i] - 'a';
                if (node.map[index] == null)
                    node.map[index] = new TireNode();
                node = node.map[index];
                node.path++;
            }
            node.end++;
        }

        public void delete(string word) {
            if (search(word)) { 
                char[] chs = word.ToCharArray();
                TireNode node = root;
                int index = 0;
                node.path--;
                for (int i = 0; i < chs.Length; i++)
                {
                    index = chs[i] - 'a';
                    if (node.map[index].path-- == 1) {
                        node.map[index] = null;
                        return;
                    }
                    node = node.map[index];
                }
                node.end--;
            }
        }

    }
}
