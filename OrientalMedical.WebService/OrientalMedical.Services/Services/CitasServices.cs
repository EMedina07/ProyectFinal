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
    public class CitasServices : ICitasServices
    {
        private readonly IRepositoriesWrapper _wrapper = null;
        private readonly IMapper _mapper = null;
        public CitasServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        public void CreateCitas(CitasRequestDTOs citasRequestDTOs)
        {
            Citas cita = _mapper.Map<Citas>(citasRequestDTOs);

            _wrapper.CitasRepository.Create(cita);
            _wrapper.Save();
        }

        public List<CitasResponseDTOs> GetByAsistente(int asistenteId)
        {
            List<Citas> citas = _wrapper.CitasRepository.GetAll()
                                  .Where(c => c.Especialidad.AsitenteId == asistenteId)
                                  .ToList();

            List<CitasResponseDTOs> citasResponseDTOs = new List<CitasResponseDTOs>();

            foreach (var item in citas)
            {
                citasResponseDTOs.Add(_mapper.Map<CitasResponseDTOs>(item));
            }

            return citasResponseDTOs;
        }

        public List<CitasResponseDTOs> GetByDoctor(int doctorId)
        {
            List<Citas> citas = _wrapper.CitasRepository.GetAll()
                                  .Where(c => c.DoctorId == doctorId)
                                  .ToList();

            List<CitasResponseDTOs> citasResponseDTOs = new List<CitasResponseDTOs>();

            foreach (var item in citas)
            {
                citasResponseDTOs.Add(_mapper.Map<CitasResponseDTOs>(item));
            }

            return citasResponseDTOs;
        }

        public void UpdateCitas(int citaId, CitasRequestDTOs citasRequestDTOs)
        {
            Citas citaCurrent = _wrapper.CitasRepository.GetAll().Where(c => c.CitaId == citaId).FirstOrDefault();
            
            Citas cita = _mapper.Map<Citas>(citasRequestDTOs);
            cita.CitaId = citaId;
            cita.Estado = citaCurrent.Estado;
            cita.Comentario = cita.Comentario;

            _wrapper.CitasRepository.Update(cita);
            _wrapper.Save();
        }
    }
}
