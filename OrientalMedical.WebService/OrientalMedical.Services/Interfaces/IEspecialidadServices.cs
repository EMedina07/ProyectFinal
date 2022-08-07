using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IEspecialidadServices
    {
        void CreateEspecialidad(EspecialidadRequestDTOs especialidadDTOs);
        void UpdateEspecialidad(int especialidadId, EspecialidadRequestDTOs especialidadDTOs);
        List<EspecialidadResponseDTOs> GetEspecialidades(int doctorId);
        List<EspecialidadesForSelect> GetEspecialidadForAsistente(int asistenteId);
    }
}
