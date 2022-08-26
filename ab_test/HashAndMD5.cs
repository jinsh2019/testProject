using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Security.Cryptography;
using System.Text;

namespace ab_test
{
    /* 
        |   Method | Content |     Mean |     Error |    StdDev |      Max |       Min |  Gen 0 | Allocated |
        |--------- |-------- |---------:|----------:|----------:|---------:|----------:|-------:|----------:|
        |  TestMD5 |     aaa | 1.010 us | 0.0199 us | 0.0449 us | 1.083 us | 0.8853 us | 0.0706 |     448 B |
        | TestSHA1 |     aaa | 1.136 us | 0.0227 us | 0.0233 us | 1.185 us | 1.1021 us | 0.0801 |     504 B |
    */
    [MaxColumn]
    [MinColumn]
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net472)]
    public class HashAndMD5
    {

        public string GetMD5(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            using (var md5 = MD5.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(input);
                var hashResult = md5.ComputeHash(buffer);
                return BitConverter.ToString(hashResult).Replace("-", String.Empty);
            }
        }

        public string GetSHA1(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            using (var sha1 = SHA1.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(input);
                var hashResult = sha1.ComputeHash(buffer);
                return BitConverter.ToString(hashResult).Replace("-", String.Empty);
            }
        }

        /// <summary>
        /// 基准测试多参数
        /// </summary>
        //[Params("aaa", "https://www.baidu.com/img/bd_logo1.png")]
        [Params("aaa")]
        public string Content { get; set; }
        [Benchmark]
        public void TestMD5()
        {
            //this.GetMD5("https://www.baidu.com/img/bd_logo1.png");
            this.GetMD5(Content);
        }

        [Benchmark]
        public void TestSHA1()
        {
            //this.GetSHA1("https://www.baidu.com/img/bd_logo1.png");
            this.GetSHA1(Content);
        }
    }
}
