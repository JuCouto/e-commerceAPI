using APIVenda.Net.Authentication.DTOs;
using APIVenda.Net.DTOs;
using APIVenda.Net.Entities;
using AutoMapper;

namespace APIVenda.Net.Mapping;

public class DtoToDomainMapping : Profile
{
    public DtoToDomainMapping ()
    {
        CreateMap<ProdutoDto, Produto>();
        CreateMap<ProdutoDto, ProdutoCadastroDto>();
        CreateMap<ProdutoCadastroDto, Produto>();
        CreateMap<ProdutoCadastroDto, ProdutoDto>();

        CreateMap<VendaDto, Venda>();
        

        CreateMap<VendedorDto, Vendedor>();
        
        CreateMap<UsuarioDto, Usuario>();
        CreateMap<UsuarioResponseDto, Usuario>();
        CreateMap<UsuarioResponseDto, UsuarioDto>();
        CreateMap<UsuarioResponseDto, UsuarioUpdateDto>();
        
        CreateMap<UsuarioDto, LoginDto>();

        CreateMap<LoginDto, UsuarioDto>();

        CreateMap<EnderecoDto, Endereco>();
        CreateMap<EnderecoDto, EnderecoResponseDto>();
        CreateMap<EnderecoResponseDto, Endereco>();
        CreateMap<EnderecoResponseDto, EnderecoDto>();
    }

}
