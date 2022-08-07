using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Personal
    {
        public Personal()
        {
            Ausencia = new HashSet<Ausencia>();
            Citas = new HashSet<Citas>();
            Especialidad = new HashSet<Especialidad>();
            Horario = new HashSet<Horario>();
            InverseDoctor = new HashSet<Personal>();
            Paciente = new HashSet<Paciente>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int PersonalId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Ocupacion { get; set; }
        public int? DoctorId { get; set; }
        public bool IsActive { get; set; }

        public virtual Personal Doctor { get; set; }
        public virtual ICollection<Ausencia> Ausencia { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
        public virtual ICollection<Especialidad> Especialidad { get; set; }
        public virtual ICollection<Horario> Horario { get; set; }
        public virtual ICollection<Personal> InverseDoctor { get; set; }
        public virtual ICollection<Paciente> Paciente { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
