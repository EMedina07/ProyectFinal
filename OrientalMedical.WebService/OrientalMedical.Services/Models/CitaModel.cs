using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Models
{
    public class CitaModel
    {
        public int CitaId { get; set; }
        public DateTime FechaCita { get; set; }
        public string Especialidad { get; set; }
        public string Doctor { get; set; }
        public string Paciente { get; set; }
        public string Telefono { get; set; }
        public int Estado { get; set; }
        public string Comentario { get; set; }
    }
}
