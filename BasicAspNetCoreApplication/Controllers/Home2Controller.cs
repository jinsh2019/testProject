using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace BasicAspNetCoreApplication.Controllers
{
    public class Home2Controller : AbpController
    {
        public IActionResult Index()
        {
            return Content("Hello World!");
        }
    }
}