namespace App.DataAccess.Configurations.Interfaces
{
    public interface IUserSettings
    {
        double AccountLockoutTimeInMinutes { get; }
        int MaxBadLogonCout { get; }
        int PasswordChangeInterval { get; }
    }
}
