using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.Authentication.Entities;
using APIVenda.Net.Entities;
using APIVenda.Net.Services;
using System.Security.Claims;

namespace APIVenda.Net.Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<ResultService> GenerateTokenAsync(LoginDto loginDto);
        public string GenerateRefreshToken();
        public string GenerateToken(IEnumerable<Claim> claims);
        RefreshToken GetRefreshTokenAsync(string email);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<ResultService<RefreshToken>> SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<ResultService> DeleteRefreshTokenAsync(string email);
        Task<ResultService<Usuario>> Authenticate(LoginDto loginDto);
    }
}
