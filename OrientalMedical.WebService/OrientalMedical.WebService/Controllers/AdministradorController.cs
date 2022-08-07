using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientalMedical.Damin.Models.DTOs;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Validations;
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
    public class AdministradorController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;
        private readonly IUserServices _userServices;
        private readonly IAdministradorServices _administradorServices;
        private readonly IOperadorServices _services;

        public AdministradorController(IDoctorServices doctorServices, IUserServices userServices, IAdministradorServices administradorServices, IOperadorServices services)
        {
            _doctorServices = doctorServices;
            _userServices = userServices;
            _administradorServices = administradorServices;
            _services = services;
        }

        [HttpPut("ResetearContraseña")]
        public IActionResult UpdatePassword(int userId, string clave)
        {
            try
            {
                _administradorServices.ResetearClave(userId, clave);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers(string? userName, string? nombre, string? cedula, int? page)
        {
            int records = 10;
            int _page = 0;
            int total_records = 0;
            int total_pages = 0;

            var allUsers = _administradorServices.ObtenerUser();

            try
            {

                List<UsuarioDTOs> usuarios = null;

                if (userName != null || nombre != null || cedula != null)
                {
                    usuarios = allUsers.Where(u => u.Usuario == userName || u.Nombre == nombre || u.Cedula == cedula).ToList();
                }
                else
                {
                    _page = page ?? 1;
                    total_records = allUsers.Count;
                    total_pages = Convert.ToInt32(Math.Ceiling(total_records / (double) records));
                    
                    usuarios = allUsers.OrderBy(u => u.Nombre).Skip((_page - 1) * records).Take(records).ToList();
                }

                return Ok(new {
                    Pages = total_pages,
                    Records = usuarios,
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
