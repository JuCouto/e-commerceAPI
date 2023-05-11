namespace APIVenda.Net.DTOs;

public class ProdutoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public decimal Preco { get; set; }
}
