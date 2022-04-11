using LinearOrNoneTest;
using System;
using System.Collections.Generic;
using static System.Console;

namespace LinearTest
{
    class Program
    {
        #region 基础结构: 数组，链表 （操作：CRUD）

        #endregion

        #region 线性结构
        // 逻辑结构：栈、队列、散列表的实现
        // 升级后的逻辑结构： 循环队列、双端队列、用栈实现队列等 ==> 实践
        #endregion

        #region 非线性结构
        // 逻辑结构：树、图的实现 (操作： CRUD)
        // 升级后的逻辑结构: 二叉堆、优先级队列
        // 二次升级的数据结构： AVL、红黑树、B/B+树

        #endregion
        static void Main(string[] args)
        {
            // 随机读取
            int[] array = new int[] { 3, 1, 2, 5, 4, 9, 7, 2 };
            WriteLine(array[3]);
            // 随机更新
            int[] array1 = new int[] { 3, 1, 2, 5, 4, 9, 7, 2 };
            array1[5] = 10;
            // 随机插入
            MyArray myArray = new MyArray(10);
            myArray.insert(0, 3);
            myArray.insert(1, 7);
            myArray.insert(2, 9);
            myArray.insert(3, 5);
            myArray.insert(1, 6);
            myArray.OutPut();
            // 随机删除
            myArray.delete(0);
            myArray.delete(0);
            myArray.delete(0);
            myArray.delete(0);
            myArray.delete(0);
            myArray.OutPut();

            // 链表的操作
            MyLinkedList myLinkedList = new MyLinkedList();
            myLinkedList.insert(3, 0);
            myLinkedList.insert(7, 1);
            myLinkedList.insert(9, 2);
            myLinkedList.insert(5, 3);
            myLinkedList.insert(6, 1);
            myLinkedList.remove(0);
            myLinkedList.output();

            // 循环链表
            MyQueue myQueue = new MyQueue(6);
            myQueue.enQueue(3);
            myQueue.enQueue(5);
            myQueue.enQueue(6);
            myQueue.enQueue(8);
            myQueue.enQueue(1);
            myQueue.deQueue();
            myQueue.deQueue();
            myQueue.deQueue();
            myQueue.deQueue();
            myQueue.deQueue();
            myQueue.enQueue(2);
            myQueue.enQueue(4);
            myQueue.enQueue(9);
            myQueue.output();

            // no-linear 
            List<int?> inputList = new List<int?>() { 3, 2, 9, null, null, 10, null, null, 8, null, 4 };
            var treeNode = TreeNode.createBinaryTree(inputList);
            // 前中后
            TreeNode.preOrderTraveral(treeNode);
            WriteLine();
            TreeNode.inOrderTravel(treeNode);
            WriteLine();
            TreeNode.postOrderTravel(treeNode);
            WriteLine();
            // 非递归使用堆栈的遍历
            TreeNode.preOrderTravelWithStack(treeNode);
            WriteLine();
            // 层级遍历
            TreeNode.levelOrderTraversal(treeNode);
            WriteLine();

            // 二叉堆
            int[] array10 = new int[] { 1, 3, 2, 6, 5, 7, 8, 9, 10, 0 };
            MyStack.heapSort(array10);

            // 图
            Graph graph = new Graph(6);

            graph.adj[0].Add(1);
            graph.adj[0].Add(2);
            graph.adj[0].Add(3);

            graph.adj[1].Add(0);
            graph.adj[1].Add(3);
            graph.adj[1].Add(4);

            graph.adj[2].Add(0);

            graph.adj[3].Add(0);
            graph.adj[3].Add(1);
            graph.adj[3].Add(4);
            graph.adj[3].Add(5);

            graph.adj[4].Add(1);
            graph.adj[4].Add(3);
            graph.adj[4].Add(5);

            graph.adj[5].Add(3);
            graph.adj[5].Add(4);

            Graph.dfs(graph, 0, new bool[graph.size]);
            WriteLine();
            Graph.bfs(graph, 0, new bool[graph.size], new Queue<int>());
            WriteLine();

            WriteLine("Hello World!");
        }
    }
}
