﻿using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    class UserRepository : BaseRepository<Usuarios>, IUserRepository
    {
        private readonly OrientalMedicalDBContext _context;
        public UserRepository(OrientalMedicalDBContext context) : base(context)
        {
            _context = context;
        }

        public bool IsCurrentPassWord(string password)
        {
            return this.GetAll().Select(u => u.Clave).Contains(password);
        }

        public int GetUserId(string userName, string password)
        {
            return this.GetAll().Where(u => u.Usuario == userName && u.Clave == password)
                                 .Select(u => u.PersonalId).FirstOrDefault();
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
