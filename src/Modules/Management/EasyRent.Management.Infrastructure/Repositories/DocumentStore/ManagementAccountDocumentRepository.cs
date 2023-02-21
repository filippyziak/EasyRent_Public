using EasyRent.Infrastructure.DocumentStore;
using EasyRent.Management.Infrastructure.DocumentStore.Documents;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.Management.Infrastructure.Repositories.DocumentStore;

public class ManagementAccountDocumentRepository : MongoDbDocumentRepository<ManagementAccountDocument>, IManagementAccountDocumentRepository
{
    public ManagementAccountDocumentRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
    {
    }
}