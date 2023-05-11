using APIVenda.Net.Enum;
using System.Text.Json.Serialization;

namespace APIVenda.Net.DTOs;

public class VendaDto
{
    [JsonIgnore]
    public Guid Id { get;  set; }

    [JsonIgnore]
    public DateTime DataVenda { get;  set; }

    [JsonIgnore]
    public int NumeroPedido { get;  set; }

    public Guid ProdutoId { get; set; }

    public Guid VendedorId { get; set; }

    [JsonIgnore]
    public string? CodigoProduto { get; set; }

    [JsonIgnore]
    public decimal Preco { get; set; }

    public StatusVenda StatusVenda { get; set; }
}
