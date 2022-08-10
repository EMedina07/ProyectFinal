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
            especialidad.IsActive = true;

            _wrapper.EspecialidadRepository.Create(especialidad);
            _wrapper.Save();
        }

        public void DeleteEspecialidad(int especialidadId)
        {
            Especialidad especialidad = _wrapper.EspecialidadRepository.GetAll()
                                                .Where(e => e.IsActive != false)
                                                .Where(e => e.EspecialidadId == especialidadId)
                                                .FirstOrDefault();

            especialidad.IsActive = false;

            _wrapper.EspecialidadRepository.Update(especialidad);
            _wrapper.Save();
        }

        public EspecialidadResponseDTOs GetEspecialidadDetail(int especialidadId)
        {
            Especialidad especialidad = _wrapper.EspecialidadRepository.GetAll()
                                                .Where(e => e.IsActive != false)
                                                .Where(e => e.EspecialidadId == especialidadId)
                                                .FirstOrDefault();

            EspecialidadResponseDTOs especialidadDTOs = _mapper.Map<EspecialidadResponseDTOs>(especialidad);

            return especialidadDTOs;
        }

        public List<EspecialidadesForSelect> GetEspecialidades(int doctorId)
        {
            return  _wrapper.EspecialidadRepository.GetAll()
                            .Where(e => e.DoctorId == doctorId)
                            .Where(e => e.IsActive != false)
                            .Select(e => new EspecialidadesForSelect
                            {
                                EspecialidadId = e.EspecialidadId,
                                Especialidad = e.Ciencia.Ciencia
                            })
                            .ToList();
        }

        public bool IsRegistared(int doctorId, int cienciaMedicaId)
        {
            int results = _wrapper.EspecialidadRepository.GetAll()
                            .Where(e => e.IsActive != false)
                            .Where(e => e.DoctorId == doctorId && e.CienciaId == cienciaMedicaId)
                            .ToList().Count;

            if(results != 0)
            {
                return true;
            }

            return false;
        }

        public void UpdateEspecialidad(int especialidadId, EspecialidadRequestDTOs especialidadDTOs)
        {
            Especialidad especialidad = _mapper.Map<Especialidad>(especialidadDTOs);
            especialidad.EspecialidadId = especialidadId;
            especialidad.IsActive = true;

            _wrapper.EspecialidadRepository.Update(especialidad);
            _wrapper.Save();
        }
    }
}
