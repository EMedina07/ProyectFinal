using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Asistente
    {
        public Asistente()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int AsistenteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public int? DoctorId { get; set; }

        public virtual Personal Doctor { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
