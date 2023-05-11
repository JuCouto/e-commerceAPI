using APIVenda.Net.Entities;

namespace APIVenda.Net.Interfaces;

public interface IProdutoRepository
{
    Task<Produto> GetByIdAsync(Guid id);
    Task<ICollection<Produto>> GetAllAsync();
    Task<Produto> CreateAsync(Produto produto);
    Task EditAsync(Produto produto);
    Task DeleteAsync(Produto produto);
    Task<Guid> GetIdByCodProdAsync(string codigo);
}
