using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternStudy.Patterns
{
    public class Singleton
    {
        private static readonly Singleton instance = new Singleton(); 
        private Singleton() { }
        public static Singleton getInstance() => instance;
    }
}
