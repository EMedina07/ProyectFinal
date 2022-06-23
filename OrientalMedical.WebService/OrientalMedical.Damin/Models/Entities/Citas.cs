using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Entities
{
    public partial class Citas
    {
        public int CitaId { get; set; }
        public int EspecialidadId { get; set; }
        public int DoctorId { get; set; }
        public int PacienteId { get; set; }
        public int Estado { get; set; }

        public virtual Personal Doctor { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
