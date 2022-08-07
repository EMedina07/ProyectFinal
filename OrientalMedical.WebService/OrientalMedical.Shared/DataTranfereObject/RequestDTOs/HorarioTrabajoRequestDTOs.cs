﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Shared.DataTranfereObject.RequestDTOs
{
    public class HorarioTrabajoRequestDTOs
    {
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string MinutosPorPaciente { get; set; }
        public bool? Lunes { get; set; }
        public bool? Martes { get; set; }
        public bool? Miecoles { get; set; }
        public bool? Jueves { get; set; }
        public bool? Viernes { get; set; }
        public bool? Sabado { get; set; }
    }
}
