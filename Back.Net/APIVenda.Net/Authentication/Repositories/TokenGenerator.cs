using APIVenda.Net.Authentication.Interfaces;
using APIVenda.Net.Entities;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIVenda.Net.Authentication.Repositories;

public class TokenGenerator : ITokenGenerator
{
    private readonly IWebHostEnvironment _environment;

    public TokenGenerator(IWebHostEnvironment environment)
    {
        _environment = environment;

    }

    public dynamic Generator(Usuario usuario)
    {
        var tokenHandle = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Settings.secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
           {
                // Claim - informações que serão retornadas dentro do token.
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Email", usuario.EmailUsuario),
                //new Claim("Cpf", user.Cpf),
                //  new Claim("Role", user.Role.ToString())
            }),

            Expires = _environment.IsDevelopment() || _environment.IsStaging()
                ? DateTime.UtcNow.AddYears(1)
                : DateTime.UtcNow.AddHours(8),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandle.CreateToken(tokenDescriptor);
        return tokenHandle.WriteToken(token);

    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenHandle = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = _environment.IsDevelopment() || _environment.IsStaging()
                ? DateTime.UtcNow.AddYears(1)
                : DateTime.UtcNow.AddHours(8),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandle.CreateToken(tokenDescriptor);
        return tokenHandle.WriteToken(token);
    }
}
