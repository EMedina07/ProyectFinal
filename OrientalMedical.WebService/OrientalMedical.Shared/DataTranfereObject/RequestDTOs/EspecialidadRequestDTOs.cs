using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.RequestDTOs
{
    public class EspecialidadRequestDTOs
    {
        public string Especialidad1 { get; set; }
        public int DoctorId { get; set; }
        public int? SecreteriaId { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
    }
}
