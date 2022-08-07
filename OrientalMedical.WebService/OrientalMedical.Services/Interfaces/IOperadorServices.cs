using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IOperadorServices
    {
        void RegisterOperador(int doctorId, OperadorRequestDTOs operadorRequestDTOs);
        void UpdateOperador(int operadorId, OperadorRequestDTOs operadorRequestDTOs);
        OperadorResponseDTOs GetOperadorDetail(int operadorId);
        bool IsResgistered(string cedula);
        bool IsNewCedula(int personalId, string cedula);
        List<OperadorResponseDTOs> GetOperadoresByDoctor(int doctorId);
        void DeleteOperador(int operadorId);
    }
}
