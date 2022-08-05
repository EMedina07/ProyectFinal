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

        public bool AsistenIsAvailable(int asistenteId, string horaInicio, string horaFin)
        {
            List<Especialidad> especialidades = _wrapper.EspecialidadRepository.GetAll()
                                                        .Where(e => e.AsitenteId == asistenteId)
                                                        .ToList();
            if (especialidades.Count == 0)
            {
                return true;
            }

            var inicioTime = DateTime.Parse(horaInicio).TimeOfDay;
            var finTime = DateTime.Parse(horaFin).TimeOfDay;

            foreach (var item in especialidades)
            {
                var horaInicioEnEspecialidad = DateTime.Parse(item.HoraInicio).TimeOfDay;
                var horaFinEnEspecialidad = DateTime.Parse(item.HoraFin).TimeOfDay;

                if (inicioTime > horaInicioEnEspecialidad && finTime < horaFinEnEspecialidad)
                {
                    return false;
                }
            }

            return true;
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

        public string HorarioDisponible(int asistenteId)
        {
            return _wrapper.EspecialidadRepository.GetAll()
                           .Where(e => e.AsitenteId == asistenteId)
                           .Select(e => e.HoraFin).Max();
        }

        public bool IsRegisterd(int doctorId, string especialidad)
        {
            int itemsCount = _wrapper.EspecialidadRepository.GetAll()
                                     .Where(e => e.DoctorId == doctorId && e.Especialidad1 == especialidad)
                                     .ToList().Count;

            if(itemsCount == 0)
            {
                return false;
            }

            return true;
        }

        public List<EspecialidadesForSelect> GetEspecialidadForAsistente(int asistenteId)
        {
            return _wrapper.EspecialidadRepository.GetAll()
                                              .Where(e => e.AsitenteId == asistenteId)
                                              .Select(e => new EspecialidadesForSelect
                                              {
                                                  EspecialidadId = e.EspecialidadId,
                                                  Especialidad = e.Especialidad1
                                              }).ToList();
        }
    }
}
