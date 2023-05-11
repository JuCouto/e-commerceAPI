using System.Text.Json.Serialization;

namespace APIVenda.Net.DTOs;

public class ProdutoCadastroDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public decimal Preco { get; set; }
}
