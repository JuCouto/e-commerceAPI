using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APIVenda.Net.Enum;
using APIVenda.Net.Validations;
using APIVenda.Net.DTOs;
using APIVenda.Net.Migrations;

namespace APIVenda.Net.Entities;

[Table("tb_endereco")]
public class Endereco
{
    [Key] 
    public Guid Id { get; private set; }

    [StringLength(80)]
    [Required(ErrorMessage = "Logradouro é obrigatório", AllowEmptyStrings = false)]
    [Column("logradouro")]
    public string Logradouro { get; private set; }

    [StringLength(80)]
    [Required(ErrorMessage = "Bairro é obrigatório", AllowEmptyStrings = false)]
    [Column("bairro")]
    public string Bairro { get; private set; }

    [StringLength(15)]
    [Required(ErrorMessage = "Numero é obrigatório", AllowEmptyStrings = false)]
    [Column("numero")]
    public string Numero { get; private set; }

    [StringLength(80)]
    [Column("complemento")]
    public string? Complemento { get; private set; }

    [StringLength(40)]
    [Required(ErrorMessage = "Cidade é obrigatório", AllowEmptyStrings = false)]
    [Column("cidade")]
    public string Cidade { get; private set; }

    [StringLength(20)]
    [Required(ErrorMessage = "UF é obrigatório", AllowEmptyStrings = false)]
    [Column("uf")]
    public string Uf { get; private set; }

    [StringLength(20)]
    [Required(ErrorMessage = "CEP é obrigatório", AllowEmptyStrings = false)]
    [Column("cep")]
    public string Cep { get; private set; }

    [ForeignKey("usuario")]
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get;  set; }

    public Endereco(Guid id,string logradouro, string bairro, string numero, string? complemento, string cidade,
        string uf, string cep)
    {
        DomainValidationException.When(string.IsNullOrEmpty(id.ToString()), "Id deve ser informado!");
        Id = id;
        Validation(logradouro, bairro, numero, complemento, cidade, uf, cep);
    }
    public Endereco( string logradouro, string bairro,string numero, string? complemento, string cidade,
        string uf, string cep)
    {
        Validation(logradouro, bairro, numero, complemento, cidade, uf, cep);
    }

    public Endereco(string logradouro, string? bairro, string? numero, string? complemento, string? cidade, string uf, string? cEP, Usuario usuario)
    {
        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Cidade = cidade;
        Uf = uf;
        Cep = cEP;
        Usuario = usuario;
    }

    private void Validation( string logradouro, string bairro,
       string numero, string? complemento, string cidade, string uf, string cep)
    {
        DomainValidationException.When(string.IsNullOrEmpty(logradouro), "Logradouro deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(bairro), "Bairro deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(numero), "Informar s/n para casa sem número!");
        DomainValidationException.When(string.IsNullOrEmpty(cidade), "Cidade deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(uf), "UF deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(cep), "CEP deve ser informado!");

        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Cidade = cidade;
        Uf = uf;
        Cep = cep;
        
    }
}
