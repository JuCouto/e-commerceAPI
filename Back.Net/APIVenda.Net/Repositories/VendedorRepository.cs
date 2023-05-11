using APIVenda.Net.Context;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Net.Repositories;

public class VendedorRepository : IVendedorRepository
{
    private readonly ApiVendaDbContext _dbContext;

    public VendedorRepository(ApiVendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Vendedor> GetByIdAsync(Guid id)
    {
        return await _dbContext.Vendedores.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async  Task<ICollection<Vendedor>> GetAllAsync()
    {
        return await _dbContext.Vendedores.ToListAsync();
    }

    public async Task<Vendedor> CreateAsync(Vendedor vendedor)
    {
        _dbContext.Add(vendedor);
        await _dbContext.SaveChangesAsync();
        return vendedor;
    }

    
    public async Task EditAsync(Vendedor vendedor)
    {
        _dbContext.Update(vendedor);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Vendedor vendedor)
    {
        _dbContext.Remove(vendedor);
        await _dbContext.SaveChangesAsync();
    }

}
