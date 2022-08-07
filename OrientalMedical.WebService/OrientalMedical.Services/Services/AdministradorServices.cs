using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.DTOs;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Services.Services
{
    public class AdministradorServices : IAdministradorServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IUserServices _userServices = null;
        private readonly IDoctorServices _doctorServices = null;

        public AdministradorServices(IRepositoriesWrapper wrapper, IUserServices userServices, IDoctorServices doctorServices)
        {
            _wrapper = wrapper;
            _userServices = userServices;
            _doctorServices = doctorServices;
        }

        public List<UsuarioDTOs> ObtenerUser()
        {
            return _wrapper.AdministradorRepository.ObtenerUser();
        }

        public void ResetearClave(int userId, string clave)
        {
            _userServices.UpdatePasswordByUserId(userId, clave);
        }
    }
}
