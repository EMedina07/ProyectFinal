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
        private readonly IRepositoriesWrapper _wrapper;
        private readonly IMapper _mapper;
        public DoctorServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        public DoctorResponseDTOs GetDoctorDetail(int doctorId)
        {
            Personal doctor = _wrapper.personalRepository.GetByFilter(d => d.PersonalId == doctorId)
                           .FirstOrDefault();

            DoctorResponseDTOs doctorDTOs = _mapper.Map<DoctorResponseDTOs>(doctor);

            return doctorDTOs;
        }

        public void RegisterDoctor(DoctorRequestDTOs doctorDTOs)
        {
            Personal doctor = _mapper.Map<Personal>(doctorDTOs);

            _wrapper.personalRepository.Create(doctor);
            _wrapper.Save();
        }

        public void UpdateDoctor(int doctorID, DoctorRequestDTOs doctorDTOs)
        {
            Personal doctor = _mapper.Map<Personal>(doctorDTOs);

            doctor.PersonalId = doctorID;
            _wrapper.personalRepository.Update(doctor);

            _wrapper.Save();
        }
    }
}
