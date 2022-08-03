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
            EspecialidadAsitente = new HashSet<Especialidad>();
            EspecialidadDoctor = new HashSet<Especialidad>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int PersonalId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Ocupacion { get; set; }
        public string UsuarioCreador { get; set; }

        public virtual ICollection<Ausencia> Ausencia { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
        public virtual ICollection<Especialidad> EspecialidadAsitente { get; set; }
        public virtual ICollection<Especialidad> EspecialidadDoctor { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
