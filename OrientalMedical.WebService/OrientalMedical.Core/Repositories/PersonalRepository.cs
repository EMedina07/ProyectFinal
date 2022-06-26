using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public int GetLastId()
        {
            return this.GetAll().Max(p => p.PersonalId);
        }

        public bool IsResgistered(string cedula)
        {
            return this.GetAll().Select(p => p.Cedula).Contains(cedula);
        }
    }
}
