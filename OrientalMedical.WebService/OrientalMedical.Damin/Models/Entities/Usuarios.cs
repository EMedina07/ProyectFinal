using System;
using System.Collections.Generic;

namespace OrientalMedical.Damin.Entities
{
    public partial class Usuarios
    {
        public int UsuarioId { get; set; }
        public int PersonalId { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }

        public virtual Personal Personal { get; set; }
    }
}
