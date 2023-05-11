using APIVenda.Net.Entities;

namespace APIVenda.Net.Interfaces;

public interface IEnderecoRepository
{
    Task<Endereco> GetByIdAsync(Guid id);
    Task<ICollection<Endereco>> GetAllAsync();
    Task<Endereco> CreateAsync(Endereco endereco);
    Task EditAsync(Endereco endereco);
    Task DeleteAsync(Endereco endereco);
}
