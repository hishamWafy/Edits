using Istijara.API.Helpers;
using Istijara.Core.Interfaces;
using Istijara.Core.Interfaces.Services;
using Istijara.Repository.Repositories;
using Istijara.Service.Services;

namespace Istijara.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<EmailToUserNameResolver>();
            return services;
        }
    }
}
