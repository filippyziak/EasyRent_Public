namespace EasyRent.Management.Integrations.Management;

public record UpdateAccountTypeIngrationEvent : ManagementIntegrationEvent
{
    public UpdateAccountTypeIngrationEvent() => EventType = ManagementEventType.UpdateAccountType;
}