
// 1116. 打印零与奇偶数
public class ZeroEvenOdd
{
    private int n;
    private SemaphoreSlim first = new SemaphoreSlim(1, 1);
    private SemaphoreSlim second = new SemaphoreSlim(0, 1);
    private SemaphoreSlim third = new SemaphoreSlim(0, 1);

    public ZeroEvenOdd(int n)
    {
        this.n = n;
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Zero(Action<int> printNumber)
    {
        for (int i = 1; i <= n; i++)
        {
            first.Wait();
            printNumber(0);
            if (i % 2 == 0)
                second.Release(); // second + 1
            else
                third.Release(); // third + 1
        }
    }

    public void Even(Action<int> printNumber)
    {
        for (int i = 2; i <= n; i += 2)
        {
            second.Wait();   // second -1
            printNumber(i);
            first.Release(); // first + 1
        }
    }

    public void Odd(Action<int> printNumber)
    {
        for (int i = 1; i <= n; i += 2)
        {
            third.Wait();     // third - 1
            printNumber(i);
            first.Release();  // first + 1
        }
    }
}