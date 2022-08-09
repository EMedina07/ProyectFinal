using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.ResponseDTOs
{
    public class AusenciaResponseDTOs
    {
        public int AusenciaId { get; set; }
        public int DoctorId { get; set; }
        public string MotivoAusencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaReintegro { get; set; }
    }
}
