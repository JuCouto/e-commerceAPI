using APIVenda.Net.Context;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Net.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApiVendaDbContext _dbContext;

    public ProdutoRepository(ApiVendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Produto> GetByIdAsync(Guid id)
    {
        return await _dbContext.Produtos.SingleOrDefaultAsync(p => p.Id == id);
    }
    public async Task<ICollection<Produto>> GetAllAsync()
    {
        return await _dbContext.Produtos.ToListAsync();
    }

    public async Task<Produto> CreateAsync(Produto produto)
    {
        _dbContext.Add(produto);
        await _dbContext.SaveChangesAsync();
        return produto;
    }
        
    public async Task EditAsync(Produto produto)
    {
        _dbContext.Update(produto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Produto produto)
    {
        _dbContext.Remove(produto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Guid> GetIdByCodProdAsync(string codigo)
    {
        // Se achar o codProduto retorna o id, senão retorna nulo.
        return (Guid)((await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Codigo == codigo))?.Id  ?? null);
    }
}
