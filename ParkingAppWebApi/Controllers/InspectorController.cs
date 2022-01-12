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
using ParkingAppWebApi.EncryptDecrypt;
using ParkingAppWebApi.FunctionsDbParking;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InspectorController : ControllerBase
    {

        private readonly ParkingDBContext _parkingDBContext;
        public InspectorController(ParkingDBContext parkingDBContext)
        {
            _parkingDBContext = parkingDBContext;
        }

        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var inspectorNeedChangePassword = await _parkingDBContext.Inspectors.Where(x => x.IdInspector == changePasswordDTO.IdInspector).FirstOrDefaultAsync();
            var decryptedPass = EncryptingDecrypting.ConvertToDecrypt(inspectorNeedChangePassword.Password);
            if (decryptedPass == changePasswordDTO.OldPassword)
            {
                if (changePasswordDTO.NewPassword == changePasswordDTO.ConfirmPassword)
                {
                    inspectorNeedChangePassword.Password = EncryptingDecrypting.ConvertToEncrypt(changePasswordDTO.NewPassword);
                    await _parkingDBContext.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Yeni parollar eyni deyil");
                }
            }
            else
            {
                return BadRequest("kohne parol sehvdir");
            }
            return Ok("Parol ugurla deyishdirildi");
        }


        //[HttpGet("search-payments")]
        //public async Task<ActionResult<IEnumerable<Payments>>> SearchPayments([FromQuery]string serialNumber)
        //{
        //    var allPayments = await _parkingDBContext.Payments.Where(x => x.SerialNumber == serialNumber).OrderByDescending(m => m.PayDate).ToArrayAsync();
        //    if (allPayments.Length == 0)
        //    {
        //        return BadRequest("Odenish tapilmadi");
        //    }
        //    return allPayments;
        //}


        [HttpPost("search-payment")]
        public IEnumerable<PaymentsAllowed> SearchPayment(SearchPaymentDTO searchPaymentDTO)
        {

            DataTable dt = ParkingFunctions.GetAllowedPayments(searchPaymentDTO.SerialNum, searchPaymentDTO.SearchDate.ToString());

            var result =  (from rw in dt.Select()
                          select new PaymentsAllowed
                          {
                              IdPayment = Convert.ToInt32(rw["id_payment"]),
                              IdUser = Convert.ToInt32(rw["id_user"]),
                              SerialNumber = Convert.ToString(rw["serial_number"]),
                              IdZone = Convert.ToInt32(rw["id_zone"]),
                              IdParking = Convert.ToInt32(rw["id_parking"]),
                              Price = Convert.ToDecimal(rw["price"]),
                              PayDate = Convert.ToDateTime(rw["pay_date"]),
                              IdCar = Convert.ToInt32(rw["id_car"]),
                              DateFrom = Convert.ToDateTime(rw["date_from"]),
                              DateTo = Convert.ToDateTime(rw["date_to"]),
                              ParkingName = Convert.ToString(rw["name"]),
                              Allowed = Convert.ToString(rw["allowed"]),


                          });
            return result;

            //var allowedPayments = await _parkingDBContext.Database.ExecuteSqlCommand("execute dbo.sp_allowed_payments @serialNumber={0}, @seachingDate={1} ")


            //var allowedPayments = await _parkingDBContext.Payments.FromSqlRaw<PaymentsAllowed>("execute dbo.sp_allowed_payments @serialNumber={0}, @seachingDate={1}", serialNum, searchDate).ToListAsync();

            //      return allowedPayments;
            //DataTable dt = ParkingFunctions.GetAllowedPayments(searchPaymentDTO.SerialNum, searchPaymentDTO.SearchDate);
            //return getPrice;
        }

    }
}
