using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OrientalMedical.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ICitasServices _services;

        public CitasController(ICitasServices services)
        {
            _services = services;
        }

        [HttpDelete("EliminarCita")]
        public IActionResult DeleteHorario(int citaId)
        {
            try
            {
                _services.DeleteCita(citaId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerEstadosDeCitas")]
        public IActionResult GetCitasState()
        {
            try
            {
                var states = OptionsEnumsHelper.GetCitasStates();

                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerCitasPorDoctor")]
        public IActionResult GetCitasByDoctor(int doctorId)
        {
            try
            {
                var citas = _services.GetByDoctor(doctorId);

                return Ok(citas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerCita")]
        public IActionResult GetCitaDetail(int citaId)
        {
            try
            {
                var cita = _services.GetCitaDetail(citaId);

                return Ok(cita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarCitas")]
        public IActionResult CreateCitas(int asistenteId, [FromBody] CitasRequestDTOs citasRequestDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (DateTime.Parse(citasRequestDTOs.FechaCita.ToString("dd/MM/yyyy")).ToString("dddd", new CultureInfo("es-ES")) == "domingo")
                {
                    return BadRequest("Favor de ingresar un dia diadefere del Domingo");
                }

                if (!_services.FechaCitaIsValid(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Horario no valido favor consultar el horario de su doctor");
                }

                if (!_services.FechaIsAvailable(citasRequestDTOs.PacienteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest($"El paciente ya posee una cita en esta fecha favor de validar con el paciente");
                }

                if (!_services.DoctorIsAvailable(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Doctor no disponible favor consultar las ausencias Programadas");
                }

                if(!_services.HorarioIsValid(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Tiempo por paciente establecido no es valido");
                }

                if (_services.HoraCitaIsOcuped(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Doctor ya posee una cita programada en este horario");
                }

                _services.CreateCitas(asistenteId, citasRequestDTOs);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarCita")]
        public IActionResult UpdateCita(int citaId, int asistenteId, [FromBody] CitasRequestDTOs citasRequestDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (citaId == 0)
                {
                    return BadRequest("Id no valido");
                }

                if (DateTime.Parse(citasRequestDTOs.FechaCita.ToString("dd/MM/yyyy")).ToString("dddd", new CultureInfo("es-ES")) == "domingo")
                {
                    return BadRequest("Favor de ingresar un dia diadefere del Domingo");
                }

                if (!_services.FechaCitaIsValid(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Horario no valido favor consultar el horario de su doctor");
                }

                if (!_services.FechaIsAvailable(citasRequestDTOs.PacienteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest($"El paciente ya posee una cita en esta fecha favor de validar con el paciente");
                }

                if (!_services.DoctorIsAvailable(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Doctor no disponible favor consultar las ausencias Programadas");
                }

                if (!_services.HorarioIsValid(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Tiempo por paciente establecido no es valido");
                }

                if (_services.HoraCitaIsOcuped(asistenteId, citasRequestDTOs.FechaCita))
                {
                    return BadRequest("Doctor ya posee una cita programada en este horario");
                }

                _services.UpdateCitas(citaId, citasRequestDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpPut("TrabajarCitas")]
        public IActionResult TrabajarCitas([FromBody] ManejoDeCitasModel manejoDeCitasModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (manejoDeCitasModel.CitaId == 0)
                {
                    return BadRequest("Id no valido");
                }

                _services.TrabajarCitas(manejoDeCitasModel);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpGet("ObtenerCitasPorAsistentes")]
        public IActionResult GetByAsistente(int asistenteId, DateTime? fechaInicio, DateTime? fechaFin, int? Estado, string? doctor, string? especialidad, string? paciente, int? page)
        {
            int records = 10;
            int _page = 0;
            int total_records = 0;
            int total_pages = 0;

            var allCitas = _services.GetByAsistente(asistenteId, fechaInicio, fechaFin, Estado);

            try
            {

                List<CitaModel> citas = null;

                if (doctor != null || especialidad != null || paciente != null)
                {
                    citas = allCitas.Where(c => c.Doctor == doctor || c.Especialidad == especialidad || c.Paciente == paciente).ToList();
                }
                else
                {
                    _page = page ?? 1;
                    total_records = allCitas.Count;
                    total_pages = Convert.ToInt32(Math.Ceiling(total_records / (double)records));

                    citas = allCitas.OrderBy(p => p.Paciente).Skip((_page - 1) * records).Take(records).ToList();
                }

                return Ok(new
                {
                    Pages = total_pages,
                    Records = citas,
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
