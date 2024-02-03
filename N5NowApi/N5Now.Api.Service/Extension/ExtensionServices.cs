using N5Now.Api.IService;
using N5Now.Api.IRepository;
using N5Now.Api.Repository;
using N5Now.Api.Logic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using N5Now.Api.Data;

namespace N5Now.Api.Service.Extension
{
    public static class ExtensionServices
    {
        public static IServiceCollection AddPermissionService([NotNullAttribute] this IServiceCollection services)
        {
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<PermissionLogic, PermissionLogic>();

            services.AddDbContext<N5nowContext>(ServiceLifetime.Transient);

            return services;
        }
    }
}
