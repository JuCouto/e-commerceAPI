using APIVenda.Net.Entities;
using APIVenda.Net.Enum;

namespace APIVenda.Net.Interfaces;

public interface IVendaRepository
{
    Task<Venda> GetByIdAsync(Guid id);
    Task<ICollection<Venda>> GetAllAsync();
    Task<ICollection<Venda>> GetByVendedorIdAsync(Guid vendedorId);
    Task<ICollection<Venda>> GetByProdutoIdAsync(Guid produtoId);
    Task <Venda> ObterStatus (StatusVenda statusVenda);
    Task<Venda> CreateAsync(Venda venda);
    Task EditAsync(Venda venda);
    Task DeleteAsync(Venda venda);
}
