using APIVenda.Net.Authentication.Entities;
using APIVenda.Net.Authentication.Interfaces;
using APIVenda.Net.Context;

namespace APIVenda.Net.Authentication.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly ApiVendaDbContext _dbContext;
    

    public TokenRepository(ApiVendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  RefreshToken GetRefreshToken(string email)
    {
        return _dbContext.Tokens.FirstOrDefault(x => x.Email == email);
    }

    public async Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refreshToken)
    {
        _dbContext.Tokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();
        return refreshToken;
    }

    public async Task DeleteRefreshTokenAsync(RefreshToken refreshToken)
    {
        _dbContext.Remove(refreshToken);
        await _dbContext.SaveChangesAsync();
    }
}
