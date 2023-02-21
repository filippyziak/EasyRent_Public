using System;
using EasyRent.EventSourcing;

namespace EasyRent.Identity.Domain.Account.DomainEvents.V1;

public record AccountCreated(
    Guid AccountId,
    string EmailAddress,
    byte[] PasswordHash,
    string PasswordSalt,
    string AccountType
) : IDomainEvent;