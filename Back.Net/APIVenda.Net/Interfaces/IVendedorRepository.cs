using APIVenda.Net.Entities;

namespace APIVenda.Net.Interfaces;

public interface IVendedorRepository
{
    Task<Vendedor> GetByIdAsync(Guid id);
    Task<ICollection<Vendedor>> GetAllAsync();
    Task<Vendedor> CreateAsync(Vendedor vendedor);
    Task EditAsync(Vendedor vendedor);
    Task DeleteAsync(Vendedor vendedor);
    
}
