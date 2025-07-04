using Finance.Communication.Request;
using FluentValidation;

namespace Finance.Application.Validators; 
public class WalletMovimentValidator: AbstractValidator<WalletMovimentRequest> {
    private readonly string[] AcceptedCurrencies = new[] { "USD", "EUR", "AOA", "BRL" };
    public WalletMovimentValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("O valor da movimentação deve ser maior que zero.");

        RuleFor(c => c.currency)
           .NotEmpty().WithMessage("A moeda é obrigatória.")
           .Must(moeda => AcceptedCurrencies.Contains(moeda))
           .WithMessage("Moeda inválida. As moedas aceitas são: USD, EUR, AOA, BRL.");

        RuleFor(x => x.Description)
              .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.");
    }
}
