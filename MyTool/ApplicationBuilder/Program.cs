using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Console = System.Console;

namespace ApplicationBuilder
{
    class Program
    {
        public delegate void MyDelegate();

        public static int i = 0;
        static void Main(string[] args)
        {

            var app = new ApplicationBuilder.ApplicatoinBuilder();
            var lamdaExp = new Func<HttpContext, Func<Task>, Task>(async (context, next) =>
            {
                Console.WriteLine(i++ +" begin");
                await next();
                Console.WriteLine(i++ +" end");
            });
            app.Use(lamdaExp);
            app.Use(lamdaExp);

            var firstMiddleWare = app.Build();
            firstMiddleWare(new myHttpContext());



            //    var my1 = new MyDelegate(fun1);
            //    var my2 =  new MyDelegate(fun2);;
            //    MyDelegate my = fun1;
            //    my += fun2;
            //    my();
        }

        public static void fun1()
        {
            Console.WriteLine("method1");
        }
        public static void fun2()
        {
            Console.WriteLine("method2");
        }
    }

    public class myHttpContext : HttpContext
    {
        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override IFeatureCollection Features { get; }
        public override HttpRequest Request { get; }
        public override HttpResponse Response { get; }
        public override ConnectionInfo Connection { get; }
        public override WebSocketManager WebSockets { get; }
        public override AuthenticationManager Authentication { get; }
        public override ClaimsPrincipal User { get; set; }
        public override IDictionary<object, object> Items { get; set; }
        public override IServiceProvider RequestServices { get; set; }
        public override CancellationToken RequestAborted { get; set; }
        public override string TraceIdentifier { get; set; }
        public override ISession Session { get; set; }
    }
}
