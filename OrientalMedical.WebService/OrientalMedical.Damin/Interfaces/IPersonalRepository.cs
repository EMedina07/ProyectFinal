using OrientalMedical.Damin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Damin.Interfaces
{
    public interface IPersonalRepository : IRepositorioBase<Personal>
    {
        int GetLastId();
        bool IsResgistered(string cedula);
        bool IsNewCedula(int personalId, string cedula);
        string GetUserCreador(int personalId);
    }
}
