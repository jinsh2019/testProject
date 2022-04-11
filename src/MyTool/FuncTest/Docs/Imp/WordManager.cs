using System;
using System.Collections.Generic;
using System.Text;

namespace FuncTest
{
    public class WordManager
    {
        private readonly Queue<Word> _words = new Queue<Word>();
        private readonly object _lockQueue = new object();

        public void AddWord(Word t)
        {
            lock (_lockQueue)
            {
                _words.Enqueue(t);
            }
        }

        public bool IsDocumentsAvailable => _words.Count > 0;

        public Word GetWord()
        {
            Word w;
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
