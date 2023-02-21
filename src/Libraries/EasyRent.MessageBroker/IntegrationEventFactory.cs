namespace EasyRent.MessageBroker;

public static class IntegrationEventFactory
{
    public static TIntegrationEvent WithData<TIntegrationEvent>(object data)
        where TIntegrationEvent : IntegrationEvent, new()
        => new TIntegrationEvent { Data = data };

    public static TIntegrationEvent WithoutData<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent, new()
        => new TIntegrationEvent();
}