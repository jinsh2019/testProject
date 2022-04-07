using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using coreLearning.filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace coreLearning.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _iConfiguration;
        public HomeController(ILogger<HomeController> logger,IConfiguration iConfiguration){
            _logger = logger;
            _iConfiguration = iConfiguration;
            }
        [CustomerActionFilter]
        public IActionResult Index()
        {
            this._logger.LogWarning($"This is {typeof(HomeController)} index method");
            return View();
        }
    }
}
