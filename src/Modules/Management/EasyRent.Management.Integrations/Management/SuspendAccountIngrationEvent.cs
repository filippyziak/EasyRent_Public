namespace EasyRent.Management.Integrations.Management;

public record SuspendAccountIngrationEvent : ManagementIntegrationEvent
{
    public SuspendAccountIngrationEvent() => EventType = ManagementEventType.SuspendAccount;
}