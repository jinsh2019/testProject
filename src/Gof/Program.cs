using System;

namespace Gof
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                /// 规范1
                Oracle oracle = new Oracle();
                oracle.Add();
                oracle.Delete();
                oracle.Query();
            }
            {
                /// 规范2
                Mysql mysql = new Mysql();
                mysql.Add();
                mysql.Delete();
                mysql.Query();
            }
            /// 类适配器 结构型设计模式
            {
                RedisHelperInherit rds = new RedisHelperInherit();
                rds.Add();
                rds.Delete();
                rds.Query();
            }
            /// 对象适配器
            {
                RedisHelperCombination rds = new RedisHelperCombination();
                rds.Add();
                rds.Delete();
                rds.Query();
            }
            /// 代理模式
            {
                ISubject subject = new RealSubject();
                subject.GetSomething();
                subject.DoSomething();
            }
            {
                ISubject subject = new ProxySubject();
                subject.GetSomething();
                subject.DoSomething();
            }
            Console.WriteLine("Hello World!");
            ///  责任链模式 行为型设计模式
            {
                ApplyContext applyContext = new ApplyContext();
                AbstractAuditor pm = new PM()
                {
                    Name = "peter"
                };
                AbstractAuditor charge = new Charge()
                {
                    Name = "charles"
                };
                AbstractAuditor manager = new Manager()
                {
                    Name = "Manager"
                };
                AbstractAuditor director = new Director()
                {
                    Name = "Director"
                };
                AbstractAuditor ceo = new CEO()
                {
                    Name = "ceo"
                };
                pm.SetNext(charge);
                charge.SetNext(manager);
                manager.SetNext(director);
                director.SetNext(ceo);


                pm.Audit(applyContext);
            }
            {// 双空判断单例模式 创建模式型设计模式

                var single = Singleton.CreateSingleton();
            }
            { //
                var single = new Singleton2();
            }
            {
                //IFactory factory = new string();
            }
        }

    }

}
