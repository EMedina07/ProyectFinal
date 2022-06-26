using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Damin.Interfaces
{
    public interface IRepositoriesWrapper
    {
        string password { get; }
        IPersonalRepository personalRepository { get; }
        IUserRepository UserRepository { get; }

        void Save();
    }
}
