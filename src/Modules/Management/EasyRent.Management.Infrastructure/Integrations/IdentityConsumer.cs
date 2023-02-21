using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.Identity.Integrations.Identity;
using EasyRent.Identity.Integrations.Identity.Data;
using EasyRent.Identity.Integrations.Identity.Factories;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Core.Handlers.Accounts.Command.CreateAccount;
using EasyRent.Management.Domain.Accounts.ValueObjects;
using EasyRent.MessageBroker;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.Shared.Models;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EasyRent.Management.Infrastructure.Integrations;

public class IdentityConsumer : RabbitMqBaseMessageConsumer<IdentityIntegrationEvent>
{
    private IMediator _mediator;
    private readonly IDIProvider _diProvider;

    public IdentityConsumer(ILogger logger,
        IOptionsMonitor<MessageBrokerConfiguration> messageBrokerConfigurationOptions,
        IModel channel,
        IDIProvider diProvider) : base(logger, messageBrokerConfigurationOptions, channel)
    {
        _diProvider = diProvider;
    }

    public override string ExchangeName => IdentityIntegrationEventFactory.ExchangeName;

    protected override async Task<bool> OnMessageReceivedAsync(IdentityIntegrationEvent message, CancellationToken cancellationToken)
    {
        using (var scope = _diProvider.CreateScope())
        {
            _mediator = scope.ResolveService<IMediator>();

            Logger.Info("Reservation event type: {EventType}", message.EventType);

            var command = PrepareIdentityCommand(message);

            if (command is null)
            {
                Logger.Trace("Reservation integration event does not match to any command");
                return true;
            }

            var result = await _mediator.Send(command, cancellationToken);

            return ((BaseResponse)result).IsSucceeded;
        }
    }

    private static IBaseRequest PrepareIdentityCommand(IdentityIntegrationEvent message)
        => message.EventType switch
        {
            IdentityEventType.CreateAccount => PrepareCreateAccountCommand(message),
            _ => null
        };

    private static CreateAccountCommand PrepareCreateAccountCommand(IdentityIntegrationEvent message)
    {
        var eventData = message.ReadDataFromJson<CreateAccountIngrationEventData>();

        return new CreateAccountCommand
        {
            AccountId = message.AccountId,
            Type = Enum.Parse<AccountTypeEnum>(eventData.accountType)
        };
    }
}