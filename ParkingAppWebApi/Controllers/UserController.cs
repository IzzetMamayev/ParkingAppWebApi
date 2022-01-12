using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ParkingAppWebApi.Data;
using ParkingAppWebApi.Models;
using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.DTOs;
using ParkingAppWebApi.EncryptDecrypt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ParkingDBContext _parkingDBContext;
        public UserController(ParkingDBContext parkingDBContext)
        {
            _parkingDBContext = parkingDBContext;
        }

        //-----------------------GetAllCars/SelectAddedCar/EditSelectedCar/RemoveSelectedCar----------------------------------

        [HttpGet("get-all-added-cars/{idUser}")]
        public async Task<ActionResult<IEnumerable<User_Cars>>> GetAllAddedAutoMobiles(int idUser)
        {
            var allAddedAutoMobiles = await _parkingDBContext.User_Cars.Where(x => x.IdUser == idUser).ToListAsync();
            
            if (allAddedAutoMobiles.Count == 0)
            {
                return BadRequest("Elave etdiyiniz mashin movcud deyil");
            }
            return allAddedAutoMobiles;
        }

        [HttpGet("select-added-car/{idCar}")]
        public async Task<ActionResult<User_Cars>> SelectAddedCar(int idCar)
        {
            var selectedCar = await _parkingDBContext.User_Cars.Where(x => x.IdCar == idCar).FirstOrDefaultAsync();
            if (selectedCar == null)
            {
                return BadRequest("Sechdiyiniz mashin bazada movcud deyil");
            }
            return selectedCar;
        }

        [HttpPost("add-car")]
        public async Task<ActionResult<User_Cars>> AddAuto(Auto autoAddDTO)
        {
            if (autoAddDTO.SerialNumber.Length !=7 && autoAddDTO.SerialNumber.Length != 8)
            {
                return BadRequest("Zehmet olmasa qeydiyyat nishanini duzgun daxil edin");
            }

            if (await CheckAddedCar(autoAddDTO.IdUser, autoAddDTO.SerialNumber))
            {
                return BadRequest("Bu seriya nomresi ile mashin artiq elave etmisiniz");
            }

            var addAuto = new User_Cars
            {
                IdUser = autoAddDTO.IdUser,
                IdType = autoAddDTO.IdType,
                IdModel = autoAddDTO.IdModel,
                SerialNumber = autoAddDTO.SerialNumber,
                Createdt = DateTime.Now
            };

            _parkingDBContext.User_Cars.Add(addAuto);
            await _parkingDBContext.SaveChangesAsync();

            return addAuto;
        }
        private async Task<bool> CheckAddedCar(int idUser, string serialNumber)
        {
            var existingAuto = await _parkingDBContext.User_Cars.AnyAsync(x => x.IdUser == idUser && x.SerialNumber == serialNumber);
            return existingAuto;
        }


        [HttpPut("edit-added-car")]
        public async Task<ActionResult<User_Cars>> UpdateAddedAuto(AutoUpdateDTO autoUpdateDTO)
        {
            var updateSelectAuto = await _parkingDBContext.User_Cars.Where(x => x.IdCar == autoUpdateDTO.idCar).FirstOrDefaultAsync();
            if (updateSelectAuto != null && updateSelectAuto.IdUser == autoUpdateDTO.IdUser)
            {
                if (await AutoExists(autoUpdateDTO.SerialNumber))
                {
                    return BadRequest("Bu qeydiyyat nishanli mashin bazada movcuddur");
                }
                updateSelectAuto.IdType = autoUpdateDTO.IdType;
                updateSelectAuto.IdModel = autoUpdateDTO.IdModel;
                updateSelectAuto.SerialNumber = autoUpdateDTO.SerialNumber;

                _parkingDBContext.User_Cars.Update(updateSelectAuto);
                await _parkingDBContext.SaveChangesAsync();

                return updateSelectAuto;

            }
            return BadRequest("Zehmet olmasa mashini sechin");
        }

        [HttpDelete("remove-car")]
        public async Task<ActionResult> RemoveAuto(int idCar)
        {
            var autoToRemove = await _parkingDBContext.User_Cars.Where(x => x.IdCar == idCar).FirstOrDefaultAsync();
            if (autoToRemove != null)
            {
                _parkingDBContext.User_Cars.Remove(autoToRemove);
                await _parkingDBContext.SaveChangesAsync();

                return Ok("Avtomobil ugurla silindi");
            }

            return BadRequest("Zehmet olmasa mashini sechin");

        }


        [HttpGet("get-all-car-models")]
        public async Task<ActionResult<IEnumerable<Car_Models>>> GetAllAutoModels()
        {
            return await _parkingDBContext.Car_Models.ToListAsync();
        }

        [HttpGet("get-all-car-types")]
        public async Task<ActionResult<IEnumerable<Car_Types>>> GetAllAutoTypes()
        {
            return await _parkingDBContext.AutoTypes.ToListAsync();
        }


        private async Task<bool> AutoExists(string serial)
        {
            var existingAuto = await _parkingDBContext.User_Cars.AnyAsync(x => x.SerialNumber == serial);
            return existingAuto;
        }


        //-----------Parkings_And_Zones---------------------------------------------------------
        [HttpGet("get-all-zones")]
        public async Task<ActionResult<IEnumerable<Zones>>> GetAllZones()
        {
            return await _parkingDBContext.Zones.ToListAsync();
        }

        [HttpGet("get-all-parkings-by-zone/{idZone}")]
        public async Task<ActionResult<IEnumerable<Parkings>>> GetAllParkingsByZone(int idZone)
        {
            var allParkingsInZone = await _parkingDBContext.Parkings.Where(x => x.IdZone == idZone).ToListAsync();
            if (allParkingsInZone.Count == 0)
            {
                return BadRequest("Sechdiyiniz zona uzre parking tapilmadi");
            }
            return allParkingsInZone;
        }

        [HttpGet("get-all-parkings")]
        public async Task<ActionResult<IEnumerable<Parkings>>> GetAllParkings()
        {
            return await _parkingDBContext.Parkings.ToListAsync();
        }


        //-------------------ChangePasswordOfUSer-------------------------------------------------
 
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var userNeedChangePassword = await _parkingDBContext.ParkingUsers.Where(x => x.IdUser == changePasswordDTO.IdUser).FirstOrDefaultAsync();
            var decryptedPass = EncryptingDecrypting.ConvertToDecrypt(userNeedChangePassword.Password);
            if (decryptedPass == changePasswordDTO.OldPassword)
            {
                if (changePasswordDTO.NewPassword == changePasswordDTO.ConfirmPassword)
                {
                    userNeedChangePassword.Password = EncryptingDecrypting.ConvertToEncrypt(changePasswordDTO.NewPassword);
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

        //-------------------ChangeUserInfo-------------------------------------------------
        [HttpPut("change-user-info")]
        public async Task<ActionResult<UserDTO>> ChangeUserInfo(ChangeUserInfoDTO changeUserInfoDTO)
        {
            var userNeedChangeInfo = await _parkingDBContext.ParkingUsers.Where(x => x.IdUser == changeUserInfoDTO.IdUser).FirstOrDefaultAsync();
            if (userNeedChangeInfo != null && userNeedChangeInfo.IdUser == changeUserInfoDTO.IdUser)
            {
                userNeedChangeInfo.Name = changeUserInfoDTO.Name;
                userNeedChangeInfo.Surname = changeUserInfoDTO.Surname;
                userNeedChangeInfo.Email = changeUserInfoDTO.Email;

                _parkingDBContext.ParkingUsers.Update(userNeedChangeInfo);
                await _parkingDBContext.SaveChangesAsync();

                return new UserDTO
                {
                    Phone = userNeedChangeInfo.Phone,
                    IdUser = userNeedChangeInfo.IdUser,
                    Name = userNeedChangeInfo.Name,
                    Surname = userNeedChangeInfo.Surname,
                    Email = userNeedChangeInfo.Email
                };
            }
            return BadRequest("Zehmet olmasa duzgun deyishiklik edin");
        }

    }
}
