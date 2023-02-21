using System;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.Infrastructure.Abstractions;
using EasyRent.Infrastructure.Abstractions.RetryPolicy;
using Newtonsoft.Json;
using NLog;
using ILogger = EasyRent.Infrastructure.Abstractions.Abstractions.ILogger;

namespace EasyRent.EventSourcing;

public abstract class DefaultProjection<TEventType> : IProjection
{
    const int RetriesCount = 3;

    protected readonly ILogger Logger;
    private readonly IDIProvider _diProvider;

    protected IDIScope Scope;

    protected DefaultProjection(ILogger logger,
        IDIProvider diProvider)
    {
        Logger = logger;
        _diProvider = diProvider;
    }

    public Type EventType => typeof(TEventType);


    public async Task ProjectAsync(object @event)
    {
        using (ScopeContext.PushProperty(LoggingScope.Projection.ScopeName,
                   LoggingScope.Projection.ParseScopeMessage(@event, GetType())))
        using (Scope = _diProvider.CreateScope())
        {
            try
            {
                var defaultRetryPolicy = Scope.ResolveService<IDefaultRetryPolicy>();

                var eventInJson = JsonConvert.SerializeObject(@event);

                Logger.Info("Projecting event: {Event}", eventInJson);

                await defaultRetryPolicy.ExecutePolicyAsync(async _ => await ProjectEventAsync((TEventType)@event),
                    RetriesCount);

                Logger.Info("Projecting event completed: {Event}", eventInJson);
            }
            catch (Exception e)
            {
                Logger.Error("An error occurred during projecting event", e);
                throw;
            }
        }
    }

    protected abstract Task ProjectEventAsync(TEventType @event);
}