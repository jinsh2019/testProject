using System.Collections.Concurrent;
using static System.Console;

namespace MultiThreadDemo
{
    /// <summary>
    /// 1. 资源竞争
    /// 2. 线程状态
    /// 3. lock 同步代码块
    /// 4. lock 死锁逻辑
    /// 5. 异常捕获
    /// 6. Monitor TryEntry() 超时尝试
    /// </summary>
    internal class Nov
    {
        // 资源竞争
        public void PrintNumber()
        {
            WriteLine("Starting.......");
            for (int i = 1; i < 10; i++)
            {
                WriteLine(i);
            }
        }

        public void PrintNumberWithDelay()
        {
            WriteLine("Starting.......");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.1));
                WriteLine(i);
            }
        }


        public void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(0.1));
        }
        // 线程状态
        public void PirntNumbersWithStatus()
        {

            Console.WriteLine("starting... PirntNumbersWithStatus");
            Console.WriteLine(Thread.CurrentThread.ThreadState.ToString());
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }

        // lock 同步代码块
        public void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }
        // lock 死锁逻辑
        public void LockTooMuch(object lock1, object lock2)
        {
            lock (lock1)
            {
                Thread.Sleep(1000); // 逻辑
                //lock (lock2) ;
            }
        }

        // 异常捕获，不好的例子
        public void BadFaultyThread()
        {
            WriteLine("Starting a faulty thread...");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            //throw new Exception("Boom!");         // 可以打开
        }
        // 异常捕获，好的例子
        public void FaultyThread()
        {
            try
            {
                WriteLine("Starting a faulty thread...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                throw new Exception("Boom!");
            }
            catch (Exception ex)
            {
                WriteLine("Exception handled: {0}", ex.Message);
            }
        }

        #region CountdonwEvent Sample
        /// <summary>
        /// CountdownEvent 的应用
        /// 表示在计数变为零时处于有信号状态的同步基元
        /// </summary>
        public void CountdonwEvent()
        {
            CountdownEvent countdownEvent = new CountdownEvent(4);
            for (int i = 1; i < 5; i++)
            {
                int v = i;
                Task.Run(() =>
                {
                    Thread.Sleep(v * 50);
                    countdownEvent.Signal();
                    WriteLine(v);
                });
            }
            countdownEvent.Wait();//等待4个信号全部来袭后，继续往下执行
            Console.WriteLine("完成咯");
            countdownEvent.Dispose();
        }
        /// <summary>
        /// CountDownEventIIAsync
        /// </summary>
        /// <returns></returns>
        public async Task CountDownEventIIAsync()
        {
            // Initialize a queue and a CountdownEvent
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>(Enumerable.Range(0, 10000));
            CountdownEvent cde = new CountdownEvent(10000); // initial count = 10000

            // This is the logic for all queue consumers
            Action consumer = () =>
            {
                int local;
                // decrement CDE count once for each element consumed from queue
                while (queue.TryDequeue(out local)) cde.Signal();
            };

            // Now empty the queue with a couple of asynchronous tasks
            Task t1 = Task.Factory.StartNew(consumer);
            Task t2 = Task.Factory.StartNew(consumer);

            // And wait for queue to empty by waiting on cde
            cde.Wait(); // will return when cde count reaches 0

            Console.WriteLine("Done emptying queue.  InitialCount={0}, CurrentCount={1}, IsSet={2}",
                cde.InitialCount, cde.CurrentCount, cde.IsSet);

            // Proper form is to wait for the tasks to complete, even if you know that their work
            // is done already.
            await Task.WhenAll(t1, t2);

            // Resetting will cause the CountdownEvent to un-set, and resets InitialCount/CurrentCount
            // to the specified value
            cde.Reset(10);

            // AddCount will affect the CurrentCount, but not the InitialCount
            cde.AddCount(2);

            Console.WriteLine("After Reset(10), AddCount(2): InitialCount={0}, CurrentCount={1}, IsSet={2}",
                cde.InitialCount, cde.CurrentCount, cde.IsSet);

            // Now try waiting with cancellation
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Cancel(); // cancels the CancellationTokenSource
            try
            {
                cde.Wait(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("cde.Wait(preCanceledToken) threw OCE, as expected");
            }
            finally
            {
                cts.Dispose();
            }
            // It's good to release a CountdownEvent when you're done with it.
            cde.Dispose();
        }
        #endregion

        SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(4);

        public void AccessDatabase(string name, int seconds)
        {
            Console.WriteLine($"{name} waits to access a database.");
            _semaphoreSlim.Wait();
            Console.WriteLine($"{name} was granted an access to a database.");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine($"{name} is completed.");
            _semaphoreSlim.Release();
        }

        private static AutoResetEvent _workerEvent = new AutoResetEvent(false);
        private static AutoResetEvent _mainEvent = new AutoResetEvent(false);

        public void Process(int seconds)
        {
            Console.WriteLine("Starting a long running work...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("Work is done!");
            _workerEvent.Set();                             // 2. 发送信号量给1
            Console.WriteLine("waiting for a main thread to complete its work.");
            _mainEvent.WaitOne();                           // 3. 阻止worker线程
            Console.WriteLine("Starting second operation...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("work is done!");
            _workerEvent.Set();                             // 6. 发送信号量给5
        }

        public void MainProcess()
        {
            var t = new Thread(() => Process(10));
            t.Start();
            Console.WriteLine("Waiting for another thread to complete work");
            _workerEvent.WaitOne();                         // 1. 阻止主线程             
            Console.WriteLine("First operation is completed!");
            Console.WriteLine("Performing an operation on a main thread");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _mainEvent.Set();                               // 4. 发送信号量给 3
            Console.WriteLine("Now running the second operation on a second thread");
            _workerEvent.WaitOne();                         // 5 阻止主线程
            Console.WriteLine("Second operation is completed!");
        }


        ManualResetEvent _mmainEvent = new ManualResetEvent(false);
        public void TravelThroughGates(string threadName, int seconds)
        {
            Console.WriteLine($"{threadName} falls to sleep");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine($"{threadName}  waits for the gates to open!");
            _mmainEvent.WaitOne();
            Console.WriteLine($"{threadName} enters the gates!");
        }
        public void mainTravelThroughGates()
        {
            var t1 = new Thread(() => TravelThroughGates("thread 1", 5));
            var t2 = new Thread(() => TravelThroughGates("thread 2", 6));
            var t3 = new Thread(() => TravelThroughGates("thread 3", 12));
            t1.Start();
            t2.Start();
            t3.Start();

            Thread.Sleep(TimeSpan.FromSeconds(6));
            Console.WriteLine("The gates are now open!");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            _mainEvent.Reset();
            Console.WriteLine("The gates have been closed!");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            WriteLine("The gates are now open for the second time!");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("The gates have been closed!");
            _mainEvent.Reset();
        }
    }

    public class Counter : CounterBase
    {
        public int Count { get; private set; }
        public override void Decrement()
        {
            Count++;
        }

        public override void Increment()
        {
            Count--;
        }
    }

    public class CounterWithLock : CounterBase
    {
        private readonly object _syncRoot = new object();
        public int Count { get; private set; }
        public override void Decrement()
        {
            lock (_syncRoot)
            {
                Count++;
            }
        }

        public override void Increment()
        {
            lock (_syncRoot)
            {
                Count--;
            }
        }
    }

    /// <summary>
    /// 不使用Lock， 而使用Interlocked
    /// </summary>
    public class CounterWithNoLock : CounterBase
    {
        private int _count;
        public int GetCount()
        {
            return _count;
        }
        public override void Decrement()
        {
            Interlocked.Increment(ref _count);
        }

        public override void Increment()
        {
            Interlocked.Decrement(ref _count);
        }
    }
    public abstract class CounterBase
    {

        public abstract void Increment();
        public abstract void Decrement();
    }
}
