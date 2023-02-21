using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.ActivateAccount;

public record ActivateAccountResponse(Error Error = null) : BaseResponse(Error);