using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1_AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "a{{message}}z";
            string message = "shijin";
            var result = str.Replace("{{message}}", message);
            Console.WriteLine(result);
            Console.ReadKey();
//            var payload = new Dictionary<string, object>
//{
//    { "claim1", 0 },
//    { "claim2", "claim2-value" }
//};
//            var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

//            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
//            IJsonSerializer serializer = new JsonNetSerializer();
//            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
//            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

//            var token = encoder.Encode(payload, secret);
//            Console.WriteLine(token);

//            Console.ReadKey();
            //var liststr =new List<String>();
            //var q = liststr.Where(x => x.Contains("x")).ToList();
            //foreach (var item in q)
            //{
            //    var p =item + "t";
            //    ;
            //}
            //Stopwatch watch = Stopwatch.StartNew();

            //Console.WriteLine("开始执行");
            //var t1 = Task.Run(() =>
            // {
            //     return NewMethod5Async();
            // });
            //var t2 = Task.Run(() =>
            //{
            //    return NewMethod6Async();
            //});

            //Console.WriteLine("我是主业务");
            //Console.WriteLine($"{t1.Result},{t2.Result}");
            //watch.Stop();
            //Console.WriteLine($"耗时：{watch.ElapsedMilliseconds}");

            //Console.WriteLine("Hello World!");
        }

        //利用async封装同步业务的方法
        private static async Task<string> NewMethod5Async()
        {
            Thread.Sleep(3000);
            //其它同步业务
            return "Msg1";
        }

        private static async Task<string> NewMethod6Async()
        {
            Thread.Sleep(5000);
            //其它同步业务
            return "Msg2";
        }

        public Program()
        {
        }

    }
}
