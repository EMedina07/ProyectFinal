using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IAusenciasServices
    {
        void RegistrarAusencia(AucenciasRequestDTOs aucenciasRequestDTOs);
        void UpdateAusencia(int ausenciaId, AucenciasRequestDTOs aucenciasRequestDTOs);
        void DeleteAusencia(int ausenciaId);
        List<AusenciaResponseDTOs> GetAusenciasByAsistente(int asistenteId);
        AusenciaResponseDTOs GetAusenciaDetail(int ausenciaId);
        bool IsResgistered(DateTime fechaInicio, DateTime fechaFin, string motivoAsencia);
    }
}
