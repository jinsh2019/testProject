using System;
using System.Globalization;
using static System.Console;

namespace MatrixProject
{
    class Program
    {

        #region 转圈打印矩阵
        /// <summary>
        /// (tR,tC) 左上角的点， (dR,dC) 右下角的点 
        /// </summary>
        /// <param name="matrix"></param>
        public static void spiralOrderPrint(int[][] matrix)
        {
            int tR = 0;
            int tC = 0;
            int dR = matrix.Length - 1;
            int dC = matrix[0].Length - 1;
            while (tR <= dR && tC <= dC)
            {
                printEdge(matrix, tR++, tC++, dR--, dC--);
            }
        }

        private static void printEdge(int[][] m, int tR, int tC, int dR, int dC)
        {
            if (tR == dR) // 子矩阵只有一行
            {
                for (int i = tC; i <= dC; i++)
                {
                    Write(m[tR][i] + " ");
                }
            }
            else if (tC == dC) // 子矩阵只有一列
            {
                for (int i = tR; i <= dR; i++)
                {
                    Write(m[i][tC] + " ");
                }
            }
            else // 一般情况
            {
                int curC = tC;
                int curR = tR;
                while (curC != dC)
                {
                    Write(m[tR][curC] + " ");
                    curC++;
                }
                while (curR != dR)
                {
                    Write(m[curR][dC] + " ");
                    curR++;
                }
                while (curC != tC)
                {
                    Write(m[dR][curC] + " ");
                    curC--;
                }
                while (curR != tR)
                {
                    Write(m[curR][tC] + " ");
                    curR--;
                }
            }
        }
        #endregion

        #region 将正方形矩阵顺时针转动90°
        public static void rotate(int[][] matrix)
        {
            int tR = 0;
            int tC = 0;
            int dR = matrix.Length - 1;
            int dC = matrix[0].Length - 1;

            while (tR < dR)
            {
                rotateEdge(matrix, tR++, tC++, dR--, dC--);
            }
        }

        private static void rotateEdge(int[][] m, int tR, int tC, int dR, int dC)
        {
            int times = dC - tC;
            int tmp = 0;
            for (int i = 0; i != times; i++)
            {
                tmp = m[tR][tC + i];
                m[tR][tC + i] = m[dR - i][tC]; // 从左下角开始往上4次
                m[dR - i][tC] = m[dR][dC - i]; // 从右下角开始往左4次
                m[dR][dC - i] = m[tR + i][dC]; // 从右上角开始往下4次
                m[tR + i][dC] = tmp;
            }
        }
        #endregion

        #region 之字形打印矩阵

        #endregion
        static void Main(string[] args)
        {
            int[] p = { 1, 2, 3, 4 };
            int[] p1 = { 5, 6, 7, 8 };
            int[] p2 = { 9, 10, 11, 12 };
            int[] p3 = { 13, 14, 15, 16 };
            int[][] matrix = { p, p1, p2, p3 };
            //int[] p = { 3 };
            //int[] p1 = { 4 };
            //int[][] matrix = { p, p1 };
            spiralOrderPrint(matrix);
            rotate(matrix);
            WriteLine("Hello World!");
        }
    }
}
