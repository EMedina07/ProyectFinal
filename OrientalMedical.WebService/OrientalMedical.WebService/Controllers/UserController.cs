using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientalMedical.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices = null;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("Login")]
        public IActionResult UserAccess(User user)
        {
            try
            {
                if (_userServices.IsAnUser(user.UserName, user.Password) != true)
                {
                    return BadRequest("Usuario o contraseña incorrecto");
                }

                var userDetail = _userServices.GetUserDetail(user.UserName, user.Password);

                return Ok(userDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("CambiarContraseña")]
        public IActionResult UpdatePassword(int personalId, ShangePasswordDTOs shangePasswordDTOs)
        {
            try
            {
                if (!_userServices.IsCurrentPassWord(personalId, shangePasswordDTOs.CurrentPassword))
                {
                    return BadRequest("La contraseña actual es incorrecta");
                }

                _userServices.UpdatePassword(personalId, shangePasswordDTOs.NewPassword);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
