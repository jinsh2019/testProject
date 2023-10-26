using CDS;
using System;

namespace SortTest
{
    /// <summary>
    /// 平衡二叉树 AVL树
    /// </summary>
    internal class AVL
    {
        public int GetHeight(TreeNode root)
        {
            if (root == null) 
                return 0;
            else 
                return root.height;
        }
        public TreeNode Insert(TreeNode root, int val)
        {
            if (root == null)
            {
                root = new TreeNode();
                root.val = val;
                root.left = null;
                root.right = null;
                root.height = 1;
            }
            else if (val < root.val)
            {
                root.left = Insert(root.left, val);
                root.height = Math.Max(GetHeight(root.left), GetHeight(root.right)) + 1;

                if (GetHeight(root.left) - GetHeight(root.right) == 2)
                {
                    if (val < root.left.val)        // LL
                    {
                        root = RotateRight(root);
                    }
                    else                            // LR
                    {
                        root = RotateLeftRight(root);
                    }
                }
            }
            else
            {
                root.right = Insert(root.right, val);
                root.height = Math.Max(GetHeight(root.left), GetHeight(root.right)) + 1;
                if (GetHeight(root.left) - GetHeight(root.right) == -2)
                {
                    if (val > root.right.val)       // RR
                    {
                        root = RotateLeft(root);
                    }
                    else
                    {                               // RL
                        root = RotateRightLeft(root);
                    }
                }
            }

            return root;
        }
        /// <summary>
        /// LL 右旋
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TreeNode RotateRight(TreeNode root)
        {
            TreeNode temp = root.left;             // 暂存L节点
            root.left = temp.right;                // L的右节点赋值给root的左
            temp.right = root;                     // 进行左旋
                                                   // 更新高度
            root.height = Math.Max(GetHeight(root.left), GetHeight(root.right)) + 1;
            temp.height = Math.Max(GetHeight(temp.left), GetHeight(temp.right)) + 1;
            return temp;
        }
        /// <summary>
        /// RR  左旋
        /// root
        ///     R
        ///  RL   RR   
        ///  
        ///      R 
        ///  root  RR
        ///     RL
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TreeNode RotateLeft(TreeNode root)
        {
            TreeNode temp = root.right;             // 暂存R节点
            root.right = temp.left;                 // 将R的左赋值给root的右
            temp.left = root;                       // 进行右旋
                                                    // 更新高度
            root.height = Math.Max(GetHeight(root.left), GetHeight(root.right)) + 1;
            temp.height = Math.Max(GetHeight(temp.left), GetHeight(temp.right)) + 1;
            return temp;
        }

        /// <summary>
        /// LR 左右旋
        ///     Root
        ///   L
        ///     LR
        ///   LR1 LR2 
        ///   
        ///     Root
        ///   LR
        /// L   LR2
        ///  LR1
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TreeNode RotateLeftRight(TreeNode root)
        {
            root.left = RotateLeft(root.left);              // 先进行右旋
            root = RotateRight(root);                       // 再对整棵树进行左旋
            return root;
        }


        /// <summary>
        /// RL 右左旋
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TreeNode RotateRightLeft(TreeNode root)
        {
            root.right = RotateRight(root.right);           // 先进行左旋
            root = RotateLeft(root);                        // 再对整棵树进行右旋
            return root;
        }
    }
}
