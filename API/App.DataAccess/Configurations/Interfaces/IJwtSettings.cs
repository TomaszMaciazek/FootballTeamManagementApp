namespace App.DataAccess.Configurations.Interfaces
{
    public interface IJwtSettings
    {
        string Audience { get; }
        string Issuer { get; }
        string Key { get; }
        int TokenExpirationInMinutes { get; }
    }
}
