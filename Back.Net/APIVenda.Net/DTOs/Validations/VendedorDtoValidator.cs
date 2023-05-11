using FluentValidation;

namespace APIVenda.Net.DTOs.Validations;

public class VendedorDtoValidator : AbstractValidator<VendedorDto>
{
    public VendedorDtoValidator()
    {
        RuleFor(X => X.Nome)
          .NotEmpty()
          .NotNull()
          .WithMessage("O nome do vendedor deve ser informado");

        RuleFor(X => X.CPF)
          .NotEmpty()
          .NotNull()
          .IsValidCPF()
          .WithMessage("O CPF deve ser informado");

        RuleFor(X => X.Email)
          .NotEmpty()
          .NotNull()
          .EmailAddress()
          .WithMessage("Email deve ser informado corretamente");

    }
}
