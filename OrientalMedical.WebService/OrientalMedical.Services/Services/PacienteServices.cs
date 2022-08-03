using AutoMapper;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Entities;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Services.Services
{
    public class PacienteServices : IPacienteServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        public PacienteServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        /*public void DeletePaciente(int pacienteId)
        {
            Paciente paciente = _wrapper.PacienteRepository.GetAll().Where(p => p.PacienteId == pacienteId)
                                        .FirstOrDefault();

            _wrapper.PacienteRepository.Delete(paciente);
            _wrapper.Save();
        }*/

        public List<PacienteResponseDTOs> GetPacientes()
        {
            List<Paciente> pacientes = _wrapper.PacienteRepository.GetAll().ToList();

            List<PacienteResponseDTOs> PacientesDTOs = new List<PacienteResponseDTOs>();

            foreach (var item in pacientes)
            {
                PacientesDTOs.Add(_mapper.Map<PacienteResponseDTOs>(item));
            }

            return PacientesDTOs;
        }

        public void PacienteRegister(string user, PacienteRequestDTOs pacienteDTOs)
        {
            Paciente paciente = _mapper.Map<Paciente>(pacienteDTOs);
            paciente.UsuarioCreador = user;

            _wrapper.PacienteRepository.Create(paciente);
            _wrapper.Save();
        }

        public void UpdatePaciente(int pacienteId, PacienteRequestDTOs pacienteDTOs)
        {
            Paciente paciente = _mapper.Map<Paciente>(pacienteDTOs);
            paciente.PacienteId = pacienteId;
            paciente.UsuarioCreador = _wrapper.PacienteRepository.GetUserCreador(pacienteId);

            _wrapper.PacienteRepository.Update(paciente);
            _wrapper.Save();
        }

        public bool IsResgistered(string cedula)
        {
            int someResult = _wrapper.PacienteRepository.GetAll().Where(p => p.Cedula == cedula).ToList().Count;

            if (someResult != 0)
            {
                return true;
            }

            return false;
        }

        public bool IsNewCedula(int id, string cedula)
        {
            string cedulaActual = _wrapper.PacienteRepository.GetAll().Where(p => p.PacienteId == id)
                                      .FirstOrDefault().Cedula;

            if (cedulaActual == cedula)
            {
                return true;
            }

            return false;
        }
    }
}
