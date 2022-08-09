using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OrientalMedical.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioServices _services;
        private readonly IHorarioTrabajoServices _horarioTrabajoServices;

        public HorarioController(IHorarioServices services, IHorarioTrabajoServices horarioTrabajoServices)
        {
            _services = services;
            _horarioTrabajoServices = horarioTrabajoServices;
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

        [HttpGet("ObtenerHorario")]
        public IActionResult GetHorarioDetail(int horarioId)
        {
            try
            {
                var horario = _services.GetHorarioDetail(horarioId);

                return Ok(horario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("EliminarHorario")]
        public IActionResult DeleteHorario(int horarioId)
        {
            try
            {
                _services.DeleteHorario(horarioId);
                _horarioTrabajoServices.deleteHorario(horarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerHorarioDelDoctor")]
        public IActionResult GetHorarioByDoctor(int doctorId)
        {
            try
            {
                var horarios = _services.GetHorarioByDoctor(doctorId);

                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarHorario")]
        public IActionResult CreateHorario(int doctorId, [FromBody] HorarioTrabajoResponseDTOs horarioRequestDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (_services.HaveHorarioRegistrated(doctorId))
                {
                    return BadRequest("Ya se encuentra con horario registrado");
                }

                _services.CreateHorario(doctorId, new HorarioRequestDTOs 
                {
                    HoraInicio = horarioRequestDTOs.HoraInicio,
                    HoraFin = horarioRequestDTOs.HoraFin,
                    MinutosPorPaciente = horarioRequestDTOs.MinutosPorPaciente,
                });

                _horarioTrabajoServices.CreateHorario(doctorId, horarioRequestDTOs);

                return Ok(new { Detail = "Registro exitoso" });
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarHorario")]
        public IActionResult UpdateHorario(int horarioId, [FromBody] HorarioTrabajoResponseDTOs horarioRequestDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                _services.UpdateHorario(horarioId, new HorarioRequestDTOs
                {
                    HoraInicio = horarioRequestDTOs.HoraInicio,
                    HoraFin = horarioRequestDTOs.HoraFin,
                    MinutosPorPaciente = horarioRequestDTOs.MinutosPorPaciente,
                });

                _horarioTrabajoServices.UpdateHorario(horarioId, horarioRequestDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
