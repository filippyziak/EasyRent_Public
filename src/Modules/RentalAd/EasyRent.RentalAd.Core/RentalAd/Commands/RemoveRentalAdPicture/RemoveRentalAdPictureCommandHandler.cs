using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Infrastructure.Abstractions.Services.FileStorage;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPicture;

public class RemoveRentalAdPictureCommandHandler : IRequestHandler<RemoveRentalAdPictureCommand, RemoveRentalAdPictureResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IFileStorageService _fileStorageService;

    public RemoveRentalAdPictureCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IFileStorageService fileStorageService)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _fileStorageService = fileStorageService;
    }
    
    public async Task<RemoveRentalAdPictureResponse> Handle(RemoveRentalAdPictureCommand request, CancellationToken cancellationToken)
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
        
        rentalAd.RemovePlacePicture(new PlacePictureId(request.PictureId));

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        await _fileStorageService.DeleteFileAsync(request.PictureId.ToString(), cancellationToken);

        _logger.Info("Rental ad with ID: {RentalAdId} updated in the event stream", rentalAd.Id);
        
        return new RemoveRentalAdPictureResponse();
    }
}