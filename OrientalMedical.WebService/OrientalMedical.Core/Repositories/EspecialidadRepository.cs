using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using OrientalMedical.Damin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public class EspecialidadRepository : BaseRepository<Especialidad>, IEspecialidadRepository
    {
        private readonly OrientalMedicalSystemDBContext _context;

        public EspecialidadRepository(OrientalMedicalSystemDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
