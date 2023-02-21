using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EventStore.ClientAPI;

namespace EasyRent.EventSourcing.EventStore.Checkpoints;

public class MongoDbCheckpointStore : ICheckpointStore
{
    private readonly string _checkpointId;
    private readonly IDocumentRepository<Checkpoint> _documentRepository;

    public MongoDbCheckpointStore(string checkpointId,
        IDocumentRepository<Checkpoint> documentRepository)
    {
        _checkpointId = checkpointId;
        _documentRepository = documentRepository;
    }

    public async Task<Position> GetCheckpointAsync()
    {
        var checkpoint = await _documentRepository.FindAsync(c => c.Id == _checkpointId);

        return checkpoint is not null
            ? new Position(checkpoint.CommitPosition, checkpoint.PreparePosition)
            : Position.Start;
    }

    public async Task StoreCheckpointAsync(Position position)
    {
        var checkpoint = await _documentRepository.FindAsync(c => c.Id == _checkpointId);

        if (checkpoint is null)
        {
            checkpoint= new Checkpoint
            {
                Id = _checkpointId
            };

            checkpoint.CommitPosition = position.CommitPosition;
            checkpoint.PreparePosition = position.PreparePosition;

            await _documentRepository.StoreAsync(checkpoint);
        }
        else
        {
            checkpoint.CommitPosition = position.CommitPosition;
            checkpoint.PreparePosition = position.PreparePosition;

            await _documentRepository.ReplaceAsync(checkpoint);
        }
    }
}