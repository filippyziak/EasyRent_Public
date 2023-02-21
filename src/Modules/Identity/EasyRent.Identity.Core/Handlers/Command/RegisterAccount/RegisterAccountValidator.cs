using EasyRent.Identity.Domain.Account.ValueObjects;
using FluentValidation;

namespace EasyRent.Identity.Core.Handlers.Command.RegisterAccount;

public class RegisterAccountValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountValidator()
    {
        RuleFor(x => x.AccountType)
            .IsInEnum()
            .NotEqual(AccountTypeEnum.Moderator);
    }
}