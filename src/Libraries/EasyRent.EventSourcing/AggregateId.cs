using System;
using EasyRent.Shared;

namespace EasyRent.EventSourcing;

public record AggregateId : ValueObject<Guid>
{
    public AggregateId(Guid value)
    {
        Validator.Identifiers.ValidateGuid(value);

        Value = value;
    }
}