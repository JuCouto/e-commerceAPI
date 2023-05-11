using APIVenda.Net.Context;
using APIVenda.Net.Entities;
using APIVenda.Net.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Net.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApiVendaDbContext _dbContext;

    public UsuarioRepository(ApiVendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Usuario> GetByIdAsync(Guid id)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<ICollection<Usuario>> GetAllAsync()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    public async Task<Usuario> GetByCpfAsync(string cpf)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(c => c.Cpf == cpf);
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.EmailUsuario == email);
    }

    public async Task<Guid> GetIdByEmail(string email)
    {
        return (await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.EmailUsuario == email)).Id;
    }
    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        _dbContext.Add(usuario);
        await _dbContext.SaveChangesAsync();
        return usuario;
    }

    public async  Task DeleteAsync(Usuario usuario)
    {
        _dbContext.Remove(usuario);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditAsync(Usuario usuario)
    {
        _dbContext.Update(usuario);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Usuario> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.EmailUsuario == email && usuario.Password == password);
    }
}
