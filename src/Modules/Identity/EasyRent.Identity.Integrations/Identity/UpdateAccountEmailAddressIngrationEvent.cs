namespace EasyRent.Identity.Integrations.Identity;

public record UpdateAccountEmailAddressIngrationEvent : IdentityIntegrationEvent
{
    public UpdateAccountEmailAddressIngrationEvent() => EventType = IdentityEventType.UpdateAccountEmailAddress;
}