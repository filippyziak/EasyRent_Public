using System;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Query.GetAccountById;

public record GetAccountByIdQuery(Guid AccountId) : IRequest<GetAccountByIdResponse>;