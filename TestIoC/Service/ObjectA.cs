using System;

namespace TestIoC.IService
{
    public class ObjectA:IServiceA
    {
        public ObjectA()
        {
            Console.WriteLine("ObjectA");
        }

        public void Operation()
        {
            throw new System.NotImplementedException();
        }
    }
}