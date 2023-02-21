using System;
using EasyRent.EventSourcing;

namespace EasyRent.Identity.Domain.Account.DomainEvents.V1;

public record AccountPasswordDataUpdated(Guid AccountId,
    string NewPasswordSalt,
    byte[] NewPasswordHash
) : IDomainEvent;