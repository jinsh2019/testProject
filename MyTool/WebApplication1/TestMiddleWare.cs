using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApplication1
{
    public class TestMiddleWare
    {
        private readonly RequestDelegate _next;

        public TestMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("Message:Hello middleware");
            await _next(httpContext);
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomerMW(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TestMiddleWare>();
        }
    }
}