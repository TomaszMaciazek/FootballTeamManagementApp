using App.DataAccess.Configurations.Implementations;
using App.DataAccess.Configurations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace App.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IUserSettings, UserSettings>();
            services.AddSingleton<IJwtSettings, JwtSettings>();
            return services;
        }
    }
}
