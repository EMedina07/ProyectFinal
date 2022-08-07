using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Services.Interfaces;
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
    public class CienciasMedicasController : ControllerBase
    {
        private readonly ICienciasMedicasServices _services;

        public CienciasMedicasController(ICienciasMedicasServices services)
        {
            _services = services;
        }

        

        [HttpGet("ObtenerCienciasMedica")]
        public IActionResult GetCienciasMedicaDetail(int cienciasMedicaId)
        {
            try
            {
                var cienciasMedica = _services.GetCienciasMedicaDetail(cienciasMedicaId);

                return Ok(cienciasMedica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpDelete("EliminarCienciasMedica")]
        public IActionResult DeleteCienciasMedica(int cienciasMedicaId)
        {
            try
            {
                _services.DeleteCienciaMedica(cienciasMedicaId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpGet("ObtenerCienciasMedicasParaDoctor")]
        public IActionResult GetCienciasMedicaByDoctor()
        {
            try
            {
                var cienciasMedicas = _services.GetAllCienciasMedicas();

                return Ok(cienciasMedicas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarCienciasMedicas")]
        public IActionResult CreateCienciasMedicas([FromBody] CienciasMedicasRequestDTOs cienciasMedicaDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (_services.CienciaMedicaIsRegistrated(cienciasMedicaDTOs.Ciencia))
                {
                    return BadRequest("La especialidad ya se encuentra registrada");
                }

                _services.CreateCienciaMedica(cienciasMedicaDTOs);

                return Ok(new { Detail = "Registro exitoso" });
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarCienciasMedica")]
        public IActionResult UpdateCienciasMedica(int cienciasMedicaId, [FromBody] CienciasMedicasRequestDTOs cienciasMedicaDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (_services.CienciaMedicaIsRegistrated(cienciasMedicaDTOs.Ciencia))
                {
                    return BadRequest("La especialidad ya se encuentra registrada");
                }

                _services.UpdateCienciaMedica(cienciasMedicaId, cienciasMedicaDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpGet("ObtenerEspecialidades")]
        public IActionResult GetEspecialidades(string? especialidad, int? page)
        {
            int records = 10;
            int _page = 0;
            int total_records = 0;
            int total_pages = 0;

            var allCienciasMedicas = _services.GetAllCienciasMedicas();

            try
            {

                List<CienciasMedicasResponseDTOs> cienciasMedicas = null;

                if (especialidad != null)
                {
                    cienciasMedicas = allCienciasMedicas.Where(c => c.Ciencia == especialidad).ToList();
                }
                else
                {
                    _page = page ?? 1;
                    total_records = allCienciasMedicas.Count;
                    total_pages = Convert.ToInt32(Math.Ceiling(total_records / (double)records));

                    cienciasMedicas = allCienciasMedicas.OrderBy(c => c.Ciencia).Skip((_page - 1) * records).Take(records).ToList();
                }

                return Ok(new
                {
                    Pages = total_pages,
                    Records = cienciasMedicas,
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
