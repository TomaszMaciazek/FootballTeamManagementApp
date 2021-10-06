using System;
using System.IdentityModel.Tokens.Jwt;

namespace App.API.Models
{
    public class AuthenticationResult
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
