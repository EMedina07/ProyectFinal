using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using OrientalMedical.Damin.Models.DTOs;
using OrientalMedical.Damin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public class AdministradorRepository : BaseRepository<Usuarios>, IAdministradorRepository
    {
        private readonly OrientalMedicalSystemDBContext _context;

        public AdministradorRepository(OrientalMedicalSystemDBContext context) : base(context)
        {
            _context = context;
        }

        public List<UsuarioDTOs> ObtenerUser()
        {
            return this.GetAll().Where(u => u.Personal.IsActive != false).Select(u => new UsuarioDTOs
            {
                UsuarioId = u.UsuarioId,
                Usuario = u.Usuario,
                Nombre = u.Personal.Nombre,
                Apellido = u.Personal.Apellido,
                Cedula = u.Personal.Cedula
            }).AsEnumerable().ToList();
        }
    }
}
