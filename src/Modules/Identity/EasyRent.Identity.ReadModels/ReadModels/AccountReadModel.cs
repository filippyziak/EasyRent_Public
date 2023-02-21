using System;

namespace EasyRent.Identity.ReadModels.ReadModels;

public record AccountReadModel
{
    public Guid AccountId { get; init; }
    public string EmailAddress { get; init; }
    public string AccountType { get; init; }
    public string State { get; init; }
}