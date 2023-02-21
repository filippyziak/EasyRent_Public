using System;
using EasyRent.EventSourcing;

namespace EasyRent.Identity.Domain.Account.DomainEvents.V1;

public record AccountActivated(Guid AccountId) : IDomainEvent;