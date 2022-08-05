using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using System;
using System.Collections.Generic;
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

        /*[HttpGet("ObtenerCitasPorDoctor")]
        public IActionResult GetCitasByDoctor(int doctorId)
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
        }*/

        [HttpPost("RegistrarCitas")]
        public IActionResult CreateCitas([FromBody] CitasRequestDTOs citasRequestDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                _services.CreateCitas(citasRequestDTOs);

                return Ok(new { Detail = "Registro exitoso" });
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarCita")]
        public IActionResult UpdateCita(int citaId, [FromBody] CitasRequestDTOs citasRequestDTOs)
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

                _services.UpdateCitas(citaId, citasRequestDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
