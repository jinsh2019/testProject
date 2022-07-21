using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July.daily
{
    internal class day13
    {

        // 记录所有合法的括号组合
        List<string> res = new List<string>();
        // 回溯过程中的路径
        LinkedList<char> track = new LinkedList<char>();
        public IList<string> GenerateParenthesis(int n)
        {
            if (n == 0)
                return new List<string>();

            backtrack(n, n);
            return res;
        }
        // left 左括号数， right 有括号数
        void backtrack(int left, int right)
        {

            // base Case
            if (right < left) 
                return;
            if (left < 0 || right < 0) 
                return;

            if (left == 0 && right == 0) // 左右括号使用完
            {
                res.Add(new string(track.ToArray()));
                return;
            }

            track.AddLast('('); // 不停加((((
            backtrack(left - 1, right);
            track.RemoveLast();// (((

            track.AddLast(')');
            backtrack(left, right - 1);// ))))
            track.RemoveLast(); // )))
        }

        public void SortColors(int[] nums)
        {
            int len = nums.Length;
            if (len < 2)
                return;
            // all in [0,p0] == 0
            // all in (p0, i) == 1
            // all in [p2,len-1] ==2
            int p0 = -1; // 占位指针
            int i = 0;  // 从第一个元素开始
            int p2 = len - 1;  // 从最后一个元素开始
            while (i <= p2)
            {
                if (nums[i] == 0)
                {
                    p0++;
                    swap(nums, i, p0);
                    i++;
                }
                else if (nums[i] == 1)
                {
                    i++;
                }
                else 
                {
                    //if (nums[i] == 2)
                    swap(nums, i, p2);
                    p2--;
                }
            }
        }

        void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        // 杨辉三角
        // 定义：输入行数，返回该行的杨辉三角数列
        public IList<int> GetRow(int rowIndex)
        {
            IList<int> curRow = new List<int>();
            // 每一行开头是 1
            curRow.Add(1);

            // base case
            if (rowIndex == 0) return curRow; // 第一行,连尾巴都没有

            // 递归计算出上一行；中间过程：上一行的i位置+(i+1)位置等于本行的i位置
            IList<int> preRow = GetRow(rowIndex - 1);
            for (int i = 0; i < preRow.Count - 1; i++)
            {
                curRow.Add(preRow[i] + preRow[i + 1]);
            }
            // 每一行结尾是 1
            curRow.Add(1); //  第二行， 有头也有尾（1,1）

            return curRow;
        }
    }


}
