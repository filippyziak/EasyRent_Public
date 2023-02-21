using System.Threading.Tasks;

namespace EasyRent.EventSourcing;

public interface IProjectionStrategy
{
    Task ProjectEventAsync(object @event);
}