using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using OrientalMedical.Damin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public class PersonalRepository : BaseRepository<Personal>, IPersonalRepository
    {
        private readonly OrientalMedicalSystemDBContext _context;
        public PersonalRepository(OrientalMedicalSystemDBContext context) : base(context)
        {
            _context = context;
        }

        public int GetLastId()
        {
            return this.GetAll().Max(p => p.PersonalId);
        }

        public bool IsNewCedula(int personalId, string cedula)
        {
            string cedulaActual = this.GetAll().Where(p => p.PersonalId == personalId)
                                      .FirstOrDefault().Cedula;

            if(cedulaActual != cedula)
            {
                return false;
            }

            return true;
        }

        public bool IsResgistered(string cedula)
        {
            return this.GetAll().Select(p => p.Cedula).Contains(cedula);
        }
    }
}
