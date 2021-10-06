using BC = BCrypt.Net.BCrypt;


namespace App.UserMiddleware.Helpers
{
    public static class PasswordHashHelper
    {
        public static string HashPassword(string password) => BC.HashPassword(password);

        public static bool Verify(string passwordInput, string passwordHash) => BC.Verify(passwordInput, passwordHash);
    }
}
