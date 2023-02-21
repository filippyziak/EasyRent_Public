using System;
using EasyRent.Identity.Domain.Account.ValueObjects;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountType;

public record UpdateAccountTypeCommand(Guid AccountId, AccountTypeEnum NewAccountType) : IRequest<UpdateAccountTypeResponse>;