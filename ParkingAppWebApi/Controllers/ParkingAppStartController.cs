using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace ParkingAppWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingAppStart : ControllerBase
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public string Get()
        {
            logger.Info("Parking Web api is ready");
            return "Parking app is ready";
        }
    }
}
