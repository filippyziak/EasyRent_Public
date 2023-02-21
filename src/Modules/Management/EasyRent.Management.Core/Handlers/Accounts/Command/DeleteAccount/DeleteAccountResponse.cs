using EasyRent.Shared.Models;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.DeleteAccount;

public record DeleteAccountResponse(Error Error = null) : BaseResponse(Error);