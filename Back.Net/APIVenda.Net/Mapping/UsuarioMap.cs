using APIVenda.Net.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIVenda.Net.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Email)
                .HasColumnName("Email");

            builder.HasOne(usuario => usuario.Endereco).WithOne(endereco => endereco.Usuario);
        }
    }
}
