using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    class PersonalRepository : BaseRepository<Personal>, IPersonalRepository
    {
        private readonly OrientalMedicalDBContext _context;
        public PersonalRepository(OrientalMedicalDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
