using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using OrientalMedical.Damin.Models.Entities;
using OrientalMedical.Shared.Utilities.Helpers;
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

            if (user.Clave.DesEncriptar() != password)
            {
                return false;
            }

            return true;
        }

        public int GetUserId(string userName, string password)
        {
            int personalId = 0;
            var users = this.GetAll().Where(u => u.Usuario == userName && u.IsActive != false).ToList();

            foreach (var item in users)
            {
                if (item.Clave.DesEncriptar() == password)
                {
                    personalId = item.PersonalId;
                }
            }

            return personalId;
        }

        public bool IsAnUser(string userName, string password)
        {
            int count = 0;

            var users = this.GetAll().Where(u => u.Usuario == userName && u.IsActive != false).ToList();

            foreach (var item in users)
            {
                if(item.Clave.DesEncriptar() == password)
                {
                    count++;
                }
            }

            if(count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
