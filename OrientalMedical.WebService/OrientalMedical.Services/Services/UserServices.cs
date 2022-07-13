﻿using AutoMapper;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Entities;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Models;
using OrientalMedical.Shared.DataTranfereObject.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrientalMedical.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IRepositoriesWrapper _wrapper;
        private readonly IMapper _mapper;

        public UserServices(IRepositoriesWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        public void CreateCredentials(string userName, string password)
        {
            Usuarios user = new Usuarios();
            user.Usuario = userName;
            user.Clave = _wrapper.Password;
            user.PersonalId = _wrapper.personalRepository.GetLastId();

            _wrapper.UserRepository.Create(user);
            _wrapper.Save();
        }

        public UserInformation GetUserDetail(string userName, string password)
        {
            int personalId = _wrapper.UserRepository.GetUserId(userName, password);

            Personal personal = _wrapper.personalRepository
                                        .GetByFilter(p => p.PersonalId == personalId)
                                        .FirstOrDefault();

            UserInformation userInformation = _mapper.Map<UserInformation>(personal);

            return userInformation;
        }

        public UserResponseDTOs GetCredentials(string cedula)
        {
            int personalId = _wrapper.personalRepository.GetAll().Where(p => p.Cedula == cedula)
                                                .FirstOrDefault().PersonalId;

            Usuarios user = _wrapper.UserRepository.GetAll().Where(u => u.PersonalId == personalId)
                                .FirstOrDefault();

            UserResponseDTOs credentials = _mapper.Map<UserResponseDTOs>(user);

            return credentials;
        }

        public bool IsAnUser(string userName, string password)
        {
            return _wrapper.UserRepository.IsAnUser(userName, password);
        }

        public bool IsCurrentPassWord(int personalId, string password)
        {
            return _wrapper.UserRepository.IsCurrentPassWord(personalId, password);
        }

        public void UpdatePassword(int personalId, string password)
        {
            Usuarios user = _wrapper.UserRepository.GetByFilter(u => u.PersonalId == personalId)
                                    .FirstOrDefault();

            user.Clave = password;
            _wrapper.UserRepository.Update(user);
            _wrapper.Save();
        }

        public void UpdatePasswordByUserId(int userId, string password)
        {
            Usuarios user = _wrapper.UserRepository.GetByFilter(u => u.UsuarioId == userId)
                                   .FirstOrDefault();

            user.Clave = password;
            _wrapper.UserRepository.Update(user);
            _wrapper.Save();
        }
    }
}
