using System;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.DocumentStore;

namespace EasyRent.Management.Infrastructure.DocumentStore.Documents;

[BsonCollection("Accounts")]
public class ManagementAccountDocument : BaseDocument
{
    public Guid AccountId { get; set; }
    public string State { get; set; }
    public string Type { get; set; }
}