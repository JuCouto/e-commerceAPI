using APIVenda.Net.Enum;
using FluentValidation;

namespace APIVenda.Net.DTOs.Validations;

public class UsuarioDtoValidator : AbstractValidator<UsuarioDto>
{
    public UsuarioDtoValidator()
    {
        //RuleFor(X => X.NomeCompleto)
        //      .NotEmpty()
        //      .NotNull()
        //      .WithMessage("O nome do vendedor deve ser informado");

        //RuleFor(X => X.Cpf)
        //      .NotEmpty()
        //      .NotNull()
        //      .IsValidCPF()
        //      .WithMessage("O CPF deve ser informado");

        RuleFor(X => X.EmailUsuario)
              .NotEmpty()
              .NotNull()
              .EmailAddress()
              .WithMessage("Email deve ser informado corretamente");

        //RuleFor(x => x.Password)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Password deve ser informado!");

        //RuleFor(x => x)
        //    .Must(p =>
        //        p.Role == Role.USUARIO ||
        //        p.Role == Role.VENDEDOR
        //        )
        //    .WithMessage("Role inválida");
    }
}
