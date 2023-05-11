using APIVenda.Net.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIVenda.Net.Mapping;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        // Mapeando Chave estrangeira(tipo de relacionamento).
        builder.ToTable("produto");
        builder.HasKey(x => x.Id);

        builder.HasMany(produto => produto.Vendas).WithOne(venda => venda.Produto).HasForeignKey(venda => venda.Produto);
    }

}
