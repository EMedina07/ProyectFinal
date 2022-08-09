using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.ResponseDTOs
{
    public class CitasResponseDTOs
    {
        public int CitaId { get; set; }
        public DateTime FechaCita { get; set; }
        public int EspecialidadId { get; set; }
        public int DoctorId { get; set; }
        public int PacienteId { get; set; }
        public int Estado { get; set; }
    }
}
