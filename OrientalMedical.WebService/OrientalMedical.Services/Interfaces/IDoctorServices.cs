using OrientalMedical.Services.Models;
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
        List<DoctorForSelect> GetDoctorForAsistente(int asistenteId);
        void DeleteDoctor(int doctorId);
        int GetDoctorId(string cedula);
    }
}
