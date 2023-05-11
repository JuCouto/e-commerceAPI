using System.Text.Json.Serialization;

namespace APIVenda.Net.DTOs;

public class EnderecoResponseDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
}
