using Finance.Communication.Request;
using FluentValidation;

namespace Finance.Application.Validators; 
public class FetchWalletMovValidator:AbstractValidator<WalletMovQueryRequest> {
    public FetchWalletMovValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("A data inicial é obrigatória.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("A data final é obrigatória.")
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("A data final deve ser maior ou igual à data inicial.");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("A página deve ser maior ou igual a 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("O tamanho da página deve estar entre 1 e 100.");
    }
}
