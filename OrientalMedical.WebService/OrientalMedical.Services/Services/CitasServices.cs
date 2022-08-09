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
            cita.Estado = 0;
            cita.IsActive = true;

            _wrapper.CitasRepository.Create(cita);
            _wrapper.Save();
        }

        public List<CitaModel> GetByAsistente(int asistenteId, DateTime? fechaInicio, DateTime? fechaFin, int? status)
        {
            int doctorId = (int)_wrapper.personalRepository.GetAll()
                                   .Where(a => a.PersonalId == asistenteId)
                                   .FirstOrDefault().DoctorId;

            List<CitaModel> citas = _wrapper.CitasRepository.GetAll()
                                  .Where(c => c.IsActive != false && c.DoctorId == doctorId)
                                  .Select(c => new CitaModel()
                                  {
                                      CitaId = c.CitaId,
                                      FechaCita = c.FechaCita,
                                      Especialidad = c.Especialidad.Ciencia.Ciencia,
                                      Doctor = c.Doctor.Nombre + " " + c.Doctor.Apellido,
                                      Paciente = c.Paciente.Nombre + " " + c.Paciente.Apellido,
                                      Telefono = c.Paciente.Telefono,
                                      Estado = c.Estado,
                                      Comentario = c.Comentario
                                  }).ToList();

            if (fechaInicio != null && fechaFin != null && status != 0)
            {
                return citas.Where(c => c.Estado == status)
                   .Where(c => c.FechaCita >= fechaInicio && c.FechaCita <= fechaFin).ToList();
            }
            else
            {
                if (fechaInicio != null && fechaFin != null)
                {
                    return citas.Where(c => c.FechaCita >= fechaInicio && c.FechaCita <= fechaFin).ToList();
                }
                else
                {
                    if (status != null)
                    {
                        return citas.Where(c => c.Estado == status).ToList();
                    }
                }
            }

            return citas.ToList();
        }

        public List<CitasResponseDTOs> GetByDoctor(int doctorId)
        {
            List<Citas> citas = _wrapper.CitasRepository.GetAll()
                                  .Where(c => c.IsActive != false && c.DoctorId == doctorId)
                                  .Where(c => c.Estado == 1)
                                  .ToList();

            List<CitasResponseDTOs> citasResponseDTOs = new List<CitasResponseDTOs>();

            foreach (var item in citas)
            {
                citasResponseDTOs.Add(_mapper.Map<CitasResponseDTOs>(item));
            }

            return citasResponseDTOs;
        }

        public CitasResponseDTOs GetCitaDetail(int citaId)
        {
            Citas cita = _wrapper.CitasRepository.GetAll().Where(c => c.CitaId == citaId)
                                 .FirstOrDefault();

            CitasResponseDTOs citasResponseDTOs = _mapper.Map<CitasResponseDTOs>(cita);

            return citasResponseDTOs;
        }

        public void TrabajarCitas(ManejoDeCitasModel manejoDeCitasModel)
        {
            Citas cita = _wrapper.CitasRepository.GetAll().Where(c => c.CitaId == manejoDeCitasModel.CitaId)
                                 .FirstOrDefault();

            cita.Estado = manejoDeCitasModel.Estado;
            cita.Comentario = manejoDeCitasModel.Comentario;

            _wrapper.CitasRepository.Update(cita);
            _wrapper.Save();
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
