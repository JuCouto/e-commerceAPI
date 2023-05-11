using APIVenda.Net.Enum;
using APIVenda.Net.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIVenda.Net.Entities;

[Table("tb_vendedor")]
public class Vendedor
{
    [Key]
    public Guid Id { get; private set; }

    [StringLength(11)]
    [Required(ErrorMessage = "CPF é obrigatório", AllowEmptyStrings = false)]
    [Column("cpf")]
    public string CPF { get; private set; }

    [StringLength(50)]
    [Required(ErrorMessage = "Nome do vendedor é obrigatório", AllowEmptyStrings = false)]
    [Column("nome_vendedor")]
    public string Nome { get; private set; }

    [StringLength(50)]
    [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
    [Column("email")]
    public string Email { get; private set; }

    [StringLength(50)]
    [Required(ErrorMessage = "Telefone é obrigatório", AllowEmptyStrings = false)]
    [Column("telefone")]
    public string Telefone { get; private set; }

    [Required(ErrorMessage = "Role é obrigatório", AllowEmptyStrings = false)]
    [Column("role")]
    public Role Role { get; private set; }

    public bool Active { get; private set; }

    public ICollection<Venda> Vendas { get; set; }


    public Vendedor(Guid id, string cPF, string nome, string email, string telefone)
    {
        DomainValidationException.When(string.IsNullOrEmpty(id.ToString()), "Id deve ser informado!");
        Id = id;
        Validation(cPF, nome, email, telefone);
        Vendas = new List<Venda>();
    }

    private void Validation(string cPF, string nome, string email, string telefone)
    {
        DomainValidationException.When(string.IsNullOrEmpty(cPF), " CPF deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(nome), " Nome deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(email), " Email deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(telefone), " Telefone deve ser informado!");

        CPF = cPF;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}
