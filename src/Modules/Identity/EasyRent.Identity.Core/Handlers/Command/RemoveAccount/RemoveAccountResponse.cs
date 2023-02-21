using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.RemoveAccount;

public record RemoveAccountResponse(Error Error = null) : BaseResponse(Error);