using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Gof
{
    class Singleton
    {
        private static Singleton _singleton = null;
        private static readonly object singleton_lock = new object();
        private Singleton()
        {
            long lResult = 0;
            for (int i = 0; i < 10000000; i++)
            {
                lResult += 1;
            }
            Thread.Sleep(200);
            Console.WriteLine($"{this.GetType().Name}被构造一次{Thread.CurrentThread.ManagedThreadId}");
        }

        public static Singleton CreateSingleton()
        {
            if (_singleton == null) // 双空判断。为了多线程并发，节省时间。
            {
                lock (singleton_lock)
                {
                    if (_singleton == null) /// 用于第一次并发判断，防止
                    {
                        _singleton = new Singleton();
                    }
                }
            }

            return _singleton;
        }


    }
    /// <summary>
    /// 静态构造函数的方式 迫不及待 =饥饿型
    /// </summary>
    class Singleton2
    {
        private static Singleton2 _singletonSecond = null;
        static Singleton2()
        {
            _singletonSecond = new Singleton2();
        }
        public Singleton2()
        {
        }
    }
    /// <summary>
    /// 静态字段 （由CLR 保障）迫不及待 =饥饿型
    /// </summary>
    class Singleton3
    {
        private static Singleton3 _singletonThird = new Singleton3();
    }

    /// <summary>
    /// 原型模式
    /// 产生一个实例：
    /// 1. New() 一个对象
    /// 2. 克隆技术 a 浅克隆 适合简单对象，可以产生大量对象; b 深克隆 序列化、反序列化
    /// 3. 反序列化
    /// 4. 反射技术， 调用构造函数
    /// </summary>
    class SingletonPrototype
    {
        public int Id { get; set; }
        public string Name { get; set; }  

        private SingletonPrototype()
        {
            long lResult = 0;
            for (int i = 0; i < 10000000; i++)
            {
                lResult += 1;
            }
            Thread.Sleep(200);
            Console.WriteLine($"{this.GetType().Name}被构造一次{Thread.CurrentThread.ManagedThreadId}");
        }
        private static SingletonPrototype _singletonPrototype = new SingletonPrototype();
        public static SingletonPrototype CreateSingleton()
        {
            return (SingletonPrototype)_singletonPrototype.MemberwiseClone(); //  浅克隆 适合简单对象
        }
    }
}
