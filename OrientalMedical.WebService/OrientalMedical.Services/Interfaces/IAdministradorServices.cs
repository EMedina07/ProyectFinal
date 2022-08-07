using OrientalMedical.Damin.Models.DTOs;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IAdministradorServices
    {
        List<UsuarioDTOs> ObtenerUser();
        void ResetearClave(int personalId, string clave);
    }
}
