using EasyRent.DependencyInjection;
using EasyRent.Identity.Core.Handlers.Command.ActivateAccount;
using EasyRent.Identity.Core.Handlers.Command.RemoveAccount;
using EasyRent.Identity.Core.Handlers.Command.SuspendAccount;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Integrations.Management;
using EasyRent.Management.Integrations.Management.Factories;
using EasyRent.MessageBroker;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.Shared.Models;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EasyRent.Identity.Infrastructure.Integrations;

public class ManagementConsumer : RabbitMqBaseMessageConsumer<ManagementIntegrationEvent>
{
    private IMediator _mediator;
    private readonly IDIProvider _diProvider;

    public ManagementConsumer(ILogger logger,
        IOptionsMonitor<MessageBrokerConfiguration> messageBrokerConfigurationOptions,
        IModel channel,
        IDIProvider diProvider) : base(logger, messageBrokerConfigurationOptions, channel)
    {
        _diProvider = diProvider;
    }

    public override string ExchangeName => ManagementIntegrationEventFactory.ExchangeName;

    protected override async Task<bool> OnMessageReceivedAsync(ManagementIntegrationEvent message, CancellationToken cancellationToken)
    {
        using (var scope = _diProvider.CreateScope())
        {
            _mediator = scope.ResolveService<IMediator>();

            Logger.Info("Reservation event type: {EventType}", message.EventType);

            var command = PrepareManagementCommand(message);

            if (command is null)
            {
                Logger.Trace("Reservation integration event does not match to any command");
                return true;
            }

            var result = await _mediator.Send(command, cancellationToken);

            return ((BaseResponse)result).IsSucceeded;
        }
    }

    private static IBaseRequest PrepareManagementCommand(ManagementIntegrationEvent message)
        => message.EventType switch
        {
            ManagementEventType.ArchiveAccount => new RemoveAccountCommand(message.AccountId),
            ManagementEventType.SuspendAccount => new SuspendAccountCommand(message.AccountId),
            ManagementEventType.ActiveAccount => new ActivateAccountCommand(message.AccountId),
            _ => null
        };
}