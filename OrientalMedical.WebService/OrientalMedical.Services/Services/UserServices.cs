using AutoMapper;
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

            if (this.UserAlreadyExit(userName))
            {
                user.Usuario = this.RestablecerUser(userName);
            }
            else
            {
                user.Usuario = userName;
            }

            user.Clave = _wrapper.Password;
            user.PersonalId = _wrapper.personalRepository.GetLastId();
            user.Estado = 0;

            _wrapper.UserRepository.Create(user);
            _wrapper.Save();
        }

        public UserInformation GetUserDetail(string userName, string password)
        {
            int personalId = _wrapper.UserRepository.GetUserId(userName, password);

            UserInformation userInformation = _wrapper.UserRepository
                                        .GetByFilter(u => u.PersonalId == personalId)
                                        .Select(u => new UserInformation
                                        {
                                            PersonalId = u.Personal.PersonalId,
                                            UserID = u.UsuarioId,
                                            Nombre = u.Personal.Nombre,
                                            Apellido = u.Personal.Apellido,
                                            Cedula = u.Personal.Cedula,
                                            Ocupacion = u.Personal.Ocupacion,
                                            UserState = u.Estado
                                        })
                                        .FirstOrDefault(); 

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
            user.Estado = 1;

            _wrapper.UserRepository.Update(user);
            _wrapper.Save();
        }

        public void UpdatePasswordByUserId(int userId, string password)
        {
            Usuarios user = _wrapper.UserRepository.GetByFilter(u => u.UsuarioId == userId)
                                   .FirstOrDefault();

            user.Clave = password;
            user.Estado = 0;

            _wrapper.UserRepository.Update(user);
            _wrapper.Save();
        }

        public bool IsActive(int userId)
        {
            int state =  _wrapper.UserRepository.GetAll().Where(u => u.UsuarioId == userId).Select(u => u.Estado).FirstOrDefault();

            if(state > 1)
            {
                return false;
            }

            return true;
        }

        public void BloquearUser(string userName)
        {
            Usuarios user = _wrapper.UserRepository.GetByFilter(u => u.Usuario == userName)
                                    .FirstOrDefault();

            user.Estado = 2;

            _wrapper.UserRepository.Update(user);
            _wrapper.Save();
        }

        public bool UserAlreadyExit(string userName)
        {
            string userNameFound = _wrapper.UserRepository.GetByFilter(u => u.Usuario == userName)
                                    .Select(u => u.Usuario)
                                    .FirstOrDefault();

            if (userNameFound == null)
            {
                return false;
            }

            return true;
        }

        private string RestablecerUser(string userName)
        {
            string usuario = "";
            int pos = userName.Length - 1;

            do
            {
                Random numRandom = new Random();
                usuario = userName.Remove(pos, 1).Insert(pos, numRandom.Next(1, 10).ToString());
            } while (this.UserAlreadyExit(usuario));

            return usuario;
        }
    }
}
