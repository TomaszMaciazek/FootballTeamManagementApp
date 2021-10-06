
using App.DataAccess.Configurations.Interfaces;
using App.Model.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace App.UserMiddleware.Services
{
    public interface IJwtService
    {
        string CreateJwtToken(User user);
    }

    public class JwtService : IJwtService
    {
        private readonly IJwtSettings _settings;

        public JwtService(IJwtSettings settings)
        {
            _settings = settings;
        }

        public string CreateJwtToken(User user)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var tokenExpirationInMinutes = _settings.TokenExpirationInMinutes;

            var token =  new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: GetTokenClais(user),
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(tokenExpirationInMinutes)),
                signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static IEnumerable<Claim> GetTokenClais(User user) {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
            };
            foreach(var role in user.Roles)
            {
                claims.AddRange(role.Claims.Select(x => x.ToClaim()));
            }
            return claims;
        }
    }
}
