using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Paciente
    {
        public Paciente()
        {
            Citas = new HashSet<Citas>();
        }

        public int PacienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }
    }
}
