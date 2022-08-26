using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ab_test
{
    /*
    | Method |      Mean |    Error |   StdDev |       Max |       Min | Allocated |
    |------- |----------:|---------:|---------:|----------:|----------:|----------:|
    | Single | 112.52 us | 2.181 us | 3.395 us | 120.45 us | 106.30 us |     104 B |
    |  First |  29.91 us | 0.551 us | 0.489 us |  30.92 us |  29.29 us |     104 B |
     */
    [MaxColumn]
    [MinColumn]
    [MemoryDiagnoser]
    public class LinqToObject
    {
        private List<string> Keys = new List<string>();
        private int Size = 10000;
        private string Key = "key";

        public LinqToObject()
        {
            Enumerable.Range(0, Size).ToList().ForEach(m => Keys.Add(m.ToString()));
            Keys.Insert(Size / 4, Key);
        }
        [Benchmark]
        public string Single() => Keys.SingleOrDefault(x => x == Key);
        [Benchmark]
        public string First() => Keys.FirstOrDefault(x => x == Key);
    }
}
