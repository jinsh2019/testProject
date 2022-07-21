using CDS;
using System.Text;

namespace July.algorithms.day4
{
    internal class SerializeTree
    {
        string SEP = ",";
        string NULL = "#";

        public string Serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            serialize(root, sb);
            return sb.ToString();
        }

        private void serialize(TreeNode root, StringBuilder sb)
        {
            if (root == null)
            {
                sb.Append(NULL).Append(SEP);
                return;
            }

            sb.Append(root.val).Append(SEP);
            serialize(root.left, sb);
            serialize(root.right, sb);
        }

        public TreeNode Deserialize(string data)
        {
            LinkedList<string> nodes = new LinkedList<string>();
            foreach (string s in data.Split(","))
            {
                nodes.AddLast(s);
            }
            return deserialize(nodes);
        }

        public TreeNode deserialize(LinkedList<string> nodes)
        {
            if (nodes.Count == 0)
                return null;

            string first = nodes.First();
            nodes.RemoveFirst();

            if (first == NULL) // 遇到NULL 就返回了
                return null;

            TreeNode root = new TreeNode(int.Parse(first));
            root.left = deserialize(nodes);
            root.right = deserialize(nodes);
            return root;
        }
    }
}
