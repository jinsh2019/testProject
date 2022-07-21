using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace CDS
{
    public static class CommonHelper
    {
        // 构造链表 1-> 2 -> 3 -> 4
        public static ListNode BuildLinkNode(int[] arr)
        {
            if (arr == null)
                return null;

            ListNode lnode = null;
            for (int i = arr.Length - 1; i >= 0; i--)
                lnode = lnode == null ? new ListNode(arr[i]) : new ListNode(arr[i], lnode);
            return lnode;
        }


        /// <summary>
        /// 构造二维数组
        /// </summary>
        /// <param name="my2DArray">输入多维数组</param>
        /// <returns></returns>
        public static int[,] BuildTo2D(int[][] my2DArray)
        {
            if (my2DArray == null)
                return null;

            int m = my2DArray.GetLength(0);
            int n = my2DArray[0].GetLength(0);
            int[,] ans = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ans[i, j] = my2DArray[i][j];
                }
            }
            return ans;
        }

        public static Node BuildNode(int[] arr)
        {
            if (arr == null)
                return null;

            Node node = null;
            for (int i = arr.Length - 1; i >= 0; i--)
                node = node == null ? new Node(arr[i]) : new Node(arr[i], null, null, null);
            return node;
        }

        /// <summary>
        /// 打印二维数组
        /// </summary>
        /// <param name="board"></param>
        public  static void Print2DMatrix(char[][] board)
        {
            int m = board.Length;
            
            for (int i = 0; i < m; i++)
            {
                int n = board[i].Length;
                for (int j = 0; j < n; j++)
                {
                    Write(board[i][j] + ",");
                }
                WriteLine();
            }
        }

        /// <summary>
        /// 打印二维数组
        /// </summary>
        /// <param name="board"></param>
        public static void Print2DMatrix(int[][] board)
        {
            int m = board.Length;
            for (int i = 0; i < m; i++)
            {
                int n = board[i].Length;
                for (int j = 0; j < n; j++)
                {
                    Write(board[i][j] + ",");
                }
                WriteLine();
            }
        }
    }
}
