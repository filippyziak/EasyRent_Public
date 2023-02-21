using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.DocumentStore;

namespace EasyRent.EventSourcing.EventStore.Checkpoints;

[BsonCollection("Checkpoints")]
public class Checkpoint : BaseDocument
{
    public long CommitPosition { get; set; }
    public long PreparePosition { get; set; }
}