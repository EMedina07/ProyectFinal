using AutoMapper;
using OrientalMedical.Damin.Entities;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Services.Interfaces;
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
        public DoctorResponseDTOs GetDoctorDetail(int doctorId)
        {
            Personal doctor = _wrapper.personalRepository.GetByFilter(d => d.PersonalId == doctorId)
                           .FirstOrDefault();

            DoctorResponseDTOs doctorDTOs = _mapper.Map<DoctorResponseDTOs>(doctor);

            return doctorDTOs;
        }

        public bool IsResgistered(string cedula)
        {
            return _wrapper.personalRepository.IsResgistered(cedula);
        }

        public void RegisterDoctor(DoctorRequestDTOs doctorDTOs)
        {
            Personal doctor = _mapper.Map<Personal>(doctorDTOs);
            doctor.Ocupacion = doctorDTOs.Ocupacion.ToLower();
            

            _wrapper.personalRepository.Create(doctor);
            _wrapper.Save();

            string userName = doctor.Nombre.Substring(0, 1).ToUpper() +
                              doctor.Apellido.Substring(0, 1).ToUpper() +
                              doctor.Cedula.Substring(5, 4);

            _userServices.CreateCredentials(userName, _wrapper.password);
        }

        public void UpdateDoctor(int doctorID, DoctorRequestDTOs doctorDTOs)
        {
            Personal doctor = _mapper.Map<Personal>(doctorDTOs);
            doctor.Ocupacion = doctorDTOs.Ocupacion.ToLower();
            doctor.PersonalId = doctorID;

            _wrapper.personalRepository.Update(doctor);

            _wrapper.Save();
        }
    }
}
