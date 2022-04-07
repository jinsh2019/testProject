using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RecurtionTest
{
    public class Node
    {
        public Node(int date)
        {
            this.data = date;
        }

        public int data { get; set; }
        public Node next { get; set; }

        public Node left { get; set; }
        public Node right { get; set; }
    }
}
