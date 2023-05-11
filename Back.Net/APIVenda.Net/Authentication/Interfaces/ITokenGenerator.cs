using APIVenda.Net.Entities;

namespace APIVenda.Net.Authentication.Interfaces;

public interface ITokenGenerator
{

    dynamic Generator(Usuario usuario);
}
