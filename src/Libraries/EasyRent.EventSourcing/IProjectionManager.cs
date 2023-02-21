using System.Threading.Tasks;

namespace EasyRent.EventSourcing;

public interface IProjectionManager
{
    Task StartAsync();
    void Stop();
}