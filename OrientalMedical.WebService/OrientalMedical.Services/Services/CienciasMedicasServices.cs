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
    public class CienciasMedicasServices : ICienciasMedicasServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        public CienciasMedicasServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        public bool CienciaMedicaIsRegistrated(string cienciaMedica)
        {
            int cienciasMedicaCount = _wrapper.CienciasMedicasRepository.GetAll()
                                      .Where(c => c.IsActive != false)
                                      .Where(c => c.Ciencia == cienciaMedica)
                                      .Count();

            if(cienciasMedicaCount != 0)
            {
                return true;
            }

            return false;
        }

        public void CreateCienciaMedica(CienciasMedicasRequestDTOs CienciasMedicaRequestDTOs)
        {
            Ciencias cienciaMedica = _mapper.Map<Ciencias>(CienciasMedicaRequestDTOs);
            cienciaMedica.IsActive = true;

            _wrapper.CienciasMedicasRepository.Create(cienciaMedica);
            _wrapper.Save();
        }

        public void DeleteCienciaMedica(int cienciaMedicaId)
        {
            Ciencias cienciaMedica = _wrapper.CienciasMedicasRepository.GetAll()
                                      .Where(c => c.CienciaId == cienciaMedicaId)
                                      .FirstOrDefault();

            cienciaMedica.IsActive = false;

            _wrapper.CienciasMedicasRepository.Update(cienciaMedica);
            _wrapper.Save();
        }

        public List<CienciasMedicasResponseDTOs> GetAllCienciasMedicas()
        {
            List<Ciencias> cienciaMedicas = _wrapper.CienciasMedicasRepository.GetAll()
                                               .Where(c => c.IsActive != false).ToList();

            List<CienciasMedicasResponseDTOs> cienciaMedicasDTOs = new List<CienciasMedicasResponseDTOs>();

            foreach (var item in cienciaMedicas)
            {
                cienciaMedicasDTOs.Add(_mapper.Map<CienciasMedicasResponseDTOs>(item));
            }

            return cienciaMedicasDTOs;
        }

        public CienciasMedicasResponseDTOs GetCienciasMedicaDetail(int cienciaMedicaId)
        {
            Ciencias cienciaMedica = _wrapper.CienciasMedicasRepository.GetAll()
                                               .Where(c => c.IsActive != false && c.CienciaId == cienciaMedicaId)
                                               .FirstOrDefault();

            CienciasMedicasResponseDTOs cienciaMedicasDTOs = _mapper.Map<CienciasMedicasResponseDTOs>(cienciaMedica);

            return cienciaMedicasDTOs;
        }

        public void UpdateCienciaMedica(int cienciaMedicaId, CienciasMedicasRequestDTOs CienciasMedicaRequestDTOs)
        {
            Ciencias cienciaMedica = _mapper.Map<Ciencias>(CienciasMedicaRequestDTOs);
            cienciaMedica.CienciaId = cienciaMedicaId;
            cienciaMedica.IsActive = true;

            _wrapper.CienciasMedicasRepository.Update(cienciaMedica);
            _wrapper.Save();
        }
    }
}
