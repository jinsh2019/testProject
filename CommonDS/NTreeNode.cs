﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CDS
{
    public class NTreeNode<T>
    {
        public T val { get; set; }
        public NTreeNode<T>[] Children { get; set; }
    }
}