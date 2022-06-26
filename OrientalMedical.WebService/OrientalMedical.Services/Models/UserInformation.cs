using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Models
{
    public class UserInformation
    {
        public int PersonalId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Ocupacion { get; set; }
    }
}
