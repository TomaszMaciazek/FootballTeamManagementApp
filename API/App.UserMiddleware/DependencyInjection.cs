using App.UserMiddleware.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UserMiddleware
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserMiddleware(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
