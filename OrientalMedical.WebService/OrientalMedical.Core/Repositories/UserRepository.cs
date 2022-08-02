using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using OrientalMedical.Damin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    class UserRepository : BaseRepository<Usuarios>, IUserRepository
    {
        private readonly OrientalMedicalSystemDBContext _context;
        private readonly IPersonalRepository _personalRepository;
        public UserRepository(OrientalMedicalSystemDBContext context, IPersonalRepository personalRepository) : base(context)
        {
            _context = context;
            _personalRepository = personalRepository;
        }

        public UserRepository(OrientalMedicalSystemDBContext context) : base(context)
        {
        }

        public bool IsCurrentPassWord(int personalId, string password)
        {
            Usuarios user = this.GetAll().Where(u => u.PersonalId == personalId).FirstOrDefault();
            
            if(user.Clave != password)
            {
                return false;
            }

            return true;
        }

        public int GetUserId(string userName, string password)
        {
            return this.GetAll().Where(u => u.Usuario == userName && u.Clave == password)
                                 .Select(u => (int)u.PersonalId).FirstOrDefault();
        }

        public bool IsAnUser(string userName, string password)
        {
            Usuarios user = null;

            user = this.GetAll().Where(u => u.Usuario == userName && u.Clave == password)
                       .FirstOrDefault();

            if(user == null)
            {
                return false;
            }

            return true;
        }
    }
}
