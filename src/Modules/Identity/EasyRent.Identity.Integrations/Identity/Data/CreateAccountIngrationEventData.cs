namespace EasyRent.Identity.Integrations.Identity.Data;

public record CreateAccountIngrationEventData(string emailAddress,
    string accountType);