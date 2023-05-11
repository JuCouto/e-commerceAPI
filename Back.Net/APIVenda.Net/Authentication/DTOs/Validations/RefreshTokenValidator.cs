using APIVenda.Net.Authentication.Entities;
using FluentValidation;

namespace APIVenda.Net.Authentication.DTOs.Validations;

public class RefreshTokenValidator : AbstractValidator<RefreshToken>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("CorporativeEmail Corporativo deve ser informado!");

        RuleFor(x => x.TokenRefresh)
            .NotEmpty()
            .WithMessage("RefreshToken deve ser informado!");
    }
}
