using APIVenda.Net.Enum;
using System.Text.Json.Serialization;

namespace APIVenda.Net.DTOs;

public class UsuarioUpdateDto
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; }
    public string EmailUsuario { get; set; }
    public string Cpf { get; set; }
    public string TelefoneUsuario { get; set; }
    public DateTime DataNascimento { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}
