using APIVenda.Net.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIVenda.Net.Mapping;

public class VendedorMap : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.ToTable("vendedor");
        builder.HasKey(x => x.Id);

        builder.HasMany(vendedor => vendedor.Vendas).WithOne(venda => venda.Vendedor).HasForeignKey(c => c.VendedorId);
    }
}
