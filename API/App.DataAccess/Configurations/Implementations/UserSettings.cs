using App.DataAccess.Configurations.Interfaces;
using Microsoft.Extensions.Configuration;

namespace App.DataAccess.Configurations.Implementations
{
    public class UserSettings : IUserSettings
    {
        private readonly IConfiguration _configuration;

        public UserSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int MaxBadLogonCout { get => _configuration.GetValue<int>("BadLogonMaxCount"); }
        public double AccountLockoutTimeInMinutes { get => _configuration.GetValue<double>("AccountLockoutTimeInMinutes"); }
    }
}
