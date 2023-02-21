using System;
using EasyRent.EventSourcing;

namespace EasyRent.Management.Domain.Accounts.DomainEvents.V1;

public record AccountSuspended(Guid AccountId) : IDomainEvent;