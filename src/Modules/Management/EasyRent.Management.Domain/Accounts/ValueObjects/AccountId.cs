using System;
using EasyRent.EventSourcing;

namespace EasyRent.Management.Domain.Accounts.ValueObjects;

public record AccountId(Guid Value) : AggregateId(Value);