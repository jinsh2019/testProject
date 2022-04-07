using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Algorithm
{
    static class SearchAlgorithm
    {
        /// <summary>
        /// 入口
        /// </summary>
        public static void Show()
        {
            int[] array = new int[10];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Random(i + DateTime.Now.Millisecond).Next(100, 999);
            }

            {
                //Console.WriteLine($"自组织");
                //array.ShowSelf();
                //Console.WriteLine($"自组织 执行完毕");
                //array.SequentialSearchWithSelfOrganizaing(array[4]);
            }
            {
                //Console.WriteLine($"自组织28");
                //array.ShowSelf();
                //Console.WriteLine($"自组织28 执行完毕");
                //array.SequentialSearchWithSelfOrganizaing28(array[4]);
            }
            {
                Console.WriteLine($"二分查找");
                array.BinarySearchSelf(array[4]);
                Console.WriteLine($"二分查找 执行完毕");

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        private static void ShowSelf(this int[] arr)
        {
            foreach (var i in arr)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        #region 顺序查找 Level1 O(n)


        #endregion
        #region 自组织查找 Level2 0(n)
        /// <summary>
        /// 自组织查找法 （冒泡法）
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        public static bool SequentialSearchWithSelfOrganizaing(this int[] arr, int iNumber)
        {
            for (int index = 0; index < arr.Length - 1; index++)
            {
                if (arr[index] == iNumber)
                {
                    if (index > 0)
                    {
                        int temp = arr[index - 1];
                        arr[index - 1] = arr[index];
                        arr[index] = temp;
                        arr.ShowSelf();
                    }

                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 自组织查找法 （28原则）
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        public static bool SequentialSearchWithSelfOrganizaing28(this int[] arr, int iNumber)
        {
            for (int index = 0; index < arr.Length - 1; index++)
            {
                if (arr[index] == iNumber)
                {
                    if (index > (arr.Length * 0.2)) // 是不是在后80%-- 是的话才移动
                    {
                        if (index > 0)
                        {
                            int temp = arr[index - 1];
                            arr[index - 1] = arr[index];
                            arr[index] = temp;
                            arr.ShowSelf();
                        }

                    }
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 二分查找法 返回索引 log2n
        /// <summary>
        /// 二分查找法，先排序再查找
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BinarySearchSelf(this int[] arr, int value)
        {
            int right = arr.Length - 1;
            int left = 0;
            int middle;
            while (left <= right)
            {
                middle = (right + left) / 2;
                if (arr[middle] == value)
                {
                    return middle;
                }
                else if (value < arr[middle])
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }

            return -1;
        }
        /// <summary>
        /// 带递归的查找法
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="value"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int BinarySearchRecursion(this int[] arr, int value, int left, int right)
        {
            if (left > right)
            {
                return -1;
            }
            else
            {
                int middle = (int)(right + left) / 2;
                if (value < arr[middle])
                {
                    return arr.BinarySearchRecursion(value, left, middle - 1);
                }
                else if (value == arr[middle])
                {
                    return middle;
                }
                else
                {
                    return arr.BinarySearchRecursion(value, middle + 1, right);
                }
            }
        }


        #endregion

    }
    /// <summary>
    /// 二叉树 二叉查找树
    /// </summary>
    public class BinaryTreeDemo
    {
        private class CustomTreeNode
        {
            public int iData { get; set; }
            // public  CustomTreeNode[] Child { get; set; } // 任意数
            public CustomTreeNode Left { get; set; }
            public CustomTreeNode Right { get; set; }

            #region 查找



            #endregion

            public void Insert(int i)
            {
                CustomTreeNode newNode = new CustomTreeNode();
                newNode.iData = i;
                //if()
            }
        }
    }
    /// <summary>
    /// 时间复杂度
    /// </summary>
    class BigODemo
    {
        /// <summary>
        /// 2n+2
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        private static long Method1(int iNumber)
        {
            long lResult = 0;// 1
            int i = 0;// 1
            for (; i < iNumber; i++) //n
            {
                lResult += 1;// n
            }

            return lResult;
        }
        // 大0 描述的是随着输入值的趋势，二不是具体时间 0(n)

        /// <summary>
        /// 2n^2+2n+2
        /// O(n^2)
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        private static long Method2(int iNumber)
        {
            long lResult = 0;// 1
            for (int i = 0; i < iNumber; i++) //i++ n
            {
                for (int j = 0; j < iNumber; j++) // j++ n^2 j n=0 n
                {
                    lResult += i + j;// n^2
                }
            }

            return lResult;
        }


        /// <summary>
        /// 2^x =n  x=log2n --> O(logn)
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        private static long Method3(int iNumber)
        {
            int iResult = 1;
            while (iResult <= iNumber)
            {
                iResult = iResult * 2;// 2^x =n  x=log2n --> O(logn)
            }

            return iResult;
        }
        // 二分法，二叉树的查找方法都是log2n


        /// <summary>
        /// 2^x =n  x=log2n --> O(logn)
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        private static long Method3_1(int iNumber)
        {
            int iResult = 1;
            while (iResult <= iNumber)
            {
                iResult = iResult * 3;// 3^x =n  x=log3n --> O(logn)
            }

            return iResult;
        }
        // 二分法，二叉树的查找方法的复杂度都是0（logn）

        /// <summary>
        /// 2^x =n  x=log2n --> O(logn)
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        private static long Method4(int iNumber)
        {
            long lResult = 1;
            int iResult = 0;
            for (int i = 0; i < iNumber; i++)
            {
                while (iResult <= iNumber)
                {
                    iResult = iResult * 2;// 2^x =n  x=log2n --> O(log2n)
                }
                lResult += iResult;
                iResult = 1;
            }

            return lResult;// O(n*log2n)
        }

        /// 0(1) 表示常量
        /// 
        private static long Method5(int iNumber)
        {
            long lResult = 0;
            return lResult + iNumber * iNumber + iNumber; 
        }

        /// <summary>
        /// 0(2^n)
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        private static long Method6(int iNumber)
        {
            long lResult = 1;
            for (int i = 0; i < iNumber; i++) // 以指数级增加 2^n 的数量级
            {
                lResult *= 2;
            }
            long lResultTarget = 1;
            for (int i = 0; i < lResult; i++) //  2^n
            {
                lResultTarget = lResultTarget + i; // 2^n 循环此运算
            }

            return lResultTarget;
        }
        /// <summary>
        /// 0(n!)
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        private static long Method7(int iNumber)
        {
            long lResult = 1;
            for (int i = 0; i < iNumber; i++) // 以指数级增加 n 的阶乘
            {
                lResult *= i;
            }
            long lResultTarget = 1;
            for (int i = 0; i < lResult; i++) //  n 的阶乘
            {
                lResultTarget = lResultTarget + i; // n 的阶乘
            }

            return lResultTarget;
        }
    }
}
