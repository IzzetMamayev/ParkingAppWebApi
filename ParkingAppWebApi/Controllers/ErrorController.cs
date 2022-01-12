using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Data;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ParkingDBContext _parkingDBContext;
        public ErrorController(ParkingDBContext parkingDBContext)
        {
            _parkingDBContext = parkingDBContext;
        }

        [HttpPost("logging")]
        public async Task<ActionResult<Nlog>> AddLogs(Nlog logs)
        {
            var newLogs = new Nlog
            {
                MachineName = logs.MachineName,
                Logged = logs.Logged,
                Level = logs.Level,
                Message = logs.Message,
                Logger = logs.Logger,
                Properties = logs.Properties,
                Callsite = logs.Callsite,
                Exception = logs.Exception

            };

             _parkingDBContext.Nlogs.Add(newLogs);
            await _parkingDBContext.SaveChangesAsync();


            return newLogs;
        }

    }
}
