using EasyRent.Shared.Models;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.ActivateAccount;

public record ActivateAccountResponse(Error Error = null) : BaseResponse(Error);