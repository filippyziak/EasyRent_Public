using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EasyRent.EventSourcing.EventStore.Checkpoints;

public interface ICheckpointStore
{
    Task<Position> GetCheckpointAsync();
    Task StoreCheckpointAsync(Position position);
}