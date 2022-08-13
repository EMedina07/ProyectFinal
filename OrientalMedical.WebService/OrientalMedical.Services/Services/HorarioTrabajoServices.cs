using AutoMapper;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Entities;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Services.Services
{
    public class HorarioTrabajoServices : IHorarioTrabajoServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        public HorarioTrabajoServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        public void CreateHorario(int doctorId, HorarioTrabajoResponseDTOs horarioRequestDTOs)
        {
            DiasLaborables diasLaborables = new DiasLaborables();
            diasLaborables.Lunes = horarioRequestDTOs.Lunes;
            diasLaborables.Martes = horarioRequestDTOs.Martes;
            diasLaborables.Miecoles = horarioRequestDTOs.Miecoles;
            diasLaborables.Jueves = horarioRequestDTOs.Jueves;
            diasLaborables.Viernes = horarioRequestDTOs.Viernes;
            diasLaborables.Sabado = horarioRequestDTOs.Sabado;
            diasLaborables.HorarioId = _wrapper.HorarioRepository.GetAll()
                                               .Where(h => h.DoctorId == doctorId && h.IsActive != false)
                                               .FirstOrDefault().HorarioId;
            diasLaborables.IsActive = true;

            _wrapper.DiasLaborablesRepository.Create(diasLaborables);
            _wrapper.Save();
        }

        public void deleteHorario(int horarioId)
        {
            DiasLaborables diasLaborables = _wrapper.DiasLaborablesRepository.GetAll()
                                                    .Where(d => d.HorarioId == horarioId && d.IsActive != false)
                                                    .FirstOrDefault();

            diasLaborables.IsActive = false;

            _wrapper.DiasLaborablesRepository.Update(diasLaborables);
            _wrapper.Save();
        }

        public void UpdateHorario(int horarioId, HorarioTrabajoResponseDTOs horarioRequestDTOs)
        {
            DiasLaborables diasLaborables = new DiasLaborables();
            diasLaborables.DiasLaborablesId = horarioRequestDTOs.DiasLaborablesId;
            diasLaborables.Lunes = horarioRequestDTOs.Lunes;
            diasLaborables.Martes = horarioRequestDTOs.Martes;
            diasLaborables.Miecoles = horarioRequestDTOs.Miecoles;
            diasLaborables.Jueves = horarioRequestDTOs.Jueves;
            diasLaborables.Viernes = horarioRequestDTOs.Viernes;
            diasLaborables.Sabado = horarioRequestDTOs.Sabado;
            diasLaborables.HorarioId = horarioId;
            diasLaborables.IsActive = true;

            _wrapper.DiasLaborablesRepository.Update(diasLaborables);
            _wrapper.Save();
        }
    }
}
