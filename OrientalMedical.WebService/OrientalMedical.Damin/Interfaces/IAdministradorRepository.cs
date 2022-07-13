using OrientalMedical.Damin.Models.DTOs;
using OrientalMedical.Damin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Damin.Interfaces
{
    public interface IAdministradorRepository : IRepositorioBase<Usuarios>
    {
       List<UsuarioDTOs> ObtenerUser();
    }
}
