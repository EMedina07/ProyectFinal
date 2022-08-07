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
    public class HorarioServices : IHorarioServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        public HorarioServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        public void CreateHorario(int doctorId, HorarioRequestDTOs horarioRequestDTOs)
        {
            Horario horario = _mapper.Map<Horario>(horarioRequestDTOs);
            horario.DoctorId = doctorId;
            horario.IsActive = true;

            _wrapper.HorarioRepository.Create(horario);
            _wrapper.Save();
        }

        public void DeleteHorario(int horarioId)
        {
            Horario horario = _wrapper.HorarioRepository.GetAll()
                                      .Where(h => h.HorarioId == horarioId)
                                      .FirstOrDefault();

            horario.IsActive = false;

            _wrapper.HorarioRepository.Update(horario);
            _wrapper.Save();
        }

        public HorarioTrabajoResponseDTOs GetHorarioByDoctor(int doctorId)
        {
            return _wrapper.HorarioRepository.GetAll()
                                      .Where(h => h.IsActive != false && h.DoctorId == doctorId)
                                      .Select(h => new HorarioTrabajoResponseDTOs()
                                      {
                                          HorarioId = h.HorarioId,
                                          HoraInicio = h.HoraInicio,
                                          HoraFin = h.HoraFin,
                                          MinutosPorPaciente = h.MinutosPorPaciente,
                                          DiasLaborablesId = h.DiasLaborables.FirstOrDefault().DiasLaborablesId,
                                          Lunes = h.DiasLaborables.FirstOrDefault().Lunes,
                                          Martes = h.DiasLaborables.FirstOrDefault().Martes,
                                          Miecoles = h.DiasLaborables.FirstOrDefault().Miecoles,
                                          Jueves = h.DiasLaborables.FirstOrDefault().Jueves,
                                          Viernes = h.DiasLaborables.FirstOrDefault().Viernes,
                                          Sabado = h.DiasLaborables.FirstOrDefault().Sabado
                                      }).FirstOrDefault();
        }

        public HorarioTrabajoResponseDTOs GetHorarioDetail(int horarioId)
        {
            return _wrapper.HorarioRepository.GetAll()
                                      .Where(h => h.IsActive != false && h.HorarioId == horarioId)
                                      .Select(h => new HorarioTrabajoResponseDTOs()
                                      {
                                          HorarioId = h.HorarioId,
                                          HoraInicio = h.HoraInicio,
                                          HoraFin = h.HoraFin,
                                          MinutosPorPaciente = h.MinutosPorPaciente,
                                          DiasLaborablesId = h.DiasLaborables.FirstOrDefault().DiasLaborablesId,
                                          Lunes = h.DiasLaborables.FirstOrDefault().Lunes,
                                          Martes = h.DiasLaborables.FirstOrDefault().Martes,
                                          Miecoles = h.DiasLaborables.FirstOrDefault().Miecoles,
                                          Jueves = h.DiasLaborables.FirstOrDefault().Jueves,
                                          Viernes = h.DiasLaborables.FirstOrDefault().Viernes,
                                          Sabado = h.DiasLaborables.FirstOrDefault().Sabado
                                      }).FirstOrDefault();
        }

        public bool HaveHorarioRegistrated(int doctorId)
        {
            int horariosCount = _wrapper.HorarioRepository.GetAll()
                                      .Where(h => h.IsActive != false)
                                      .Where(h => h.DoctorId == doctorId)
                                      .Count();

            if (horariosCount != 0)
            {
                return true;
            }

            return false;
        }

        public void UpdateHorario(int horarioId, HorarioRequestDTOs horarioRequestDTOs)
        {
            Horario horario = _mapper.Map<Horario>(horarioRequestDTOs);
            horario.HorarioId = horarioId;
            horario.DoctorId = _wrapper.HorarioRepository.GetAll()
                                       .Where(h => h.HorarioId == horarioId)
                                       .FirstOrDefault().DoctorId;
                                       
            horario.IsActive = true;

            _wrapper.HorarioRepository.Update(horario);
            _wrapper.Save();
        }
    }
}
