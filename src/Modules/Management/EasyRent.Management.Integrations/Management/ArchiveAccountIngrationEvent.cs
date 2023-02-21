namespace EasyRent.Management.Integrations.Management;

public record ArchiveAccountIngrationEvent : ManagementIntegrationEvent
{
    public ArchiveAccountIngrationEvent() => EventType = ManagementEventType.ArchiveAccount;
}