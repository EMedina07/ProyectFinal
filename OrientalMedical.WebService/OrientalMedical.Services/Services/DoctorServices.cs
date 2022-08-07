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
    public class DoctorServices : IDoctorServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        private readonly IUserServices _userServices = null;
        public DoctorServices(IRepositoriesWrapper wrapper, IMapper mapper, IUserServices userServices)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _userServices = userServices;
        }

        public void DeleteDoctor(int doctorId)
        {
            Personal doctor =  _wrapper.personalRepository.GetAll().Where(d => d.PersonalId == doctorId)
                                       .FirstOrDefault();

            doctor.IsActive = false;

            _wrapper.personalRepository.Update(doctor);
            _wrapper.Save();
        }

        public DoctorResponseDTOs GetDoctorDetail(int doctorId)
        {
            Personal doctor = _wrapper.personalRepository.GetAll().Where(d => d.PersonalId == doctorId && d.IsActive != false)
                           .FirstOrDefault();

            DoctorResponseDTOs doctorDTOs = _mapper.Map<DoctorResponseDTOs>(doctor);

            return doctorDTOs;
        }

        public List<DoctorForSelect> GetDoctorForAsistente(int asistenteId)
        {
            int doctorId = (int)_wrapper.personalRepository.GetAll()
                                   .Where(a => a.PersonalId == asistenteId)
                                   .FirstOrDefault().DoctorId;

            return _wrapper.EspecialidadRepository.GetAll()
                                              .Where(e => e.DoctorId == doctorId)
                                              .Select(e => new DoctorForSelect
                                              {
                                                  DoctorId = e.DoctorId,
                                                  Doctor = e.Doctor.Nombre + " " + e.Doctor.Apellido
                                              }).ToList();
        }

        public int GetDoctorId(string cedula)
        {
            return _wrapper.personalRepository.GetAll()
                           .Where(d => d.Cedula == cedula && d.IsActive != false)
                           .FirstOrDefault().PersonalId;
        }

        public bool IsNewCedula(int personalId, string cedula)
        {
            return _wrapper.personalRepository.IsNewCedula(personalId, cedula);
        }

        public bool IsResgistered(string cedula)
        {
            return _wrapper.personalRepository.IsResgistered(cedula);
        }

        public void RegisterDoctor(DoctorRequestDTOs doctorDTOs)
        {
            Personal doctor = _mapper.Map<Personal>(doctorDTOs);
            doctor.Ocupacion = doctorDTOs.Ocupacion.ToLower();
            doctor.IsActive = true;

            _wrapper.personalRepository.Create(doctor);
            _wrapper.Save();

            string userName = doctor.Nombre.Substring(0, 1).ToUpper() +
                              doctor.Apellido.Substring(0, 1).ToUpper() +
                              doctor.Cedula.Substring(5, 4);

            _userServices.CreateCredentials(userName, _wrapper.Password);
        }

        public void UpdateDoctor(int doctorID, DoctorRequestDTOs doctorDTOs)
        {
            Personal doctor = _mapper.Map<Personal>(doctorDTOs);
            doctor.Ocupacion = doctorDTOs.Ocupacion.ToLower();
            doctor.PersonalId = doctorID;
            doctor.IsActive = true;

            _wrapper.personalRepository.Update(doctor);

            _wrapper.Save();
        }
    }
}
