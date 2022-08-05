﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.RequestDTOs
{
    public class CitasRequestDTOs
    {
        public DateTime FechaCita { get; set; }
        public int EspecialidadId { get; set; }
        public int DoctorId { get; set; }
        public int PacienteId { get; set; }
        public int Estado { get; set; }
        public string Comentario { get; set; }
    }
}
