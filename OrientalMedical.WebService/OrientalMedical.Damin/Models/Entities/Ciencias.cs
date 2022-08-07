using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Models.Entities
{
    public partial class Ciencias
    {
        public Ciencias()
        {
            Especialidad = new HashSet<Especialidad>();
        }

        public int CienciaId { get; set; }
        public string Ciencia { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Especialidad> Especialidad { get; set; }
    }
}
