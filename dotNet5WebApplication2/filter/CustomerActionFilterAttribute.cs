using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet5WebApplication2.Controllers
{
    /// <summary>
    ///  action 级别生效
    /// </summary>
    public class CustomerActionFilterAttribute : Attribute, IActionFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomerActionFilterAttribute)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomerActionFilterAttribute)} OnActionExecuting");
        }
    }

    public class CustomerResourceFilterAttribute : Attribute, IResourceFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order{ get;set;}

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(ResourceExecutedContext)} OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(ResourceExecutedContext)} OnResourceExecuting");
        }
    }

    public class CustomerResultFilterAttribute : Attribute, IResultFilter,IFilterMetadata, IOrderedFilter
    {
        public int Order{ get;set;}

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomerResultFilterAttribute)} OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomerResultFilterAttribute)} OnResultExecuting");
        }
    }
    /// <summary>
    /// controller 级别生效
    /// </summary>
    public class CustomerControllerActionFilterAttribute : Attribute, IActionFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order => throw new NotImplementedException();

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 全局 级别生效
    /// @ConfigureServices
    /// </summary>
    public class CustomerGlobalActionFilterAttribute : Attribute, IActionFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order => throw new NotImplementedException();

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
