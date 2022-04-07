using System;
using System.IO;
using System.Linq;

namespace DirectRead
{
    class Program
    {
        static void Main(string[] args)
        {

            var dt = new DateTime(2021, 02, 13);
            var formatdt = (dt.Year * 1000 + dt.DayOfYear - 1900000).ToString();;
            string[] fileList = new DirectoryInfo(".")
                                    .EnumerateFileSystemInfos("*").Select(item => item.Name).ToArray();
            fileList.ToList().ForEach(item => Console.WriteLine(item));
        }
    }
}
