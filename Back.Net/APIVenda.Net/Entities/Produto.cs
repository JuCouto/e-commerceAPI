using APIVenda.Net.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace APIVenda.Net.Entities;

[Table("tb_produto")]
public class Produto
{
    [Key]
    [Column("idproduto")]
    public Guid Id { get; set; }

    [StringLength(80)]
    [Required(ErrorMessage = "Nome do produto é obrigatório", AllowEmptyStrings = false)]
    [Column("nome")]
    public string? Nome { get; set; }

    
    /// <summary>
    /// Código do produto
    /// </summary>
    /// /// <example>B22</example>
    [StringLength(50)]
    [Required(ErrorMessage = "Código deve ser informado", AllowEmptyStrings = false)]
    [Column("codigo")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "O preço deve ser informado", AllowEmptyStrings = false)]
    [Column("preco")]
    public decimal Preco { get; set; }
     
    public ICollection<Venda> Vendas { get; set; }

    public Produto()
    {
    }

    public Produto(Guid id, string nome, string codigo, decimal preco)
    {
        DomainValidationException.When(string.IsNullOrEmpty(id.ToString()), "Id deve ser informado!");
        Id = id;
        Validation(nome, codigo, preco);
        Vendas =  new List<Venda>();
    }


    public Produto(string nome, string codigo, decimal preco)
    {
        Validation(nome, codigo, preco);
        Nome = nome;
        Codigo = codigo;
        Preco = preco;
        Vendas = new List<Venda>();
    }

    private void Validation(string nome, string codigo, decimal preco)
    {
        DomainValidationException.When(string.IsNullOrEmpty(nome), "Nome do produto deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(codigo), "Código do produto deve ser informado!");
        DomainValidationException.When(preco < 0, " O preço deve ser informado!");

        Nome = nome;
        Codigo = codigo;
        Preco = preco;
    }
}
