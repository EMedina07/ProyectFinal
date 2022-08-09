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
    public class AusenciasServices : IAusenciasServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;

        public AusenciasServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        public void DeleteAusencia(int ausenciaId)
        {
            Ausencia ausencia = _wrapper.AucenciasRepository.GetAll()
                                      .Where(a => a.AusenciaId == ausenciaId)
                                      .FirstOrDefault();

            ausencia.IsActive = false;

            _wrapper.AucenciasRepository.Update(ausencia);
            _wrapper.Save();
        }

        public AusenciaResponseDTOs GetAusenciaDetail(int ausenciaId)
        {
            Ausencia ausencia = _wrapper.AucenciasRepository
                                       .GetAll().Where(a => a.AusenciaId == ausenciaId)
                                       .FirstOrDefault();

            AusenciaResponseDTOs ausenciaDTOs = _mapper.Map<AusenciaResponseDTOs>(ausencia);

            return ausenciaDTOs;
        }

        public List<AusenciaResponseDTOs> GetAusenciasByAsistente(int asistenteId)
        {
            int doctorId = (int)_wrapper.personalRepository.GetAll()
                                   .Where(o => o.PersonalId == asistenteId)
                                   .FirstOrDefault().DoctorId;

            RemoberAusencias(doctorId);

            List<Ausencia> ausencias = _wrapper.AucenciasRepository.GetAll()
                                       .Where(a => a.IsActive != false)
                                       .Where(a => a.DoctorId == doctorId)
                                       .ToList();

            List<AusenciaResponseDTOs> ausenciasDTOs = new List<AusenciaResponseDTOs>();

            foreach (var item in ausencias)
            {
                ausenciasDTOs.Add(_mapper.Map<AusenciaResponseDTOs>(item));
            }

            return ausenciasDTOs;
        }

        private void RemoberAusencias(int doctorId)
        {
            List<Ausencia> ausencias = _wrapper.AucenciasRepository.GetAll()
                                       .Where(a => a.DoctorId == doctorId)
                                       .ToList();

            foreach (var item in ausencias)
            {
                if (item.FechaReintegro < DateTime.Now)
                {
                    this.DeleteAusencia(item.AusenciaId);
                }
            }
        }

        public bool IsResgistered(DateTime fechaInicio, DateTime fechaFin, string motivoAsencia)
        {
            int results = _wrapper.AucenciasRepository.GetAll()
                                  .Where(a => a.IsActive != false)
                                  .Where(a => a.MotivoAusencia == motivoAsencia || a.FechaInicio == fechaInicio || a.FechaReintegro == fechaFin)
                                  .ToList().Count;

            if(results != 0)
            {
                return true;
            }

            return false;
        }

        public void RegistrarAusencia(AucenciasRequestDTOs aucenciasRequestDTOs)
        {
            Ausencia ausencia = _mapper.Map<Ausencia>(aucenciasRequestDTOs);
            ausencia.IsActive = true;

            _wrapper.AucenciasRepository.Create(ausencia);
            _wrapper.Save();
        }

        public void UpdateAusencia(int ausenciaId, AucenciasRequestDTOs aucenciasRequestDTOs)
        {
            Ausencia ausencia = _mapper.Map<Ausencia>(aucenciasRequestDTOs);
            ausencia.AusenciaId = ausenciaId;
            ausencia.IsActive = true;

            _wrapper.AucenciasRepository.Update(ausencia);

            _wrapper.Save();
        }
    }
}
