namespace EasyRent.Identity.Integrations.Identity;

public record CreateAccountIngrationEvent: IdentityIntegrationEvent
{
    public CreateAccountIngrationEvent() => EventType = IdentityEventType.CreateAccount;
}