using System;
using Newtonsoft.Json;

namespace EasyRent.MessageBroker;

public abstract record IntegrationEvent : IMessage
{
    public string MessageId { get; init; } = Guid.NewGuid().ToString();
    public virtual string Type => GetType().Name;
    public long Timestamp { get; init; } = DateTime.UtcNow.Ticks;
    public object Data { get; init; }

    protected IntegrationEvent()
    {
    }

    public TData ReadDataFromJson<TData>()
        where TData : class
        => JsonConvert.DeserializeObject<TData>(Data?.ToString() ?? string.Empty);
}