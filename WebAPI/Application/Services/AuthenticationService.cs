using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Application.DTOs;
using WebAPI.Application.Services.Interfaces;
using WebAPI.Common;

namespace WebAPI.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<AuthenticationResponse> GetJwtToken(string secretKey)
        {
            var expiresDate = DateTime.UtcNow.AddMinutes(Constants.ExpiresMinutes);
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, Constants.RoleAdmin),
                    new Claim(ClaimTypes.Role, Constants.RoleAdmin)
                }),
                Expires = expiresDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResult = tokenHandler.WriteToken(token);
            return Task.Run(() => new AuthenticationResponse
            {
                Token = tokenResult,
                Expiration = token.ValidTo
            });
        }
    }
}
