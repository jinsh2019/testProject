using System;

namespace TestIoC.IService
{
    public class ObjectB:IServiceB
    {
        private IServiceA _serviceA;
        public ObjectB(IServiceA serviceA)
        {
            _serviceA = serviceA;
            Console.WriteLine("ObjectB");
        }

        public void Operation()
        {
            throw new System.NotImplementedException();
        }
    }
}