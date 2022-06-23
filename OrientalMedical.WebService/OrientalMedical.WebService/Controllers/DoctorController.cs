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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Objecto no valido");
                }

                _services.RegisterDoctor(doctorDTOs);

                //PropietarioResponseDTO propietarioCreated = _services.LastItem();

                return Ok(); //CreatedAtRoute("GetPropietarioById", new { id = propietarioCreated.Id }, propietarioCreated);
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
