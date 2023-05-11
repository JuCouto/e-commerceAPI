using APIVenda.Net.DTOs;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIVenda.Net.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnderecoController : ControllerBase
{
    private readonly IEnderecoService _enderecoService;

    public EnderecoController(IEnderecoService enderecoService)
    {
        _enderecoService = enderecoService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _enderecoService.GetAllAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Pesquisa o endereço pelo CEP fornecido.
    /// </summary>
    /// <remarks>
    /// <param name="">Lista complementos do endereço buscados na API dos Correios.</param>
    /// </remarks>
    /// <response code = "200">CEP localizado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    [Route("/cep/{cep}")]
    public async Task<ActionResult> GetByViaCep(string cep)
    {
        var result = await _enderecoService.ConsultarCep(cep);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Cadastra endereço de um usuário.
    /// </summary>
    /// <remarks>
    /// <param name="">A partir do ID de um usuário é possível realizar o cadastro.</param>
    /// </remarks>
    /// <response code = "200">Endereço cadastrado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPost]

    public async Task<ActionResult> PostAsync([FromBody] EnderecoResponseDto enderecoResponseDto)
    {
        var result = await _enderecoService.CreateAsync(enderecoResponseDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Atualiza o endereço de um usuário.
    /// </summary>
    /// <remarks>
    /// <param name="">A partir do ID de um usuário é possível realizar a atualização do endereço.</param>
    /// </remarks>
    /// <response code = "200">Endereço atualizado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] EnderecoDto enderecoDto)
    {
        var result = await _enderecoService.UpdateAsync(enderecoDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}

