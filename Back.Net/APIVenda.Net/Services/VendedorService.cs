using APIVenda.Net.DTOs;
using APIVenda.Net.DTOs.Validations;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Repositories;
using AutoMapper;
using FluentValidation;

namespace APIVenda.Net.Services;

public class VendedorService : IVendedorService
{
    readonly IVendedorRepository _vendedorRepository;
    readonly IMapper _mapper;

    public VendedorService(IVendedorRepository vendedorRepository, IMapper mapper)
    {
        _vendedorRepository = vendedorRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<ICollection<VendedorDto>>> GetAllAsync()
    {
        var vendedores = await _vendedorRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<VendedorDto>>(vendedores));
    }

    public async  Task<ResultService<VendedorDto>> GetByIdAsync(Guid id)
    {
        var vendedor = await _vendedorRepository.GetByIdAsync(id);
        if (vendedor == null)
            return ResultService.Fail<VendedorDto>("Produto não foi encontrado");

        return ResultService.Ok(_mapper.Map<VendedorDto>(vendedor));
    }

    public async Task<ResultService<VendedorDto>> CreateAsync(VendedorDto vendedorDto)
    {
        if (vendedorDto == null)
            return ResultService.Fail<VendedorDto>("Necessário preencher os dados solicitados!");

        var resultado = new VendedorDtoValidator().Validate(vendedorDto);
        if (!resultado.IsValid)
            return ResultService.RequestError<VendedorDto>("Problemas na validação", resultado);
       
        var novoVendedor = _mapper.Map<Vendedor>(vendedorDto);
        var dadosSalvos = await _vendedorRepository.CreateAsync(novoVendedor);
        return ResultService.Ok(_mapper.Map<VendedorDto>(dadosSalvos));
    }

    public async Task<ResultService> UpdateAsync(VendedorDto vendedorDto)
    {
        if (vendedorDto == null)
            return ResultService.Fail<VendedorDto>("Necessário preencher dados solicitados!");

        var resultado = new VendedorDtoValidator().Validate(vendedorDto);
        if (!resultado.IsValid)
            return ResultService.RequestError<ProdutoDto>("Problemas na validação", resultado);

        var vendedor = await _vendedorRepository.GetByIdAsync(vendedorDto.Id);
        if (vendedor == null)
            return ResultService.Fail("Vendedor não encontrado!");

        vendedor = _mapper.Map(vendedorDto, vendedor);
        await _vendedorRepository.EditAsync(vendedor);
        return ResultService.Ok("Vendedor Editado com sucesso!");
    }

    public async Task<ResultService> RemoveAsync(Guid id)
    {
        var vendedor = await _vendedorRepository.GetByIdAsync(id);
        if (vendedor == null)
            return ResultService.Fail("Produto não encontrado!");

        await _vendedorRepository.DeleteAsync(vendedor);
        return ResultService.Ok($"Produto com o id {id} foi deletado com sucesso!");
    }
}
