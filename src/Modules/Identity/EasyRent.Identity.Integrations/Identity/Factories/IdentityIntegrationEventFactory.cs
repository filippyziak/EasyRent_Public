using System;
using EasyRent.Identity.Integrations.Identity.Data;
using EasyRent.MessageBroker;

namespace EasyRent.Identity.Integrations.Identity.Factories;

public static class IdentityIntegrationEventFactory
{
    public static string ExchangeName => "identity";

    public static UpdateAccountEmailAddressIngrationEvent Update(Guid accountId,
        string emailAddress)
        => IntegrationEventFactory.WithData<UpdateAccountEmailAddressIngrationEvent>(
                new UpdateAccountEmailAddressIngrationEventData(emailAddress))
            with
            {
                AccountId = accountId
            };

    public static CreateAccountIngrationEvent Create(Guid accountId,
        string emailAddress,
        string accountType)
        => IntegrationEventFactory.WithData<CreateAccountIngrationEvent>(
                new CreateAccountIngrationEventData(emailAddress, accountType))
            with
            {
                AccountId = accountId
            };
}