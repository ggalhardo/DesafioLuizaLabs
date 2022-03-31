using Microsoft.Extensions.DependencyInjection;
using PDesafioLuizaLabs.Application.Interfaces;
using PDesafioLuizaLabs.Application.Services;
using PDesafioLuizaLabs.Data.Repositories;
using PDesafioLuizaLabs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioLuizaLabs.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService, UserService>();

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion
        }
    }
}
