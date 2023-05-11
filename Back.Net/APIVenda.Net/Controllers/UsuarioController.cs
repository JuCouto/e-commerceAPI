using APIVenda.Net.DTOs;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIVenda.Net.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Pesquisa de usuários cadastrados.
    /// </summary>
    /// <remarks>
    /// <param name="">Lista todos os usuários cadastrados no sistema.</param>
    /// </remarks>
    /// <response code = "200">Usuárioss listados com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllAsync()
    {
        var resultado = await _usuarioService.GetAllAsync();
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest();
    }

    /// <summary>
    /// Pesquisa de usuário cadastrado pelo Id.
    /// </summary>
    /// <remarks>
    /// <param name="">Busca o usuário cadastrado no sistema pelo Id.</param>
    /// </remarks>
    /// <response code = "200">Usuário encontrado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id)
    {
        var resultado = await _usuarioService.GetByIdAsync(id);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest();
    }

    /// <summary>
    /// Pesquisa de usuário ativo cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Busca os usuários ativos cadastrados no sistema.</param>
    /// </remarks>
    /// <response code = "200">Usuários encontrados com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    [Route("ativo")]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllActiveAsync()
    {
        var result = await _usuarioService.GetAllActiveAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Cadastra um usuário..
    /// </summary>
    /// <remarks>
    /// <param name="">Cadastra um usuário no sistema.</param>
    /// </remarks>
    /// <response code = "200">Usuáriocadastrado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPost]
    //[Authorize(Policy = "SA/AD-register")]
    [AllowAnonymous]
    public async Task<ActionResult> PostAsync([FromBody] UsuarioDto usuarioDto)
    {
        var result = await _usuarioService.CreateAsync(usuarioDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Atualiza um usuário cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Atualiza um usuário cadastrado no sistema.</param>
    /// </remarks>
    /// <response code = "200">Usuário atualizado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    //[Authorize(Policy = "SA/AD-register")]
    [AllowAnonymous]
    public async Task<ActionResult> UpdateAsync([FromBody] UsuarioResponseDto usuarioResponseDto)
    {
        var result = await _usuarioService.UpdateAsync(usuarioResponseDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Desativa um usuário cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Desativa um usuário cadastrado no sistema.</param>
    /// </remarks>
    /// <response code = "200">Usuário desativado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    [Route("deactivate/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult> DeactivateUserAsync(Guid id)
    {
        var result = await _usuarioService.DeactivateUserAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Reativa um usuário cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Reativa um usuário cadastrado no sistema.</param>
    /// </remarks>
    /// <response code = "200">Usuário reativado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    [Route("reactivate/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult> ReactivateUserAsync(Guid id)
    {
        var result = await _usuarioService.ReactivateUserAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

}
