using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface ICienciasMedicasServices
    {
        void CreateCienciaMedica(CienciasMedicasRequestDTOs CienciasMedicaRequestDTOs);
        void UpdateCienciaMedica(int cienciaMedicaId, CienciasMedicasRequestDTOs CienciasMedicaRequestDTOs);
        List<CienciasMedicasResponseDTOs> GetAllCienciasMedicas();
        CienciasMedicasResponseDTOs GetCienciasMedicaDetail(int cienciaMedicaId);
        void DeleteCienciaMedica(int cienciaMedicaId);
        bool CienciaMedicaIsRegistrated(string cienciaMedica);
    }
}
