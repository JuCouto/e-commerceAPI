using APIVenda.Net.Authentication.DTOs.Validations;
using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.DTOs;
using APIVenda.Net.DTOs.Validations;
using APIVenda.Net.Entities;
using APIVenda.Net.Enum;
using APIVenda.Net.Interfaces;
using AutoMapper;

namespace APIVenda.Net.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ResultService<ICollection<UsuarioResponseDto>>> GetAllAsync()
    {
       var dados = await _usuarioRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<UsuarioResponseDto>>(dados));
    }

    public async Task<ResultService<UsuarioResponseDto>> GetByIdAsync(Guid id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return ResultService.Fail<UsuarioResponseDto>($"Usuário com id {id} não foi encontrado");

        return ResultService.Ok(_mapper.Map<UsuarioResponseDto>(usuario));
    }

    public async Task<ResultService<ICollection<UsuarioResponseDto>>> GetAllActiveAsync()
    {
        var listaUsuario = await _usuarioRepository.GetAllAsync();
        var novaLista = new List<Usuario>();

        foreach(var usuario in listaUsuario)
            if (usuario.Ativo) novaLista.Add(usuario);

        return ResultService.Ok(_mapper.Map<ICollection<UsuarioResponseDto>>(novaLista));
    }
        

    public async Task<ResultService<Usuario>> GetByEmailAsync(string email)
    {
        var emailUsuario = await _usuarioRepository.GetByEmailAsync(email);

        if (emailUsuario == null)
            return ResultService.Fail<Usuario>($"Não foi localizado usuário com o email {email}.");

        return ResultService.Ok(_mapper.Map<Usuario>(emailUsuario));
    }

    public async Task<ResultService<UsuarioResponseDto>> CreateAsync(UsuarioDto usuarioDto)
    {
        //// gerando senha aleatoria
        var newPassword = new string(Enumerable.Repeat("abcdefghijk0123456789@#lmnopqrstuvwxyz", 7)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());

        // encriptando a senha
        usuarioDto.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

        if (usuarioDto == null)
            return ResultService.Fail<UsuarioResponseDto>("Objeto deve ser informado");

        var resultado = new UsuarioDtoValidator().Validate(usuarioDto);
        if (!resultado.IsValid)
            return ResultService.RequestError<UsuarioResponseDto>("Problemas de validade", resultado);

        var verificaEmail = await _usuarioRepository.GetByEmailAsync(usuarioDto.EmailUsuario);        
        var verificaCpf = await _usuarioRepository.GetByCpfAsync(usuarioDto.Cpf);

        if (verificaEmail != null || verificaCpf != null)
            return ResultService.Fail<UsuarioResponseDto>("Usuário já existe");

        if (usuarioDto.DataNascimento.ToString() == "01/01/0001")
            return ResultService.Fail<UsuarioResponseDto>("Data de nascimento inválida");

        var usuario = _mapper.Map<Usuario>(usuarioDto); // criação
        var data = await _usuarioRepository.CreateAsync(usuario);

        usuarioDto.Password = newPassword;
       // BuildAndSendEmail(userDto);

        return ResultService.Ok(_mapper.Map<UsuarioResponseDto>(data));
    }

    public async Task<ResultService> UpdateAsync(UsuarioResponseDto usuarioResponseDto)
    {
        if (usuarioResponseDto == null)
            return ResultService.Fail("Objeto deve ser informado!");

        var usuario = await _usuarioRepository.GetByIdAsync(usuarioResponseDto.Id);
        if (usuario == null)
            return ResultService.Fail($"Usuário com o id {usuarioResponseDto.Id} não foi encontrado!");

        var resultado = new UsuarioDtoValidator().Validate(_mapper.Map<UsuarioDto>(usuarioResponseDto));
        if (!resultado.IsValid)
            return ResultService.RequestError<UsuarioDto>("Problemas de validade", resultado);

        if (!string.Equals(usuarioResponseDto.EmailUsuario, usuario.EmailUsuario))
        {
            var verificaEmail = await _usuarioRepository.GetByEmailAsync(usuarioResponseDto.EmailUsuario);
            if (verificaEmail != null)
                return ResultService.Fail<UsuarioResponseDto>("Já existe um usuário com este e-mail pessoal");
        }

        if (!string.Equals(usuarioResponseDto.Cpf, usuario.Cpf))
        {
            var verificaCpf = await _usuarioRepository.GetByCpfAsync(usuarioResponseDto.Cpf);
            if (verificaCpf != null)
                return ResultService.Fail<UsuarioResponseDto>("Já existe um usuário com este cpf");
        }

        usuario = _mapper.Map(usuarioResponseDto, usuario); // Edicão
        await _usuarioRepository.EditAsync(usuario);
        return ResultService.Ok($"Usuário com o id {usuario.Id} foi editado com sucesso!");
    }

    public async Task<ResultService> EditRoleAsync(Guid id, int role)
    {
        var user = await _usuarioRepository.GetByIdAsync(id);
        if (user == null)
            return ResultService.Fail($"Usuário com o id {id} não foi encontrado!");

        switch (role)
        {
            case 1:
                user.setRole(Role.USUARIO);
                break;
            case 2:
                user.setRole(Role.VENDEDOR);
                break;
            case 3:
                user.setRole(Role.ADMINISTRADOR);
                break;

            default:
                return ResultService.Fail(" Role inválida ou nula. Passe:" +
                                          " '1' para 'Usuario'," +
                                          " '2' para 'Vendedor'," +
                                          " '3' para 'Vendedor',"
                                          );
        }

        await _usuarioRepository.EditAsync(user);
        return ResultService.Ok($"Usuário com o id {id} foi editado com sucesso!");
    }

    public async Task<ResultService> DeactivateUserAsync(Guid id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return ResultService.Fail<UsuarioDto>("Usuário não encontrado");

        usuario.setAtivo(false);
        await _usuarioRepository.EditAsync(usuario);
        return ResultService.Ok($"Usuário {usuario.Id} foi desativado!");
       
    }
                  
    public async Task<ResultService> ReactivateUserAsync(Guid id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return ResultService.Fail<UsuarioDto>("Usuário não encontrado");

        usuario.setAtivo(true);
        await _usuarioRepository.EditAsync(usuario);
        return ResultService.Ok($"Usuário com o id {usuario.Id} foi reativado!");
    }

    public async Task<ResultService> RemoveAsync(Guid id)
    {
        var pessoa = await _usuarioRepository.GetByIdAsync(id);
        if (pessoa == null)
            return ResultService.Fail<UsuarioDto>("Usuário não encontrado");

        await _usuarioRepository.DeleteAsync(pessoa);
        return ResultService.Ok($"Usuário com o id {id} foi deletado com sucesso!");
    }

    public async Task<ResultService> UpdatePasswordAsync(UpdatePasswordDto UpdatePasswordDto)
    {
        if (UpdatePasswordDto == null)
            return ResultService.Fail("Objeto deve ser informado!");

        var usuario = await _usuarioRepository.GetByEmailAsync(UpdatePasswordDto.Email);
        if (usuario == null)
            return ResultService.Fail($"Usuário com o email {UpdatePasswordDto.Email} não foi encontrado!");

        var validPass = BCrypt.Net.BCrypt.Verify(UpdatePasswordDto.AtualPassword, usuario.Password);

        if (!validPass)
            return ResultService.Fail("Senha incorreta");

        usuario.setPassword(BCrypt.Net.BCrypt.HashPassword(UpdatePasswordDto.NovaPassword));
        await _usuarioRepository.EditAsync(usuario);
        return ResultService.Ok($"Usuário do id {usuario.Id} foi editado com sucesso!");
    }

    public async Task<ResultService> RecoveryPassword(RecoverPasswordDto recoverPasswordDto)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(recoverPasswordDto.Email);
        if (usuario == null)
            return ResultService.Fail("Usuário não foi encontrado!");

        var result = new RecoverPasswordValidator().Validate(recoverPasswordDto);
        if (!result.IsValid)
            return ResultService.RequestError<UsuarioResponseDto>("Problemas de validade", result);

        if (!recoverPasswordDto.Codigo.Equals(usuario.CodigoToResetPassword))
            return ResultService.Fail("Código inválido!");

        var hasPassword = BCrypt.Net.BCrypt.HashPassword(recoverPasswordDto.Password);
        usuario.setPassword(hasPassword);
        usuario.setCodeToResetPassword(null);

        await _usuarioRepository.EditAsync(usuario);
        return ResultService.Ok("Senha alterada com sucesso!");
    }
}
