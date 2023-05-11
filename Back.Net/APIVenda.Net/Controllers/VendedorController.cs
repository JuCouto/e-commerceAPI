using APIVenda.Net.DTOs;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIVenda.Net.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendedorController :  ControllerBase
{
    private readonly IVendedorService _vendedorService;

    public VendedorController(IVendedorService vendedorService)
    {
        _vendedorService = vendedorService;
    }

    /// <summary>
    /// Pesquisa de vendedores cadastrados.
    /// </summary>
    /// <remarks>
    /// <param name="">Lista todos os vendedores cadastrados no sistema.</param>
    /// </remarks>
    /// <response code = "200">Vendedores listados com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var resultado = await _vendedorService.GetAllAsync();
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest();
    }

    /// <summary>
    /// Pesquisa de vendedor cadastrado pelo Id.
    /// </summary>
    /// <remarks>
    /// <param name="">Busca o vendedor cadastrado no sistema pelo Id.</param>
    /// </remarks>
    /// <response code = "200">Vendedor encontrado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id)
    {
        var resultado = await _vendedorService.GetByIdAsync(id);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest();
    }

    /// <summary>
    /// Cadastro de vendedores.
    /// </summary>
    /// <remarks>
    /// <param name="">Cadastra umvendedor no sistema.</param>
    /// </remarks>
    /// <response code = "200">Vendedor cadastrado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] VendedorDto vendedorDto)
    {
        var resultado = await _vendedorService.CreateAsync(vendedorDto);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest(resultado);
    }

    /// <summary>
    /// Atualiza um vendedor cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Atualiza um vendedor cadastrado no sistema.</param>
    /// </remarks>
    /// <response code = "200">Vendedor atualizado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] VendedorDto vendedorDto)
    {
        var result = await _vendedorService.UpdateAsync(vendedorDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Deleta um vendedor cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Deleta um vendedor cadastrados no sistema pelo Id.</param>
    /// </remarks>
    /// <response code = "200">Vendedor deletado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var result = await _vendedorService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
