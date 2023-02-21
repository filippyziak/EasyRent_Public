using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.SuspendAccount;

public record SuspendAccountResponse(Error Error = null) : BaseResponse(Error);