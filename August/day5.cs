using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace August
{
    internal class day5
    {
        public int FindDuplicate(int[] nums)
        {
            int slow = 0, fast = 0;
            do
            {
                slow = nums[slow];
                fast = nums[nums[fast]];
            } while (slow != fast);  // 第一次相遇，fast是两倍速度

            slow = 0;
            while (slow != fast)
            {
                slow = nums[slow];
                fast = nums[fast];
            } // 第二次相遇，fast与slow相同速度
            return slow;
        }

        // 中序遍历的下一个(后继)节点
        public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
        {
            if (root == null)
                return null;

            TreeNode successor = null;
            if (root.val > p.val)
            {
                successor = InorderSuccessor(root.left, p);
                // 父节点收到 null 的话说明自己是 successor
                if (successor == null)  
                    successor = root;
            }
            if (root.val < p.val)
            {
                successor = InorderSuccessor(root.right, p);
            }
            if (root.val == p.val)
            {
                // 我是目标节点，
                // 我的 successor 要么是右子树的最小节点，要么是父节点
                successor = getMinNode(root.right);
            }

            return successor;
        }
        // BST 中最左侧的节点就是最小节点
        TreeNode getMinNode(TreeNode p)
        {
            while (p != null && p.left != null)
            {
                p = p.left;
            }
            return p;
        }
    }
}
