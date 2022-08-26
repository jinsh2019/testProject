using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace DesignPatternStudy.Patterns
{
    // 迭代器模式
    public interface Iterator
    {
        public bool hasNext();
        public Object next();
    }
    
    public interface Container
    {
        public Iterator getIterator();
    }

    public class NameRepository : Container
    {
        public String[] names = { "Robert", "John", "Julie", "Lora" };

        public Iterator getIterator()
        {
            return new NameIterator(names);
        }
    }
    
    internal class NameIterator : Iterator
    {

        int index; 
        private String[] _names;
        public NameIterator(String[] names)
        {
            _names = names;
        }
        public bool hasNext()
        {
            if (index < _names.Length)
            {
                return true;
            }
            return false;
        }


        public Object next()
        {
            if (this.hasNext())
            {
                return _names[index++];
            }
            return null;
        }
    }
}
