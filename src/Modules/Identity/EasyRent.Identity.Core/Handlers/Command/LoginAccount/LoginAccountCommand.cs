using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.LoginAccount;

public class LoginAccountCommand : IRequest<LoginAccountResponse>
{
    public string EmailAddress { get; init; }
    public string Password { get; init; }
}