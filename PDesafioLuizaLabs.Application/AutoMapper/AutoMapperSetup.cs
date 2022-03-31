using AutoMapper;
using DesafioLuizaLabs.Domain.Entities;
using PDesafioLuizaLabs.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDesafioLuizaLabs.Application.AutoMapper
{
    public class AutoMapperSetup: Profile
    {
        public AutoMapperSetup()
        {
            #region ViewModelToDomain
            CreateMap<UserViewModelGet, User>();
            CreateMap<UserViewModelPost, User>();
            CreateMap<UserViewModelPut, User>();
            #endregion

            #region DomainToViewModel
            CreateMap<User, UserViewModelGet>();
            CreateMap<User, UserViewModelPost>();
            CreateMap<User, UserViewModelPut>();
            #endregion
        }
    }
}
