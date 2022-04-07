using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BaseDemo002MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // IHostBuilder ËÞÖ÷ °üÀ¨webHost
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseContentRoot(path)
             .ConfigureWebHostDefaults(webBuilder => // WebHostBuild
             {
                 webBuilder.UseStartup<Startup>();
             });
        }

    }
}
