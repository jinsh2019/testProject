using System;
using System.Collections.Generic;
using System.Text;

namespace TreeProject
{
    /// <summary>
    /// 树的结点
    /// </summary>
    public class Node
    {
        public int Value { get; set; }
        public Node left { get; set; }
        public Node right { get; set; }
        public Node(int data)
        {
            this.Value = data;
        }
    }
}
