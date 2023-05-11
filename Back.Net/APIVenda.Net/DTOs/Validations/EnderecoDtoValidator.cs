using FluentValidation;

namespace APIVenda.Net.DTOs.Validations;

public class EnderecoDtoValidator : AbstractValidator<EnderecoDto>
{
    public EnderecoDtoValidator()
    {
        RuleFor(X => X.CEP)
          .NotEmpty()
          .NotNull()
          .WithMessage("O nome do produto deve ser informado");

       
    }

}
