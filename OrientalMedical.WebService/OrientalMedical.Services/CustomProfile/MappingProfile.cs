using AutoMapper;
using OrientalMedical.Damin.Models.Entities;
using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.RequestDTOs;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrientalMedical.Services.CustomProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Personal, DoctorResponseDTOs>();
            CreateMap<DoctorRequestDTOs, Personal>();
            CreateMap<Personal, UserInformation>();
            CreateMap<Usuarios, UserResponseDTOs>();
            CreateMap<OperadorRequestDTOs, Personal>();
            CreateMap<Personal, OperadorResponseDTOs>();
            CreateMap<EspecialidadRequestDTOs, Especialidad>();
            CreateMap<Especialidad, EspecialidadResponseDTOs>();
            CreateMap<PacienteRequestDTOs, Paciente>();
            CreateMap<Paciente, PacienteResponseDTOs>();
        }
    }
}
