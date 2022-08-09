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
    public class AusenciasController : ControllerBase
    {
        private readonly IAusenciasServices _services;

        public AusenciasController(IAusenciasServices services)
        {
            _services = services;
        }

        [HttpGet("ObtenerAusencia")]
        public IActionResult GetAusenciasDetail(int ausenciasId)
        {
            try
            {
                var ausencia = _services.GetAusenciaDetail(ausenciasId);

                return Ok(ausencia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("EliminarAusencia")]
        public IActionResult DeleteAusencia(int ausenciaId)
        {
            try
            {
                _services.DeleteAusencia(ausenciaId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("ObtenerAusenciasPorAsistente")]
        public IActionResult GetAusenciasByAsistente(int asistenteId)
        {
            try
            {
                var ausencias = _services.GetAusenciasByAsistente(asistenteId);

                return Ok(ausencias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegistrarAusencia")]
        public IActionResult RegistrarAusencia(AucenciasRequestDTOs aucenciasRequestDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if (_services.IsResgistered(aucenciasRequestDTOs.FechaInicio, aucenciasRequestDTOs.FechaReintegro, aucenciasRequestDTOs.MotivoAusencia))
                {
                    return BadRequest("La ausencia ya se encuentra registrada");
                }

                _services.RegistrarAusencia(aucenciasRequestDTOs);

                return Ok(new { Detail = "Registro exitoso" });
            }
            catch (Exception)
            {
                return StatusCode(500, $"Error del servidor");
            }
        }

        [HttpPut("ModicicarAusencia")]
        public IActionResult UpdateAusencia(int ausenciaId, AucenciasRequestDTOs aucenciasRequestDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                _services.UpdateAusencia(ausenciaId, aucenciasRequestDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
