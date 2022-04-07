using System;
using System.Collections.Generic;
using static System.Console;

namespace LinearTest
{
    public class TreeNode
    {
        int data;
        TreeNode leftChild;
        TreeNode rightChild;

        public TreeNode(int data)
        {
            this.data = data;
        }

        public static TreeNode createBinaryTree(List<int?> inputList)
        {
            TreeNode node = null;
            if (inputList == null || inputList.Count == 0)
                return null;

            int? data = inputList[0];
            inputList.RemoveAt(0);
            if (data != null)
            {
                node = new TreeNode(data.Value);
                node.leftChild = createBinaryTree(inputList);
                node.rightChild = createBinaryTree(inputList);
            }
            return node;
        }

        // 前序遍历 3 2 9 10 8 4
        public static void preOrderTraveral(TreeNode node)
        {
            if (node == null)
                return;
            Write(node.data + " ");
            preOrderTraveral(node.leftChild);
            preOrderTraveral(node.rightChild);
        }

        // 中序遍历 9 2 10 3 8 4
        public static void inOrderTravel(TreeNode node)
        {
            if (node == null)
                return;
            inOrderTravel(node.leftChild);
            Write(node.data + " ");
            inOrderTravel(node.rightChild);
        }

        // 后序遍历 9 10 2 4 8 3
        public static void postOrderTravel(TreeNode node)
        {
            if (node == null)
                return;
            postOrderTravel(node.leftChild);
            postOrderTravel(node.rightChild);
            Write(node.data + " ");
        }

        public static void preOrderTravelWithStack(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode treeNode = root;
            while (treeNode != null || stack.Count != 0)
            {
                while (treeNode != null)
                {
                    Write(treeNode.data + " ");
                    stack.Push(treeNode);
                    treeNode = treeNode.leftChild;
                }

                // 如果节点没有左孩子，则弹出栈顶节点，访问节点右孩子
                if (stack.Count != 0)
                {
                    treeNode = stack.Pop();
                    treeNode = treeNode.rightChild;
                }
            }
        }

        // 层序遍历
        public static void levelOrderTraversal(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                TreeNode node = queue.Dequeue();
                Write(node.data + " ");
                if (node.leftChild != null)
                    queue.Enqueue(node.leftChild);
                if (node.rightChild != null)
                    queue.Enqueue(node.rightChild);
            }
        }
    }

    /*
              3
         2          8
      9    10   nvl    4      
     
     */
}
