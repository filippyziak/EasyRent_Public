using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.DocumentStore;

namespace EasyRent.Identity.Infrastructure.DocumentStore.Documents;

[BsonCollection("Accounts")]
public class AccountDocument : BaseDocument
{
    public string AccountId { get; set; }
    public string EmailAddress { get; set; }
    public byte[] PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string AccountType { get; set; }
    public string State { get; set; }
}