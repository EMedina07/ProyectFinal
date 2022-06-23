using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Entities
{
    public partial class Especialidad
    {
        public int EspecialidadId { get; set; }
        public string Especialidad1 { get; set; }
        public int DoctorId { get; set; }
        public int? SecreteriaId { get; set; }
        public int? TotalPacientes { get; set; }

        public virtual Personal Doctor { get; set; }
        public virtual Personal Secreteria { get; set; }
    }
}
