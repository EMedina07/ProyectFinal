using AutoMapper;
using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
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

        public OperadorResponseDTOs GetOperadorDetail(int operadorId)
        {
            Personal operador = _wrapper.personalRepository.GetByFilter(d => d.PersonalId == operadorId)
                                        .FirstOrDefault();

            OperadorResponseDTOs operadorDTOs = _mapper.Map<OperadorResponseDTOs>(operador);

            return operadorDTOs;
        }

        public List<OperadorForSelectModel> GetOperadoresForSelect(int doctorId)
        {
            List<OperadorForSelectModel> operadores = _wrapper.personalRepository.GetAll()
                                                             .Where(o => o.DoctorId == doctorId)
                                                             .Select(o => new OperadorForSelectModel
                                                             {
                                                                 OperadorId = o.PersonalId,
                                                                 OperadorIdentification = o.Cedula + " - " + o.Nombre
                                                             }).ToList();

            return operadores;
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

            _wrapper.personalRepository.Update(operador);

            _wrapper.Save();
        }
    }
}
