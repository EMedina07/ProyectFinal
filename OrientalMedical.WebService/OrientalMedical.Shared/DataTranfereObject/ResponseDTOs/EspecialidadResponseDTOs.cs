using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.ResponseDTOs
{
    public class EspecialidadResponseDTOs
    {
        public int EspecialidadId { get; set; }
        public string Especialidad1 { get; set; }
        public int DoctorId { get; set; }
        public int? AsitenteId { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string MinutosPorPaciente { get; set; }
    }
}
