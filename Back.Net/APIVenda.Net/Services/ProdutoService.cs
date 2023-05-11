using APIVenda.Net.DTOs;
using APIVenda.Net.DTOs.Validations;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using AutoMapper;
using FluentValidation;

namespace APIVenda.Net.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private IMapper _mapper;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<ProdutoDto>> GetByIdAsync(Guid id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null)
            return ResultService.Fail<ProdutoDto>("Produto não foi encontrado");
      

        return ResultService.Ok<ProdutoDto>(_mapper.Map<ProdutoDto>(produto));
    }

    public async Task<ResultService<ICollection<ProdutoDto>>> GetAllAsync()
    {
        var produtos = await _produtoRepository.GetAllAsync();
        return ResultService.Ok<ICollection<ProdutoDto>>(_mapper.Map<ICollection<ProdutoDto>>(produtos));
    }

    public async Task<ResultService<ProdutoDto>> CreateAsync(ProdutoCadastroDto produtoCadastroDto)
    {
        if (produtoCadastroDto == null)
            return ResultService.Fail<ProdutoDto>("Necessário preencher os dados solicitados!");

        var resultado = new ProdutoDtoValidator().Validate(_mapper.Map<ProdutoDto>(produtoCadastroDto));
        if(!resultado.IsValid)
            return ResultService.RequestError<ProdutoDto>("Problemas na validação", resultado);

        var novoProduto = _mapper.Map<Produto>(produtoCadastroDto);
        var dadosSalvos = await _produtoRepository.CreateAsync(novoProduto);
        return ResultService.Ok<ProdutoDto>(_mapper.Map<ProdutoDto>(dadosSalvos));

    }

    public async Task<ResultService> UpdateAsync(ProdutoDto produtoDto)
    {
        if (produtoDto == null)
            return ResultService.Fail<ProdutoDto>("Necessário preencher dados solicitados!");

        var produto = await _produtoRepository.GetByIdAsync(produtoDto.Id);
        if (produto == null)
            return ResultService.Fail("Produto não encontrado!");
        
        var resultado = new ProdutoDtoValidator().Validate(produtoDto);
        if (!resultado.IsValid)
            return ResultService.RequestError<ProdutoDto>("Problemas na validação", resultado);

        // Dessa forma mantém o produto mapeado, não perde a informação que veio do banco de dados.
        produto = _mapper.Map(produtoDto, produto);
        await _produtoRepository.EditAsync(produto);
        return ResultService.Ok("Produto Editado com sucesso!");

    }

    public async Task<ResultService> RemoveAsync(Guid id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null)
            return ResultService.Fail("Produto não encontrado!");

        await _produtoRepository.DeleteAsync(produto);
        return ResultService.Ok($"Produto com o id {id} foi deletado com sucesso!");

    }

}
