using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Horario
    {
        public Horario()
        {
            DiasLaborables = new HashSet<DiasLaborables>();
        }

        public int HorarioId { get; set; }
        public int DoctorId { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string MinutosPorPaciente { get; set; }
        public bool IsActive { get; set; }

        public virtual Personal Doctor { get; set; }
        public virtual ICollection<DiasLaborables> DiasLaborables { get; set; }
    }
}
