using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.RegisterAccount;

public record RegisterAccountResponse(Error Error = null) : BaseResponse(Error);