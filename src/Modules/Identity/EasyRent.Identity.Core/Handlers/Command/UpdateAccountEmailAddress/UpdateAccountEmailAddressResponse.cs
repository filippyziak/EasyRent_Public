using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountEmailAddress;

public record UpdateAccountEmailAddressResponse(Error Error = null) : BaseResponse(Error);