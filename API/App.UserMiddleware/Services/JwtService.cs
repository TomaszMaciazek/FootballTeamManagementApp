
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
        private readonly IJwtSettings _jwtSettings;
        private readonly IUserSettings _userSetting;

        public JwtService(IJwtSettings jwtSettings, IUserSettings userSetting)
        {
            _jwtSettings = jwtSettings;
            _userSetting = userSetting;
        }

        public string CreateJwtToken(User user)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var tokenExpirationInMinutes = _jwtSettings.TokenExpirationInMinutes;

            var token =  new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: GetTokenClais(user, _userSetting.PasswordChangeInterval),
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(tokenExpirationInMinutes)),
                signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static IEnumerable<Claim> GetTokenClais(User user, int passwordChangeInterval) {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("passwordChangeRequired", user.LastPasswordSet < DateTime.Now.AddDays(-1 *  passwordChangeInterval) ? "true" : "false")
            };
            claims.AddRange(user.Role.Claims.Select(x => x.ToClaim()));
            return claims.Distinct();
        }
    }
}
