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
        List<EspecialidadesForSelect> GetEspecialidades(int asistenteId);
        List<EspecialidadesForSelect> GetEspecialidadesByDoctorId(int doctorId);
        EspecialidadResponseDTOs GetEspecialidadDetail(int especialidadId);
        void DeleteEspecialidad(int especialidadId);
        bool IsRegistared(int doctorId, int cienciaMedicaId);

    }
}
