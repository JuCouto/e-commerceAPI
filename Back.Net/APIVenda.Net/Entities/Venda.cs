using APIVenda.Net.Enum;
using APIVenda.Net.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace APIVenda.Net.Entities;

[Table("tb_venda")]
public class Venda
{
    [Key]
    public Guid Id { get; private set; }

    [Required(ErrorMessage = "Data da venda é obrigatória", AllowEmptyStrings = false)]
    [Column("data_venda")]
    public DateTime DataVenda { get; private set; }

    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("numero_pedido")]
    public int NumeroPedido { get; private set; }

    public Guid VendedorId { get;  set; }
    public virtual  Vendedor Vendedor { get;  set; }

     public Guid ProdutoId { get;  set; }
    public virtual Produto Produto { get; set; }

    [Required(ErrorMessage = "Status da venda é obrigatório", AllowEmptyStrings = false)]
    [Column("statusVenda")]
    public StatusVenda  StatusVenda { get; private set; }

    public Venda(Guid id, DateTime dataVenda, int numeroPedido, Guid vendedorId, Vendedor vendedor, Guid produtoId, Produto produto, StatusVenda statusVenda)
    {
        Id = id;
        DataVenda = dataVenda;
        NumeroPedido = numeroPedido;
        VendedorId = vendedorId;
        Vendedor = vendedor;
        ProdutoId = produtoId;
        Produto = produto;
        StatusVenda = statusVenda;
    }

    public Venda(DateTime dataVenda, Vendedor vendedor, Produto produto, StatusVenda statusVenda)
    {
        DataVenda = dataVenda;
        Vendedor = vendedor;
        Produto = produto;
        StatusVenda = statusVenda;
    }

    public Venda(DateTime dataVenda, Vendedor vendedor, Produto produto)
    {
        DataVenda = dataVenda;
        Vendedor = vendedor;
        Produto = produto;
        
    }


    public Venda(Guid id, Guid produtoId, Guid vendedorId, StatusVenda statusVenda)
    {
        DomainValidationException.When(string.IsNullOrEmpty(id.ToString()), "Id deve ser informado!");
        Id = id;
        Validation(produtoId, vendedorId, statusVenda);
    }

    public Venda(Guid produtoId,  Guid vendedorId, StatusVenda statusVenda)
    {
        Validation(produtoId, vendedorId, statusVenda);
    }

    private void Validation(Guid produtoId, Guid vendedorId, StatusVenda statusVenda)
    {
        DomainValidationException.When(produtoId == null, " O Número do produtodo deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(vendedorId.ToString()), "O nome do vendedor deve ser informado!");

        ProdutoId = produtoId;
        VendedorId= vendedorId;
        StatusVenda= statusVenda;
        DataVenda = DateTime.Now;
    }

    public void SetStatusVenda(StatusVenda statusVenda)
    {
        StatusVenda = statusVenda;
    }
}
