using Finance.Communication.Request;
using Finance.Domain.Entities;
using Finance.Exception;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Validators; 
public class TransactionValidator: AbstractValidator<TransactionRequest> 
{ private readonly  string[] AcceptedCurrencies = new[] { "USD", "EUR", "AOA", "BRL" }; 
    public TransactionValidator()
    {
        RuleFor(c => c.ReceiverWalletId).NotEmpty().WithMessage(ResourceMessage.NameEmpty);
        RuleFor(c => c.Amount).GreaterThan(0).NotEmpty().WithMessage(ResourceMessage.NameEmpty);
        RuleFor(c => c.currency)
       .NotEmpty().WithMessage("A moeda é obrigatória.")
       .Must(moeda => AcceptedCurrencies.Contains(moeda))
       .WithMessage("Moeda inválida. As moedas aceitas são: USD, EUR, AOA, BRL.");
    }
}
