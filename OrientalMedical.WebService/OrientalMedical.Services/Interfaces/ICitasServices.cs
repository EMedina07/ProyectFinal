using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface ICitasServices
    {
        void CreateCitas(CitasRequestDTOs citasRequestDTOs);
        void UpdateCitas(int citaId, CitasRequestDTOs citasRequestDTOs);
        List<CitasResponseDTOs> GetByDoctor(int doctorId);
    }
}
