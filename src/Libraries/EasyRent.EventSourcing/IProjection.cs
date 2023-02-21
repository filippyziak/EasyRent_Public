using System;
using System.Threading.Tasks;

namespace EasyRent.EventSourcing;

public interface IProjection
{
    Type EventType { get; }

    Task ProjectAsync(object @event);
}