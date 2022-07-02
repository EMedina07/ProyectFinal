using OrientalMedical.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IUserServices
    {
        void CreateCredentials(string userName, string password);
        bool IsAnUser(string userName, string password);
        UserInformation GetUserDetail(string userName, string password);
        void UpdatePassword(int personalId, string password);
        bool IsCurrentPassWord(int personalId, string password);
    }
}
