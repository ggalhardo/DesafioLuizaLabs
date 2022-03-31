using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioLuizaLabs.Swagger
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services) 
        {
            return services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DesafioLuizaLabsWeb .NET Core",
                    Version = "v1",
                    Description = "Api para integração no DesafioLuizaLabs",
                    Contact = new OpenApiContact
                    {
                        Name = "Glauco Guiherme Galhardo",
                        Email = "glauco.galhardo@gmail.com"
                    }
                });

                var xmlPath = Path.Combine("wwwroot", "ApiDoc.xml");

                opt.IncludeXmlComments(xmlPath);

            });
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger().UseSwaggerUI(c =>
            {
                c.RoutePrefix = "documentation";
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API v1");
            });
        }

    }
}
