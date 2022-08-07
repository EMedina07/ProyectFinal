using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IHorarioTrabajoServices
    {
        void CreateHorario(int doctorId, HorarioTrabajoResponseDTOs horarioRequestDTOs);
        void UpdateHorario(int horarioId, HorarioTrabajoResponseDTOs horarioRequestDTOs);
        void deleteHorario(int horarioId);
    }
}
