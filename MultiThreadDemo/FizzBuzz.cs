
public class FizzBuzz
{
    private int n;
    private readonly AutoResetEvent fizz = new AutoResetEvent(true);
    private readonly AutoResetEvent buzz = new AutoResetEvent(false);
    private readonly AutoResetEvent fizzbuzz = new AutoResetEvent(false);
    private readonly AutoResetEvent number = new AutoResetEvent(false);

    public FizzBuzz(int n)
    {
        this.n = n;
    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz)
    {
        for (int i = 1; i <= n; i++)
        {
            fizz.WaitOne();
            if (i % 3 == 0 && i % 5 != 0)
            {
                printFizz();
            }
            buzz.Set();
        }

    }

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz)
    {
        for (int i = 1; i <= n; i++)
        {
            buzz.WaitOne();
            if (i % 5 == 0 && i % 3 != 0)
            {
                printBuzz();
            }

            fizzbuzz.Set();
        }
    }

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz)
    {
        for (int i = 1; i <= n; i++)
        {
            fizzbuzz.WaitOne();
            if (i % 3 == 0 && i % 5 == 0)
            {
                printFizzBuzz();
            }
            number.Set();
        }
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber)
    {
        for (int i = 1; i <= n; i++)
        {
            number.WaitOne();
            if (i % 3 != 0 && i % 5 != 0)
            {
                printNumber(i);
            }
            fizz.Set();
        }
    }
}