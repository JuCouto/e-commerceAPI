using APIVenda.Net.DTOs;
using APIVenda.Net.Services;

namespace APIVenda.Net.Interfaces;

public interface IVendedorService
{
    Task<ResultService<ICollection<VendedorDto>>> GetAllAsync();
    Task<ResultService<VendedorDto>> GetByIdAsync(Guid id);
    Task<ResultService<VendedorDto>> CreateAsync(VendedorDto vendedorDto);
    
    Task<ResultService> UpdateAsync(VendedorDto vendedorDto);
    Task<ResultService> RemoveAsync(Guid id);
}
