using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NLog;
using ParkingAppWebApi.Data;
using ParkingAppWebApi.DTOs;
using ParkingAppWebApi.EncryptDecrypt;
using ParkingAppWebApi.Models;
using ParkingAppWebApi.Services;

namespace ParkingAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private readonly TokenService _tokenService;
        private readonly ParkingDBContext _parkingDBContext;
        public AccountController(ParkingDBContext parkingDBContext, TokenService tokenService /*, UserManager<ParkingUsers> userManager, SignInManager<ParkingUsers> signInManager*/)
        {
            _tokenService = tokenService;
            _parkingDBContext = parkingDBContext;

        }

        [HttpPost("user/register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            string textPrefix = registerDTO.Phone;
            textPrefix = textPrefix.Remove(3);

            if (await UserExists(registerDTO.Phone))
            {
                return BadRequest("Bu telefon nomresi ile qeydiyyat olunub");
            }
            if (textPrefix != "994" || registerDTO.Phone.Length != 12 )
            {
                return BadRequest("Zehmet olmasa telefon nomresini duzgun qaydada daxil edin");
            }

            var newUser = new ParkingUsers
            {
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                Phone = registerDTO.Phone,
                Password = EncryptingDecrypting.ConvertToEncrypt(registerDTO.Password),
                Createdt = DateTime.Now

            };

            _parkingDBContext.ParkingUsers.Add(newUser);
            await _parkingDBContext.SaveChangesAsync();

            return new UserDTO
            {
                Phone = newUser.Phone,
                Token = _tokenService.CreateToken(newUser),
                IdUser = newUser.IdUser,
                Name = newUser.Name,
                Surname = newUser.Surname,
                Email = newUser.Email
            };
        }


        [HttpPost("user/login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                logger.Trace("User using {loginDTO} model", loginDTO);
                string textPrefix = loginDTO.Phone;
                textPrefix = textPrefix.Remove(3);
                UserDTO loginUser;

                if (textPrefix != "994" || loginDTO.Phone.Length != 12)
                {
                    logger.Error("User don't use right format of phone number");
                    return BadRequest("Zehmet olmasa telefon nomresini duzgun qaydada daxil edin");
                }

                var user = await _parkingDBContext.ParkingUsers.SingleOrDefaultAsync(x => x.Phone == loginDTO.Phone);
                if (user == null)
                {
                    return Unauthorized("Bele bir istifadechi tapilmadi");
                }

                if (EncryptingDecrypting.ConvertToDecrypt(user.Password) != loginDTO.Password)
                {
                    return Unauthorized("Parol sehvdir");
                }

                loginUser = new UserDTO
                {
                    Phone = user.Phone,
                    Token = _tokenService.CreateToken(user),
                    IdUser = user.IdUser,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email
                };
                logger.Info("Succesful login");

                return loginUser;


            }
            catch (Exception message)
            {
                logger.Error(message.ToString());
            }
            return Ok();
        }

    


        [HttpPost("inspector/register")]
        public async Task<ActionResult<InspectorDTO>> RegisterInspector(RegisterInspectorDTO registerInspectorDTO)
        {
            string textPrefix = registerInspectorDTO.Phone;
            textPrefix = textPrefix.Remove(3);

            if (await InspectorExist(registerInspectorDTO.Phone))
            {
                return BadRequest("Bu telefon nomresi ile qeydiyyat olunub");
            }
            if (textPrefix != "994" || registerInspectorDTO.Phone.Length != 12)
            {
                return BadRequest("Zehmet olmasa telefon nomresini duzgun qaydada daxil edin");
            }

            var newInspector = new Inspectors
            {
                Name = registerInspectorDTO.Name,
                Surname = registerInspectorDTO.Surname,
                Phone = registerInspectorDTO.Phone,
                Password = EncryptingDecrypting.ConvertToEncrypt(registerInspectorDTO.Password),
                Createdt = DateTime.Now,
                IdZone = registerInspectorDTO.IdZone,
                IdParking = registerInspectorDTO.IdParking
            };

            _parkingDBContext.Inspectors.Add(newInspector);
            await _parkingDBContext.SaveChangesAsync();

            return new InspectorDTO
            {
                Phone = newInspector.Phone,
                Token = _tokenService.CreateTokenInspector(newInspector),
                IdInspector = newInspector.IdInspector,
                Name = newInspector.Name,
                Surname = newInspector.Surname,
                IdZone = newInspector.IdZone,
                IdParking = newInspector.IdParking,
            };
        }

        [HttpPost("inspector/login")]
        public async Task<ActionResult<InspectorDTO>> LoginInspector(LoginDTO loginDTO)
        {
            string textPrefix = loginDTO.Phone;
            textPrefix = textPrefix.Remove(3);

            if (textPrefix != "994" || loginDTO.Phone.Length != 12)
            {
                return BadRequest("Zehmet olmasa telefon nomresini duzgun qaydada daxil edin");
            }

            var inspector = await _parkingDBContext.Inspectors.SingleOrDefaultAsync(x => x.Phone == loginDTO.Phone);
            if (inspector == null)
            {
                return Unauthorized("Bele bir inspector tapilmadi");
            }

            if (EncryptingDecrypting.ConvertToDecrypt(inspector.Password) != loginDTO.Password)
            {
                return Unauthorized("Parol sehvdir");
            }

            return new InspectorDTO
            {
                Phone = inspector.Phone,
                Token = _tokenService.CreateTokenInspector(inspector),
                IdInspector = inspector.IdInspector,
                Name = inspector.Name,
                Surname = inspector.Surname,
                IdZone = inspector.IdZone,
                IdParking = inspector.IdParking,
            };
        }


        private async Task<bool> UserExists(string userPhone)
        {
            return await _parkingDBContext.ParkingUsers.AnyAsync(x => x.Phone == userPhone);
        }

        private async Task<bool> InspectorExist(string userPhone)
        {
            return await _parkingDBContext.Inspectors.AnyAsync(x => x.Phone == userPhone);
        }



    }
}






//    [HttpPost("changepassword")]
//    [Obsolete]
//    public async Task<ActionResult<ParkingUsers>> ChangePassword(ChangePasswordDTO changePasswordDTO)
//    {
//        var newPass = new SqlParameter("newPass", changePasswordDTO.NewPassword);
//        var idUser = new SqlParameter("idUser", changePasswordDTO.UserId);
//        await _parkingDBContext.Database.ExecuteSqlCommandAsync($"UPDATE parking_users SET password = (newPass) WHERE id_user =");
//        await _parkingDBContext.SaveChangesAsync();
//        //var user = _parkingDBContext.ParkingUsers.Where(x => x.IdUser == changePasswordDTO.UserId);

//        //if (changePasswordDTO.NewPassword == changePasswordDTO.ConfirmPassword )
//        //{



//    }

//        return Ok("Parolunuz ugurla deyishdirildi login olduz");

//}




//private readonly UserManager<ParkingUsers> _userManager;
//private readonly SignInManager<ParkingUsers> _signInManager;

//private readonly ParkingDBContext _parkingDBContext;

//public UserController(ParkingDBContext parkingDBContext, UserManager<ParkingUsers> userManager, SignInManager<ParkingUsers> signInManager)
//{
//    _parkingDBContext = parkingDBContext;
//    _userManager = userManager;
//    _signInManager = signInManager;
//}


////[HttpPost("addautomobiles")]
////public async Task<ParkingUsers> AddAutomobiles(ChangePasswordDTO changePasswordDTO)
////{
////    var userID = GetUserByIdAsync(changePasswordDTO.)
////}

//[HttpPost("changepassword")]
//public async Task<ParkingUsers> ChangePassword(ChangePasswordDTO changePasswordDTO)
//{

//    var userID = GetUserByIdAsync(changePasswordDTO.)
//        }




//public async Task<ParkingUsers> GetUserByIdAsync(int id)
//{
//    return await _parkingDBContext.ParkingUsers.FindAsync(id);
//}


//---------------------S_Identity-----------------------------
//[httppost("register")]
//public async task<actionresult<parkingusers>> register([frombody] registerdto registerdto)
//{
//    var userexist = await _usermanager.findbynameasync(registerdto.username);
//    if (userexist != null)
//    {
//        return badrequest("cannot do it user is exist");
//    }

//    parkingusers parkinguser = new parkingusers
//    {
//        email = registerdto.email,
//        securitystamp = guid.newguid().tostring(),
//        username = registerdto.username
//    };

//    var result = await _usermanager.createasync(parkinguser, registerdto.password);
//    if (!result.succeeded)
//    {
//        return badrequest("baaad");
//    }
//    return ok();
//}
