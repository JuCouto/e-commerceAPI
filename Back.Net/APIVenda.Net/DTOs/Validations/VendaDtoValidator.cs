using FluentValidation;

namespace APIVenda.Net.DTOs.Validations;

public class VendaDtoValidator : AbstractValidator<VendaDto>
{
    public  VendaDtoValidator()
    {
        RuleFor(X => X.ProdutoId)
         .NotEmpty()
         .NotNull()
         .WithMessage("O Id produto deve ser informado");

        //RuleFor(X => X.StatusVenda)
        // .NotEmpty()
        // .NotNull()
        // .WithMessage("O status da venda deve ser informado");
    }
}
