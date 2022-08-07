using AutoMapper;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Entities;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Services.Services
{
    public class OperadorServices : IOperadorServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        private readonly IUserServices _userServices = null;
        public OperadorServices(IRepositoriesWrapper wrapper, IMapper mapper, IUserServices userServices)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _userServices = userServices;
        }

        public void DeleteOperador(int operadorId)
        {
            Personal doctor = _wrapper.personalRepository.GetAll().Where(d => d.PersonalId == operadorId)
                                       .FirstOrDefault();

            doctor.IsActive = false;

            _wrapper.personalRepository.Update(doctor);
            _wrapper.Save();
        }

        public List<OperadorResponseDTOs> GetOperadoresByDoctor(int doctorId)
        {
            List<Personal> operadores = _wrapper.personalRepository.GetAll()
                                        .Where(o => o.DoctorId == doctorId && o.IsActive != false)
                                        .ToList();

            List<OperadorResponseDTOs> operadoresDTOs = new List<OperadorResponseDTOs>();

            foreach (var item in operadores)
            {
                operadoresDTOs.Add(_mapper.Map<OperadorResponseDTOs>(item));
            }

            return operadoresDTOs;
        }

        public OperadorResponseDTOs GetOperadorDetail(int operadorId)
        {
            Personal operador = _wrapper.personalRepository.GetAll()
                                        .Where(o => o.PersonalId == operadorId && o.DoctorId != null && o.IsActive != false)
                                        .FirstOrDefault();

            OperadorResponseDTOs operadorDTOs = _mapper.Map<OperadorResponseDTOs>(operador);

            return operadorDTOs;
        }

        public bool IsNewCedula(int personalId, string cedula)
        {
            return _wrapper.personalRepository.IsNewCedula(personalId, cedula);
        }

        public bool IsResgistered(string cedula)
        {
            return _wrapper.personalRepository.IsResgistered(cedula);
        }

        public void RegisterOperador(int doctorId, OperadorRequestDTOs operadorDTOs)
        {
            Personal operador = _mapper.Map<Personal>(operadorDTOs);
            operador.Ocupacion = _wrapper.Operador;
            operador.DoctorId = doctorId;
            operador.IsActive = true;


            _wrapper.personalRepository.Create(operador);
            _wrapper.Save();

            string userName = operador.Nombre.Substring(0, 1).ToUpper() +
                              operador.Apellido.Substring(0, 1).ToUpper() +
                              operador.Cedula.Substring(5, 4);

            _userServices.CreateCredentials(userName, _wrapper.Password);
        }

        public void UpdateOperador(int operadorId, OperadorRequestDTOs operadorDTOs)
        {
            Personal operador = _mapper.Map<Personal>(operadorDTOs);
            operador.Ocupacion = operador.Ocupacion = _wrapper.Operador;
            operador.PersonalId = operadorId;
            operador.DoctorId = _wrapper.personalRepository.GetAll()
                                        .Where(o => o.PersonalId == operadorId)
                                        .FirstOrDefault().DoctorId;
            operador.IsActive = true;

            _wrapper.personalRepository.Update(operador);

            _wrapper.Save();
        }
    }
}
