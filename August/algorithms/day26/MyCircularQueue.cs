namespace August.algorithms.day26
{
    // 设计循环队列
    // 取余
    internal class MyCircularQueue
    {
        // 头部
        private int front;
        // 尾部
        private int rear;
        // 容量
        private int capacity;
        // 元素数组
        private int[] elements;

        public MyCircularQueue(int k)
        {
            // For Example:
            // capacity = 10+1
            capacity = k + 1;
            elements = new int[capacity];
            rear = front = 0;
        }
        // 向循环队列插入一个元素
        public bool EnQueue(int value)
        {
            if (IsFull())
                return false;
            elements[rear] = value;
            rear = (rear + 1) % capacity; // rear 往前近一格
            return true;
        }
        // 从循环队列中删除一个元素
        public bool Dequeue()
        {
            if (IsEmpty())
                return false;
            front = (front + 1) % capacity; // front 往前进一格
            return true;
        }
        // 从队首获取元素
        public int Front() {
            if (IsEmpty())
                return -1;

            return elements[front];
        }
        // 获取队尾元素
        public int Rear()
        {
            if (IsEmpty()) return -1;

            return elements[(rear - 1 + capacity) % capacity];
        }
        // 是否空
        private bool IsEmpty() => rear == front;

        // 是否满了
        public bool IsFull() => (rear + 1) % capacity == front;
    }
}
