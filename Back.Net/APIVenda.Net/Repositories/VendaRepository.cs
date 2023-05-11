using APIVenda.Net.Context;
using APIVenda.Net.Entities;
using APIVenda.Net.Enum;
using APIVenda.Net.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Net.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly ApiVendaDbContext _dbContext;

        public VendaRepository(ApiVendaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venda> GetByIdAsync(Guid id)
        {
            return await _dbContext.Vendas
                 .Include(x => x.Produto)
                            .Include(x => x.Vendedor)
                            .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<ICollection<Venda>> GetAllAsync()
        {
            return await _dbContext.Vendas
                .Include(x => x.Produto)
                            .Include(x => x.Vendedor)
                            .ToListAsync();
        }

        public async Task<Venda> CreateAsync(Venda venda)
        {
            _dbContext.Add(venda);
            await _dbContext.SaveChangesAsync();
            return venda;
        }

        public async Task DeleteAsync(Venda venda)
        {
            _dbContext.Remove(venda);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Venda venda)
        {
            _dbContext.Update(venda);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Venda>> GetByVendedorIdAsync(Guid vendedorId)
        {
            return await _dbContext.Vendas
                            .Include(x => x.Produto)
                            .Include(x => x.Vendedor)
                            .Where(x => x.VendedorId == vendedorId).ToListAsync();
        }

        public async Task<ICollection<Venda>> GetByProdutoIdAsync(Guid produtoId)
        {
            return await _dbContext.Vendas
                            .Include(x => x.Produto)
                            .Include(x => x.Vendedor)
                            .Where(x => x.ProdutoId == produtoId).ToListAsync();
        }

        public async Task<Venda> ObterStatus(StatusVenda status)
        {
            return await _dbContext.Vendas
                .Include(ve => ve.StatusVenda == status)
                .FirstOrDefaultAsync(v => v.StatusVenda.Equals(status));
        }
    }
}
