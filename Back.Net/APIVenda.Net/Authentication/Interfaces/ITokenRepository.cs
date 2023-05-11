using APIVenda.Net.Authentication.Entities;

namespace APIVenda.Net.Authentication.Interfaces;

public interface ITokenRepository
{
    Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refreshToken);
    public RefreshToken GetRefreshToken(string email);
    Task DeleteRefreshTokenAsync(RefreshToken refreshToken);
}
