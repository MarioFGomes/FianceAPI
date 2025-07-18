﻿using Finance.Communication.Request;
using Finance.Exception;
using FluentValidation;

namespace Finance.Application.Validators; 
public class SingInValidator: AbstractValidator<SingInRequest> {
    public SingInValidator()
    {
        RuleFor(c => c.Email)
        .NotEmpty().WithMessage(ResourceMessage.EmptyEmail)
        .MaximumLength(150).WithMessage(ResourceMessage.EmailMaximumcharacters)
        .EmailAddress().WithMessage(ResourceMessage.EmailInvalid);

        RuleFor(c => c.Password)
           .NotEmpty().WithMessage(ResourceMessage.PasswordEmpty)
           .MinimumLength(6).WithMessage(ResourceMessage.PasswordInvalid)
           .MaximumLength(100).WithMessage("A senha deve ter no máximo 100 caracteres.")
           .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
           .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
           .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um número.");
    }
}
