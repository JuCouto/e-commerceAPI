using FluentValidation;

namespace APIVenda.Net.DTOs.Validations;

public class ProdutoDtoValidator : AbstractValidator<ProdutoDto>
{
    public ProdutoDtoValidator()
    {
       
        RuleFor(X => X.Nome)
          .NotEmpty()
          .NotNull()
          .WithMessage("O nome do produto deve ser informado");

        RuleFor(X => X.Codigo)
         .NotEmpty()
         .NotNull()
         .WithMessage("O código do produto deve ser informado");

        RuleFor(X => X.Preco)
         .NotEmpty()
         .NotNull()
         .WithMessage("O valor do produto deve ser informado");
    }
}
