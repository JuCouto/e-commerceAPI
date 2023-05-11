using APIVenda.Net.Authentication.Entities;
using APIVenda.Net.Entities;
using Microsoft.EntityFrameworkCore;


namespace APIVenda.Net.Context;

public class ApiVendaDbContext : DbContext
{
    public ApiVendaDbContext(DbContextOptions<ApiVendaDbContext> options) : base(options)
    {}
        
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<RefreshToken> Tokens { get; set; }

    
    //Esse método indica que deve aplicar as configurações dessa classe
    protected override void OnModelCreating(ModelBuilder builder)
    {        
        builder.Entity<Endereco>()
               .Property(p => p.Cep).HasMaxLength(9);

        // Relacionamento one-to-one.
        builder.Entity<Usuario>().HasOne(usuario => usuario.Endereco).WithOne(endereco => endereco.Usuario)
            .HasForeignKey<Endereco>(b => b.UsuarioId);

    }

    // Configurar convenção.
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
        
}
