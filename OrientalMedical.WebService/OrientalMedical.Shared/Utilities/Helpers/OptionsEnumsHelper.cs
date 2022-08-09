using OrientalMedical.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Shared.Utilities.Helpers
{
    public static class OptionsEnumsHelper
    {
        public static List<SelectListForCitas> GetCitasStates()
        {
            return EnumHelper.GetEnumValues<Enums.CitaState>()
                             .Select(f => new SelectListForCitas()
                             {
                                 Text = f.GetEnumDescription(),
                                 Value = f.GetHashCode().ToString()
                             }).ToList();
        }
    }
}
