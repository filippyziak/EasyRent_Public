using EasyRent.Shared.Models;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.SuspendAccount;

public record SuspendAccountResponse(Error Error = null) : BaseResponse(Error);