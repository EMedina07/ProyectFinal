using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Damin.Entities;
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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _services;

        public DoctorController(IDoctorServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetDoctorDetail(int doctorId)
        {
            try
            {
                var doctor = _services.GetDoctorDetail(doctorId);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateDoctor([FromBody] DoctorRequestDTOs doctorDTOs)
        {
            string ocupacion = doctorDTOs.Ocupacion.ToLower();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                if(ocupacion != "doctor")
                {
                    return BadRequest($"{ocupacion} no es una ocupacion valida, en este campo debe ingresar doctor");
                }

                if (_services.IsResgistered(doctorDTOs.Cedula))
                {
                    return BadRequest($"Ya hay un doctor registrado con este numero de cedula");
                }

                _services.RegisterDoctor(doctorDTOs);

                return Ok(); 
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpPut]
        public IActionResult UpdatePropietario(int id, [FromBody] DoctorRequestDTOs doctorDTOs)
        {
            try
            {
                if (doctorDTOs == null)
                {
                    return BadRequest("Objecto no valido");
                }

                _services.UpdateDoctor(id, doctorDTOs);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }
    }
}
