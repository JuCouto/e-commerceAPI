using APIVenda.Net.Enum;
using System.Text.Json.Serialization;

namespace APIVenda.Net.DTOs;

public class UsuarioDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string NomeCompleto { get;  set; }
    public string EmailUsuario { get;  set; }
    public string Cpf { get;  set; }
    public string TelefoneUsuario { get;  set; }
    public DateTime DataNascimento { get; set; }
    public Role Role { get; set; }
    public bool Ativo { get; set; }
   
    public string Password { get;  set; }
}
