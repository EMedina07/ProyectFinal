using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OrientalMedical.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadServices _services;

        public EspecialidadController(IEspecialidadServices services)
        {
            _services = services;
        }

        [HttpGet("GetHoras")]
        public IActionResult GetHoras()
        {
            int horaByDay = 12;
            int horaInicio = 8;

            List<string> horas = new List<string>();

            for (int i = 0; i < horaByDay; i++)
            {
                horas.Add(DateTime.Parse($"{horaInicio + i}:00:00").ToString("hh:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return Ok(horas);
        }

        [HttpGet("ObtenerEspecialidadPorAsistente")]
        public IActionResult GetEsoecialidadByAsistente(int asistenteId)
        {
            try
            {
                var doctor = _services.GetEspecialidadForAsistente(asistenteId);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerEspecialidades")]
        public IActionResult GetEspecialidades(int doctorId)
        {
            try
            {
                var especialidades = _services.GetEspecialidades(doctorId);

                return Ok(especialidades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarEspecialidades")]
        public IActionResult CreateEspecialidad([FromBody] EspecialidadRequestDTOs especialidadDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (_services.IsRegisterd(especialidadDTOs.DoctorId, especialidadDTOs.Especialidad1))
                {
                    return BadRequest($"Ya tiene esta especialidad registrada");
                }

                if (!_services.AsistenIsAvailable(especialidadDTOs.AsitenteId, especialidadDTOs.HoraInicio, especialidadDTOs.HoraInicio))
                {
                    var horaDisponible = DateTime.Parse($"{_services.HorarioDisponible(especialidadDTOs.AsitenteId)}").ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);

                    return BadRequest($"Asistente no disponible favor ingresar una hora de inicio superio a {horaDisponible}");
                }

                _services.CreateEspecialidad(especialidadDTOs);

                return Ok(new { Detail = "Registro exitoso"});
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarDoctor")]
        public IActionResult UpdateDoctor(int id, [FromBody] EspecialidadRequestDTOs especialidadDTOs)
        {
            try
            {
                if (especialidadDTOs == null)
                {
                    return BadRequest("Objecto no valido");
                }

                if (id == 0)
                {
                    return BadRequest("Id no valido");
                }

                _services.UpdateEspecialidad(id, especialidadDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
