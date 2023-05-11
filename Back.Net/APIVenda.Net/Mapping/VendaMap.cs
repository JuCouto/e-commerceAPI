using APIVenda.Net.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIVenda.Net.Mapping;

public class VendaMap : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.HasOne(c => c.Produto)
               .WithMany(p => p.Vendas);

        builder.HasOne(c => c.Vendedor)
               .WithMany(p => p.Vendas);
        
        builder.Property(c => c.NumeroPedido)
               .ValueGeneratedOnAdd();


        builder.ToTable("Vendas");
    }

}
