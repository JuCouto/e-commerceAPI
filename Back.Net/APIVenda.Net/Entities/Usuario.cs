using APIVenda.Net.Enum;
using APIVenda.Net.Validations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Numerics;

namespace APIVenda.Net.Entities;

[Table("usuario")]
public class Usuario : IdentityUser
{
    [Key] 
    public Guid Id { get; private set; }
    

    [StringLength(80)]
    [Required(ErrorMessage = "Nome completo é obrigatório", AllowEmptyStrings = false)]
    [Column("nome_completo")]
    public string NomeCompleto { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
    [Column("email_usuario")]
    public string EmailUsuario { get; private set; }

    [StringLength(15)]
    [Required(ErrorMessage = "CPF é obrigatório", AllowEmptyStrings = false)]
    [Column("cpf")]
    public string Cpf { get; private set; }

    [StringLength(14)]
    [Required(ErrorMessage = "Telefone é obrigatório", AllowEmptyStrings = false)]
    [Column("telefone")]
    public string TelefoneUsuario { get; private set; }

    [Required(ErrorMessage = "Data de nascimento é obrigatória", AllowEmptyStrings = false)]
    [Column("data_nascimento")]
    public DateTime DataNascimento { get; private set; }

    [Required(ErrorMessage = "Role é obrigatório", AllowEmptyStrings = false)]
    [Column("role")]
    public Role Role { get; private set; }

    public bool Ativo { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Senha é obrigatório", AllowEmptyStrings = false)]
    [Column("password")]
    public string Password { get; private set; }

    [StringLength(100)]
    [Column("codigo")]
    public string? CodigoToResetPassword { get; private set; }

   // public Guid EnderecoId { get;  set; }

    public Endereco Endereco { get; set; }

    public Usuario(Guid id, string nomeCompleto, string emailUsuario,string cpf, string telefoneUsuario,
        DateTime dataNascimento, Role role, string password)
    {
        DomainValidationException.When( string.IsNullOrEmpty(id.ToString()), "Id deve ser informado!");
        Id = id;
        Validation(nomeCompleto, emailUsuario, cpf, telefoneUsuario, dataNascimento, role, password);
    }
    public Usuario(string emailUsuario, string password)
    {
        EmailUsuario = emailUsuario;
        Password =  password;
    }

    public Usuario(string nomeCompleto, string emailUsuario, string cpf, string telefoneUsuario,
       DateTime dataNascimento, Role role, string password)
    {
        Validation(nomeCompleto, emailUsuario, cpf, telefoneUsuario, dataNascimento, role, password);
    }
    

    private void Validation( string nomeCompleto, string emailUsuario, string cpf, string telefoneUsuario,
        DateTime dataNascimento, Role role, string password)
    {
        DomainValidationException.When(string.IsNullOrEmpty(nomeCompleto), "Nome completo deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(emailUsuario), "E-mail pessoal deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(cpf), "O CPF deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(telefoneUsuario), "Telefone deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(role.ToString()), "Role deve ser informada!");
        
        NomeCompleto = nomeCompleto;
        EmailUsuario = emailUsuario;
        Cpf = cpf;
        TelefoneUsuario = telefoneUsuario;
        DataNascimento = dataNascimento;
        Role = role;
        Password = password;
       
    }

    public void setPassword(string password)
    {
        Password = password;
    }
    public void setRole(Role role)
    {
        Role = role;
    }

    public void setAtivo(bool value)
    {
        Ativo = value;
    }
    public void setCodeToResetPassword(string codigo)
    {
        CodigoToResetPassword = codigo;
    }
}
