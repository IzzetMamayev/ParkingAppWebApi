using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Data;
using ParkingAppWebApi.DTOs;
using ParkingAppWebApi.Models;
using ParkingAppWebApi.FunctionsDbParking;
using Newtonsoft.Json.Converters;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ParkingDBContext _parkingDBContext;
        public PaymentController(ParkingDBContext parkingDBContext)
        {
            _parkingDBContext = parkingDBContext;
        }


        [HttpPost("get-price")]
        public decimal GetPrice(GetPriceDTO getPriceDTO)
        {
            decimal getPrice = ParkingFunctions.GetPriceFromParkingDB(getPriceDTO.idUser, getPriceDTO.idZone, getPriceDTO.idParking, getPriceDTO.fromDate, getPriceDTO.fromTime, getPriceDTO.toDate, getPriceDTO.toTime, getPriceDTO.idCar);
            return getPrice;
        }



        [HttpPost("add-payment")]
        public async Task<ActionResult<Payments>> AddPayment(AddPaymentDTO addPaymentDTO)
        {
            var addPayment = new Payments
            {            
                IdUser = addPaymentDTO.IdUser,
                SerialNumber = addPaymentDTO.SerialNumber,
                IdParking = addPaymentDTO.IdParking,
                IdZone = addPaymentDTO.IdZone,
                Price = addPaymentDTO.Price,
                IdCar = addPaymentDTO.IdCar,
                DateFrom = addPaymentDTO.DateFrom,
                DateTo = addPaymentDTO.DateTo

            };

            _parkingDBContext.Payments.Add(addPayment);
            await _parkingDBContext.SaveChangesAsync();

            return addPayment;
        }


        [HttpGet("get-all-payments/{idUser}")]
        public async Task<ActionResult<IEnumerable<PaymentsWithParkingName>>> GetAllAddedPayments(int idUser)
        {

            var allPayments = await (from paym in _parkingDBContext.Payments
                                     join park in _parkingDBContext.Parkings on paym.IdParking equals park.IdParking
                                     select new PaymentsWithParkingName
                                     {
                                         IdPayment = paym.IdPayment,
                                         IdUser = paym.IdUser,
                                         SerialNumber = paym.SerialNumber,
                                         IdZone = paym.IdZone,
                                         IdParking = paym.IdParking,
                                         ParkingName = park.Name,
                                         Price = paym.Price,
                                         PayDate = paym.PayDate,
                                         IdCar = paym.IdCar,
                                         DateFrom = paym.DateFrom,
                                         DateTo = paym.DateTo

                                     }).OrderByDescending(m => m.PayDate).ToListAsync();


            return allPayments;

            //var allPayments = await _parkingDBContext.Payments.Where(x => x.IdUser == idUser).OrderByDescending(m => m.PayDate).ToArrayAsync();;
            //if (allPayments.Length == 0)
            //{
            //    return BadRequest("Odenishiniz movcud deyil");
            //}
            //return allPayments;
        }
    }
}
