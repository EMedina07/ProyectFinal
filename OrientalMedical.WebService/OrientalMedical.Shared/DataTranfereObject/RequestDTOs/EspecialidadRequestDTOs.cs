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
        public int? TotalPacientes { get; set; }
    }
}
