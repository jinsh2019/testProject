using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuneProject.cls
{
    internal class Class2
    {

        // kv -- 记录 (subTree Path + freq)
        Dictionary<string, int> memo = new Dictionary<string, int>();
        // res
        IList<TreeNode> res = new List<TreeNode>();


        // 遍历 backtracking  v 
        // 问题分解 dp        x
        public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
        {
            traverse(root);
            return res;
        }

        string traverse(TreeNode root)
        {
            if (root == null)
                return "#";

            // 迭代关系
            string left = traverse(root.left);  // #|#,#,3
            string right = traverse(root.right);// #|#

            string subTree = left + "," + right + "," + root.val;// #,#,3|#,#,3,#
            int freq = 0;
            if (memo.ContainsKey(subTree))
                freq = memo[subTree];
            if (freq == 1)
            {
                res.Add(root);// 3|2
            }
            memo[subTree] = freq + 1;
            return subTree;
        }
    }
}
