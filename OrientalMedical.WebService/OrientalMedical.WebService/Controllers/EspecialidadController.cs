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

        [HttpGet("ObtenerEspecialidadByAsistente")]
        public IActionResult GetEspecialidadByAsistente(int asistenteId)
        {
            try
            {
                var doctor = _services.GetEspecialidadDetail(asistenteId);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerEspecialidad")]
        public IActionResult GetEspecialidad(int especialidadId)
        {
            try
            {
                var doctor = _services.GetEspecialidadDetail(especialidadId);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("EliminarEspecialidad")]
        public IActionResult DeleteEspecialidad(int especialidadId)
        {
            try
            {
                _services.DeleteEspecialidad(especialidadId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerEspecialidadesPorDoctor")]
        public IActionResult GetEspecialidades(int doctorId)
        {
            try
            {
                var especialidades = _services.GetEspecialidadesByDoctorId(doctorId);

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

                if (_services.IsRegistared(especialidadDTOs.DoctorId, especialidadDTOs.CienciaId))
                {
                    return BadRequest($"Ya cuenta con esta especialidad");
                }

                _services.CreateEspecialidad(especialidadDTOs);

                return Ok(new { Detail = "Registro exitoso"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del servidor" + ex);
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
