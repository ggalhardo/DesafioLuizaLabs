using DesafioLuizaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using PDesafioLuizaLabs.Domain.Models;

namespace PDesafioLuizaLabs.Data.Extensions
{
    public static class ModelBuilderExtension
    {

        public static ModelBuilder ApplyGlobalConfigurations(this ModelBuilder builder)
        {

            foreach(IMutableEntityType entityTypes in builder.Model.GetEntityTypes())
            {
                foreach(IMutableProperty property in entityTypes.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(Entity.Id):
                            property.IsKey();
                            break;
                        case nameof(Entity.DateCreated):
                            property.IsNullable = false;
                            property.SetDefaultValue(DateTime.Now);
                            break;
                        case nameof(Entity.DateUpdated):
                            property.IsNullable = true;
                            break;
                        case nameof(Entity.IsDeleted):
                            property.IsNullable = false;
                            property.SetDefaultValue(false);
                            break;
                        default:
                            break;
                    }
                }
            }

            return builder;
        }

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(
                    new User { Id = Guid.Parse("ad3f3e51-5a97-4325-8f70-b7b642c0e0ec"), Name = "User Default", Senha = "7C4A8D09CA3762AF61E59520943DC26494F8941B", ConfirmacaoSenha = "7C4A8D09CA3762AF61E59520943DC26494F8941B", Email = "user_default@desafioluizalabs.com", DateCreated = new DateTime(2022,03,28), IsDeleted = false, DateUpdated = null}
                );

            return builder;
        }

    }
}
