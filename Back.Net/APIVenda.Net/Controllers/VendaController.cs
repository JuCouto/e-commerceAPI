using APIVenda.Net.DTOs;
using APIVenda.Net.Enum;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIVenda.Net.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendaController : ControllerBase
{
    private readonly IVendaService _vendaService;

    public VendaController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    /// <summary>
    /// Pesquisa de Vendas cadastradas.
    /// </summary>
    /// <remarks>
    /// <param name="">Lista todas as vendas cadastradas no sistema.</param>
    /// </remarks>
    /// <response code = "200">Vendas listadas com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var resultado = await _vendaService.GetAllAsync();
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest();
    }

    /// <summary>
    /// Pesquisa de Venda cadastrada.
    /// </summary>
    /// <remarks>
    /// <param name="">Lista venda cadastrada pelo Id no sistema.</param>
    /// </remarks>
    /// <response code = "200">Venda listada com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id)
    {
        var resultado = await _vendaService.GetByIdAsync(id);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest();
    }

    /// <summary>
    /// Cadastro de vendas.
    /// </summary>
    /// <remarks>
    /// <param name="">Cadastra uma venda no sistema.</param>
    /// </remarks>
    /// <response code = "200">Venda cadastrada com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] VendaDto vendaDto)
    {
        var resultado = await _vendaService.CreateAsync(vendaDto);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest(resultado);
    }

    /// <summary>
    /// Atualiza uma venda cadastrada.
    /// </summary>
    /// <remarks>
    /// <param name="">Atualiza uma venda cadastrada no sistema.</param>
    /// </remarks>
    /// <response code = "200">Venda atualizada com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    
    public async Task<ActionResult> UpdateAsync([FromBody] VendaDto vendaDto)
    {
        var result = await _vendaService.UpdateAsync(vendaDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Atualiza o Status de uma venda cadastrada.
    /// </summary>
    /// <remarks>
    /// <param name="">Atualiza o Status de uma venda cadastrada no sistema.</param>
    /// </remarks>
    /// <response code = "200">Venda atualizada com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    [Route("status")]
    public async Task<ActionResult> UpdateStatusVendaAsync([FromQuery] Guid id, StatusVenda status)
    {
        var result = await _vendaService.UpdateStatusVendaAsync(id, status);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    /// <summary>
    /// Deleta uma venda cadastrada.
    /// </summary>
    /// <remarks>
    /// <param name="">Deleta uma venda cadastrada no sistema pelo Id.</param>
    /// </remarks>
    /// <response code = "200">Venda deletada com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var result = await _vendaService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
