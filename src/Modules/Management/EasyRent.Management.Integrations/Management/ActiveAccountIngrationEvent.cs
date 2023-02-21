namespace EasyRent.Management.Integrations.Management;

public record ActiveAccountIngrationEvent : ManagementIntegrationEvent
{
    public ActiveAccountIngrationEvent() => EventType = ManagementEventType.ActiveAccount;
}