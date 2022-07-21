using CDS;
using System.Text;

namespace July.algorithms.day2
{
    internal class SerializeTree
    {
        string SEP = ",";
        string NULL = "#";

        public string serialize(TreeNode root)
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

        public TreeNode deserialize(string data) {
            LinkedList<string> nodes = new LinkedList<string>();
            foreach (string s in data.Split(",")) {
                nodes.AddLast(s);
            }
            return deserialize(nodes);
        }

        private TreeNode deserialize(LinkedList<string> nodes)
        {
            if (nodes.Count == 0)
                return null;

            string first = nodes.First();
            nodes.RemoveFirst();

            if (first == NULL)
            {
                return null;
            }
            TreeNode root = new TreeNode(int.Parse(first));
            root.left = deserialize(nodes);
            root.right = deserialize(nodes);

            return root;
        }
    }
}
