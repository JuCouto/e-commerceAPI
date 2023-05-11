using APIVenda.Net.DTOs;
using APIVenda.Net.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIVenda.Net.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{

    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    /// <summary>
    /// Pesquisa de produtos cadastrados.
    /// </summary>
    /// <remarks>
    /// <param name="">Lista todos os produtos cadastrados no sistema.</param>
    /// </remarks>
    /// <response code = "200">Produtos listados com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var resultado = await _produtoService.GetAllAsync();
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest();
    }

    /// <summary>
    /// Pesquisa de produtos cadastrados pelo Id.
    /// </summary>
    /// <remarks>
    /// <param name="">Busca o produto cadastrado no sistema pelo Id.</param>
    /// </remarks>
    /// <response code = "200">Produto encontrado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id)
    {
        var resultado = await _produtoService.GetByIdAsync(id);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest("Produto não encontrado no sistema");
    }

    /// <summary>
    /// Cadastro de produtos.
    /// </summary>
    /// <remarks>
    /// <param name="">Cadastra um produto no sistema.</param>
    /// </remarks>
    /// <response code = "200">Produto cadastrado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] ProdutoCadastroDto produtoCadastroDto)
    {
        var resultado = await _produtoService.CreateAsync(produtoCadastroDto);
        if (resultado.IsSuccess)
            return Ok(resultado);

        return BadRequest(resultado);
    }

    /// <summary>
    /// Atualiza um produto cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Atualiza um produto cadastrado no sistema.</param>
    /// </remarks>
    /// <response code = "200">Produto atualizado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] ProdutoDto produtoDto)
    {
        var result = await _produtoService.UpdateAsync(produtoDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Deleta um produto cadastrado.
    /// </summary>
    /// <remarks>
    /// <param name="">Deleta um produto cadastrados no sistema pelo Id.</param>
    /// </remarks>
    /// <response code = "200">Produto deletado com sucesso</response>
    /// <response code = "400">Retorna erros de validação</response>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var result = await _produtoService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
