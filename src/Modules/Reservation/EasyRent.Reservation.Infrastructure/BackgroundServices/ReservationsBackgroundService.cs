using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Reservation.Core.Reservation.Commands.FinishPlaceReservations;
using MediatR;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace EasyRent.Reservation.Infrastructure.BackgroundServices;

public class ReservationsBackgroundService : BackgroundService
{
    private CrontabSchedule _schedule;
    private DateTime _nextRun;
    private const string Schedule = "1 0 * * *";
    private readonly IMediator _mediator;

    public ReservationsBackgroundService(IMediator mediator)
    {
        _mediator = mediator;
        _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions());
        _nextRun = _schedule.GetNextOccurrence(DateTime.Now.AddDays(-1));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            var now = DateTime.Now;
            if (now > _nextRun)
            {
                await _mediator.Send(new FinishPlaceReservationsCommand());
                _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            }

            await Task.Delay(5000, stoppingToken); //5 seconds delay
        } while (!stoppingToken.IsCancellationRequested);
    }
}