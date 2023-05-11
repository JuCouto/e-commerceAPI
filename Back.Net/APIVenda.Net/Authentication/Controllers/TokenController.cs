using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIVenda.Net.Authentication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("token")]
    public async Task<ActionResult> PostTokenAsync([FromForm] LoginDto loginDto)
    {
        var resultado = await _tokenService.GenerateTokenAsync(loginDto);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest(resultado);
    }
}

