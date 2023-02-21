using System;
using EasyRent.MessageBroker;

namespace EasyRent.Identity.Integrations.Identity;

public record IdentityIntegrationEvent : IntegrationEvent, IIdentityIntegrationEvent
{
    public IdentityEventType EventType { get; init; } = IdentityEventType.Undefined;
    public Guid AccountId { get; init; }
}