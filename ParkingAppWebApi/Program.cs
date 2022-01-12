using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ParkingAppWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


































//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using NLog.Web;

//namespace ParkingAppWebApi
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
//            try
//            {
//                logger.Debug("Main easy");
//                CreateHostBuilder(args).Build().Run();
//            }
//            catch (Exception exception)
//            {
//                //NLog: catch setup errors
//                logger.Error(exception, "Stopped program because of exception");
//                throw;
//            }
//            finally
//            {
//                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
//                NLog.LogManager.Shutdown();
//            }
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//      .ConfigureWebHostDefaults(webBuilder =>
//      {
//          webBuilder.UseStartup<Startup>();
//      })
//      .ConfigureLogging(logging =>
//      {
//          logging.ClearProviders();
//          logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
//      })
//      .UseNLog();  // NLog: Setup NLog for Dependency injection
//    }
//}
