using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public class RepositoriesWrapper : IRepositoriesWrapper
    {
        public const string PASSWORD = "1234";

        private IPersonalRepository _personalRepository = null;
        private IUserRepository _userRepository = null;
        private readonly OrientalMedicalDBContext _context;

        public RepositoriesWrapper(OrientalMedicalDBContext context)
        {
            _context = context;
        }

        public IPersonalRepository personalRepository
        {
            get
            {
                if (_personalRepository == null)
                {
                    _personalRepository = new PersonalRepository(_context);
                }

                return _personalRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public string password
        {
            get
            {
                return PASSWORD;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
