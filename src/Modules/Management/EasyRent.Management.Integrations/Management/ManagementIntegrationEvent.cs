using System;
using EasyRent.MessageBroker;

namespace EasyRent.Management.Integrations.Management;

public record ManagementIntegrationEvent : IntegrationEvent, IManagementIntegrationEvent
{
    public ManagementEventType EventType { get; init; } = ManagementEventType.Undefined;
    public Guid AccountId { get; init; }
}