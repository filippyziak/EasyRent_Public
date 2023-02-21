using System;
using EasyRent.EventSourcing;

namespace EasyRent.Identity.Domain.Account.ValueObjects;

public record AccountId(Guid Value) : AggregateId(Value);