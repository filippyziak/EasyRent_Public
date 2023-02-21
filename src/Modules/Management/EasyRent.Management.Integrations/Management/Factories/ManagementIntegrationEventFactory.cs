using System;
using EasyRent.Management.Integrations.Management.Data;
using EasyRent.MessageBroker;

namespace EasyRent.Management.Integrations.Management.Factories;

public static class ManagementIntegrationEventFactory
{
    public static string ExchangeName => "management";

    public static UpdateAccountTypeIngrationEvent Update(Guid accountId,
        string acountType)
        => IntegrationEventFactory.WithData<UpdateAccountTypeIngrationEvent>(
                new UpdateAccountTypeIngrationEventData(acountType))
            with
            {
                AccountId = accountId
            };

    public static SuspendAccountIngrationEvent Suspend(Guid accountId)
        => IntegrationEventFactory.WithoutData<SuspendAccountIngrationEvent>()
            with
            {
                AccountId = accountId
            };

    public static ArchiveAccountIngrationEvent Archive(Guid accountId)
        => IntegrationEventFactory.WithoutData<ArchiveAccountIngrationEvent>()
            with
            {
                AccountId = accountId
            };
    
    public static ActiveAccountIngrationEvent Active(Guid accountId)
        => IntegrationEventFactory.WithoutData<ActiveAccountIngrationEvent>()
            with
            {
                AccountId = accountId
            };
}