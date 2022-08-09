using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Damin.Interfaces
{
    public interface IRepositoriesWrapper
    {
        string Password { get; }
        string Operador { get; }

        IPersonalRepository personalRepository { get; }
        IUserRepository UserRepository { get; }
        IEspecialidadRepository EspecialidadRepository { get; }
        IAdministradorRepository AdministradorRepository { get; }
        IPacienteRepository PacienteRepository { get; }
        ICitasRepository CitasRepository { get; }
        ICienciasMedicasRepository CienciasMedicasRepository { get; }
        IHorarioRepository HorarioRepository { get; }
        IDiasLaborablesRepository DiasLaborablesRepository { get; }
        IAucenciasRepository AucenciasRepository { get; }

        void Save();
    }
}
