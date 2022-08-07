using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class DiasLaborables
    {
        public int DiasLaborablesId { get; set; }
        public int HorarioId { get; set; }
        public bool? Lunes { get; set; }
        public bool? Martes { get; set; }
        public bool? Miecoles { get; set; }
        public bool? Jueves { get; set; }
        public bool? Viernes { get; set; }
        public bool? Sabado { get; set; }
        public bool IsActive { get; set; }

        public virtual Horario Horario { get; set; }
    }
}
