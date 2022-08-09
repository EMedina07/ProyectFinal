using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.RequestDTOs
{
    public class AucenciasRequestDTOs
    {
        public int DoctorId { get; set; }
        public string MotivoAusencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaReintegro { get; set; }
    }
}
