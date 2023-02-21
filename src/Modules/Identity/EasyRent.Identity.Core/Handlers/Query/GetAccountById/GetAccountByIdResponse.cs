using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Shared.Models;

namespace EasyRent.Identity.Core.Handlers.Query.GetAccountById;

public record GetAccountByIdResponse(Error Error = null) : BaseResponse(Error)
{
    public AccountReadModel Account { get; init; }
}