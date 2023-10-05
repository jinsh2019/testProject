using System;
using System.Collections.Generic;

namespace SortTest
{
    /// <summary>
    /// 155. 最小栈
    /// ["MinStack","push","push","push","getMin","pop","top","getMin"]
    /// [[],[-2],[0],[-3],[],[],[],[]]
    /// </summary>
    public class MinStack
    {
        // stack 一般栈
        public Stack<int> stack;
        // minStack 具有 GetMin()的性质
        public Stack<int> minStack;

        public MinStack()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="val"></param>
        public void Push(int val)
        {
            stack.Push(val);
            if (minStack.Count == 0 || val <= minStack.Peek())
                minStack.Push(val);
        }
        /// <summary>
        /// 出栈
        /// </summary>
        public void Pop()
        {
            if (stack.Count != 0)
            {
                int sTop = stack.Pop();
                /// 维护minStack
                if (sTop == minStack.Peek())
                    minStack.Pop();
            }
        }
        /// <summary>
        /// 取栈顶元素(一般属性)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int Top()
        {
            if (stack.Count != 0)
                return stack.Peek();
            throw new InvalidOperationException();
        }

        /// <summary>
        /// 特殊性质: 取最小值
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int GetMin()
        {
            if (minStack.Count != 0)
                return minStack.Peek();

            throw new InvalidOperationException();
        }
    }
}
