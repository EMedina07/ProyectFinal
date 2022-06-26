using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.Interfaces
{
    public interface IOperadorServices
    {
        void RegisterOperador();
        void UpdateOperador();
        void GetDoctorOperadorByEspecialidad(int especialidadId);
        void DeleteOperador(int operadorId);
        bool IsResgistered(string cedula);
    }
}
