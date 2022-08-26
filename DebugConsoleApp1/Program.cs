using System.Runtime.InteropServices;

namespace DebugConsoleApp1
{
    internal class Program
    {

        [DllImport("ConsoleApplication1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static int calc_size(int size);


        static List<string> list = new List<string>();
        static void Main(string[] args)
        {

            {
                // 分析非托管内存工具 1. process Explorer； 2. perfView 定位内存函数
                // result
                /*
                 Name                                        	Inc %	             Inc	  Inc Ct
 ||      + consoleapplication1!calc_size    	 98.9	   2,044,517,632	 514,169
 ||       + consoleapplication1!operator new	 98.9	   2,044,517,632	 514,169
                 */
                //for (int i = 0; i < int.MaxValue; i++)
                //{
                //    var size = calc_size(1000);
                //    Console.WriteLine($"i={i}");
                //}
            }

            {
                Task.Run(() =>
                {
                    for (int i = 0; i < int.MaxValue; i++)
                    {
                        list.Add(string.Join(",", Enumerable.Range(0, 1000)));
                        Console.WriteLine(i);
                    }
                });


                Console.ReadLine();
            }
        }


    }
}