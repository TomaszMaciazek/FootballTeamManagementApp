using App.DataAccess.Configurations.Interfaces;
using Microsoft.Extensions.Configuration;

namespace App.DataAccess.Configurations.Implementations
{

    public class JwtSettings : IJwtSettings
    {
        private readonly IConfiguration _configuration;

        public JwtSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Issuer { get => _configuration["Jwt:Issuer"]; }
        public string Audience { get => _configuration["Jwt:Audience"]; }
        public int TokenExpirationInMinutes { get => _configuration.GetValue<int>("Jwt:TokenExpirationInMinutes"); }
        public string Key { get => _configuration["Jwt:Key"]; }
    }
}
