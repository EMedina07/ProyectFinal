﻿using AutoMapper;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Entities;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using OrientalMedical.Shared.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void CreateCitas(int asistenteId, CitasRequestDTOs citasRequestDTOs)
        {
            Citas cita = _mapper.Map<Citas>(citasRequestDTOs);
            cita.DoctorId = _wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);
            cita.Estado = 0;
            cita.IsActive = true;

            _wrapper.CitasRepository.Create(cita);
            _wrapper.Save();
        }

        public List<CitaModel> GetByAsistente(int asistenteId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            int doctorId = (int)_wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);

            List<CitaModel> citas = _wrapper.CitasRepository.GetAll()
                                  .Where(c => c.IsActive != false && c.DoctorId == doctorId)
                                  .Where(c => c.Paciente.AsistenteId == asistenteId)
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

            if (fechaInicio != null && fechaFin != null)
            {
                return citas.Where(c => c.FechaCita >= fechaInicio && c.FechaCita <= fechaFin).ToList();
            }
            else
            {
                if (fechaInicio != null && fechaFin != null)
                {
                    return citas.Where(c => c.FechaCita >= fechaInicio && c.FechaCita <= fechaFin).ToList();
                }
            }

            return citas.ToList();
        }

        public List<CitaModel> GetByDoctor(int doctorId)
        {
            List<CitaModel> citas = _wrapper.CitasRepository.GetAll()
                                  .Where(c => c.IsActive != false && c.DoctorId == doctorId)
                                  .Where(c => c.Estado == 1)
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

            List<CitaModel> citasDeldia = new List<CitaModel>();

            foreach (var item in citas)
            {
                if (DateTime.Compare(DateTime.Parse(item.FechaCita.ToString("dd/MM/yyyy")), DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"))) == 0)
                {
                    citasDeldia.Add(item);
                }
            }

            return citasDeldia;
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
            cita.DoctorId = this.GetCitaDetail(citaId).DoctorId;
            cita.IsActive = true;
            cita.Estado = citaCurrent.Estado;
            cita.Comentario = cita.Comentario;

            _wrapper.CitasRepository.Update(cita);
            _wrapper.Save();
        }

        public bool DoctorIsAvailable(int asistenteId, DateTime fechaInicio)
        {
            int doctorId = _wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);

            var ausencias = _wrapper.AucenciasRepository.GetAll()
                                    .Where(a => a.IsActive != false && a.DoctorId == doctorId)
                                    .ToList();

            if (ausencias.Count == 0)
            {
                return true;
            }

            var fechaCita = DateTime.Parse(fechaInicio.ToString("dd/MM/yyyy"));

            foreach (var item in ausencias)
            {
                var horaInicioEnEspecialidad = DateTime.Parse(item.FechaInicio.ToString("dd/MM/yyyy"));
                var horaFinEnEspecialidad = DateTime.Parse(item.FechaReintegro.ToString("dd/MM/yyyy"));

                if (fechaCita >= horaInicioEnEspecialidad && fechaCita <= horaFinEnEspecialidad)
                {
                    return false;
                }
            }

            return true;
        }

        public bool FechaIsAvailable(int pacienteId, DateTime fechaInicio)
        {
            var allCitas = _wrapper.CitasRepository.GetAll()
                                   .Where(c => c.IsActive != false && c.PacienteId == pacienteId)
                                   .ToList();
            int asistenteId = _wrapper.PacienteRepository.GetAll()
                                      .Where(p => p.PacienteId == pacienteId)
                                      .FirstOrDefault().AsistenteId;

            int doctorId = _wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);
            var horario = _wrapper.HorarioRepository.GetAll()
                                          .Where(h => h.IsActive != false && h.DoctorId == doctorId)
                                          .FirstOrDefault();

            var horaCita = DateTime.Parse(fechaInicio.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture)).TimeOfDay;
            var fechaCita = DateTime.Parse(fechaInicio.ToString("dd/MM/yyyy"));

            foreach (var item in allCitas)
            {
                var horaItem = DateTime.Parse(item.FechaCita.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture)).TimeOfDay;
                var horaItemFinish = DateTime.Parse(item.FechaCita.AddMinutes(int.Parse(horario.MinutosPorPaciente)).ToString("HH:mm:ss tt", CultureInfo.InvariantCulture)).TimeOfDay;

                if (fechaCita == DateTime.Parse(item.FechaCita.ToString("dd/MM/yyyy")))
                {
                    if(TimeSpan.Compare(horaCita, horaItem) >= 0 && TimeSpan.Compare(horaCita, horaItemFinish) <= 0)
                    return false;
                }
            }

            return true;
        }

        public bool FechaCitaIsValid(int asistenteId, DateTime fechaCita)
        {
            int doctorId = _wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);
            var horario = _wrapper.HorarioRepository.GetAll()
                                          .Where(h => h.IsActive != false && h.DoctorId == doctorId)
                                          .FirstOrDefault();

            string cita = fechaCita.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture);

            if(TimeSpan.Compare(DateTime.Parse(cita).TimeOfDay, DateTime.Parse(horario.HoraFin).TimeOfDay) >= 0 || TimeSpan.Compare(DateTime.Parse(cita).TimeOfDay, DateTime.Parse(horario.HoraInicio).TimeOfDay) < 0)
            {
                return false;
            }

            return true;
        }

        public bool HoraCitaIsOcuped(int asistenteId, DateTime fechaCitaIngreso)
        {
            string cita;
            int doctorId = _wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);
            var citas = _wrapper.CitasRepository.GetAll()
                                .Where(c => c.IsActive != false && c.DoctorId == doctorId)
                                .ToList();
            var fechaCita = DateTime.Parse(fechaCitaIngreso.ToString("dd/MM/yyyy"));

            foreach (var item in citas)
            {
                var fechaItem = DateTime.Parse(item.FechaCita.ToString("dd/MM/yyyy"));
                if (fechaCita == fechaItem)
                {
                    cita = fechaCitaIngreso.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture);

                    foreach (var citaCurrent in citas)
                    {
                        var fechaC = citaCurrent.FechaCita.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture);
                        if (TimeSpan.Compare(DateTime.Parse(cita).TimeOfDay, DateTime.Parse(fechaC).TimeOfDay) == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool HorarioIsValid(int asistenteId, DateTime fechaCita)
        {
            int doctorId = _wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);
            var horario = _wrapper.HorarioRepository.GetAll()
                                          .Where(h => h.IsActive != false && h.DoctorId == doctorId)
                                          .FirstOrDefault();

            var cita = fechaCita.ToString("HH:mm:ss tt", CultureInfo.InvariantCulture);

            int minutoCita = DateTime.Parse(cita).Minute;

            if(minutoCita != 0 && minutoCita != 20)
            {
                if(minutoCita != int.Parse(horario.MinutosPorPaciente))
                {
                    return false;
                }
            }

            return true;
        }

        public void DeleteCita(int citaId)
        {
            Citas cita = _wrapper.CitasRepository.GetAll()
                                 .Where(c => c.CitaId == citaId)
                                 .FirstOrDefault();

            cita.IsActive = false;

            _wrapper.CitasRepository.Update(cita);
            _wrapper.Save();
        }

       public bool DiaIsAvailable(int asistenteId, DateTime fechaCita)
       {
            string diaCita = DateTime.Parse(fechaCita.ToString("dd/MM/yyyy")).ToString("dddd", new CultureInfo("es-ES"));
            
            if(diaCita == "miércoles")
            {
                diaCita = "miercoles";
            }

            if (diaCita == "Sábado")
            {
                diaCita = "miercoles";
            }

            if (diaCita == "domingo" || !this.GetDiasLaborables(asistenteId).Contains(diaCita))
            {
                return false;
            }

            return true;
       }

       private List<string> GetDiasLaborables(int asistenteId)
       {
            List<string> dias = new List<string>();

            int doctorId = _wrapper.personalRepository.GetDoctorIdByAsistente(asistenteId);
            var horario = _wrapper.HorarioRepository.GetAll()
                                          .Where(h => h.IsActive != false && h.DoctorId == doctorId)
                                          .FirstOrDefault();
            List<DiasLaborables> diasLaborables = _wrapper.DiasLaborablesRepository.GetAll()
                                                          .Where(d => d.IsActive != false)
                                                          .Where(d => d.HorarioId == horario.HorarioId)
                                                          .ToList();

            foreach (var item in diasLaborables)
            {
                if(item.Lunes != false)
                {
                    dias.Add(item.PathOf(i => i.Lunes).ToLower());
                }
                if (item.Martes != false)
                {
                    dias.Add(item.PathOf(i => i.Martes).ToLower());
                }
                if (item.Miecoles != false)
                {
                    dias.Add(item.PathOf(i => i.Miecoles).ToLower());
                }
                if (item.Jueves != false)
                {
                    dias.Add(item.PathOf(i => i.Jueves).ToLower());
                }
                if (item.Viernes != false)
                {
                    dias.Add(item.PathOf(i => i.Viernes).ToLower());
                }
                if (item.Sabado != false)
                {
                    dias.Add(item.PathOf(i => i.Sabado).ToLower());
                }
            }

            return dias;
       }

        public bool IsFechaInicioMayorQueFechaFin(DateTime fechaInicio, DateTime fechaFin)
        {
            var fecha1 = DateTime.Parse(fechaInicio.ToString("dd/MM/yyyy"));
            var fecha2 = DateTime.Parse(fechaFin.ToString("dd/MM/yyyy"));

            if (fecha1 > fecha2)
            {
                return true;
            }

            return false;
        }
    }
}
