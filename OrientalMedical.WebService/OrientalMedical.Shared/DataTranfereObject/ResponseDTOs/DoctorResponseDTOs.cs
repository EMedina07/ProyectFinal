using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.ResponseDTOs
{
    public class DoctorResponseDTOs
    {
        public int PersonalId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Ocupacion { get; set; }
        public string? Especialidad { get; set; }
    }
}
