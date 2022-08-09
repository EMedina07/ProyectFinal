using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using OrientalMedical.Damin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public class AucenciasRepository : BaseRepository<Ausencia>, IAucenciasRepository
    {
        public AucenciasRepository(OrientalMedicalSystemDBContext context) : base(context)
        {
        }
    }
}
