using APIVenda.Net.DTOs;
using APIVenda.Net.Services;

namespace APIVenda.Net.Interfaces;

public interface IEnderecoService
{
    Task<ResultService<ICollection<EnderecoDto>>> GetAllAsync();
    Task<ResultService<EnderecoDto>> GetByIdAsync(Guid id);
    Task<ResultService<EnderecoResponseDto>> CreateAsync(EnderecoResponseDto enderecoResponseDto);
    Task<ResultService> UpdateAsync(EnderecoDto enderecoDto);
    Task<ResultService> RemoveAsync(Guid id);

    Task<ResultService<EnderecoResponseDto>> ConsultarCep(object cep);
}
