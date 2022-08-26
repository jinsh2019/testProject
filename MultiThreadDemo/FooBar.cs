using System.Threading;
//1115.交替打印 FooBar
public class FooBar
{
    ManualResetEvent second = new ManualResetEvent(false);
    ManualResetEvent first = new ManualResetEvent(false);
    private int n;

    public FooBar(int n)
    {
        this.n = n;
        first.Set();
    }

    public void Foo(Action printFoo)
    {

        for (int i = 0; i < n; i++)
        {
            // printFoo() outputs "foo". Do not change or remove this line.
            first.WaitOne();
            printFoo();
            first.Reset();
            second.Set();
        }
    }

    public void Bar(Action printBar)
    {

        for (int i = 0; i < n; i++)
        {
            // printBar() outputs "bar". Do not change or remove this line.
            second.WaitOne();
            printBar();
            second.Reset();
            first.Set();
        }
    }
}