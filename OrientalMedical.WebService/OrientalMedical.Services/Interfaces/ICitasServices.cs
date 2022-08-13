using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface ICitasServices
    {
        void CreateCitas(int asistenteId, CitasRequestDTOs citasRequestDTOs);
        void UpdateCitas(int citaId, CitasRequestDTOs citasRequestDTOs);
        CitasResponseDTOs GetCitaDetail(int citaId);
        List<CitaModel> GetByDoctor(int doctorId);
        List<CitaModel> GetByAsistente(int asistenteId, DateTime? fechaInicio, DateTime? fechaFin);
        void TrabajarCitas(ManejoDeCitasModel manejoDeCitasModel);
        bool DoctorIsAvailable(int asistenteId, DateTime fechaCita);
        bool FechaIsAvailable(int pacienteId, DateTime fechaCita);
        bool FechaCitaIsValid(int asistenteId, DateTime fechaCita);
        bool HoraCitaIsOcuped(int asistenteId, DateTime fechaCitaIngreso);
        bool HorarioIsValid(int asistenteId, DateTime fechaCita);
        bool DiaIsAvailable(int asistenteId, DateTime fechaCita);
        bool IsFechaInicioMayorQueFechaFin(DateTime fechaInicio, DateTime fechaFin);
        void DeleteCita(int citaId);
    }
}
