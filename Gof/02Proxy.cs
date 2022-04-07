using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace Gof
{
    interface ISubject
    {
        void DoSomething();
        bool GetSomething();
    }
    /// <summary>
    /// 核心业务团队
    /// </summary>
    class RealSubject : ISubject
    {
        public void DoSomething()
        {
            throw new NotImplementedException();
        }

        public bool GetSomething()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// +日志、+异常、+事务、+缓存
    /// 业务逻辑和通用逻辑分离
    /// 1.异常代理 2.日志代理 3.**代理 4. 缓存代理 5. 延迟代理 6，鉴权 7 事务 8 监控
    ///
    /// AOP面向切面编程，大部分都是基于代理实现
    /// </summary>
    class ProxySubject : ISubject
    {
        private static ISubject _iSubject = new RealSubject(); // 3.代理代理

        public void DoSomething()
        {
            try // 1.异常代理
            {
                Console.WriteLine("before"); // 2.日志代理
                _iSubject.DoSomething();
                Console.WriteLine("after");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool GetSomething()
        {
            string key = $"{nameof(ProxySubject)}_{nameof(GetSomething)}";
            bool bResult = false;
            if (CustomCache.Exist(key))
            {
                bResult = CustomCache.Get<bool>(key);
            }
            else
            {
                bResult = _iSubject.GetSomething();
                CustomCache.Add(key,bResult);
            }
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 4. 缓存代理
    /// </summary>
    class CustomCache
    {
        private static Dictionary<string, object> _customCacheDictionary = new Dictionary<string, object>();

        public static void Add(string key, object value)
        {
            _customCacheDictionary.Add(key, value);
        }

        public static T Get<T>(string key)
        {
            return (T)_customCacheDictionary[key];
        }

        public static bool Exist(string key)
        {
            return _customCacheDictionary.ContainsKey(key);
        }
    }
}
