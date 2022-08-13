using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OrientalMedical.Shared.Utilities
{
    public class Enums
    {
        public enum CitaState
        {
            [Description("Registrada")]
            Registrada,
            [Description("Confirmada")]
            Confirmada,
            [Description("Cancelada")]
            Cancelada,
            [Description("Atendida")]
            Atendida
        }

        public static string GetState(int state)
        {
            string estado = "";

            switch (state)
            {
                case 0:
                    estado = "Inactivo";
                break;

                case 1:
                    estado = "Activo";
                break;

                case 2:
                    estado = "Bloqueado";
                break;
            }

            return estado;
        }
    }
}
