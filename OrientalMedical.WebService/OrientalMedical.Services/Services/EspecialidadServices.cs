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
    public class EspecialidadServices : IEspecialidadServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        public EspecialidadServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        public void CreateEspecialidad(EspecialidadRequestDTOs especialidadDTOs)
        {
            Especialidad especialidad = _mapper.Map<Especialidad>(especialidadDTOs);

            _wrapper.EspecialidadRepository.Create(especialidad);
            _wrapper.Save();
        }

        public List<EspecialidadResponseDTOs> GetEspecialidades(int doctorId)
        {
            List<Especialidad> especialidades = _wrapper.EspecialidadRepository.GetAll()
                                                        .Where(e => e.DoctorId == doctorId)
                                                        .ToList();

            List<EspecialidadResponseDTOs> especialidadDTOs = new List<EspecialidadResponseDTOs>();

            foreach (var especialidad in especialidades)
            {
                especialidadDTOs.Add(_mapper.Map<EspecialidadResponseDTOs>(especialidad));
            }

            return especialidadDTOs;
        }

        public void UpdateEspecialidad(int especialidadId, EspecialidadRequestDTOs especialidadDTOs)
        {
            Especialidad especialidad = _mapper.Map<Especialidad>(especialidadDTOs);
            especialidad.EspecialidadId = especialidadId;

            _wrapper.EspecialidadRepository.Update(especialidad);
            _wrapper.Save();
        }
    }
}
