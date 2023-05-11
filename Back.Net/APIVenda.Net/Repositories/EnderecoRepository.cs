using APIVenda.Net.Context;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Net.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    readonly ApiVendaDbContext _dbContext;

    public EnderecoRepository(ApiVendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Endereco> GetByIdAsync(Guid id)
    {
        return await _dbContext.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);
    }

    public async Task<ICollection<Endereco>> GetAllAsync()
    {
       return await _dbContext.Enderecos.ToListAsync();
    }

    public async Task<Endereco> CreateAsync(Endereco endereco)
    {
        _dbContext.Add(endereco);
        await _dbContext.SaveChangesAsync();
        return endereco;
    }


    public async Task EditAsync(Endereco endereco)
    {
        _dbContext.Update(endereco);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Endereco endereco)
    {
        _dbContext.Remove(endereco);
        await _dbContext.SaveChangesAsync();
    }

       
}
