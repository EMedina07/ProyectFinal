using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Damin.Models.DTOs;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Validations;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientalMedical.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;
        private readonly IUserServices _userServices;
        private readonly IAdministradorServices _administradorServices;

        public AdministradorController(IDoctorServices doctorServices, IUserServices userServices, IAdministradorServices administradorServices)
        {
            _doctorServices = doctorServices;
            _userServices = userServices;
            _administradorServices = administradorServices;
        }

        [HttpPost("RegistrarDoctor")]
        public IActionResult CreateDoctor([FromBody] DoctorRequestDTOs doctorDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (!PersonalValidations.IsDoctor(doctorDTOs.Ocupacion))
                {
                    return BadRequest($"{doctorDTOs.Ocupacion} no es una ocupacion valida, en este campo debe ingresar doctor");
                }

                if (!PersonalValidations.CelulaContainChar(doctorDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener numeros");
                }

                if (!PersonalValidations.CelulaLengthIsValid(doctorDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener 11 caracteres numericos");
                }

                if (_doctorServices.IsResgistered(doctorDTOs.Cedula))
                {
                    return BadRequest($"Ya hay un perfil registrado con este numero de cedula");
                }

                _administradorServices.RegistrarDoctor(doctorDTOs);

                return Ok(_userServices.GetCredentials(doctorDTOs.Cedula));
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ResetearContraseña")]
        public IActionResult UpdatePassword(int userId, string clave)
        {
            try
            {
                _administradorServices.ResetearClave(userId, clave);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers(string userName)
        {
            try
            {
                List<UsuarioDTOs> usuarios = null;

                if (userName != null)
                {
                    usuarios = _administradorServices.ObtenerUser().Where(u => u.Usuario == userName).ToList();
                }
                else
                {
                    usuarios = _administradorServices.ObtenerUser();
                }

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
