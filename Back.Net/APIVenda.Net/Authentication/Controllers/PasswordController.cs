using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.Authentication.Interfaces;
using APIVenda.Net.Authentication.Services;
using APIVenda.Net.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIVenda.Net.Authentication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PasswordController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public PasswordController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    ///     Atualiza a senha de um usuário.
    /// </summary>
    [HttpPut]
    [Route("password")]
    [AllowAnonymous]
    public async Task<ActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDto passwordDto)
    {
        var result = await _usuarioService.UpdatePasswordAsync(passwordDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    ///     Redefinir senha do usuario.
    /// </summary>
    [HttpPut]
    [Route("recover")]
    [AllowAnonymous]
    public async Task<ActionResult> RecoveryPassword(RecoverPasswordDto recoverPasswordDto)
    {
        var result = await _usuarioService.RecoveryPassword(recoverPasswordDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
