using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}
