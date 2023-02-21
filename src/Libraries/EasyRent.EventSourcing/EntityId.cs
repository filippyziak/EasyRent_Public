using System;
using EasyRent.Shared;

namespace EasyRent.EventSourcing;

public abstract record EntityId : ValueObject<Guid>
{
    public EntityId(Guid value)
    {
        Validator.Identifiers.ValidateGuid(value);

        Value = value;
    }
}