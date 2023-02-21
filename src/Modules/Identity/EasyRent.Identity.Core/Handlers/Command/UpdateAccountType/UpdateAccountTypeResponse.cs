using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountType;

public record UpdateAccountTypeResponse(Error Error = null) : BaseResponse(Error);