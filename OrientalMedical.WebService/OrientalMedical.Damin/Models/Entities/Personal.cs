using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Personal
    {
        public Personal()
        {
            Citas = new HashSet<Citas>();
            EspecialidadAsitente = new HashSet<Especialidad>();
            EspecialidadDoctor = new HashSet<Especialidad>();
            InverseDoctor = new HashSet<Personal>();
        }

        public int PersonalId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Ocupacion { get; set; }
        public int? DoctorId { get; set; }

        public virtual Personal Doctor { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
        public virtual ICollection<Especialidad> EspecialidadAsitente { get; set; }
        public virtual ICollection<Especialidad> EspecialidadDoctor { get; set; }
        public virtual ICollection<Personal> InverseDoctor { get; set; }
    }
}
