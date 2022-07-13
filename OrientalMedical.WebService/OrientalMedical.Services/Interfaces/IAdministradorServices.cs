using OrientalMedical.Damin.Models.DTOs;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IAdministradorServices
    {
        void RegistrarDoctor(DoctorRequestDTOs doctorDTOs);
        List<UsuarioDTOs> ObtenerUser();
        void ResetearClave(int personalId, string clave);
    }
}
