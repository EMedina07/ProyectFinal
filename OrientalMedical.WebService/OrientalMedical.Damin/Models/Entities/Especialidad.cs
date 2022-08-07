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
        public int DoctorId { get; set; }
        public int CienciaId { get; set; }
        public bool IsActive { get; set; }

        public virtual Ciencias Ciencia { get; set; }
        public virtual Personal Doctor { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
