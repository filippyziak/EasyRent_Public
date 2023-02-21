using EasyRent.Identity.Core.Handlers.Command.RegisterAccount;
using EasyRent.Identity.Domain.Account.ValueObjects;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace EasyRent.Identity.Infrastructure.BackgroundServices;

public class IdentityDataSeederBackgroundService : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public IdentityDataSeederBackgroundService(IMediator mediator,
        ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _mediator.Send(new RegisterAccountCommand("admin@admin.pl",
                "admin",
                AccountTypeEnum.Moderator));
            _logger.Info("Identity data successfully seeded");
        }
        catch (Exception e)
        {
            _logger.Error("Seeding Identity data failed", e);
        }
    }
}