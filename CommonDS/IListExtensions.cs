﻿using System.Collections.Generic;

namespace CDS
{
    // 链表扩展方法，交换元素
    public static class IListExtensions
    {
        public static void Swap<T>(
            this IList<T> list,
            int firstIndex,
            int secondIndex
        )
        {
            //Contract.Requires(list != null);
            //Contract.Requires(firstIndex >= 0 && firstIndex < list.Count);
            //Contract.Requires(secondIndex >= 0 && secondIndex < list.Count);
            if (firstIndex == secondIndex)
            {
                return;
            }
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
}
