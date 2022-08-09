using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public class RepositoriesWrapper : IRepositoriesWrapper
    {
        public const string PASSWORD = "1234";
        public const string OPERADOR = "operador";

        private IPersonalRepository _personalRepository = null;
        private IUserRepository _userRepository = null;
        private IEspecialidadRepository _especialidadRepository = null;
        private IAdministradorRepository _administradorRepository = null;
        private IPacienteRepository _pacienteRepository = null;
        private ICitasRepository _citasRepository = null;
        private ICienciasMedicasRepository _cienciasMedicasRepository = null;
        private IHorarioRepository _horarioRepository = null;
        private IDiasLaborablesRepository _diasLaborablesRepository = null;
        private IAucenciasRepository _aucenciasRepository = null;

        private readonly OrientalMedicalSystemDBContext _context;

        public RepositoriesWrapper(OrientalMedicalSystemDBContext context)
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

        public string Password
        {
            get
            {
                return PASSWORD;
            }
        }

        public string Operador
        {
            get
            {
                return OPERADOR;
            }
        }

        public IEspecialidadRepository EspecialidadRepository
        {
            get
            {
                if (_especialidadRepository == null)
                {
                    _especialidadRepository = new EspecialidadRepository(_context);
                }

                return _especialidadRepository;
            }
        }

        public IAdministradorRepository AdministradorRepository
        {
            get
            {
                if(_administradorRepository == null)
                {
                    _administradorRepository = new AdministradorRepository(_context);
                }

                return _administradorRepository;
            }
        }

        public IPacienteRepository PacienteRepository 
        {
            get
            {
                if (_pacienteRepository == null)
                {
                    _pacienteRepository = new PacienteRepository(_context);
                }

                return _pacienteRepository;
            }
        }

        public ICitasRepository CitasRepository 
        {
            get
            {
                if (_citasRepository == null)
                {
                    _citasRepository = new CitasRepository(_context);
                }

                return _citasRepository;
            }
        }

        public ICienciasMedicasRepository CienciasMedicasRepository
        {
            get
            {
                if (_cienciasMedicasRepository == null)
                {
                    _cienciasMedicasRepository = new CienciasMedicasRepository(_context);
                }

                return _cienciasMedicasRepository;
            }
        }

        public IHorarioRepository HorarioRepository
        {
            get
            {
                if (_horarioRepository == null)
                {
                    _horarioRepository = new HorarioRepository(_context);
                }

                return _horarioRepository;
            }
        }

        public IDiasLaborablesRepository DiasLaborablesRepository
        {
            get
            {
                if (_diasLaborablesRepository == null)
                {
                    _diasLaborablesRepository = new DiasLaborablesRepository(_context);
                }

                return _diasLaborablesRepository;
            }
        }

        public IAucenciasRepository AucenciasRepository
        {
            get
            {
                if (_aucenciasRepository == null)
                {
                    _aucenciasRepository = new AucenciasRepository(_context);
                }

                return _aucenciasRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
