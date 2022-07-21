using CDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuneProject.cls
{
    internal class Class1
    {
        // 杨辉三角1
        // 使用递归的方式进行求解
        public IList<IList<int>> Generate1(int numRows)
        {
            // base case
            if (numRows == 1)
            {
                IList<IList<int>> triangle1 = new List<IList<int>>();
                // 先把第一层装进去作为 baseCase
                IList<int> firstRow = new List<int>();
                firstRow.Add(1);
                triangle1.Add(firstRow);
                return triangle1;
            }
            // 递归生成高度为 numRows - 1 的杨辉三角 
            IList<IList<int>> triangle = Generate1(numRows - 1);

            // 根据最底层元素生成一行新元素
            IList<int> bottomRow = triangle[triangle.Count - 1];
            IList<int> newRow = new List<int>();
            newRow.Add(1);
            for (int i = 0; i < bottomRow.Count - 1; i++)
            {
                newRow.Add(bottomRow[i] + bottomRow[i + 1]);
            }
            newRow.Add(1);

            triangle.Add(newRow);

            return triangle;
        }

        // 杨辉三角
        // 使用遍历的方式进行求解
        public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if (numRows < 1)
                return res;

            List<int> firstRow = new List<int>();
            firstRow.Add(1);
            res.Add(firstRow);
            for (int i = 2; i <= numRows; i++)
            {
                IList<int> preRow = res[res.Count - 1];
                res.Add(generateNextRow(preRow));
            }
            return res;
        }

        private IList<int> generateNextRow(IList<int> preRow)
        {
            IList<int> curRow = new List<int>();
            curRow.Add(1);
            for (int i = 0; i < preRow.Count - 1; i++)
            {
                curRow.Add(preRow[i] + preRow[i + 1]);
            }
            curRow.Add(1);
            return curRow;
        }

        // 树的右视角
        public IList<int> RightSideView(TreeNode root)
        {
            List<int> res = new List<int>();
            if (root == null)
                return res;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                int sz = q.Count;

                TreeNode last = q.Peek();
                for (int i = 0; i < sz; i++)
                {
                    TreeNode cur = q.Dequeue();
                    if (cur.right != null)
                        q.Enqueue(cur.right);
                    if (cur.left != null)
                        q.Enqueue(cur.left);
                }
                // 每一层的最后一个结点就是二叉树的右侧视图
                res.Add(last.val);
            }
            return res;
        }

        public static void printReverse(char[] str)
        {
            helper(0, str);
        }

        private static void helper(int index, char[] str)
        {
            if (str == null || index >= str.Length)
            {
                return;
            }

            helper(index + 1, str);
            Console.WriteLine(str[index]);
        }

        // 2, 1, 2, 4, 3
        public static int[] nextGreaterElement(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            Stack<int> s = new Stack<int>(); // s 存的是一堵高矮墙

            for (int i = n - 1; i >= 0; i--)
            {
                while (s.Count != 0 && s.Peek() <= nums[i]) // 没有高矮墙的阻拦
                {
                    s.Pop();                                // 不停的做pop操作
                }
                // res
                res[i] = s.Count == 0 ? -1 : s.Peek();      // 直到为0时， 返回-1 或 Peek() 出来相应的值

                s.Push(nums[i]);                            // 使用nums[i] 对高矮墙进行初始化
            }
            return res;
        }
    }
}
