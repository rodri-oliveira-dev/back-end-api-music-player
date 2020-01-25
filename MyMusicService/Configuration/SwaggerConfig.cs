
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DojoMyMusic.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API de musicas",
                    Description = "Este documento descreve a funcionalidade das API de musica",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Rodrigo de Oliveira",
                        Email = "rodrigodotnet@gmail.com",
                        Url = ""
                    }
                });

                // Incluir os comentários do código à documentação
                var xmlCommentsPath = Assembly.GetExecutingAssembly()
                    .Location.Replace("dll", "xml");

                if (!string.IsNullOrEmpty(xmlCommentsPath))
                    c.IncludeXmlComments(xmlCommentsPath);
            });

            return services;
        }
    }
}
