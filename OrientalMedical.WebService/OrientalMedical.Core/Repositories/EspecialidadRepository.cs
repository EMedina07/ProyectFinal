using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public class EspecialidadRepository : BaseRepository<Especialidad>, IEspecialidadRepository
    {
        private readonly OrientalMedicalDBContext _context;

        public EspecialidadRepository(OrientalMedicalDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
