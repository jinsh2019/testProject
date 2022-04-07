using System;
using System.Collections.Generic;
using System.Text;

namespace FuncTest
{
    public class ExcelManager
    {
        private readonly Queue<Excel> _words = new Queue<Excel>();
        private readonly object _lockQueue = new object();

        public void AddWord(Excel word)
        {
            lock (_lockQueue)
            {
                _words.Enqueue(word);
            }
        }

        public bool IsDocumentsAvailable => _words.Count > 0;

        public Excel GetWord()
        {
            Excel w;
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
