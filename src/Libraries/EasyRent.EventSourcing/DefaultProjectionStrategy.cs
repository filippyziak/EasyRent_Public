using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;

namespace EasyRent.EventSourcing;

public class DefaultProjectionStrategy : IProjectionStrategy
{
    private readonly ILogger _logger;
    private readonly IEnumerable<IProjection> _projections;

    public DefaultProjectionStrategy(ILogger logger,
        IEnumerable<IProjection> projections)
    {
        _logger = logger;
        _projections = projections;
    }

    public async Task ProjectEventAsync(object @event)
    {
        var eventProjections = _projections
            .Where(projection => projection.EventType == @event.GetType())
            .ToImmutableList();

        _logger.Trace("Found {ProjectionsCount} registered event projections for event of type: {EventType}",
            eventProjections.Count,
            @event.GetType().Name);

        await Task.WhenAll(eventProjections.Select(x => x.ProjectAsync(@event)));
    }
}