using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IPacienteServices
    {
        void PacienteRegister(string user, PacienteRequestDTOs pacienteDTOs);
        void UpdatePaciente(int pacienteId, PacienteRequestDTOs pacienteDTOs);
        //void DeletePaciente(int pacienteId);
        List<PacienteResponseDTOs> GetPacientes();
        bool IsResgistered(string cedula);
        bool IsNewCedula(int id, string cedula);
    }
}
