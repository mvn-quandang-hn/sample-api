using WebAPI.Application.DTOs;

namespace WebAPI.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponse> GetJwtToken(string secretKey);
    }
}
