using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    internal class day13
    {
        //48. 旋转图像 顺时针旋转
        public void Rotate(int[][] matrix)
        {
            int n = matrix.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = temp;
                }
            }

            foreach (var row in matrix)
            {
                reverse(row);
            }
        }

        private void reverse(int[] arr)
        {
            int i = 0, j = arr.Length - 1;
            while (i < j)
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;
                j--;
            }
        }

        // 逆时针旋转
        private void Rotate2(int[][] matrix)
        {
            int n = matrix.GetLength(0);
            // 沿左下到右上的对角线镜像对称二维矩阵
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[n - j - 1][n - i - 1];
                    matrix[n - j - 1][n - i - 1] = temp;
                }
            }

            foreach (int[] row in matrix)
            {
                reverse(row);
            }
        }

        // 54. 螺旋矩阵
        public IList<int> SpiralOrder(int[][] matrix)
        {
            int m = matrix.Length, n = matrix[0].Length;
            int upper_bound = 0, lower_bound = m - 1;
            int left_bound = 0, right_bound = n - 1;

            List<int> res = new List<int>();

            while (res.Count < m * n)
            {
                // 在顶部从左向右遍历
                if (upper_bound <= lower_bound)
                {
                    for (int j = left_bound; j <= right_bound; j++)
                    {
                        res.Add(matrix[upper_bound][j]);
                    }
                    // 上边界下移
                    upper_bound++;
                }
                // 在右侧从上向下遍历
                if (left_bound <= right_bound)
                {
                    for (int i = upper_bound; i <= lower_bound; i++)
                    {
                        res.Add(matrix[i][right_bound]);
                    }
                    // 右边界左移
                    right_bound--;
                }
                // 在底部从右向左遍历
                if (upper_bound <= lower_bound)
                {
                    for (int j = right_bound; j >= left_bound; j--)
                    {
                        res.Add(matrix[lower_bound][j]);
                    }
                    // 下边界上移
                    lower_bound--;
                }
                // 在左侧从下向上遍历
                if (left_bound <= right_bound)
                {
                    for (int i = lower_bound; i >= upper_bound; i--)
                    {
                        res.Add(matrix[i][left_bound]);
                    }
                    // 左边界右移
                    left_bound++;
                }
            }

            return res;

        }


        //59. 螺旋矩阵 II
        public int[][] GenerateMatrix(int n)
        {
            int[][] matrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                matrix[i] = new int[n];
            }

            int upper_bound = 0, lower_bound = n - 1;
            int left_bound = 0, right_bound = n - 1;

            int num = 1;
            while (num <= n * n)
            {
                if (upper_bound <= lower_bound)
                {
                    for (int j = left_bound; j <= right_bound; j++)
                    {
                        matrix[upper_bound][j] = num++;
                    }
                    upper_bound++;
                }

                if (left_bound <= right_bound)
                {
                    for (int i = upper_bound; i <= lower_bound; i++)
                    {
                        matrix[i][right_bound] = num++;
                    }
                    right_bound--;
                }

                if (upper_bound <= lower_bound)
                {
                    for (int j = right_bound; j >= left_bound; j--)
                    {
                        matrix[lower_bound][j] = num++;
                    }
                    lower_bound--;
                }

                if (left_bound <= right_bound)
                {
                    for (int i = lower_bound; i >= upper_bound; i--)
                    {
                        matrix[i][left_bound] = num++;
                    }
                    left_bound++;
                }
            }

            return matrix;
        }
    }
}
