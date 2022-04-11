using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ApplicationBuilder
{
    public class ApplicatoinBuilder
    {
        /// <summary>
        /// delegate collection
        /// </summary>
        private static readonly IList<Func<RequestDelegate, RequestDelegate>> _components =
            new List<Func<RequestDelegate, RequestDelegate>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public ApplicatoinBuilder Use(Func<HttpContext, Func<Task>, Task> middleware)
        {
            var lamdaExpr = new Func<RequestDelegate, RequestDelegate>(next =>
            {
                return context =>                            // param httpContext
                {
                    Task SimpleNext() => next(context);     // initiate  Func<Task>
                    return middleware(context, SimpleNext); // call logic code of mw
                };
            });
            return UseStardard(lamdaExpr);
        }


        public ApplicatoinBuilder UseStardard(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                Console.WriteLine("默认中间件");
                return Task.CompletedTask;
            };
            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }

            return app;
        }
    }
}