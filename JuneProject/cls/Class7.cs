using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDS;
using static System.Console;
using static CDS.CommonHelper;
namespace JuneProject.cls
{

    internal class Class7
    {
        // x 的 n 次方
        // 之后会出现栈溢出问题
        // 使用2的2的2次方，判断奇偶性，即可
        // 使用2进制位进行判定
        public double MyPow(double x, int n)
        {
            long N = n;
            return N > 0 ? quickMul(x, N) : 1.0 / quickMul(x, -N);
        }

        public double quickMul(double x, long N)
        {
            double ans = 1.0;
            double x_contribute = x;

            while (N > 0)
            {
                if (N % 2 == 1)
                {
                    // 如果 N 二进制表示的最低位为 1，那么需要计入贡献 
                    ans *= x_contribute;
                }
                // 将贡献不断地平方
                x_contribute *= x_contribute;
                // 舍弃 N 二进制表示的最低位，这样我们每次只要判断最低位即可
                N /= 2; // 4/2= 2; 2/2=1
            }
            return ans;
        }

        // 归并排序
        public int[] sortArray(int[] nums)
        {
            int len = nums.Length;
            int[] temp = new int[len];
            mergeSort(nums, 0, len - 1, temp);
            return nums;
        }

        private void mergeSort(int[] nums, int left, int right, int[] temp)
        {
            // 1. 递归终止条件
            if (left == right)
            {
                return;
            }
            // 2. 拆分，对应「分而治之」算法的「分」
            int mid = (left + right) / 2;

            mergeSort(nums, left, mid, temp);
            mergeSort(nums, mid + 1, right, temp);

            // 合并两个有序数组，对应「分而治之」的「合」
            mergeOfTwoSortedArray(nums, left, mid, right, temp);


        }

        private void mergeOfTwoSortedArray(int[] nums, int left, int mid, int right, int[] temp)
        {
            for (int t = left; t <= right; t++)
            {
                temp[t] = nums[t];
            }

            int i = left;
            int j = mid + 1;
            int k = left;
            while (i <= mid && j <= right)
            {
                if (temp[i] <= temp[j])
                {
                    // 注意写成 < 就丢失了稳定性（相同元素原来靠前的排序以后依然靠前）
                    nums[k] = temp[i];
                    k++;
                    i++;
                }
                else
                {
                    nums[k] = temp[j];
                    k++;
                    j++;
                }
            }

            while (j <= mid)
            {
                nums[k] = temp[i];
                k++;
                i++;
            }
            while (j <= right)
            {
                nums[k] = temp[j];
                k++;
                j++;
            }
        }

        int level = 0;
        // 数独问题
        public void backtrack(char[][] board, int i, int j)
        {
            int m = 9, n = 9;
            if (j == n) {
                return;
            }
            WriteLine("levels:" + j);
            for (char ch = '1'; ch <= '9'; ch++)
            {
                // 做选择
                board[i][j] = ch;
                #region log
                //WriteLine("做选择");
                //Print2DMatrix(board); 
                #endregion
                // 继续穷举下一个
                backtrack(board, i, j + 1);
                // 撤销选择
                board[i][j] = '.';
                #region log
                //Print2DMatrix(board);
                //WriteLine("撤销选择"); 
                #endregion
            }
        }

        // 36. 有效的数独，需要可视化
        public bool IsValidSudoku(char[][] board)
        {
            int[,] rows = new int[9, 9];   // 每一行每个数字的出现次数
            int[,] colums = new int[9, 9]; // 每一列每个数字出现的次数
            int[,,] subboxes = new int[3, 3, 9]; // 小九宫格中出出现的次数
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char c = board[i][j];
                    if (c != '.')
                    {
                        int index = c - '0' - 1;
                        rows[i, index]++;   // 0行，8出现的次数为1; 
                        colums[j, index]++; // 0列，8出现的次数为1
                        subboxes[i / 3, j / 3, index]++; // 小九宫格中出出现的次数
                        if (rows[i, index] > 1 || colums[j, index] > 1 ||
                        subboxes[i / 3, j / 3, index] > 1)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        // 设置矩阵为0， 需要可视化
        // 73. 矩阵置零
        // 深度遍历所有的元素
        // 从(0,0)(0,1)，到(1,0), 直到(m-1,n-1)
        public void SetZeroes(int[][] matrix)
        {
            int m = matrix.Length, n = matrix[0].Length;
            bool[] row = new bool[m];
            bool[] col = new bool[n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        row[i] = col[j] = true;
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (row[i] || col[j])
                    {
                        matrix[i][j] = 0;
                    }
                }
            }
        }

        public ListNode ReverseList1(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            ListNode last = ReverseList1(head.next);
            head.next.next = head; // head.next.next -> head
            head.next = null; // 一直到head.next 到老head 指向null
            return last; // 为了返回 newHead
        }


        // 使用dummy
        //  dummy 的next指针指向head
        // pre 指针指向dummy
        // 如果： pre.next.val == val == > pre.next = pre.next.next 删除
        // 否则:  pre= pre.next pre往前走
        // 返回: dummy.next
        public ListNode RemoveElements(ListNode head, int val)
        {
            //创建一个虚拟头结点
            ListNode dummyNode = new ListNode();
            dummyNode.next = head;
            ListNode prev = dummyNode;
            //确保当前结点后还有结点
            while (prev.next != null)
            {
                if (prev.next.val == val)
                {
                    prev.next = prev.next.next;
                }
                else
                {
                    prev = prev.next;
                }
            }
            return dummyNode.next;
        }
    }
}

