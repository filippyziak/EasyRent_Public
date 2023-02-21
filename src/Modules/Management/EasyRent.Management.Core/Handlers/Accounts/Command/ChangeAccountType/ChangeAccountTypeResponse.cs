using EasyRent.Shared.Models;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.ChangeAccountType;

public record ChangeAccountTypeResponse(Error Error = null) : BaseResponse(Error);