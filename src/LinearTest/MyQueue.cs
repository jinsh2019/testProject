using System;

namespace LinearTest
{
    public class MyQueue
    {
        private int[] array;
        private int front;// 指向队列的头部
        private int rear; // 指向队列的尾部
        public MyQueue(int capacity)
        {
            this.array = new int[capacity];
        }
        // 入队
        public void enQueue(int element)
        {
            if ((rear + 1) % array.Length == front)
            {
                throw new Exception("队列已满！");
            }
            array[rear] = element;
            rear = (rear + 1) % array.Length; // 指针偏移量
        }
        // 出队
        public int deQueue()
        {
            if (rear == front)
            {
                throw new Exception("队列已空！");
            }
            int deQueueElement = array[front];
            front = (front + 1) % array.Length; // 指针偏移量
            return deQueueElement;
        }

        public void output()
        {
            for (int i = front; i != rear; i = (i + 1) % array.Length)
            {
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }

    }
}
