using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotNet5WebApplication2.Models;
using Microsoft.Extensions.Configuration;

namespace dotNet5WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _iConfiguration;
        public HomeController(ILogger<HomeController> logger, IConfiguration iConfiguration)
        {
            _logger = logger;
            _iConfiguration = iConfiguration;
        }

        [CustomerResultFilter]
        [CustomerResourceFilter]
        [CustomerActionFilter]
        public IActionResult Index()
        {
            this._logger.LogWarning($"This is {typeof(HomeController)} index method");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
