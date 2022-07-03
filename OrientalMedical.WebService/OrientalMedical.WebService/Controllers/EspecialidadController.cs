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
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadServices _services;

        public EspecialidadController(IEspecialidadServices services)
        {
            _services = services;
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
