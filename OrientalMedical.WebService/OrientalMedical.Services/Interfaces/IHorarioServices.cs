using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IHorarioServices
    {
        void CreateHorario(int doctorId, HorarioRequestDTOs horarioRequestDTOs);
        void UpdateHorario(int horarioId, HorarioRequestDTOs horarioRequestDTOs);
        HorarioTrabajoResponseDTOs GetHorarioDetail(int horarioId);
        List<HorarioTrabajoResponseDTOs> GetHorarioByDoctor(int doctorId);
        void DeleteHorario(int horarioId);
        bool HaveHorarioRegistrated(int doctorId);
    }
}
