using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            Citas = new HashSet<Citas>();
        }

        public int EspecialidadId { get; set; }
        public string Especialidad1 { get; set; }
        public int DoctorId { get; set; }
        public int? AsitenteId { get; set; }
        public int? TotalPacientes { get; set; }

        public virtual Personal Asitente { get; set; }
        public virtual Personal Doctor { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
