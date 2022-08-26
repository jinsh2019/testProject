using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ab_test
{
    /*
     |            Method |       Mean |     Error |    StdDev |        Max |        Min |    Gen 0 |  Gen 1 | Allocated |
     |------------------ |-----------:|----------:|----------:|-----------:|-----------:|---------:|-------:|----------:|
     |     StringContact | 172.199 us | 3.1254 us | 3.2096 us | 178.471 us | 168.352 us | 454.1016 | 6.3477 |  2,783 KB |
     | StringBuildAppend |   7.800 us | 0.1545 us | 0.3191 us |   8.626 us |   7.248 us |   2.3727 | 0.0687 |     15 KB |
     
     */
    [MaxColumn]
    [MinColumn]
    [MemoryDiagnoser]
    public class StringTest
    {
        //StringBuilder
        //String

        public StringTest()
        {

        }

        [Benchmark]
        public void StringContact()
        {
            string str = string.Empty;
            for (int i = 0; i < 1000; i++)
            {
                str += i;
            }
        }
        [Benchmark]
        public void StringBuildAppend()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                stringBuilder.Append(i);
            }
            string str = stringBuilder.ToString();
        }

    }
}
