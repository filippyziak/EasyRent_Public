using System;
using EasyRent.EventSourcing;

namespace EasyRent.Management.Domain.Accounts.DomainEvents.V1;

public record AccountArchived(Guid AccountId) : IDomainEvent;