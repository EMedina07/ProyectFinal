using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.ResponseDTOs
{
    public class HorarioResponseDTOs
    {
        public int HorarioId { get; set; }
        public int DoctorId { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string MinutosPorPaciente { get; set; }
    }
}
