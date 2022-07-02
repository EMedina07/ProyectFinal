using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Validations
{
    public static class PersonalValidations
    {
        public static bool CelulaLengthIsValid(string celula)
        {
            if(celula.Length != 11)
            {
                return false;
            }

            return true;
        }

        public static bool IsDoctor(string ocupation)
        {
            ocupation = ocupation.ToLower();

            if(ocupation != "doctor")
            {
                return false;
            }

            return true;
        }

        public static bool CelulaContainChar(string celula)
        {
            try
            {
                int.Parse(celula);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
