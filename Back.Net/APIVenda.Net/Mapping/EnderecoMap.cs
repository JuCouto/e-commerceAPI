using APIVenda.Net.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIVenda.Net.Mapping;

public class EnderecoMap : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("endereco");
        builder.HasKey(x => x.Id);
                      
    }
}
