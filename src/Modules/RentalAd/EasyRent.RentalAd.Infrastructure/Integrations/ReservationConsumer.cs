using System.Threading;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.MessageBroker.RabbitMq;
using EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAdReservation;
using EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdReservation;
using EasyRent.Reservation.Events.Reservation;
using EasyRent.Reservation.Events.Reservation.Data;
using EasyRent.Reservation.Events.Reservation.Factories;
using EasyRent.Shared.Models;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EasyRent.RentalAd.Infrastructure.Integrations;

public class ReservationConsumer : RabbitMqBaseMessageConsumer<ReservationIntegrationEvent>
{
    private IMediator _mediator;
    private readonly IDIProvider _diProvider;

    public ReservationConsumer(ILogger logger,
        IOptionsMonitor<MessageBrokerConfiguration> messageBrokerConfigurationOptions,
        IModel channel,
        IDIProvider diProvider)
        : base(logger, messageBrokerConfigurationOptions, channel)
    {
        _diProvider = diProvider;
    }

    public override string ExchangeName => ReservationIntegrationEventFactory.ExchangeName;

    protected override async Task<bool> OnMessageReceivedAsync(ReservationIntegrationEvent message, CancellationToken cancellationToken)
    {
        using (var scope = _diProvider.CreateScope())
        {
            _mediator = scope.ResolveService<IMediator>();

            Logger.Info("Reservation event type: {EventType}", message.EventType);

            var command = PrepareReservationCommand(message);

            if (command is null)
            {
                Logger.Trace("Reservation integration event does not match to any command");
                return true;
            }

            var result = await _mediator.Send(command, cancellationToken);

            return ((BaseResponse)result).IsSucceeded;
        }
    }

    private static IBaseRequest PrepareReservationCommand(ReservationIntegrationEvent message)
        => message.EventType switch
        {
            ReservationEventType.ReservationCanceled => new RemoveRentalAdReservationCommand
            {
                RentalAdId = message.RentalAdId,
                RentalAdReservationId = message.ReservationId
            },
            ReservationEventType.ReservationCreated => PrepareAddRentalAdReservationCommand(message),
            _ => null
        };


    private static AddRentalAdReservationCommand PrepareAddRentalAdReservationCommand(ReservationIntegrationEvent message)
    {
        var eventData = message.ReadDataFromJson<ReservationCreatedIntegrationEventData>();

        return new AddRentalAdReservationCommand
        {
            RentalAdId = message.RentalAdId,
            PlaceReservationId = message.ReservationId,
            ArrivalDate = eventData.ArrivalDate,
            DepartureDate = eventData.DepartureDate
        };
    }
}