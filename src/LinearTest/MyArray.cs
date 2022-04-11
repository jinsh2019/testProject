using System;
using System.Collections.Generic;
using System.Text;

namespace LinearTest
{
    public class MyArray
    {
        private int[] array;
        private int size;

        public MyArray(int capacity)
        {
            this.array = new int[capacity];
            size = 0;
        }
        // 数组插入元素
        public void insert(int index, int element)
        {
            if (index < 0 || index > size)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = size - 1; i >= index; i--)
            {
                array[i + 1] = array[i];
            }

            array[index] = element;
            size++;
        }
        public int delete(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }

            int deletedElement = array[index];
            for (int i = index; i < size; i++)
            {
                array[i] = array[i+1];
            }
            size--;
            return deletedElement;
        }
        public void OutPut()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }
    }
}
