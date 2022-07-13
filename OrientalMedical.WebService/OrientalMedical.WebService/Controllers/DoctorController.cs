using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _services;
        private readonly IUserServices _userServices;

        public DoctorController(IDoctorServices services, IUserServices userServices)
        {
            _services = services;
            _userServices = userServices;
        }

        [HttpGet("ObtenerInformacionDelDoctor")]
        public IActionResult GetDoctorDetail(int doctorId)
        {
            try
            {
                var doctor = _services.GetDoctorDetail(doctorId);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
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

                if(!PersonalValidations.IsDoctor(doctorDTOs.Ocupacion))
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

                if (_services.IsResgistered(doctorDTOs.Cedula))
                {
                    return BadRequest($"Ya hay un perfil registrado con este numero de cedula");
                }

                _services.RegisterDoctor(doctorDTOs);

                return Ok(_userServices.GetCredentials(doctorDTOs.Cedula)); 
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarDoctor")]
        public IActionResult UpdateDoctor(int id, [FromBody] DoctorRequestDTOs doctorDTOs)
        {
            try
            {
                if (doctorDTOs == null)
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

                if (!_services.IsNewCedula(id, doctorDTOs.Cedula))
                {
                    if (_services.IsResgistered(doctorDTOs.Cedula))
                    {
                        return BadRequest($"Ya hay un perfil registrado con este numero de cedula");
                    }
                }

                _services.UpdateDoctor(id, doctorDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
