using System;

namespace EasyRent.Management.Integrations.Management;

public interface IManagementIntegrationEvent
{
    public ManagementEventType EventType { get; }
    public Guid AccountId { get; }
}