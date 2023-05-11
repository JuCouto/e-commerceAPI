using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APIVenda.Net.Context;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Mapping;
using APIVenda.Net.Repositories;
using APIVenda.Net.Services;
using Microsoft.EntityFrameworkCore;
using APIVenda.Net.Authentication.Interfaces;
using APIVenda.Net.Authentication.Services;
using APIVenda.Net.Authentication.Repositories;

namespace APIVenda.Net;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Injetando o banco.
        services.AddDbContext<ApiVendaDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IVendedorRepository, VendedorRepository>();
        services.AddScoped<IVendaRepository, VendaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<ITokenRepository, TokenRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Injetando AutoMapper.
        services.AddAutoMapper(typeof(DomainToDtoMapping));
       
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IVendaService, VendaService>();
        services.AddScoped<IVendedorService, VendedorService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IEnderecoService, EnderecoService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}