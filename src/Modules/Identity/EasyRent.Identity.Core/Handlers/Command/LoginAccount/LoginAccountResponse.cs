using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.LoginAccount;

public record LoginAccountResponse(Error Error = null) : BaseResponse(Error)
{
    public string JwtToken { get; init; }
    public AccountReadModel Account { get; init; }
}