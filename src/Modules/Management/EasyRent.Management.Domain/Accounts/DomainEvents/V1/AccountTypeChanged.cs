using System;
using EasyRent.EventSourcing;

namespace EasyRent.Management.Domain.Accounts.DomainEvents.V1;

public record AccountTypeChanged(Guid AccountId, string Type) : IDomainEvent;