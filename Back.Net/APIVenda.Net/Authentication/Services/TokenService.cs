using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.Authentication.DTOs.Validations;
using APIVenda.Net.Authentication.Entities;
using APIVenda.Net.Authentication.Interfaces;
using APIVenda.Net.DTOs;
using APIVenda.Net.DTOs.Validations;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIVenda.Net.Authentication.Services;

public class TokenService : ITokenService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;

    public TokenService(ITokenGenerator tokenGenerator, IUsuarioRepository usuarioRepository, IMapper mapper, ITokenRepository tokenRepository, IWebHostEnvironment environment)
    {
        _tokenGenerator = tokenGenerator;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _tokenRepository = tokenRepository;
        _environment = environment;
    }

    public async Task<ResultService> GenerateTokenAsync(LoginDto loginDto)
    {
        if (loginDto == null)
            return ResultService.Fail<dynamic>("Objeto deve ser informado");

        var validador = new UsuarioDtoValidator().Validate(_mapper.Map<UsuarioDto>(loginDto));
        if (!validador.IsValid)
            return ResultService.RequestError<dynamic>("Problemas com a validação", validador);

        var usuario = await _usuarioRepository.GetUserByEmailAndPasswordAsync(loginDto.EmailUsuario, loginDto.Password);
        if (usuario == null)
            return ResultService.Fail<dynamic>("Usuário ou senha não encontrado!!");

        return ResultService.Ok(_tokenGenerator.Generator(usuario));

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
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rnp = RandomNumberGenerator.Create();
        rnp.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public RefreshToken GetRefreshTokenAsync(string email)
    {
        var refreshToken = _tokenRepository.GetRefreshToken(email);
        if (refreshToken == null)
            throw new Exception("Não existe RefreshToken para este usuário!");

        return refreshToken;
    }
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.secret)),
            ValidateLifetime = false
        };

        var tokenHandle = new JwtSecurityTokenHandler();
        var principal = tokenHandle.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public async Task<ResultService<Usuario>> Authenticate(LoginDto loginDto)
    {
        if (loginDto == null)
            throw new Exception("Objeto deve ser informado");

        var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.EmailUsuario);
        if (usuario == null)
            return ResultService.Fail<Usuario>("E-mail incorreto");

        if (usuario.Ativo != true)
            return ResultService.Fail<Usuario>("Usuário desativado!");

        var passwordValida = BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.Password);

        return !passwordValida ? ResultService.Fail<Usuario>("Senha incorreta") : ResultService.Ok(usuario);
    }

    public async Task<ResultService<RefreshToken>> SaveRefreshTokenAsync(RefreshToken refreshToken)
    {
        if (refreshToken == null)
            throw new Exception("o objeto deve ser informado");

        var result = new RefreshTokenValidator().Validate(refreshToken);
        if (!result.IsValid)
            throw new Exception("erro de validaçao");

        return ResultService.Ok(await _tokenRepository.SaveRefreshTokenAsync(refreshToken));
    }

    public async Task<ResultService> DeleteRefreshTokenAsync(string email)
    {
        var refreshToken = _tokenRepository.GetRefreshToken(email);
        if (refreshToken == null)
            return ResultService.Fail("Não existe RefreshToken para este usuário!");

        await _tokenRepository.DeleteRefreshTokenAsync(refreshToken);
        return ResultService.Ok("Refresh token deletado com sucesso!");
    }
}
