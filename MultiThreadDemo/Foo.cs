namespace MultiThreadDemo
{
    // 1114. 按序打印
    public class Foo
    {
        ManualResetEvent second = new ManualResetEvent(false);
        ManualResetEvent third = new ManualResetEvent(false);
        public Foo()
        {

        }

        public void First(Action printFirst)
        {
            // printFirst() outputs "first". Do not change or remove this line.
            printFirst();
            second.Set();
        }

        public void Second(Action printSecond)
        {
            // printSecond() outputs "second". Do not change or remove this line.
            second.WaitOne();
            printSecond();
            third.Set();
        }

        public void Third(Action printThird)
        {
            // printThird() outputs "third". Do not change or remove this line.
            third.WaitOne();
            printThird();
        }


    }
}
