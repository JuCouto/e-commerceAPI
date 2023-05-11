using APIVenda.Net.DTOs;
using APIVenda.Net.DTOs.Validations;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Repositories;
using AutoMapper;
using Correios.Net;
using FluentValidation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APIVenda.Net.Services;

public class EnderecoService : IEnderecoService
{
    readonly IEnderecoRepository _enderecoRepository;
    readonly IMapper _mapper;
   readonly IUsuarioRepository _usuarioRepository;


    public EnderecoService(IEnderecoRepository enderecoRepository, IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _enderecoRepository = enderecoRepository;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ResultService<EnderecoDto>> GetByIdAsync(Guid id)
    {
        var enderecoId = await _enderecoRepository.GetByIdAsync(id);
        if (enderecoId == null)
            return ResultService.Fail<EnderecoDto>($"O endereço não foi encontrado");

        return ResultService.Ok(_mapper.Map<EnderecoDto>(enderecoId));
    }

    public async Task<ResultService<ICollection<EnderecoDto>>> GetAllAsync()
    {
       var endereco = await _enderecoRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<EnderecoDto>>(endereco));
    }
      

    public async Task<ResultService<EnderecoResponseDto>> CreateAsync(EnderecoResponseDto enderecoResponseDto)
    {
        if (enderecoResponseDto == null)
            return ResultService.Fail<EnderecoResponseDto>("Necessário preencher os dados solicitados!");

        var resultado = new EnderecoDtoValidator().Validate(_mapper.Map<EnderecoDto>(enderecoResponseDto));
        if (!resultado.IsValid)
            return ResultService.RequestError<EnderecoResponseDto>("Problemas na validação", resultado);

        var usuario = await _usuarioRepository.GetByIdAsync(enderecoResponseDto.UsuarioId);

        var novoEndereco = new Endereco(enderecoResponseDto.Logradouro, enderecoResponseDto.Bairro, enderecoResponseDto.Numero,
                                        enderecoResponseDto.Complemento, enderecoResponseDto.Cidade, enderecoResponseDto.UF,
                                        enderecoResponseDto.CEP,usuario);
        

        var dadosSalvos = await _enderecoRepository.CreateAsync(novoEndereco);
        return ResultService.Ok(_mapper.Map<EnderecoResponseDto>(dadosSalvos));
    }
       

    public Task<ResultService> RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ResultService> UpdateAsync(EnderecoDto enderecoDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultService<EnderecoResponseDto>> ConsultarCep(object cep)
    {
        using HttpClient Client = new HttpClient();

        EnderecoResponseDto viacep = new EnderecoResponseDto();

        HttpResponseMessage resposta = await Client.GetAsync($"http://viacep.com.br/ws/{cep}/json/");
        if (resposta.IsSuccessStatusCode)           
            viacep = await resposta.Content.ReadFromJsonAsync<EnderecoResponseDto>();
       
        return ResultService.Ok(viacep);

    }

}
