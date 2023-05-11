using APIVenda.Net.DTOs;
using APIVenda.Net.Enum;
using APIVenda.Net.Services;

namespace APIVenda.Net.Interfaces;

public interface IVendaService
{
    Task<ResultService<ICollection<VendaResponseDto>>> GetAllAsync();
    Task<ResultService<VendaDto>> GetByIdAsync(Guid id);
    Task<ResultService<VendaDto>> CreateAsync(VendaDto vendaDto);
    Task<ResultService<VendaDto>> ObterStatus(StatusVenda statusVenda);
    Task<ResultService> UpdateAsync(VendaDto vendaDto);
    Task<ResultService> UpdateStatusVendaAsync(Guid id, StatusVenda status);
    Task<ResultService> RemoveAsync(Guid id);
}
