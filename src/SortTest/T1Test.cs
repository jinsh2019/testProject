using CDS;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace SortTest
{
    internal class T1
    {
        /// <summary>
        /// 99. 恢复二叉搜索树
        /// 分析： 找到第一个与第二个错误节点，然后进行交换
        /// 1. 前一个节点
        /// 2. 定义第一，第二个错误节点
        /// 3. 记录第一个不健康节点
        /// 4. 不停更新前一个节点
        /// </summary>
        /// <param name="root"></param>
        TreeNode prev = new TreeNode(int.MinValue);     // 前一个节点
        TreeNode first, second;                         // 定义第一，第二个错误节点
        public void RecoverTree(TreeNode root)
        {
            helper(root);
            // 第一个节点与第二个节点进行交换
            int temp = first.val;
            first.val = second.val;
            second.val = temp;
        }

        private void helper(TreeNode root)
        {
            if (root == null) return;

            helper(root.left);
            if (root.val < prev.val)
            {
                if (first == null)          // 记录第一个不健康节点
                    first = prev;
                second = root;              // 不停更新，找到第二个不健康节点
            }
            prev = root;                    // 不停更新前一个节点
            helper(root.right);
        }


        /// <summary>
        /// 450. 删除二叉搜索树中的节点
        /// 分析： 根据二叉搜索树的特点，小的在右，大的在左，查找key值
        /// 找到后分三种情况
        /// 1. 这个节点是叶子节点
        /// 2. 这个节点只有一个叶子
        /// 3. 这个节点有两个叶子： 将右子树的最小值节点顶替删除的节点
        ///     3.1 获取最小节点
        ///     3.2 递归删除最小节点，获取删后的树作为右子树
        ///     3.3 使用minNode来替换root节点的左右子树
        ///     3.4. root指向minNode
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if (root == null)
                return null;

            if (key < root.val)
            {
                root.left = DeleteNode(root.left, key);
            }
            else if (key > root.val)
            {
                root.right = DeleteNode(root.right, key);
            }
            else
            {
                // 1. leaf 
                if (root.left == null && root.right == null)
                    return null;
                // 2. one leaf
                if (root.left == null)
                    return root.right;
                if (root.right == null)
                    return root.left;
                // 3. 2 leaves
                //  3.1  将右子树的最小值节点顶替删除的节点
                TreeNode minNode = getMin(root.right);              // 获取最小节点
                root.right = DeleteNode(root.right, minNode.val);   // 递归删除最小节点，获取删后的树作为右子树
                minNode.left = root.left;                           // 使用minNode来替换root节点的左右子树
                minNode.right = root.right;
                root = minNode;                                     // root指向minNode
            }

            return root;
        }

        private TreeNode getMin(TreeNode node)
        {
            while (node.left != null)
                node = node.left;

            return node;

        }



        /// <summary>
        /// 752. 打开转盘锁
        /// BFS
        /// </summary>
        /// <param name="deadends"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int OpenLock(string[] deadends, string target)
        {
            HashSet<string> deadset = new HashSet<string>(deadends);
            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();

            int step = 0;
            queue.Enqueue("0000");
            visited.Add("0000");

            while (queue.Count != 0)
            {
                int size = queue.Count;

                for (int i = 0; i < size; i++)
                {
                    string cur = queue.Dequeue();

                    if (deadset.Contains(cur))
                        continue;
                    if (cur.Equals(target))
                        return step;

                    for (int j = 0; j < 4; j++)
                    {
                        string up = PlusOne(cur, j);    // 第j个数字+1
                        if (!visited.Contains(up))
                        {
                            queue.Enqueue(up);
                            visited.Add(up);
                        }
                        string down = MinusOne(cur, j); // 第j个数字-1
                        if (!visited.Contains(down))
                        {
                            queue.Enqueue(down);
                            visited.Add(down);
                        }
                    }
                }
                step++;
            }
            return -1;
        }
        // -1
        private string MinusOne(string s, int j)
        {
            char[] chs = s.ToCharArray();
            if (chs[j] == '0')
                chs[j] = '9';
            else
                chs[j] = (char)(chs[j] - 1);

            return new string(chs);
        }
        //+1
        private string PlusOne(string s, int j)
        {
            char[] chs = s.ToCharArray();
            if (chs[j] == '9')
                chs[j] = '0';
            else
                chs[j] = (char)(chs[j] + 1);
            return new string(chs);
        }

        /// <summary>
        /// 168. Excel表列名称
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public string ConvertToTitle(int columnNumber)
        {
            StringBuilder sb = new StringBuilder();
            while (columnNumber > 0)
            {
                int a0 = (columnNumber - 1) % 26 + 1;    // 0%26 + 1= 0+1 = 1  |||| 1%26+1=1+1=2
                sb.Append((char)(a0 - 1 + 'A'));         // 0+'A' || 1+'A'
                columnNumber = (columnNumber - a0) / 26; // 从后往前数
            }
            StringBuilder columnTitle = new StringBuilder();
            for (int i = sb.Length - 1; i >= 0; i--)
                columnTitle.Append(sb[i]);

            return columnTitle.ToString();
        }

        /// <summary>
        /// 171. Excel 表列序号
        /// </summary>
        /// <param name="columnTitle"></param>
        /// <returns></returns>
        public int TitleToNumber(string columnTitle)
        {
            int number = 0;
            int multiple = 1;
            for (int i = columnTitle.Length - 1; i >= 0; i--)
            {
                int k = columnTitle[i] - 'A' + 1;           // 'A'-'A'+1 || 'B'-'A'+1
                number += k * multiple;
                multiple *= 26;                             // 26 进制
            }
            return number;
        }

        /// <summary>
        /// 81. 搜索旋转排序数组 II
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Search(int[] nums, int target)
        {
            int l = 0, r = nums.Length - 1;
            while (l <= r)
            {
                int mid = l + (r - l) / 2;
                if (nums[mid] == target)
                    return true;

                if (nums[mid] > nums[l])
                {
                    if (nums[mid] > target && nums[l] <= target) // t in [l,m)    缩小范围 r =mid-1
                        r = mid - 1;
                    else
                        l = mid + 1;                            // t not in [l,m) 缩小范围 l =mid+1
                }
                else if (nums[mid] < nums[l])
                {                                               // [l..mid) 搜索 t
                    if (nums[mid] < target && nums[r] >= target)
                        l = mid + 1;                            // t in (m,r]     缩小范围 l =mid+1
                    else
                        r = mid - 1;                            // t not in (m,r] 缩小范围 r= mid-1 
                }
                else
                {
                    l++;                                        // 去重
                }
            }
            return false;
        }

        /// <summary>
        /// 33. 搜索旋转排序数组
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int Search1(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int l = 0;
            int r = nums.Length - 1;
            int mid = 0;
            while (l <= r)
            {
                mid = l + (r - l) / 2;
                if (nums[mid] == target) return mid;

                if (nums[mid] >= nums[l])
                {                                                   // [l..mid) 搜索 t
                    if (target >= nums[l] && target < nums[mid])
                        r = mid - 1;                                // t in [l,m) 缩小范围 r =mid-1
                    else
                        l = mid + 1;                                // 缩小范围 r=mid+1
                }
                else
                {                                                   // (mid,r] 搜索 t
                    if (target <= nums[r] && target > nums[mid])
                        l = mid + 1;                                // t in (m,r] 缩小范围 l = mid+1
                    else
                        r = mid - 1;                                // 缩小范围 r= mid-1
                }
            }
            return -1;
        }
    }// class end


    public class Codec
    {
        string SEP = ",";
        string NULL = "#";


        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            serialize(root, sb);
            return sb.ToString();
        }

        private void serialize(TreeNode root, StringBuilder sb)
        {
            if (root == null)
            {
                sb.Append(NULL).Append(SEP);
                return;
            }

            sb.Append(root.val).Append(SEP);        // preOrder traverse
            serialize(root.left, sb);
            serialize(root.right, sb);
        }

        public TreeNode deserialize(string data)
        {
            List<string> list = new List<string>();
            string[] nodes = data.Split(SEP);
            for (int i = 0; i < nodes.Length; i++)
                list.Add(nodes[i]);

            return deserialize(list);
        }

        private TreeNode deserialize(List<string> list)
        {
            if (list.Count == 0)
                return null;

            string first = list[0];
            list.RemoveAt(0);
            if (first == NULL)                              // leaf 
                return null;

            TreeNode root = new TreeNode(int.Parse(first)); // preOrder traverse
            root.left = deserialize(list);
            root.right = deserialize(list);

            return root;
        }
    }
}
