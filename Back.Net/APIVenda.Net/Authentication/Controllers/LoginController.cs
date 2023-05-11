using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.Authentication.Entities;
using APIVenda.Net.Authentication.Interfaces;
using APIVenda.Net.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace APIVenda.Net.Authentication.Controllers;

[Route("api/")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUsuarioService _usuarioService;

    public LoginController(ITokenService tokenService, IUsuarioService usuarioService)
    {
        _tokenService = tokenService;
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [Route("login")]
    //[AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginDto loginDto)
    {
        var result = _tokenService.Authenticate(loginDto);
        if (!result.Result.IsSuccess)
            return BadRequest(result.Result);
        var token = _tokenService.GenerateTokenAsync(loginDto);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var model = new RefreshToken(loginDto.EmailUsuario, refreshToken);
        await _tokenService.DeleteRefreshTokenAsync(loginDto.EmailUsuario);
        await _tokenService.SaveRefreshTokenAsync(model);

        return Ok(new
        {
            token,
            refreshToken = model.TokenRefresh
        });
    }

    [HttpPost]
    [Route("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Refresh(RefreshTokenDto refreshTokenDto)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenDto.Token);
        var email = principal.FindFirstValue("CorporativeEmail");
        var savedRefreshToken = _tokenService.GetRefreshTokenAsync(email);
        if (savedRefreshToken.TokenRefresh != refreshTokenDto.RefreshToken)
            throw new SecurityTokenException("Invalid refresh!");

        var newJwtToken = _tokenService.GenerateToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        await _tokenService.DeleteRefreshTokenAsync(email);
        var model = new RefreshToken(email, newRefreshToken);
        await _tokenService.SaveRefreshTokenAsync(model);

        return new ObjectResult(new
        {
            token = newJwtToken,
            refreshToken = newRefreshToken
        });
    }
}
