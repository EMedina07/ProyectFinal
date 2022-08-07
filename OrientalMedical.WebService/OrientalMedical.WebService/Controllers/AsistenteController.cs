using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Validations;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientalMedical.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenteController : ControllerBase
    {
        private readonly IOperadorServices _services;
        private readonly IUserServices _userServices;

        public AsistenteController(IOperadorServices services, IUserServices userServices)
        {
            _services = services;
            _userServices = userServices;
        }

        [HttpGet("ObtenerInformacionDelAsistente")]
        public IActionResult GetAsistenteDetail(int operadorId)
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

        [HttpDelete("EliminarAsistente")]
        public IActionResult DeleteAsistente(int asistenteId)
        {
            try
            {
                _services.DeleteOperador(asistenteId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarAsistente")]
        public IActionResult CreateAsistente(int doctorId, [FromBody] OperadorRequestDTOs operadorDTOs)
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

        [HttpPut("ModicicarAsistente")]
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

        [HttpGet("ObtenerAsistentesPorDoctor")]
        public IActionResult GetUsers(int doctorId, string? nombre, string? apellido, string? cedula, int? page)
        {
            int records = 10;
            int _page = 0;
            int total_records = 0;
            int total_pages = 0;

            var allUsers = _services.GetOperadoresByDoctor(doctorId);

            try
            {

                List<OperadorResponseDTOs> asistentes = null;

                if (nombre != null || apellido != null || cedula != null)
                {
                    asistentes = allUsers.Where(u => u.Nombre == nombre || u.Apellido == apellido || u.Cedula == cedula).ToList();
                }
                else
                {
                    _page = page ?? 1;
                    total_records = allUsers.Count;
                    total_pages = Convert.ToInt32(Math.Ceiling(total_records / (double)records));

                    asistentes = allUsers.OrderBy(u => u.Nombre).Skip((_page - 1) * records).Take(records).ToList();
                }

                return Ok(new
                {
                    Pages = total_pages,
                    Records = asistentes,
                    currentPage = _page
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
