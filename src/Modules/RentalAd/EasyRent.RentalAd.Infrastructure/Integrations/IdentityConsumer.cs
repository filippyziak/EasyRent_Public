using System.Threading;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.Identity.Integrations.Identity;
using EasyRent.Identity.Integrations.Identity.Data;
using EasyRent.Identity.Integrations.Identity.Factories;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPlaceOwner;
using EasyRent.Shared.Models;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EasyRent.RentalAd.Infrastructure.Integrations;

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
            IdentityEventType.UpdateAccountEmailAddress => PrepareUpdateRentalAdPlaceOwnerCommand(message),
            _ => null
        };

    private static UpdateRentalAdPlaceOwnerCommand PrepareUpdateRentalAdPlaceOwnerCommand(IdentityIntegrationEvent message)
    {
        var eventData = message.ReadDataFromJson<UpdateAccountEmailAddressIngrationEventData>();

        return new UpdateRentalAdPlaceOwnerCommand
        {
            PlaceOwnerId = message.AccountId,
            NewEmailAddress = eventData.EmailAddress
        };
    }
}