﻿using OrientalMedical.Damin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Damin.Interfaces
{
    public interface IUserRepository : IRepositorioBase<Usuarios>
    {
        bool IsAnUser(string userName, string password);
        int GetUserId(string userName, string password);
        bool IsCurrentPassWord(string password);
    }
}
