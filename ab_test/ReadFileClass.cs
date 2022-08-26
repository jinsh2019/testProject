namespace ab_test
{

    // Dispose and Finalize in .net
    // 1. managed object; 2. unmanaged object
    // #Finalize is created by using the class Destructure
    // #Dispose is used by creating the Dispose() method

    // https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-2.0/b1yfkh5e(v=vs.80)
    internal class ReadFileClass : IDisposable
    {
        Boolean disposed = false;
        private System.IO.FileStream fs = new System.IO.FileStream("test.txt", System.IO.FileMode.Create);
        public void Dispose()
        {
            Dispose(true);
            //告诉GC不需要再调用Finalize方法，
            //因为资源已经被显示清理
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            //由于Dispose方法可能被多线程调用，

            //所以加锁以确保线程安全
            lock (this)
            {
                if (disposing) // 释放托管资源
                {
                    //说明对象的Finalize方法并没有被执行，
                    //在这里可以安全的引用其他实现了Finalize方法的对象
                    Console.WriteLine("Freeing managed resources.");
                }
                if (fs != null)
                {
                    fs.Dispose();
                    fs = null; //标识资源已经清理，避免多次释放
                }
            }
        }

        ~ReadFileClass()
        {
            Dispose(false);
        }
    }
}
