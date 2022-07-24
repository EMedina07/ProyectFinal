using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Ausencia
    {
        public int AusenciaId { get; set; }
        public int DoctorId { get; set; }
        public string MotivoAusencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaReintegro { get; set; }

        public virtual Personal Doctor { get; set; }
    }
}
