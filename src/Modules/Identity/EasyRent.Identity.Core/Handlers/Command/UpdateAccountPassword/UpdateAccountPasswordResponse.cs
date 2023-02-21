using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountPassword;

public record UpdateAccountPasswordResponse(Error Error = null) : BaseResponse(Error);