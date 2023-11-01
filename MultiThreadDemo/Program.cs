// See https://aka.ms/new-console-template for more information
using MultiThreadDemo;
using MultiThreadDemo.DeadLockDemo;
using static System.Console;
/*
     .Net微服务多线程MutiThread最佳实践专题(C#/.NET/.NET6/.NET5/Thread/Task/微服务）B0529
     原理: Thread, ThreadPool, Task, await ==> Application->CLR-> OS
     绕不开delegate， 可以理解为： 方法模板
 */


WriteLine($"CurrentThread:" + Thread.CurrentThread.ManagedThreadId);
#region 基础
//  单线程
{
    //for (int i = 0; i < 5; i++)
    //{
    //    string name = $"foreach_i={i}";
    //    //DoSomeThingLong(name);

    //    // 委托
    //    Action action = () => DoSomeThingLong(name); // lambda
    //    action.Invoke();
    //}
}
// 多线程
{
    //for (int i = 0; i < 10; i++)
    //{
    //    string name = $"foreach_i={i}";
    //    Action action = () => DoSomeThingLong(name);
    //    Task.Run(action); // 交给其他线程来做， 比如发邮件，短信， 消息等； 主程序不需要等待
    //}
}
#endregion

#region 高级
// 高级多线程
// 1. Task 多线程场景实战
// 2. 线程顺序掌控， 多API灵活应用
// 3. 临时变量，线程异常，线程取消
// 4. 多方案线程安全和任务分配
{
    //// 1. 主程序同步
    //WriteLine("组织团队");
    //WriteLine("分配任务");
    //WriteLine("开始干活");
    //// 2. 多线程
    //List<Task> tasks = new List<Task>();
    //tasks.Add(Task.Run(() => coding("阿莫西林", "Portal")));
    //tasks.Add(Task.Run(() => coding("扬帆起航", "Client")));
    //tasks.Add(Task.Run(() => coding("一路向前", "WeChat")));
    //tasks.Add(Task.Run(() => coding("社情不受", "Service")));
    //tasks.Add(Task.Run(() => coding("勿忘吾爱", "SqlServer")));
    //tasks.Add(Task.Run(() => coding("香格里拉", "WebApi")));

    // 3. 后序动作
    // 1. 线程回调；2.不需要阻塞所有线程，比如log；

    // 不推荐"包"线程,三层以后，神仙难救
    //Task.Run(() => { 

    //});
    {
        //// 1. 单个任务   完成回调
        //tasks[0].ContinueWith(t => WriteLine($"This signle Callback + 特别关注的milestone 完成 {Thread.CurrentThread.ManagedThreadId}"));
        //// 2. 任意一个任务完成回调
        //Task.Factory.ContinueWhenAny(tasks.ToArray(),
        //    t => WriteLine($"领取红包{Thread.CurrentThread.ManagedThreadId}"));
        //// 3. 全部任务   完成回调
        //Task.Factory.ContinueWhenAll(tasks.ToArray(),
        //    tlist => WriteLine($"联调，部署，聚餐！{Thread.CurrentThread.ManagedThreadId}"));
    }
    {
        //// 多渠道查询数据，任意渠道返回结果即可
        //Task.WaitAny(tasks.ToArray()); // 项目里程碑达成，收取30%的费用
        //WriteLine($"收取30%费用！");

        //// 主线程阻塞
        //Task.WaitAll(tasks.ToArray());
        //// Task.WaitAny(tasks.ToArray()); // 项目里程碑达成，收取30%的费用
        //WriteLine("项目交付");
    }
}

{
    //// 任务分配
    //ShowTaskDispacher();

}

#endregion

#region lc

// 1114. 按序打印
{
    Action action1 = () => printFirst();
    Action action2 = () => printSecond();
    Action action3 = () => printThird();
    var f = new Foo();
    Thread t1 = new Thread(obj =>
    {
        f.First(action1);
    });
    Thread t2 = new Thread(obj =>
    {
        f.Second(action2);
    });
    Thread t3 = new Thread(obj =>
    {
        f.Third(action3);
    });
    t3.Start();
    t2.Start();
    t1.Start();
}
// 1115.交替打印 FooBar
{
    Action action1 = () => printFoo();
    Action action2 = () => printBar1();
    FooBar fooBar = new FooBar(5);
    Thread t1 = new Thread(() =>
    {
        fooBar.Foo(action1);
    });

    Thread t2 = new Thread(() =>
    {
        fooBar.Bar(action2);
    });
    t1.Start();
    t2.Start();
}

// 1116. 打印零与奇偶数
{
    Action<int> action1 = n => printBar(n);

    ZeroEvenOdd zeroEvenOdd = new ZeroEvenOdd(5);
    Thread t1 = new Thread(obj =>
    {
        int n = (int)obj;
        zeroEvenOdd.Zero(action1);
    });
    Thread t2 = new Thread(obj =>
    {
        int n = (int)obj;
        zeroEvenOdd.Odd(action1);
    });
    Thread t3 = new Thread(obj =>
    {
        int n = (int)obj;
        zeroEvenOdd.Even(action1);
    });

    t1.Start(5);
    t2.Start(5);
    t3.Start(5);
}
// 1117. H2O 生成
{
    //Action action1 = () => releaseHydrogen();
    //Action action2 = () => releaseOxygen();

    //H2O h2o = new H2O();
    //int i = 20;
    //while (i != 0)
    //{
    //    Thread t1 = new Thread(() =>
    //    {
    //        h2o.Hydrogen(action1);
    //    });

    //    Thread t2 = new Thread(() =>
    //    {
    //        h2o.Oxygen(action2);
    //    });
    //    t1.Start();
    //    t2.Start();
    //    i--;
    //}
}
#endregion

#region DeadLocked
{
    //CancellationTokenSource cts = new CancellationTokenSource();
    //for (int i = 0; i < 50; i++)
    //{
    //    string k = "tt" + i;
    //    Task.Run(() =>
    //    {
    //        Thread.Sleep(new Random().Next(20, 100));
    //        try
    //        {
    //            if (!cts.IsCancellationRequested)
    //            {
    //                WriteLine($"{Thread.CurrentThread.ManagedThreadId} 正常开始");
    //            }
    //            else
    //            {
    //                WriteLine($"{Thread.CurrentThread.ManagedThreadId}没有正常开始");
    //                throw new Exception($"取消线程{Thread.CurrentThread.ManagedThreadId} 没有正常开始---取消");
    //            }
    //            {
    //                // 业务逻辑
    //            }
    //            if (k == "tt8")
    //            {
    //                throw new Exception($"{Thread.CurrentThread.ManagedThreadId}异常了");
    //            }
    //            if (!cts.IsCancellationRequested)
    //            {
    //                WriteLine($"{Thread.CurrentThread.ManagedThreadId} 正常结束");
    //            }
    //            else
    //            {
    //                WriteLine($"{Thread.CurrentThread.ManagedThreadId} 没有正常结束，取消了");
    //                throw new Exception($"取消线程 {Thread.CurrentThread.ManagedThreadId} 没有正常结束，取消了");
    //            }

    //        }
    //        //catch (AggregateException ae)
    //        //{
    //        //    throw;
    //        //}
    //        catch (Exception ex)
    //        {
    //            WriteLine($"{Thread.CurrentThread.ManagedThreadId} 到主线程中取消了");
    //            cts.Cancel();
    //        }
    //    });
    //}
}
#endregion


// DeadLocked
{
    //WriteLine("Main Thread Started");
    //Account Account1001 = new Account(1001, 5000);
    //Account Account1002 = new Account(1002, 3000);
    //AccountManager accountManager1 = new AccountManager(Account1001, Account1002, 5000);
    //Thread thread1 = new Thread(accountManager1.FundTransfer1);
    //thread1.Name = "Thread1";
    //AccountManager accountManager2 = new AccountManager(Account1002, Account1001, 6000);
    //Thread thread2 = new Thread(accountManager2.FundTransfer1);
    //thread2.Name = "Thread2";

    //thread1.Start();
    //thread2.Start();
    //thread1.Join();
    //thread2.Join();
    //WriteLine("Main Thread Completed");
}


#region Basic Kownledge

Nov _nov = new Nov();
{

    _nov.CountdonwEvent();
}
{


    await _nov.CountDownEventIIAsync();
}
{
    Thread t = new Thread(_nov.PrintNumber);
    t.Start();
    _nov.PrintNumber();

}
{
    Thread t = new Thread(_nov.PrintNumberWithDelay);
    t.Start();
    _nov.PrintNumber();
    t.Join();
}
{
    Console.WriteLine("with Join");
    Thread t = new Thread(_nov.PrintNumberWithDelay);
    t.Start();
    t.Join();
    _nov.PrintNumber();
}
{
    //WriteLine("Starting program...");
    //Thread t = new Thread(_nov.PrintNumberWithDelay);
    //t.Start();
    //Thread.Sleep(TimeSpan.FromSeconds(0.6));
    //t.Abort(); // not be supported
    //WriteLine("A thread has been aborted!");
    //Thread t1 = new Thread(_nov.PrintNumber);
    //t1.Start();
    //_nov.PrintNumber();
}
{
    //WriteLine("Thread States Test......");
    //Thread t = new Thread(_nov.PirntNumbersWithStatus);
    //Thread t2 = new Thread(_nov.DoNothing);
    //WriteLine(t.ThreadState.ToString());
    //t2.Start();
    //t.Start();
    //for (int i = 1; i < 30; i++)
    //{
    //    WriteLine(t.ThreadState.ToString());        // Running then WaitSleepJoin
    //}
    //Thread.Sleep(TimeSpan.FromSeconds(6));
    ////t.Abort();                                    // 
    //WriteLine(t.ThreadState.ToString());            // [Abort or] WaitSleepJoin
    //WriteLine(t2.ThreadState.ToString());           // Stopped

}
{
    // Priority: highest
    // Priority: Lowest
    //Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
}
{
    // IsBackgroud = true
    // 所有前台进程结束之后，后台开始
}
{
    // lock
    WriteLine("Incorrect counter");

    var c = new Counter();                      // 线程不安全

    var t1 = new Thread(() => _nov.TestCounter(c));
    var t2 = new Thread(() => _nov.TestCounter(c));
    var t3 = new Thread(() => _nov.TestCounter(c));
    t1.Start();
    t2.Start();
    t3.Start();
    t1.Join();
    t2.Join();
    t3.Join();
    WriteLine("Total count:{0}", c.Count);

    WriteLine("------------------------------------");
    WriteLine("Correct counter");

    var c1 = new CounterWithLock();             // 线程安全

    t1 = new Thread(() => _nov.TestCounter(c1));
    t2 = new Thread(() => _nov.TestCounter(c1));
    t3 = new Thread(() => _nov.TestCounter(c1));

    t1.Start();
    t2.Start();
    t3.Start();
    t1.Join();
    t2.Join();
    t3.Join();

    WriteLine("Total count:{0}", c1.Count);
}
{
    object lock1 = new object();
    object lock2 = new object();

    new Thread(() => _nov.LockTooMuch(lock1, lock2)).Start();

    lock (lock2)
    {
        Thread.Sleep(1000);
        WriteLine("Monitor.TryEnter allows not to get stuck, returning false after a specified time out is elapsed.");
        if (Monitor.TryEnter(lock1, TimeSpan.FromSeconds(2))) // 等待1秒后可以获得资源
            WriteLine("Acquired a protected resource succesfully!");
        else
            WriteLine("Timeout acquiring a resource!");
    }
    new Thread(() => _nov.LockTooMuch(lock1, lock2)).Start();
}

{
    // 推荐使用 在线程代码内部获取异常
    var t = new Thread(_nov.FaultyThread);
    t.Start();
    t.Join();

    try
    {
        t = new Thread(_nov.BadFaultyThread);
        t.Start();
    }
    catch (Exception ex)
    {
        Console.WriteLine("we won`t get here!");
    }
}
{
    var c1 = new CounterWithNoLock();             // 线程安全

    var t1 = new Thread(() => _nov.TestCounter(c1));
    var t2 = new Thread(() => _nov.TestCounter(c1));
    var t3 = new Thread(() => _nov.TestCounter(c1));

    t1.Start();
    t2.Start();
    t3.Start();
    t1.Join();
    t2.Join();
    t3.Join();

    WriteLine("Total count:{0}", c1.GetCount());
}
{
    // 互斥量
    const string MutexName = "CSharpThradingCookbook";

    using (var m = new Mutex(false, MutexName))
    {
        if (!m.WaitOne(TimeSpan.FromSeconds(2), false))
        {
            Console.WriteLine("Second instance is running!");
        }
        else
        {
            Console.WriteLine("Running! 3s");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            //Console.ReadLine();
            m.ReleaseMutex();                   // 尽早释放有好处
        }
    }
}
{
    // SemaphoreSlim
    //for (int i = 0; i <= 6; i++)
    //{
    //    string threadName = "Thread " + i;
    //    int secondsToWait = 2 + 2 * i;
    //    var t = new Thread(() => _nov.AccessDatabase(threadName, secondsToWait));
    //    t.Start();
    //}
}
{
    // AutoResetEvent
    _nov.MainProcess();
}
{
    // ManualResetEvent
    _nov.mainTravelThroughGates();
}
#endregion
ReadKey();

#region 私有方法
void releaseOxygen()
{
    WriteLine("O");
}

void releaseHydrogen()
{
    WriteLine("H");
}

void printBar1()
{
    WriteLine("Bar");
}

void printFoo()
{
    WriteLine("Foo");
}

void printBar(int n)
{
    WriteLine(n);
}
void printFirst()
{
    WriteLine("First");
}
void printSecond()
{
    WriteLine("Second");
}

void printThird()
{
    WriteLine("Third");
}

// method1
void DoSomeThingLong(string name)
{
    WriteLine($"name: {name},start," +
              $"CurrentThread:{Thread.CurrentThread.ManagedThreadId}," +
              $"TimeStamp:{DateTime.Now.ToString("HHmmss:fff")}");
    long lResult = 0;
    for (int i = 0; i < int.MaxValue; i++)
    {
        lResult += i;
    }

    WriteLine($"name: {name},  end," +
          $"CurrentThread:{Thread.CurrentThread.ManagedThreadId}," +
          $"TimeStamp:{DateTime.Now.ToString("HHmmss:fff")}," +
          $"{lResult}");
}

// method2
void coding(string name, string project)
{
    WriteLine($"Name: {name},project:{project},start," +
              $"CurrentThread:{Thread.CurrentThread.ManagedThreadId}," +
              $"TimeStamp:{DateTime.Now.ToString("HHmmss:fff")}");
    long lResult = 0;
    for (int i = 0; i < int.MaxValue; i++)
    {
        lResult += i;
    }
    WriteLine($"Name: {name},project:{project},  end," +
          $"CurrentThread:{Thread.CurrentThread.ManagedThreadId}," +
          $"TimeStamp:{DateTime.Now.ToString("HHmmss:fff")}," +
          $"{lResult}");
}

// method3
void ShowTaskDispacher()
{
    #region init Data
    int[][] typeArray = new int[100][];
    for (int i = 0; i < 100; i++)
    {
        int[] innerArray = new int[i];
        for (int j = 0; j < i; j++)
        {
            innerArray[j] = j;
        }
        typeArray[i] = innerArray;
    }
    #endregion
    List<Task> taskList = new List<Task>();
    int threadNum = 27;
    //// 1. 控制线程数量
    //for (int i = 0; i < typeArray.Length; i++)
    //{
    //    var currentTypeArray = typeArray[i];

    //    taskList.Add(Task.Run(() =>
    //    {
    //        WriteLine($"CurrentTrheadId = {Thread.CurrentThread.ManagedThreadId},执行{string.Join(",", currentTypeArray)}");
    //        foreach (var num in currentTypeArray)
    //        {
    //            Thread.Sleep(num);
    //        }
    //        WriteLine($"CurrentTrheadId = {Thread.CurrentThread.ManagedThreadId}, 任务完成");
    //    }));

    //    if (taskList.Count() == threadNum)
    //    {
    //        Task.WaitAny(taskList.ToArray());
    //        taskList = taskList.Where(t => t.Status == TaskStatus.Running).ToList();
    //    }

    //}

    // 2. 控制线程数量
    ParallelOptions parallelOptions = new ParallelOptions()
    {
        MaxDegreeOfParallelism = threadNum
    };

    Parallel.ForEach(typeArray, parallelOptions, currentTypeArray =>
    {
        WriteLine($"CurrentTrheadId = {Thread.CurrentThread.ManagedThreadId},    执行{string.Join(",", currentTypeArray)}");
        foreach (var num in currentTypeArray)
        {
            Thread.Sleep(num);
        }
        //WriteLine($"CurrentTrheadId = {Thread.CurrentThread.ManagedThreadId}, 任务完成");
    });

    WriteLine("parallel任务完成");
    ReadKey();
}
#endregion