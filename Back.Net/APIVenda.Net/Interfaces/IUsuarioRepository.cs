using APIVenda.Net.Entities;

namespace APIVenda.Net.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> GetByIdAsync(Guid id);
    Task<Usuario> GetByEmailAsync(string email);
    Task<Usuario> GetByCpfAsync(string cpf);
    Task<ICollection<Usuario>> GetAllAsync();
    Task<Usuario> CreateAsync(Usuario usuario);
    Task EditAsync(Usuario usuario);
    Task DeleteAsync(Usuario usuario);
    Task<Guid> GetIdByEmail(string email);
    Task<Usuario> GetUserByEmailAndPasswordAsync(string email, string password);
}
