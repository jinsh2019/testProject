using System;
using System.Collections.Generic;
using System.Text;

namespace FuncTest
{
    /// <summary>
    /// 泛型节约代码
    /// 泛型约束获取基类属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DocManager<T> where T: IDoc
    {
        private readonly Queue<T> _words = new Queue<T>();
        private readonly object _lockQueue = new object();

        public void AddWord(T word)
        {
            lock (_lockQueue)
            {
                _words.Enqueue(word);
            }
        }

        public bool IsDocumentsAvailable => _words.Count > 0;

        public T GetWord()
        {
            T w;
            lock (_lockQueue)
            {
                w = _words.Dequeue();
            }
            return w;
        }

        public void DisplayWord()
        {
            foreach (var item in _words)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}
