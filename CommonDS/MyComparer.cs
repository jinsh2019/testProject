using System;
using System.Collections.Generic;
using System.Text;

namespace CDS
{
    /// <summary>
    /// ListNodeCompare
    /// </summary>
    public class MyComparer : IComparer<ListNode>
    {
        public int Compare(ListNode x, ListNode y)
        {
            if (x != null && y != null)
            {
                return x.val.CompareTo(y.val);// 升序
            }
            else
            {
                return x == y ? 0 : (x == null ? -1 : 1);
            }

        }
    }
}
