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
    public class OperadorController : ControllerBase
    {
        private readonly IOperadorServices _services;
        private readonly IUserServices _userServices;

        public OperadorController(IOperadorServices services, IUserServices userServices)
        {
            _services = services;
            _userServices = userServices;
        }

        [HttpGet("ObtenerInformacionDelOperador")]
        public IActionResult GetDoctorDetail(int operadorId)
        {
            try
            {
                var operador = _services.GetOperadorDetail(operadorId);

                return Ok(operador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerOperadoresParaAsignarEspecialidades")]
        public IActionResult GetOperadoresForSelect(int doctorId)
        {
            try
            {
                var operadores = _services.GetOperadoresForSelect(doctorId);

                return Ok(operadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarOperador")]
        public IActionResult CreateOperador(int doctorId, [FromBody] OperadorRequestDTOs operadorDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (!PersonalValidations.CelulaContainChar(operadorDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener numeros");
                }

                if (!PersonalValidations.CelulaLengthIsValid(operadorDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener 11 caracteres numericos");
                }

                if (_services.IsResgistered(operadorDTOs.Cedula))
                {
                    return BadRequest($"Ya hay un perfil registrado con este numero de cedula");
                }

                _services.RegisterOperador(doctorId, operadorDTOs);

                return Ok(_userServices.GetCredentials(operadorDTOs.Cedula));
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarOperador")]
        public IActionResult UpdatePropietario(int operadorId, [FromBody] OperadorRequestDTOs operadorDTOs)
        {
            try
            {
                if (operadorDTOs == null)
                {
                    return BadRequest("Objecto no valido");
                }

                if (!PersonalValidations.CelulaContainChar(operadorDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener numeros");
                }

                if (!PersonalValidations.CelulaLengthIsValid(operadorDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener 11 caracteres numericos");
                }

                if (!_services.IsNewCedula(operadorId, operadorDTOs.Cedula))
                {
                    if (_services.IsResgistered(operadorDTOs.Cedula))
                    {
                        return BadRequest($"Ya hay un perfil registrado con este numero de cedula");
                    }
                }

                _services.UpdateOperador(operadorId, operadorDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
