using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IPacienteServices
    {
        void PacienteRegister(int asistenteId, PacienteRequestDTOs pacienteDTOs);
        void UpdatePaciente(int pacienteId, PacienteRequestDTOs pacienteDTOs);
        void DeletePaciente(int pacienteId);
        List<PacienteResponseDTOs> GetPacientesByAsistente(int asistenteId);
        PacienteResponseDTOs GetPacienteDetail(int pacienteId);
        bool IsResgistered(string cedula);
        bool IsNewCedula(int id, string cedula);
    }
}
