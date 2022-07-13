using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IDoctorServices
    {
        void RegisterDoctor(DoctorRequestDTOs doctorDTOs);
        void UpdateDoctor(int doctorId, DoctorRequestDTOs doctorDTOs);
        DoctorResponseDTOs GetDoctorDetail(int doctorId);
        bool IsResgistered(string cedula);
        bool IsNewCedula(int personalId, string cedula);
    }
}
