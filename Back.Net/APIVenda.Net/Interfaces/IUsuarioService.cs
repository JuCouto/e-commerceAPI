using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.DTOs;
using APIVenda.Net.Entities;
using APIVenda.Net.Services;

namespace APIVenda.Net.Interfaces;

public interface IUsuarioService
{
    Task<ResultService<UsuarioResponseDto>> CreateAsync(UsuarioDto usuarioDto);
    Task<ResultService<ICollection<UsuarioResponseDto>>> GetAllAsync();
    Task<ResultService<ICollection<UsuarioResponseDto>>> GetAllActiveAsync();
    Task<ResultService<UsuarioResponseDto>> GetByIdAsync(Guid id);
    Task<ResultService<Usuario>> GetByEmailAsync(string email);

    Task<ResultService> UpdateAsync(UsuarioResponseDto usuarioResponseDto);

    Task<ResultService> RemoveAsync(Guid id);
    Task<ResultService> DeactivateUserAsync(Guid id);
    Task<ResultService> ReactivateUserAsync(Guid id);
    Task<ResultService> EditRoleAsync(Guid id, int role);
    // Task<ResultService<User>> Authenticate(LoginDto loginDto);

    Task<ResultService> UpdatePasswordAsync(UpdatePasswordDto UpdatePasswordDto);
    Task<ResultService> RecoveryPassword(RecoverPasswordDto recoverPasswordDto);
}
