using DojoMyBusiness.Interfaces;
using DojoMyBusiness.Notificacoes;
using DojoMyBusiness.Services;
using DojoMyData.Context;
using DojoMyData.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DojoMyPlaylist.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            services.AddScoped<IMusicaRepository, MusicaRepository>();

            services.AddScoped<IPlaylistService, PlaylistService>();

            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
