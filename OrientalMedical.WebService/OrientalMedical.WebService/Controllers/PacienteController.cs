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
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteServices _services;

        public PacienteController(IPacienteServices services)
        {
            _services = services;
        }

        [HttpGet("ObtenerPaciente")]
        public IActionResult GetPacienteDetail(int pacienteId)
        {
            try
            {
                var doctor = _services.GetPacienteDetail(pacienteId);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("EliminarPaciente")]
        public IActionResult DeletePaciente(int pacienteId)
        {
            try
            {
                _services.DeletePaciente(pacienteId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarPaciente")]
        public IActionResult RegistrarPaciente(int asistenteId, [FromBody] PacienteRequestDTOs pacienteDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (!PersonalValidations.CelulaContainChar(pacienteDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener numeros");
                }

                if (!PersonalValidations.CelulaLengthIsValid(pacienteDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener 11 caracteres numericos");
                }

                if (_services.IsResgistered(pacienteDTOs.Cedula))
                {
                    return BadRequest($"Ya hay un perfil registrado con este numero de cedula");
                }

                _services.PacienteRegister(asistenteId, pacienteDTOs);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarPaciente")]
        public IActionResult UpdatePacientes(int pacienteId, [FromBody] PacienteRequestDTOs pacienteDTOs)
        {
            try
            {
                if (pacienteDTOs == null)
                {
                    return BadRequest("Objecto no valido");
                }

                if (!PersonalValidations.CelulaContainChar(pacienteDTOs.Cedula))
                {
                    return BadRequest("La cedula solo debe contener numeros");
                }

                if (!_services.IsNewCedula(pacienteId, pacienteDTOs.Cedula))
                {
                    if (_services.IsResgistered(pacienteDTOs.Cedula))
                    {
                        return BadRequest($"Ya hay un perfil registrado con este numero de cedula");
                    }
                }

                _services.UpdatePaciente(pacienteId, pacienteDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpGet("ObtenerPacientes")]
        public IActionResult GetPacientes(int asistenteId, string? nombre, string? cedula, int? page)
        {
            int records = 10;
            int _page = 0;
            int total_records = 0;
            int total_pages = 0;

            var allPacientes = _services.GetPacientesByAsistente(asistenteId);

            try
            {

                List<PacienteResponseDTOs> pacientes = null;

                if (nombre != null || cedula != null)
                {
                    pacientes = allPacientes.Where(p => p.Nombre == nombre || p.Cedula == cedula).ToList();
                }
                else
                {
                    _page = page ?? 1;
                    total_records = allPacientes.Count;
                    total_pages = Convert.ToInt32(Math.Ceiling(total_records / (double)records));

                    pacientes = allPacientes.OrderBy(p => p.Nombre).Skip((_page - 1) * records).Take(records).ToList();
                }

                return Ok(new
                {
                    Pages = total_pages,
                    Records = pacientes,
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
