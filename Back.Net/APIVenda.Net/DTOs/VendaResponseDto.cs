using APIVenda.Net.Enum;
using System.Text.Json.Serialization;

namespace APIVenda.Net.DTOs;

public class VendaResponseDto
{
    public Guid Id { get; set; }
    public int NumeroPedido { get; set; }

    public DateTime DataVenda { get; set; }

    public Guid Produto { get; set; }
    public string NomeProduto { get; set; }
    public string? CodigoProduto { get; set; }
    public decimal Preco { get; set; }

    [JsonIgnore]
    public Guid Vendedor { get; set; }

    public string NomeVendedor { get; set; }

    public StatusVenda StatusVenda { get; set; }
}
