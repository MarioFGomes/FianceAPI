using Finance.Communication.Request;
using FluentValidation;

namespace Finance.Application.Validators; 
public class WalletValidator : AbstractValidator<WalletRequest> {
    private readonly string[] AcceptedCurrencies = new[] { "USD", "EUR", "AOA", "BRL" };
    public WalletValidator()
    {
        RuleFor(c=>c.Balance).GreaterThanOrEqualTo(0).WithMessage("O Saldo inicial não pode ser menor que 0.");
        
        RuleFor(c => c.currency)
           .NotEmpty().WithMessage("A moeda é obrigatória.")
           .Must(moeda => AcceptedCurrencies.Contains(moeda))
           .WithMessage("Moeda inválida. As moedas aceitas são: USD, EUR, AOA, BRL.");
    }
}
