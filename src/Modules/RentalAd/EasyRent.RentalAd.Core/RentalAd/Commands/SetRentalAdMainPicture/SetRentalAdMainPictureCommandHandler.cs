using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.SetRentalAdMainPicture;

public class SetRentalAdMainPictureCommandHandler : IRequestHandler<SetRentalAdMainPictureCommand, SetRentalAdMainPictureResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public SetRentalAdMainPictureCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<SetRentalAdMainPictureResponse> Handle(SetRentalAdMainPictureCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        if (rentalAd.PlaceOwner.PlaceOwnerId != request.CurrentOwnerId)
        {
            throw new PermissionException(typeof(Domain.RentalAd.RentalAd));
        }
        
        var mainPicture = rentalAd.PlacePictures.FirstOrDefault(p
            => p.PlacePictureId == request.RentalAdMainPictureId) ?? throw new EntityNotFoundException(request.RentalAdMainPictureId, typeof(Domain.RentalAd.RentalAd));
        mainPicture.SetMainPicture();

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info(" Rental Ad with ID: {RentalAdId} main photo has been changed in event store", rentalAd.Id.Value);

        return new SetRentalAdMainPictureResponse();
    }
}