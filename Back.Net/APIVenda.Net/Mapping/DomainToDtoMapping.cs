using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.DTOs;
using APIVenda.Net.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APIVenda.Net.Mapping;

public class DomainToDtoMapping : Profile
{
    public DomainToDtoMapping()
    {
        CreateMap<Produto, ProdutoDto >();
        CreateMap<Produto, ProdutoCadastroDto>();
       
        CreateMap<Vendedor, VendedorDto>();

        CreateMap<Venda, VendaDto>();

        CreateMap<Venda, VendaResponseDto>()
            .ForMember(p => p.Produto, opt => opt.Ignore())
            .ForMember(p => p.Vendedor, opt => opt.Ignore())
            .ConstructUsing((model, context) =>
            {
               var dto = new VendaResponseDto
               {
                    NomeProduto = model.Produto.Nome,
                    Produto = model.Id,
                    CodigoProduto = model.Produto.Codigo,
                    DataVenda = model.DataVenda,
                    Preco = model.Produto.Preco,
                    Vendedor = model.Id,
                    NomeVendedor = model.Vendedor.Nome
                };
                return dto;
            });

        CreateMap<Usuario, UsuarioDto>();
        CreateMap<Usuario, UsuarioResponseDto>();
        CreateMap<Usuario, LoginDto>();
        CreateMap<Usuario, RefreshTokenDto>();
        CreateMap<Usuario, UpdatePasswordDto>();

        CreateMap<Endereco, EnderecoDto>();
        CreateMap<Endereco, EnderecoResponseDto>();
    }
}
