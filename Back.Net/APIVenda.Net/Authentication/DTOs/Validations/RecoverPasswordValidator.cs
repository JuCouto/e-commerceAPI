using FluentValidation;

namespace APIVenda.Net.Authentication.DTOs.Validations;

public class RecoverPasswordValidator : AbstractValidator<RecoverPasswordDto>
{
    public RecoverPasswordValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty()
           .WithMessage("Email deve ser informado!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("A sua Senha deve ser informada!");

        RuleFor(x => x.Codigo)
            .NotEmpty()
            .WithMessage("Código deve ser informado!");
    }
}
